using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using static Evergine.Bindings.WebGPU.WebGPUNative;

namespace Evergine.Bindings.WebGPU;


public unsafe struct RequestDeviceResult {
    public WGPURequestDeviceStatus  status;
    public WGPUDevice               device;
    public char*                    message;
    
    public ReadOnlySpan<char>       Message {
        get => ApiUtils.GetStr(message);
        set => ApiUtils.SetStr(value, out message);
    }
}

public delegate Action RequestDeviceCallback(in RequestDeviceResult result);

public unsafe partial struct WGPUAdapter
{
    public void requestDevice(WGPUDeviceDescriptor descriptor, RequestDeviceCallback? callback)
    {
        void* callbackUserData = null;
        if (callback is not null) {
            var callbackHandle = GCHandle.Alloc(callback);
            callbackUserData = (void*)Unsafe.As<GCHandle, nuint>(ref callbackHandle);
        }
        wgpuAdapterRequestDevice(Handle, &descriptor, &OnDeviceRequestEnded, callbackUserData);
    }
    
    [UnmanagedCallersOnly]
    private static void OnDeviceRequestEnded(WGPURequestDeviceStatus status, WGPUDevice device, char* message, void* pUserData)
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
