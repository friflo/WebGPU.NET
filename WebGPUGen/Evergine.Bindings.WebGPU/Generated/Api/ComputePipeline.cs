using System.Diagnostics;
using static Evergine.Bindings.WebGPU.WebGPUNative;

// ReSharper disable InconsistentNaming
namespace Evergine.Bindings.WebGPU;

/// <see href="https://developer.mozilla.org/en-US/docs/Web/API/GPUComputePipeline">MDN documentation</see>           
public unsafe partial struct WGPUComputePipeline
{
    public WGPUBindGroupLayout getBindGroupLayout(uint groupIndex) {
        Validate_getBindGroupLayout(groupIndex);
        var result = wgpuComputePipelineGetBindGroupLayout(this, groupIndex);
        ObjectTracker.CreateRef(result, HandleType.WGPUBindGroupLayout, null); // ref-other
        return result;
    }

    [Conditional("VALIDATE")]
    private void Validate_getBindGroupLayout(uint groupIndex) {
        ObjectTracker.ValidateHandle(this);
    }


    public void reference() {
        ObjectTracker.IncRef(this);
        wgpuComputePipelineReference(this);
    }

    public void release() {
        ObjectTracker.DecRef(this);
        wgpuComputePipelineRelease(this);
    }

    public override string? ToString() => ObjectTracker.GetLabel(Handle);
}
