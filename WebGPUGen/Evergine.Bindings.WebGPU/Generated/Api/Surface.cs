using System.Diagnostics;
using static Evergine.Bindings.WebGPU.WebGPUNative;

// ReSharper disable InconsistentNaming
namespace Evergine.Bindings.WebGPU;

/// No counterpart in JavaScript WebGPU           
public unsafe partial struct WGPUSurface
{
    public void configure(WGPUSurfaceConfiguration config) {
        Validate_configure(config);
        wgpuSurfaceConfigure(this, &config);
    }

    [Conditional("VALIDATE")]
    private void Validate_configure(WGPUSurfaceConfiguration config) {
        ObjectTracker.ValidateHandle(this);
        config.Validate();
    }

    // getCapabilities() - not generated. See: Surface_NG.cs

    public WGPUSurfaceTexture currentTexture { get {
        var result = new WGPUSurfaceTexture();
        wgpuSurfaceGetCurrentTexture(this, &result);
        return result;
    } }

    public void present() {
        Validate_present();
        wgpuSurfacePresent(this);
    }

    [Conditional("VALIDATE")]
    private void Validate_present() {
        ObjectTracker.ValidateHandle(this);
    }


    public void unconfigure() {
        Validate_unconfigure();
        wgpuSurfaceUnconfigure(this);
    }

    [Conditional("VALIDATE")]
    private void Validate_unconfigure() {
        ObjectTracker.ValidateHandle(this);
    }

    public void reference() {
        ObjectTracker.IncRef(this);
        wgpuSurfaceReference(this);
    }

    public void release() {
        ObjectTracker.DecRef(this);
        wgpuSurfaceRelease(this);
    }

    public override string? ToString() => ObjectTracker.GetLabel(Handle);
}
