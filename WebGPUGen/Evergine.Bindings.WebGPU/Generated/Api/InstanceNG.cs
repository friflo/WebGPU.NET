namespace Evergine.Bindings.WebGPU;
using static WebGPUNative;

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
}
