namespace Evergine.Bindings.WebGPU;
using static WebGPUNative;

/// <see href="https://developer.mozilla.org/en-US/docs/Web/API/GPURenderPipeline">MDN documentation</see>           
public unsafe partial struct WGPURenderPipeline
{
    public WGPUBindGroupLayout getBindGroupLayout(uint groupIndex) {
        var result = wgpuRenderPipelineGetBindGroupLayout(Handle, groupIndex);
        return result;
    }

    public void setLabel(ReadOnlySpan<char> label) {
        wgpuRenderPipelineSetLabel(Handle, label.AllocString());
    }

    public void reference() {
        wgpuRenderPipelineReference(Handle);
        ObjectTracker.IncRef(Handle);
    }

    public void release() {
        ObjectTracker.DecRef(Handle);
        wgpuRenderPipelineRelease(Handle);
    }

}
