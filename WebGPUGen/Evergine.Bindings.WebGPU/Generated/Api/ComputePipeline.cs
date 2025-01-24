namespace Evergine.Bindings.WebGPU;
using static WebGPUNative;
           
public static unsafe partial class WebGPUExtensions
{
    public static WGPUBindGroupLayout getBindGroupLayout(this WGPUComputePipeline computePipeline, uint groupIndex) {
        var result = wgpuComputePipelineGetBindGroupLayout(computePipeline, groupIndex);
        ObjectTracker.CreateRef(result.Handle);
        return result;
    }

    public static void setLabel(this WGPUComputePipeline computePipeline, ReadOnlySpan<char> label) {
        wgpuComputePipelineSetLabel(computePipeline, label.AllocString());
    }

    public static void reference(this WGPUComputePipeline computePipeline) {
        wgpuComputePipelineReference(computePipeline);
        ObjectTracker.IncRef(computePipeline.Handle);
    }

    public static void release(this WGPUComputePipeline computePipeline) {
        ObjectTracker.DecRef(computePipeline.Handle);
        wgpuComputePipelineRelease(computePipeline);
    }

}
