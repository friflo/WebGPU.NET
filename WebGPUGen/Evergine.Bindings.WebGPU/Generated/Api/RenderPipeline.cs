using System.Diagnostics;
using static Evergine.Bindings.WebGPU.WebGPUNative;

// ReSharper disable InconsistentNaming
namespace Evergine.Bindings.WebGPU;

/// <see href="https://developer.mozilla.org/en-US/docs/Web/API/GPURenderPipeline">MDN documentation</see>           
public unsafe partial struct WGPURenderPipeline
{
    public WGPUBindGroupLayout getBindGroupLayout(uint groupIndex) {
        Validate_getBindGroupLayout(Handle, groupIndex);
        var result = wgpuRenderPipelineGetBindGroupLayout(this, groupIndex);
        return result;
    }

    [Conditional("VALIDATE")]
    private void Validate_getBindGroupLayout(IntPtr handle, uint groupIndex) {
        ObjectTracker.ValidateHandle(this);
    }


    public void reference() {
        ObjectTracker.IncRef(this);
        wgpuRenderPipelineReference(this);
    }

    public void release() {
        ObjectTracker.DecRef(this);
        wgpuRenderPipelineRelease(this);
    }

    public override string? ToString() => ObjectTracker.GetLabel(Handle);
}
