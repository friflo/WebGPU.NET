namespace Evergine.Bindings.WebGPU;
using static WebGPUNative;

/// No counterpart in JavaScript WebGPU           
public unsafe partial struct WGPUSurface
{
    public void configure(WGPUSurfaceConfiguration config) {
        config.Validate();
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

    public void setLabel(Utf8 label) {
        wgpuSurfaceSetLabel(Handle, label.AllocUtf8());
    }

    public void unconfigure() {
        wgpuSurfaceUnconfigure(Handle);
    }

    public void reference() {
        ObjectTracker.IncRef(Handle);
        wgpuSurfaceReference(Handle);
    }

    public void release() {
        ObjectTracker.DecRef(Handle);
        wgpuSurfaceRelease(Handle);
    }

    public override string? ToString() => ObjectTracker.GetLabel(Handle);
}
