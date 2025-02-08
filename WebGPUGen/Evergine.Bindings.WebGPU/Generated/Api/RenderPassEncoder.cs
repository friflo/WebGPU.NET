using System.Diagnostics;
using static Evergine.Bindings.WebGPU.WebGPUNative;

// ReSharper disable InconsistentNaming
namespace Evergine.Bindings.WebGPU;

/// <see href="https://developer.mozilla.org/en-US/docs/Web/API/GPURenderPassEncoder">MDN documentation</see>           
public unsafe partial struct WGPURenderPassEncoder
{
    public void beginOcclusionQuery(uint queryIndex) {
        Validate_beginOcclusionQuery(queryIndex);
        wgpuRenderPassEncoderBeginOcclusionQuery(this, queryIndex);
    }

    [Conditional("VALIDATE")]
    private void Validate_beginOcclusionQuery(uint queryIndex) {
        ObjectTracker.ValidateHandle(this);
    }

    public void draw(uint vertexCount, uint instanceCount, uint firstVertex, uint firstInstance) {
        Validate_draw(vertexCount, instanceCount, firstVertex, firstInstance);
        wgpuRenderPassEncoderDraw(this, vertexCount, instanceCount, firstVertex, firstInstance);
    }

    [Conditional("VALIDATE")]
    private void Validate_draw(uint vertexCount, uint instanceCount, uint firstVertex, uint firstInstance) {
        ObjectTracker.ValidateHandle(this);
    }

    public void drawIndexed(uint indexCount, uint instanceCount, uint firstIndex, int baseVertex, uint firstInstance) {
        Validate_drawIndexed(indexCount, instanceCount, firstIndex, baseVertex, firstInstance);
        wgpuRenderPassEncoderDrawIndexed(this, indexCount, instanceCount, firstIndex, baseVertex, firstInstance);
    }

    [Conditional("VALIDATE")]
    private void Validate_drawIndexed(uint indexCount, uint instanceCount, uint firstIndex, int baseVertex, uint firstInstance) {
        ObjectTracker.ValidateHandle(this);
    }

    public void drawIndexedIndirect(WGPUBuffer indirectBuffer, ulong indirectOffset) {
        Validate_drawIndexedIndirect(indirectBuffer, indirectOffset);
        wgpuRenderPassEncoderDrawIndexedIndirect(this, indirectBuffer, indirectOffset);
    }

    [Conditional("VALIDATE")]
    private void Validate_drawIndexedIndirect(WGPUBuffer indirectBuffer, ulong indirectOffset) {
        ObjectTracker.ValidateHandle(this);
        ObjectTracker.ValidateHandleParam(indirectBuffer);
    }

    public void drawIndirect(WGPUBuffer indirectBuffer, ulong indirectOffset) {
        Validate_drawIndirect(indirectBuffer, indirectOffset);
        wgpuRenderPassEncoderDrawIndirect(this, indirectBuffer, indirectOffset);
    }

    [Conditional("VALIDATE")]
    private void Validate_drawIndirect(WGPUBuffer indirectBuffer, ulong indirectOffset) {
        ObjectTracker.ValidateHandle(this);
        ObjectTracker.ValidateHandleParam(indirectBuffer);
    }

    public void end() {
        Validate_end();
        wgpuRenderPassEncoderEnd(this);
    }

    [Conditional("VALIDATE")]
    private void Validate_end() {
        ObjectTracker.ValidateHandle(this);
    }

    public void endOcclusionQuery() {
        Validate_endOcclusionQuery();
        wgpuRenderPassEncoderEndOcclusionQuery(this);
    }

    [Conditional("VALIDATE")]
    private void Validate_endOcclusionQuery() {
        ObjectTracker.ValidateHandle(this);
    }

    public void executeBundles(ulong bundleCount, WGPURenderBundle bundles) {
        Validate_executeBundles(bundleCount, bundles);
        wgpuRenderPassEncoderExecuteBundles(this, bundleCount, &bundles);
    }

    [Conditional("VALIDATE")]
    private void Validate_executeBundles(ulong bundleCount, WGPURenderBundle bundles) {
        ObjectTracker.ValidateHandle(this);
    }

