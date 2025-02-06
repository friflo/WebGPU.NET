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

public delegate void UncapturedErrorCallback(WGPUErrorType errorType, Utf8 message);


public unsafe partial struct WGPUAdapter
{
    public void requestDevice(WGPUDeviceDescriptor descriptor, UncapturedErrorCallback? errorCallback, RequestDeviceCallback? callback)
    {
        if (errorCallback is not null) {
            var errorUserData = UserData.Create(default, errorCallback);
            descriptor.uncapturedErrorCallbackInfo = new WGPUUncapturedErrorCallbackInfo {
                callback = &HandleUncapturedErrorCallback,
                userdata = errorUserData
            };
        }
        var userData = UserData.Create(descriptor.Label, callback);
        wgpuAdapterRequestDevice(Handle, &descriptor, &requestDeviceCallback, userData);
    }
    
    // untested
    public Task<RequestDeviceResult> requestDeviceAsync(WGPUDeviceDescriptor descriptor, UncapturedErrorCallback? errorCallback)
    {
        var tcs = new TaskCompletionSource<RequestDeviceResult>();
        requestDevice(descriptor, errorCallback, (in RequestDeviceResult result) => {
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
    
    [UnmanagedCallersOnly]
    private static void HandleUncapturedErrorCallback(WGPUErrorType type, char* pMessage, void* pUserData)
    {
        var userData = (UserData*)pUserData;
        var callbackHandle = userData->callbackHandle;
        if (!callbackHandle.IsAllocated) {
            throw new InvalidOperationException("error callback is not allocated");
        }
        var callback = (UncapturedErrorCallback)callbackHandle.Target!;
        var message = new Utf8(pMessage);
        callback(type, message);
    }
    
    public WGPUSupportedLimits getLimits() {
        WGPUSupportedLimits result;
        var success = wgpuAdapterGetLimits(Handle, &result);
        return result;
    }
}
