using System.Diagnostics;
using static Evergine.Bindings.WebGPU.WebGPUNative;

// ReSharper disable InconsistentNaming
namespace Evergine.Bindings.WebGPU;

/// <see href="https://developer.mozilla.org/en-US/docs/Web/API/GPUQuerySet">MDN documentation</see>           
public unsafe partial struct WGPUQuerySet
{
    public void destroy() {
        Validate_destroy(Handle);
        wgpuQuerySetDestroy(Handle);
    }

    [Conditional("VALIDATE")]
    private static void Validate_destroy(IntPtr handle) {
        ObjectTracker.ValidateHandle(handle);
    }

    public uint count => wgpuQuerySetGetCount(Handle);

    public WGPUQueryType type => wgpuQuerySetGetType(Handle);

    public void setLabel(Utf8 label) {
        Validate_setLabel(Handle, label);
        wgpuQuerySetSetLabel(Handle, label.AllocUtf8());
    }

    [Conditional("VALIDATE")]
    private static void Validate_setLabel(IntPtr handle, Utf8 label) {
        ObjectTracker.ValidateHandle(handle);
    }

    public void reference() {
        ObjectTracker.IncRef(Handle);
        wgpuQuerySetReference(Handle);
    }

    public void release() {
        ObjectTracker.DecRef(Handle);
        wgpuQuerySetRelease(Handle);
    }

    public override string? ToString() => ObjectTracker.GetLabel(Handle);
}
