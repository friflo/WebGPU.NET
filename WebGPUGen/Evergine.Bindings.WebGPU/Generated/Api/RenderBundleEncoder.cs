using System.Diagnostics;
using static Evergine.Bindings.WebGPU.WebGPUNative;

// ReSharper disable InconsistentNaming
namespace Evergine.Bindings.WebGPU;

/// <see href="https://developer.mozilla.org/en-US/docs/Web/API/GPURenderBundleEncoder">MDN documentation</see>           
public unsafe partial struct WGPURenderBundleEncoder
{
    public void draw(uint vertexCount, uint instanceCount, uint firstVertex, uint firstInstance) {
        Validate_draw(Handle, vertexCount, instanceCount, firstVertex, firstInstance);
        wgpuRenderBundleEncoderDraw(this, vertexCount, instanceCount, firstVertex, firstInstance);
    }

    [Conditional("VALIDATE")]
    private static void Validate_draw(IntPtr handle, uint vertexCount, uint instanceCount, uint firstVertex, uint firstInstance) {
        ObjectTracker.ValidateHandle(handle);
    }

    public void drawIndexed(uint indexCount, uint instanceCount, uint firstIndex, int baseVertex, uint firstInstance) {
        Validate_drawIndexed(Handle, indexCount, instanceCount, firstIndex, baseVertex, firstInstance);
        wgpuRenderBundleEncoderDrawIndexed(this, indexCount, instanceCount, firstIndex, baseVertex, firstInstance);
    }

    [Conditional("VALIDATE")]
    private static void Validate_drawIndexed(IntPtr handle, uint indexCount, uint instanceCount, uint firstIndex, int baseVertex, uint firstInstance) {
        ObjectTracker.ValidateHandle(handle);
    }



    public WGPURenderBundle finish(WGPURenderBundleDescriptor descriptor) {
        Validate_finish(Handle, descriptor);
        var result = wgpuRenderBundleEncoderFinish(this, &descriptor);
        ObjectTracker.CreateRef(result, HandleType.WGPURenderBundle, descriptor._label);
        return result;
    }

    [Conditional("VALIDATE")]
    private static void Validate_finish(IntPtr handle, WGPURenderBundleDescriptor descriptor) {
        ObjectTracker.ValidateHandle(handle);
        descriptor.Validate();
    }

    public void insertDebugMarker(Utf8 markerLabel) {
        Validate_insertDebugMarker(Handle, markerLabel);
        wgpuRenderBundleEncoderInsertDebugMarker(this, markerLabel.AllocUtf8());
    }

    [Conditional("VALIDATE")]
    private static void Validate_insertDebugMarker(IntPtr handle, Utf8 markerLabel) {
        ObjectTracker.ValidateHandle(handle);
    }

    public void popDebugGroup() {
        Validate_popDebugGroup(Handle);
        wgpuRenderBundleEncoderPopDebugGroup(this);
    }

    [Conditional("VALIDATE")]
    private static void Validate_popDebugGroup(IntPtr handle) {
        ObjectTracker.ValidateHandle(handle);
    }

    public void pushDebugGroup(Utf8 groupLabel) {
        Validate_pushDebugGroup(Handle, groupLabel);
        wgpuRenderBundleEncoderPushDebugGroup(this, groupLabel.AllocUtf8());
    }

    [Conditional("VALIDATE")]
    private static void Validate_pushDebugGroup(IntPtr handle, Utf8 groupLabel) {
        ObjectTracker.ValidateHandle(handle);
    }

    public void setBindGroup(uint groupIndex, WGPUBindGroup group, ReadOnlySpan<uint> dynamicOffsets) {
        fixed (uint* ptr = dynamicOffsets) {
            wgpuRenderBundleEncoderSetBindGroup(this, groupIndex, group, (ulong)dynamicOffsets.Length, ptr);    
        }
    }

    public void setIndexBuffer(WGPUBuffer buffer, WGPUIndexFormat format, ulong offset, ulong size) {
        Validate_setIndexBuffer(Handle, buffer, format, offset, size);
        wgpuRenderBundleEncoderSetIndexBuffer(this, buffer, format, offset, size);
    }

    [Conditional("VALIDATE")]
    private static void Validate_setIndexBuffer(IntPtr handle, WGPUBuffer buffer, WGPUIndexFormat format, ulong offset, ulong size) {
        ObjectTracker.ValidateHandle(handle);
    }


    public void setPipeline(WGPURenderPipeline pipeline) {
        Validate_setPipeline(Handle, pipeline);
        wgpuRenderBundleEncoderSetPipeline(this, pipeline);
    }

    [Conditional("VALIDATE")]
    private static void Validate_setPipeline(IntPtr handle, WGPURenderPipeline pipeline) {
        ObjectTracker.ValidateHandle(handle);
    }

    public void setVertexBuffer(uint slot, WGPUBuffer buffer, ulong offset, ulong size) {
        Validate_setVertexBuffer(Handle, slot, buffer, offset, size);
        wgpuRenderBundleEncoderSetVertexBuffer(this, slot, buffer, offset, size);
    }

    [Conditional("VALIDATE")]
    private static void Validate_setVertexBuffer(IntPtr handle, uint slot, WGPUBuffer buffer, ulong offset, ulong size) {
        ObjectTracker.ValidateHandle(handle);
    }

    public void reference() {
        ObjectTracker.IncRef(this);
        wgpuRenderBundleEncoderReference(this);
    }

    public void release() {
        ObjectTracker.DecRef(this);
        wgpuRenderBundleEncoderRelease(this);
    }

    public override string? ToString() => ObjectTracker.GetLabel(Handle);
}
