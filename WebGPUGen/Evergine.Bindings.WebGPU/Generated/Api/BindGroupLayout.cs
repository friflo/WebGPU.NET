using static Evergine.Bindings.WebGPU.WebGPUNative;

// ReSharper disable InconsistentNaming
namespace Evergine.Bindings.WebGPU;

/// <see href="https://developer.mozilla.org/en-US/docs/Web/API/GPUBindGroupLayout">MDN documentation</see>           
public unsafe partial struct WGPUBindGroupLayout
{
    public void setLabel(Utf8 label) {
        wgpuBindGroupLayoutSetLabel(Handle, label.AllocUtf8());
    }

    public void reference() {
        ObjectTracker.IncRef(Handle);
        wgpuBindGroupLayoutReference(Handle);
    }

    public void release() {
        ObjectTracker.DecRef(Handle);
        wgpuBindGroupLayoutRelease(Handle);
    }

    public override string? ToString() => ObjectTracker.GetLabel(Handle);
}
