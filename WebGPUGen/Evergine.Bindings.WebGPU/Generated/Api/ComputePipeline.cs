namespace Evergine.Bindings.WebGPU;
using static WebGPUNative;
           
public unsafe partial struct WGPUComputePipeline
{
    public WGPUBindGroupLayout getBindGroupLayout(uint groupIndex) {
        var result = wgpuComputePipelineGetBindGroupLayout(Handle, groupIndex);
        return result;
    }

    public void setLabel(ReadOnlySpan<char> label) {
        wgpuComputePipelineSetLabel(Handle, label.AllocString());
    }

    public void reference() {
        wgpuComputePipelineReference(Handle);
        ObjectTracker.IncRef(Handle);
    }

    public void release() {
        ObjectTracker.DecRef(Handle);
        wgpuComputePipelineRelease(Handle);
    }

}
