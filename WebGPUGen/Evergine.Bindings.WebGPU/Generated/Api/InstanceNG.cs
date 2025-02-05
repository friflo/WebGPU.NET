using System.Runtime.InteropServices;
using static Evergine.Bindings.WebGPU.WebGPUNative;

namespace Evergine.Bindings.WebGPU;

public unsafe struct RequestAdapterResult
{
    public      WGPURequestAdapterStatus status;
    public      WGPUAdapter              adapter;
    internal    char*                    message;
    public      Utf8                     Message => ApiUtils.GetUtf8(message);
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
    
    public void requestAdapter(WGPURequestAdapterOptions options, RequestAdapterCallback? callback)
    {
        var userData = UserData.Create(default, callback);
        wgpuInstanceRequestAdapter(Handle, &options, &requestAdapterCallback, userData);
    }
    
    [UnmanagedCallersOnly]
    // delegate* unmanaged                    <WGPURequestAdapterStatus,        WGPUAdapter,         char*,         void*, void> callback
    private static void requestAdapterCallback(WGPURequestAdapterStatus status, WGPUAdapter adapter, char* message, void* pUserData)
    {
        var userData = (UserData*)pUserData;
        var callbackHandle = userData->callbackHandle;
        try {
            if (!callbackHandle.IsAllocated) {
                return;
            }
            var callback = (RequestAdapterCallback)callbackHandle.Target!;
            var result = new RequestAdapterResult {
                status = status,
                adapter = adapter,
                message = message
            };
            callback(result);
        }
        finally {
            UserData.Free(userData);
        }
    }
}
