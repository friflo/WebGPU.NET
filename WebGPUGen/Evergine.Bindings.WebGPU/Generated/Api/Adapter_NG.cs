using System.Runtime.InteropServices;
using static Evergine.Bindings.WebGPU.WebGPUNative;

namespace Evergine.Bindings.WebGPU;


public unsafe struct RequestDeviceResult
{
    public      WGPURequestDeviceStatus  status;
    public      WGPUDevice               device;
    internal    char*                    message;
    public      Utf8                     Message => ApiUtils.GetUtf8(message);
}

public delegate void RequestDeviceCallback(in RequestDeviceResult result);

public unsafe partial struct WGPUAdapter
{
    public void requestDevice(WGPUDeviceDescriptor descriptor, RequestDeviceCallback? callback)
    {
        var userData = UserData.Create(descriptor.Label, callback);
        wgpuAdapterRequestDevice(Handle, &descriptor, &requestDeviceCallback, userData);
    }
    
    // untested
    public Task<RequestDeviceResult> requestDeviceAsync(WGPUDeviceDescriptor descriptor)
    {
        var tcs = new TaskCompletionSource<RequestDeviceResult>();
        requestDevice(descriptor, (in RequestDeviceResult result) => {
            tcs.SetResult(result);
        });
        return tcs.Task;
    }
    
    [UnmanagedCallersOnly]
    // delegate* unmanaged                   <WGPURequestDeviceStatus,        WGPUDevice,        char*,         void*, void> callback
    private static void requestDeviceCallback(WGPURequestDeviceStatus status, WGPUDevice device, char* message, void* pUserData)
    {
        var userData = (UserData*)pUserData;
        var callbackHandle = userData->callbackHandle;
        try {
            if (!callbackHandle.IsAllocated) {
                return;
            }
            var callback = (RequestDeviceCallback)callbackHandle.Target!;
            ObjectTracker.CreateRef(device.Handle, HandleType.WGPUDevice, (char*)userData->label);
            var result = new RequestDeviceResult {
                status = status,
                device = device,
                message = message
            };
            callback(result);
        }
        finally {
            UserData.Free(userData);
        }
    }
    
    public WGPUSupportedLimits limits {
        get {
            WGPUSupportedLimits result;
            var success = wgpuAdapterGetLimits(Handle, &result);
            return result;
        }
    }
}
