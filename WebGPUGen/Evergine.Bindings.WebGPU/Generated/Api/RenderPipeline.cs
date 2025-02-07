using static Evergine.Bindings.WebGPU.WebGPUNative;

// ReSharper disable InconsistentNaming
namespace Evergine.Bindings.WebGPU;

/// <see href="https://developer.mozilla.org/en-US/docs/Web/API/GPURenderPipeline">MDN documentation</see>           
public unsafe partial struct WGPURenderPipeline
{
    public WGPUBindGroupLayout getBindGroupLayout(uint groupIndex) {
        Validate_getBindGroupLayout(Handle, groupIndex);
        var result = wgpuRenderPipelineGetBindGroupLayout(Handle, groupIndex);
        return result;
    }

    private static void Validate_getBindGroupLayout(IntPtr handle, uint groupIndex) {
        ObjectTracker.ValidateHandle(handle);
    }

    public void setLabel(Utf8 label) {
        Validate_setLabel(Handle, label);
        wgpuRenderPipelineSetLabel(Handle, label.AllocUtf8());
    }

    private static void Validate_setLabel(IntPtr handle, Utf8 label) {
        ObjectTracker.ValidateHandle(handle);
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
