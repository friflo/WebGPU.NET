using System.Diagnostics;
using static Evergine.Bindings.WebGPU.WebGPUNative;

// ReSharper disable InconsistentNaming
namespace Evergine.Bindings.WebGPU;

/// <see href="https://developer.mozilla.org/en-US/docs/Web/API/GPURenderBundleEncoder">MDN documentation</see>           
public unsafe partial struct WGPURenderBundleEncoder
{
    public void draw(uint vertexCount, uint instanceCount, uint firstVertex, uint firstInstance) {
        Validate_draw(vertexCount, instanceCount, firstVertex, firstInstance);
        wgpuRenderBundleEncoderDraw(this, vertexCount, instanceCount, firstVertex, firstInstance);
    }

    [Conditional("VALIDATE")]
    private void Validate_draw(uint vertexCount, uint instanceCount, uint firstVertex, uint firstInstance) {
        ObjectTracker.ValidateHandle(this);
    }

    public void drawIndexed(uint indexCount, uint instanceCount, uint firstIndex, int baseVertex, uint firstInstance) {
        Validate_drawIndexed(indexCount, instanceCount, firstIndex, baseVertex, firstInstance);
        wgpuRenderBundleEncoderDrawIndexed(this, indexCount, instanceCount, firstIndex, baseVertex, firstInstance);
    }

    [Conditional("VALIDATE")]
    private void Validate_drawIndexed(uint indexCount, uint instanceCount, uint firstIndex, int baseVertex, uint firstInstance) {
        ObjectTracker.ValidateHandle(this);
    }



    public WGPURenderBundle finish(WGPURenderBundleDescriptor descriptor) {
        Validate_finish(descriptor);
        var result = wgpuRenderBundleEncoderFinish(this, &descriptor);
        ObjectTracker.CreateRefLabel(result, HandleType.WGPURenderBundle, descriptor._label); // ref-other
        return result;
    }

    [Conditional("VALIDATE")]
    private void Validate_finish(WGPURenderBundleDescriptor descriptor) {
        ObjectTracker.ValidateHandle(this);
        descriptor.Validate();
    }

    public void insertDebugMarker(Utf8 markerLabel) {
        Validate_insertDebugMarker(markerLabel);
        wgpuRenderBundleEncoderInsertDebugMarker(this, markerLabel.AllocUtf8());
    }

    [Conditional("VALIDATE")]
    private void Validate_insertDebugMarker(Utf8 markerLabel) {
        ObjectTracker.ValidateHandle(this);
    }

    public void popDebugGroup() {
        Validate_popDebugGroup();
        wgpuRenderBundleEncoderPopDebugGroup(this);
    }

    [Conditional("VALIDATE")]
    private void Validate_popDebugGroup() {
        ObjectTracker.ValidateHandle(this);
    }

    public void pushDebugGroup(Utf8 groupLabel) {
        Validate_pushDebugGroup(groupLabel);
        wgpuRenderBundleEncoderPushDebugGroup(this, groupLabel.AllocUtf8());
    }

    [Conditional("VALIDATE")]
    private void Validate_pushDebugGroup(Utf8 groupLabel) {
        ObjectTracker.ValidateHandle(this);
    }

    public void setBindGroup(uint groupIndex, WGPUBindGroup group, ReadOnlySpan<uint> dynamicOffsets) {
        fixed (uint* ptr = dynamicOffsets) {
            wgpuRenderBundleEncoderSetBindGroup(this, groupIndex, group, (ulong)dynamicOffsets.Length, ptr);    
        }
    }

    public void setIndexBuffer(WGPUBuffer buffer, WGPUIndexFormat format, ulong offset, ulong size) {
        Validate_setIndexBuffer(buffer, format, offset, size);
        wgpuRenderBundleEncoderSetIndexBuffer(this, buffer, format, offset, size);
    }

    [Conditional("VALIDATE")]
    private void Validate_setIndexBuffer(WGPUBuffer buffer, WGPUIndexFormat format, ulong offset, ulong size) {
        ObjectTracker.ValidateHandle(this);
        ObjectTracker.ValidateHandleParam(buffer);
    }


    public void setPipeline(WGPURenderPipeline pipeline) {
        Validate_setPipeline(pipeline);
        wgpuRenderBundleEncoderSetPipeline(this, pipeline);
    }

    [Conditional("VALIDATE")]
    private void Validate_setPipeline(WGPURenderPipeline pipeline) {
        ObjectTracker.ValidateHandle(this);
        ObjectTracker.ValidateHandleParam(pipeline);
    }

    public void setVertexBuffer(uint slot, WGPUBuffer buffer, ulong offset, ulong size) {
        Validate_setVertexBuffer(slot, buffer, offset, size);
        wgpuRenderBundleEncoderSetVertexBuffer(this, slot, buffer, offset, size);
    }

    [Conditional("VALIDATE")]
    private void Validate_setVertexBuffer(uint slot, WGPUBuffer buffer, ulong offset, ulong size) {
        ObjectTracker.ValidateHandle(this);
        ObjectTracker.ValidateHandleParam(buffer);
    }

    public void reference() {
        ObjectTracker.IncRef(this);
        wgpuRenderBundleEncoderReference(this);
    }

    public void release() {
        ObjectTracker.DecRef(this);
        wgpuRenderBundleEncoderRelease(this);
    }
    
    public void Dispose() {
        ObjectTracker.DecRef(this);
        wgpuRenderBundleEncoderRelease(this);
    }

    public override string? ToString() => ObjectTracker.GetLabel(Handle);
}
