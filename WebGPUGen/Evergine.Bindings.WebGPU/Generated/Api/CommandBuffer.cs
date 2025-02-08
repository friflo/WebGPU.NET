using System.Diagnostics;
using static Evergine.Bindings.WebGPU.WebGPUNative;

// ReSharper disable InconsistentNaming
namespace Evergine.Bindings.WebGPU;

/// <see href="https://developer.mozilla.org/en-US/docs/Web/API/GPUCommandBuffer">MDN documentation</see>           
public unsafe partial struct WGPUCommandBuffer
{
    public void setLabel(Utf8 label) {
        Validate_setLabel(Handle, label);
        wgpuCommandBufferSetLabel(Handle, label.AllocUtf8());
    }

    [Conditional("VALIDATE")]
    private static void Validate_setLabel(IntPtr handle, Utf8 label) {
        ObjectTracker.ValidateHandle(handle);
    }

    public void reference() {
        ObjectTracker.IncRef(Handle);
        wgpuCommandBufferReference(Handle);
    }

    public void release() {
        ObjectTracker.DecRef(Handle);
        wgpuCommandBufferRelease(Handle);
    }

    public override string? ToString() => ObjectTracker.GetLabel(Handle);
}
