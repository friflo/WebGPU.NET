using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Evergine.Bindings.WebGPU;
using static WebGPUNative;


public unsafe struct RequestAdapterResult
{
    public WGPURequestAdapterStatus status;
    public WGPUAdapter              adapter;
    public char*                    message;
    
    public Utf8                     Message {
        get => ApiUtils.GetUtf8(message);
        set => ApiUtils.SetUtf8(value, out message);
    }
}

public delegate void RequestAdapterCallback(in RequestAdapterResult result);

/// No counterpart in JavaScript WebGPU           
public unsafe partial struct WGPUInstance
{
    public WGPUSurface createSurfaceHWND(WGPUSurfaceDescriptor descriptor, IntPtr hInstance, IntPtr hWnd)
    {
        WGPUSurfaceDescriptorFromWindowsHWND windowsSurface = new WGPUSurfaceDescriptorFromWindowsHWND()
        {
            chain = new WGPUChainedStruct()
            {
                sType = WGPUSType.SurfaceDescriptorFromWindowsHWND,
            },
            Hinstance = hInstance,
            Hwnd = hWnd
        };
        descriptor.nextInChain = &windowsSurface.chain;
        var result = wgpuInstanceCreateSurface(Handle, &descriptor);
        ObjectTracker.CreateRef(result.Handle, HandleType.WGPUSurface, descriptor.label);
        return result;
    }
    
    public void requestAdapter(WGPURequestAdapterOptions options, RequestAdapterCallback callback)
    {
        void* callbackUserData = null;
        if (callback is not null) {
            var callbackHandle = GCHandle.Alloc(callback);
            callbackUserData = (void*)Unsafe.As<GCHandle, nuint>(ref callbackHandle);
        }
        wgpuInstanceRequestAdapter(Handle, &options, &requestAdapterCallback, callbackUserData);
    }
    
    [UnmanagedCallersOnly]
    // delegate* unmanaged                    <WGPURequestAdapterStatus,        WGPUAdapter,         char*,         void*, void> callback
    private static void requestAdapterCallback(WGPURequestAdapterStatus status, WGPUAdapter adapter, char* message, void* pUserData)
    {
        var userDataHandle = Unsafe.BitCast<nuint, GCHandle>((nuint)pUserData);
        try {
            if (!userDataHandle.IsAllocated) {
                return;
            }
            var callback = (RequestAdapterCallback)userDataHandle.Target!;
            var result = new RequestAdapterResult {
                status = status,
                adapter = adapter,
                message = message
            };
            callback(result);
        }
        finally {
            userDataHandle.Free();
        }
    }
}
