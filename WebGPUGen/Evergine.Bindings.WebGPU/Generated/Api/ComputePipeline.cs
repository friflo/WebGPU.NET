using System.Diagnostics;
using static Evergine.Bindings.WebGPU.WebGPUNative;

// ReSharper disable InconsistentNaming
namespace Evergine.Bindings.WebGPU;

/// <see href="https://developer.mozilla.org/en-US/docs/Web/API/GPUComputePipeline">MDN documentation</see>           
public unsafe partial struct WGPUComputePipeline
{
    public WGPUBindGroupLayout getBindGroupLayout(uint groupIndex) {
        Validate_getBindGroupLayout(Handle, groupIndex);
        var result = wgpuComputePipelineGetBindGroupLayout(this, groupIndex);
        return result;
    }

    [Conditional("VALIDATE")]
    private static void Validate_getBindGroupLayout(IntPtr handle, uint groupIndex) {
        ObjectTracker.ValidateHandle(handle);
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
