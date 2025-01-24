namespace Evergine.Bindings.WebGPU;
using static WebGPUNative;
           
public static unsafe partial class WebGPUExtensions
{
    public static void configure(this WGPUSurface surface, WGPUSurfaceConfiguration config) {
        wgpuSurfaceConfigure(surface, &config);
    }

    public static void getCapabilities(this WGPUSurface surface, WGPUAdapter adapter, WGPUSurfaceCapabilities capabilities) {
        wgpuSurfaceGetCapabilities(surface, adapter, &capabilities);
    }

    public static void getCurrentTexture(this WGPUSurface surface, WGPUSurfaceTexture surfaceTexture) {
        wgpuSurfaceGetCurrentTexture(surface, &surfaceTexture);
    }

    public static void present(this WGPUSurface surface) {
        wgpuSurfacePresent(surface);
    }

    public static void setLabel(this WGPUSurface surface, ReadOnlySpan<char> label) {
        wgpuSurfaceSetLabel(surface, label.AllocString());
    }

    public static void unconfigure(this WGPUSurface surface) {
        wgpuSurfaceUnconfigure(surface);
    }

    public static void reference(this WGPUSurface surface) {
        wgpuSurfaceReference(surface);
        ObjectTracker.IncRef(surface.Handle);
    }

    public static void release(this WGPUSurface surface) {
        ObjectTracker.DecRef(surface.Handle);
        wgpuSurfaceRelease(surface);
    }

}
