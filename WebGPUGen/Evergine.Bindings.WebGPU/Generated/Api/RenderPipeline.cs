namespace Evergine.Bindings.WebGPU;
using static WebGPUNative;
           
public static unsafe partial class WebGPUExtensions
{
    public static WGPUBindGroupLayout getBindGroupLayout(this WGPURenderPipeline renderPipeline, uint groupIndex) {
        var result = wgpuRenderPipelineGetBindGroupLayout(renderPipeline, groupIndex);
        ObjectTracker.CreateRef(result.Handle);
        return result;
    }

    public static void setLabel(this WGPURenderPipeline renderPipeline, ReadOnlySpan<char> label) {
        wgpuRenderPipelineSetLabel(renderPipeline, label.AllocString());
    }

    public static void reference(this WGPURenderPipeline renderPipeline) {
        wgpuRenderPipelineReference(renderPipeline);
        ObjectTracker.IncRef(renderPipeline.Handle);
    }

    public static void release(this WGPURenderPipeline renderPipeline) {
        ObjectTracker.DecRef(renderPipeline.Handle);
        wgpuRenderPipelineRelease(renderPipeline);
    }

}
