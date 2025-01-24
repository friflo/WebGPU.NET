namespace Evergine.Bindings.WebGPU;
using static WebGPUNative;
           
public static unsafe partial class WebGPUExtensions
{
    public static void setLabel(this WGPUSampler sampler, ReadOnlySpan<char> label) {
        wgpuSamplerSetLabel(sampler, label.AllocString());
    }

    public static void reference(this WGPUSampler sampler) {
        wgpuSamplerReference(sampler);
        ObjectTracker.IncRef(sampler.Handle);
    }

    public static void release(this WGPUSampler sampler) {
        ObjectTracker.DecRef(sampler.Handle);
        wgpuSamplerRelease(sampler);
    }

}
