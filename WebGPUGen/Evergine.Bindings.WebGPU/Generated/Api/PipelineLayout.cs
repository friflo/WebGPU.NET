namespace Evergine.Bindings.WebGPU;
using static WebGPUNative;
           
public static unsafe partial class WebGPUExtensions
{
    public static void setLabel(this WGPUPipelineLayout pipelineLayout, ReadOnlySpan<char> label) {
        wgpuPipelineLayoutSetLabel(pipelineLayout, label.AllocString());
    }

    public static void reference(this WGPUPipelineLayout pipelineLayout) {
        wgpuPipelineLayoutReference(pipelineLayout);
        ObjectTracker.IncRef(pipelineLayout.Handle);
    }

    public static void release(this WGPUPipelineLayout pipelineLayout) {
        ObjectTracker.DecRef(pipelineLayout.Handle);
        wgpuPipelineLayoutRelease(pipelineLayout);
    }

}
