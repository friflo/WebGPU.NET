namespace Evergine.Bindings.WebGPU;
using static WebGPUNative;

/// <see href="https://developer.mozilla.org/en-US/docs/Web/API/GPUComputePipeline">MDN documentation</see>           
public unsafe partial struct WGPUComputePipeline
{
    public WGPUBindGroupLayout getBindGroupLayout(uint groupIndex) {
        var result = wgpuComputePipelineGetBindGroupLayout(Handle, groupIndex);
        return result;
    }

    public void setLabel(Utf8 label) {
        wgpuComputePipelineSetLabel(Handle, label.AllocUtf8());
    }

    public void reference() {
        ObjectTracker.IncRef(Handle);
        wgpuComputePipelineReference(Handle);
    }

    public void release() {
        ObjectTracker.DecRef(Handle);
        wgpuComputePipelineRelease(Handle);
    }

    public override string? ToString() => ObjectTracker.GetLabel(Handle);
}
