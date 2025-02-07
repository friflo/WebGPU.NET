using static Evergine.Bindings.WebGPU.WebGPUNative;

// ReSharper disable InconsistentNaming
namespace Evergine.Bindings.WebGPU;

/// <see href="https://developer.mozilla.org/en-US/docs/Web/API/GPUBindGroup">MDN documentation</see>           
public unsafe partial struct WGPUBindGroup
{
    public void setLabel(Utf8 label) {
        wgpuBindGroupSetLabel(Handle, label.AllocUtf8());
    }

    public void reference() {
        ObjectTracker.IncRef(Handle);
        wgpuBindGroupReference(Handle);
    }

    public void release() {
        ObjectTracker.DecRef(Handle);
        wgpuBindGroupRelease(Handle);
    }

    public override string? ToString() => ObjectTracker.GetLabel(Handle);
}