    public void insertDebugMarker(Utf8 markerLabel) {
        Validate_insertDebugMarker(markerLabel);
        wgpuRenderPassEncoderInsertDebugMarker(this, markerLabel.AllocUtf8());
    }

    [Conditional("VALIDATE")]
    private void Validate_insertDebugMarker(Utf8 markerLabel) {
        ObjectTracker.ValidateHandle(this);
    }

    public void popDebugGroup() {
        Validate_popDebugGroup();
        wgpuRenderPassEncoderPopDebugGroup(this);
    }

    [Conditional("VALIDATE")]
    private void Validate_popDebugGroup() {
        ObjectTracker.ValidateHandle(this);
    }

    public void pushDebugGroup(Utf8 groupLabel) {
        Validate_pushDebugGroup(groupLabel);
        wgpuRenderPassEncoderPushDebugGroup(this, groupLabel.AllocUtf8());
    }

    [Conditional("VALIDATE")]
    private void Validate_pushDebugGroup(Utf8 groupLabel) {
        ObjectTracker.ValidateHandle(this);
    }

    public void setBindGroup(uint groupIndex, WGPUBindGroup group, ReadOnlySpan<uint> dynamicOffsets) {
        fixed (uint* ptr = dynamicOffsets) {
            wgpuRenderPassEncoderSetBindGroup(this, groupIndex, group, (ulong)dynamicOffsets.Length, ptr);    
        }
    }

    public void setBlendConstant(WGPUColor color) {
        Validate_setBlendConstant(color);
        wgpuRenderPassEncoderSetBlendConstant(this, &color);
    }

    [Conditional("VALIDATE")]
    private void Validate_setBlendConstant(WGPUColor color) {
        ObjectTracker.ValidateHandle(this);
    }

    public void setIndexBuffer(WGPUBuffer buffer, WGPUIndexFormat format, ulong offset, ulong size) {
        Validate_setIndexBuffer(buffer, format, offset, size);
        wgpuRenderPassEncoderSetIndexBuffer(this, buffer, format, offset, size);
    }

    [Conditional("VALIDATE")]
    private void Validate_setIndexBuffer(WGPUBuffer buffer, WGPUIndexFormat format, ulong offset, ulong size) {
        ObjectTracker.ValidateHandle(this);
        ObjectTracker.ValidateHandleParam(buffer);
    }


    public void setPipeline(WGPURenderPipeline pipeline) {
        Validate_setPipeline(pipeline);
        wgpuRenderPassEncoderSetPipeline(this, pipeline);
    }

    [Conditional("VALIDATE")]
    private void Validate_setPipeline(WGPURenderPipeline pipeline) {
        ObjectTracker.ValidateHandle(this);
        ObjectTracker.ValidateHandleParam(pipeline);
    }

    public void setScissorRect(uint x, uint y, uint width, uint height) {
        Validate_setScissorRect(x, y, width, height);
        wgpuRenderPassEncoderSetScissorRect(this, x, y, width, height);
    }

    [Conditional("VALIDATE")]
    private void Validate_setScissorRect(uint x, uint y, uint width, uint height) {
        ObjectTracker.ValidateHandle(this);
    }

    public void setStencilReference(uint reference) {
        Validate_setStencilReference(reference);
        wgpuRenderPassEncoderSetStencilReference(this, reference);
    }

    [Conditional("VALIDATE")]
    private void Validate_setStencilReference(uint reference) {
        ObjectTracker.ValidateHandle(this);
    }

    public void setVertexBuffer(uint slot, WGPUBuffer buffer, ulong offset, ulong size) {
        Validate_setVertexBuffer(slot, buffer, offset, size);
        wgpuRenderPassEncoderSetVertexBuffer(this, slot, buffer, offset, size);
    }

    [Conditional("VALIDATE")]
    private void Validate_setVertexBuffer(uint slot, WGPUBuffer buffer, ulong offset, ulong size) {
        ObjectTracker.ValidateHandle(this);
        ObjectTracker.ValidateHandleParam(buffer);
    }

    public void setViewport(float x, float y, float width, float height, float minDepth, float maxDepth) {
        Validate_setViewport(x, y, width, height, minDepth, maxDepth);
        wgpuRenderPassEncoderSetViewport(this, x, y, width, height, minDepth, maxDepth);
    }

