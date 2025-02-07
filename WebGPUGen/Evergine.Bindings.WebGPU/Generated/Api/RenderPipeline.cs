using static Evergine.Bindings.WebGPU.WebGPUNative;

// ReSharper disable InconsistentNaming
namespace Evergine.Bindings.WebGPU;

/// <see href="https://developer.mozilla.org/en-US/docs/Web/API/GPURenderPipeline">MDN documentation</see>           
public unsafe partial struct WGPURenderPipeline
{
    public WGPUBindGroupLayout getBindGroupLayout(uint groupIndex) {
        var result = wgpuRenderPipelineGetBindGroupLayout(Handle, groupIndex);
        return result;
    }

    public void setLabel(Utf8 label) {
        wgpuRenderPipelineSetLabel(Handle, label.AllocUtf8());
    }

    public void reference() {
        ObjectTracker.IncRef(Handle);
        wgpuRenderPipelineReference(Handle);
    }

    public void release() {
        ObjectTracker.DecRef(Handle);
        wgpuRenderPipelineRelease(Handle);
    }

    public override string? ToString() => ObjectTracker.GetLabel(Handle);
}
