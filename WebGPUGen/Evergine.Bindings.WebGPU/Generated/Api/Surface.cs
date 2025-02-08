using System.Diagnostics;
using static Evergine.Bindings.WebGPU.WebGPUNative;

// ReSharper disable InconsistentNaming
namespace Evergine.Bindings.WebGPU;

/// No counterpart in JavaScript WebGPU           
public unsafe partial struct WGPUSurface
{
    public void configure(WGPUSurfaceConfiguration config) {
        Validate_configure(Handle, config);
        wgpuSurfaceConfigure(Handle, &config);
    }

    [Conditional("VALIDATE")]
    private static void Validate_configure(IntPtr handle, WGPUSurfaceConfiguration config) {
        ObjectTracker.ValidateHandle(handle);
        config.Validate();
    }

    // getCapabilities() - not generated. See: Surface_NG.cs

    public WGPUSurfaceTexture currentTexture { get {
        var result = new WGPUSurfaceTexture();
        wgpuSurfaceGetCurrentTexture(Handle, &result);
        return result;
    } }

    public void present() {
        Validate_present(Handle);
        wgpuSurfacePresent(Handle);
    }

    [Conditional("VALIDATE")]
    private static void Validate_present(IntPtr handle) {
        ObjectTracker.ValidateHandle(handle);
    }


    public void unconfigure() {
        Validate_unconfigure(Handle);
        wgpuSurfaceUnconfigure(Handle);
    }

    [Conditional("VALIDATE")]
    private static void Validate_unconfigure(IntPtr handle) {
        ObjectTracker.ValidateHandle(handle);
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
