namespace Evergine.Bindings.WebGPU;
using static WebGPUNative;
           
public unsafe partial struct WGPUSurface
{
    public void configure(WGPUSurfaceConfiguration config) {
        wgpuSurfaceConfigure(Handle, &config);
    }

    // getCapabilities() - not generated. See: Surface_NG.cs

    public WGPUSurfaceTexture currentTexture { get {
        var result = new WGPUSurfaceTexture();
        wgpuSurfaceGetCurrentTexture(Handle, &result);
        return result;
    } }

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
