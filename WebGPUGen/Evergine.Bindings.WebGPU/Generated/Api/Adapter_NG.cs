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
    struct RequestDeviceData
    {
        internal nuint callbackHandle;
        internal byte* label;
    }
    
    public void requestDevice(WGPUDeviceDescriptor descriptor, RequestDeviceCallback? callback)
    {
        var data = (RequestDeviceData*)NativeMemory.Alloc((nuint)sizeof(RequestDeviceData));
        var label = descriptor.Label.AsSpan();
        if (!label.IsEmpty) {
            var len = label.Length;
            data->label = (byte*)NativeMemory.Alloc((nuint)len + 1);
            label.CopyTo(new Span<byte>(data->label, len));
            data->label[len] = 0;
        }
        if (callback is not null) {
            var callbackHandle = GCHandle.Alloc(callback);
            data->callbackHandle = Unsafe.As<GCHandle, nuint>(ref callbackHandle);
        }
        wgpuAdapterRequestDevice(Handle, &descriptor, &requestDeviceCallback, data);
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
        var data = (RequestDeviceData*)pUserData;
        var userDataHandle = Unsafe.BitCast<nuint, GCHandle>(data->callbackHandle);
        try {
            if (!userDataHandle.IsAllocated) {
                return;
            }
            var callback = (RequestDeviceCallback)userDataHandle.Target!;
            ObjectTracker.CreateRef(device.Handle, HandleType.WGPUBuffer, (char*)data->label);
            var result = new RequestDeviceResult {
                status = status,
                device = device,
                message = message
            };
            callback(result);
        }
        finally {
            userDataHandle.Free();
            NativeMemory.Free(data->label);
            NativeMemory.Free(data);
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
