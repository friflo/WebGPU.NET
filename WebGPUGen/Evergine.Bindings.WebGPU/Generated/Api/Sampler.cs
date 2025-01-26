namespace Evergine.Bindings.WebGPU;
using static WebGPUNative;

/// <see href="https://developer.mozilla.org/en-US/docs/Web/API/GPUSampler">MDN documentation</see>           
public unsafe partial struct WGPUSampler
{
    public void setLabel(ReadOnlySpan<char> label) {
        wgpuSamplerSetLabel(Handle, label.AllocString());
    }

    public void reference() {
        wgpuSamplerReference(Handle);
        ObjectTracker.IncRef(Handle);
    }

    public void release() {
        ObjectTracker.DecRef(Handle);
        wgpuSamplerRelease(Handle);
    }

}
