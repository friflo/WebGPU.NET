using static Evergine.Bindings.WebGPU.WebGPUNative;

// ReSharper disable InconsistentNaming
namespace Evergine.Bindings.WebGPU;

/// <see href="https://developer.mozilla.org/en-US/docs/Web/API/GPURenderBundle">MDN documentation</see>           
public unsafe partial struct WGPURenderBundle
{
    public void setLabel(Utf8 label) {
        wgpuRenderBundleSetLabel(Handle, label.AllocUtf8());
    }

    public void reference() {
        ObjectTracker.IncRef(Handle);
        wgpuRenderBundleReference(Handle);
    }

    public void release() {
        ObjectTracker.DecRef(Handle);
        wgpuRenderBundleRelease(Handle);
    }

    public override string? ToString() => ObjectTracker.GetLabel(Handle);
}
