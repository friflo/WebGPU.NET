namespace Evergine.Bindings.WebGPU;
using static WebGPUNative;

/// <see href="https://developer.mozilla.org/en-US/docs/Web/API/GPUSampler">MDN documentation</see>           
public unsafe partial struct WGPUSampler
{
    public void setLabel(Utf8 label) {
        wgpuSamplerSetLabel(Handle, label.AllocUtf8());
    }

    public void reference() {
        wgpuSamplerReference(Handle);
        ObjectTracker.IncRef(Handle);
    }

    public void release() {
        ObjectTracker.DecRef(Handle);
        wgpuSamplerRelease(Handle);
    }

    public override string? ToString() => ObjectTracker.GetLabel(Handle);
}