    [Conditional("VALIDATE")]
    private void Validate_setViewport(float x, float y, float width, float height, float minDepth, float maxDepth) {
        ObjectTracker.ValidateHandle(this);
    }

    public void reference() {
        ObjectTracker.IncRef(this);
        wgpuRenderPassEncoderReference(this);
    }

    public void release() {
        ObjectTracker.DecRef(this);
        wgpuRenderPassEncoderRelease(this);
    }

    // setPushConstants() - not generated. See: RenderPassEncoder_NG.cs

    public void multiDrawIndirect(WGPUBuffer buffer, ulong offset, uint count) {
        Validate_multiDrawIndirect(buffer, offset, count);
        wgpuRenderPassEncoderMultiDrawIndirect(this, buffer, offset, count);
    }

    [Conditional("VALIDATE")]
    private void Validate_multiDrawIndirect(WGPUBuffer buffer, ulong offset, uint count) {
        ObjectTracker.ValidateHandle(this);
        ObjectTracker.ValidateHandleParam(buffer);
    }

    public void multiDrawIndexedIndirect(WGPUBuffer buffer, ulong offset, uint count) {
        Validate_multiDrawIndexedIndirect(buffer, offset, count);
        wgpuRenderPassEncoderMultiDrawIndexedIndirect(this, buffer, offset, count);
    }

    [Conditional("VALIDATE")]
    private void Validate_multiDrawIndexedIndirect(WGPUBuffer buffer, ulong offset, uint count) {
        ObjectTracker.ValidateHandle(this);
        ObjectTracker.ValidateHandleParam(buffer);
    }

    public void multiDrawIndirectCount(WGPUBuffer buffer, ulong offset, WGPUBuffer count_buffer, ulong count_buffer_offset, uint max_count) {
        Validate_multiDrawIndirectCount(buffer, offset, count_buffer, count_buffer_offset, max_count);
        wgpuRenderPassEncoderMultiDrawIndirectCount(this, buffer, offset, count_buffer, count_buffer_offset, max_count);
    }

    [Conditional("VALIDATE")]
    private void Validate_multiDrawIndirectCount(WGPUBuffer buffer, ulong offset, WGPUBuffer count_buffer, ulong count_buffer_offset, uint max_count) {
        ObjectTracker.ValidateHandle(this);
        ObjectTracker.ValidateHandleParam(buffer);
        ObjectTracker.ValidateHandleParam(count_buffer);
    }

    public void multiDrawIndexedIndirectCount(WGPUBuffer buffer, ulong offset, WGPUBuffer count_buffer, ulong count_buffer_offset, uint max_count) {
        Validate_multiDrawIndexedIndirectCount(buffer, offset, count_buffer, count_buffer_offset, max_count);
        wgpuRenderPassEncoderMultiDrawIndexedIndirectCount(this, buffer, offset, count_buffer, count_buffer_offset, max_count);
    }

    [Conditional("VALIDATE")]
    private void Validate_multiDrawIndexedIndirectCount(WGPUBuffer buffer, ulong offset, WGPUBuffer count_buffer, ulong count_buffer_offset, uint max_count) {
        ObjectTracker.ValidateHandle(this);
        ObjectTracker.ValidateHandleParam(buffer);
        ObjectTracker.ValidateHandleParam(count_buffer);
    }

    public void beginPipelineStatisticsQuery(WGPUQuerySet querySet, uint queryIndex) {
        Validate_beginPipelineStatisticsQuery(querySet, queryIndex);
        wgpuRenderPassEncoderBeginPipelineStatisticsQuery(this, querySet, queryIndex);
    }

    [Conditional("VALIDATE")]
    private void Validate_beginPipelineStatisticsQuery(WGPUQuerySet querySet, uint queryIndex) {
        ObjectTracker.ValidateHandle(this);
        ObjectTracker.ValidateHandleParam(querySet);
    }

    public void endPipelineStatisticsQuery() {
        Validate_endPipelineStatisticsQuery();
        wgpuRenderPassEncoderEndPipelineStatisticsQuery(this);
    }

    [Conditional("VALIDATE")]
    private void Validate_endPipelineStatisticsQuery() {
        ObjectTracker.ValidateHandle(this);
    }

    public override string? ToString() => ObjectTracker.GetLabel(Handle);
}
