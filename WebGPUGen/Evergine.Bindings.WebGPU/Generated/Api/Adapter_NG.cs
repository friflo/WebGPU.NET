using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using static Evergine.Bindings.WebGPU.WebGPUNative;

namespace Evergine.Bindings.WebGPU;


public unsafe struct RequestDeviceResult {
    public WGPURequestDeviceStatus  status;
    public WGPUDevice               device;
    public char*                    message;
    
    public Utf8                     Message {
        get => ApiUtils.GetUtf8(message);
        set => ApiUtils.SetUtf8(value, out message);
    }
}

public delegate void RequestDeviceCallback(in RequestDeviceResult result);

public unsafe partial struct WGPUAdapter
{
    public void requestDevice(WGPUDeviceDescriptor descriptor, RequestDeviceCallback? callback)
    {
        void* callbackUserData = null;
        if (callback is not null) {
            var callbackHandle = GCHandle.Alloc(callback);
            callbackUserData = (void*)Unsafe.As<GCHandle, nuint>(ref callbackHandle);
        }
        wgpuAdapterRequestDevice(Handle, &descriptor, &requestDeviceCallback, callbackUserData);
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
        var userDataHandle = Unsafe.BitCast<nuint, GCHandle>((nuint)pUserData);
        try {
            if (!userDataHandle.IsAllocated) {
                return;
            }
            var callback = (RequestDeviceCallback)userDataHandle.Target!;
            var result = new RequestDeviceResult {
                status = status,
                device = device,
                message = message
            };
            callback(result);
        }
        finally {
            userDataHandle.Free();
        }
    }
}
