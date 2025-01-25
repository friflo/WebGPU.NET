namespace Evergine.Bindings.WebGPU;
using static WebGPUNative;
           
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
