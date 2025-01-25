namespace Evergine.Bindings.WebGPU;
using static WebGPUNative;
           
public unsafe partial struct WGPUSurface
{
    public void configure(WGPUSurfaceConfiguration config) {
        wgpuSurfaceConfigure(Handle, &config);
    }

    public void getCapabilities(WGPUAdapter adapter, WGPUSurfaceCapabilities capabilities) {
        wgpuSurfaceGetCapabilities(Handle, adapter, &capabilities);
    }

    public void getCurrentTexture(WGPUSurfaceTexture surfaceTexture) {
        wgpuSurfaceGetCurrentTexture(Handle, &surfaceTexture);
    }

    public void present() {
        wgpuSurfacePresent(Handle);
    }

    public void setLabel(ReadOnlySpan<char> label) {
        wgpuSurfaceSetLabel(Handle, label.AllocString());
    }

    public void unconfigure() {
        wgpuSurfaceUnconfigure(Handle);
    }

    public void reference() {
        wgpuSurfaceReference(Handle);
        ObjectTracker.IncRef(Handle);
    }

    public void release() {
        ObjectTracker.DecRef(Handle);
        wgpuSurfaceRelease(Handle);
    }

}
