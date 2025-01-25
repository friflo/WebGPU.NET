namespace Evergine.Bindings.WebGPU;
using static WebGPUNative;
           
public unsafe partial struct WGPURenderPassEncoder
{
    public void beginOcclusionQuery(uint queryIndex) {
        wgpuRenderPassEncoderBeginOcclusionQuery(Handle, queryIndex);
    }

    public void draw(uint vertexCount, uint instanceCount, uint firstVertex, uint firstInstance) {
        wgpuRenderPassEncoderDraw(Handle, vertexCount, instanceCount, firstVertex, firstInstance);
    }

    public void drawIndexed(uint indexCount, uint instanceCount, uint firstIndex, int baseVertex, uint firstInstance) {
        wgpuRenderPassEncoderDrawIndexed(Handle, indexCount, instanceCount, firstIndex, baseVertex, firstInstance);
    }

    public void drawIndexedIndirect(WGPUBuffer indirectBuffer, ulong indirectOffset) {
        wgpuRenderPassEncoderDrawIndexedIndirect(Handle, indirectBuffer, indirectOffset);
    }

    public void drawIndirect(WGPUBuffer indirectBuffer, ulong indirectOffset) {
        wgpuRenderPassEncoderDrawIndirect(Handle, indirectBuffer, indirectOffset);
    }

    public void end() {
        wgpuRenderPassEncoderEnd(Handle);
    }

    public void endOcclusionQuery() {
        wgpuRenderPassEncoderEndOcclusionQuery(Handle);
    }

    public void executeBundles(ulong bundleCount, WGPURenderBundle bundles) {
        wgpuRenderPassEncoderExecuteBundles(Handle, bundleCount, &bundles);
    }

    public void insertDebugMarker(ReadOnlySpan<char> markerLabel) {
        wgpuRenderPassEncoderInsertDebugMarker(Handle, markerLabel.AllocString());
    }

    public void popDebugGroup() {
        wgpuRenderPassEncoderPopDebugGroup(Handle);
    }

    public void pushDebugGroup(ReadOnlySpan<char> groupLabel) {
        wgpuRenderPassEncoderPushDebugGroup(Handle, groupLabel.AllocString());
    }

    public void setBindGroup(uint groupIndex, WGPUBindGroup group, ulong dynamicOffsetCount, uint* dynamicOffsets) {
        wgpuRenderPassEncoderSetBindGroup(Handle, groupIndex, group, dynamicOffsetCount, dynamicOffsets);
    }

    public void setBlendConstant(WGPUColor color) {
        wgpuRenderPassEncoderSetBlendConstant(Handle, &color);
    }

    public void setIndexBuffer(WGPUBuffer buffer, WGPUIndexFormat format, ulong offset, ulong size) {
        wgpuRenderPassEncoderSetIndexBuffer(Handle, buffer, format, offset, size);
    }

    public void setLabel(ReadOnlySpan<char> label) {
        wgpuRenderPassEncoderSetLabel(Handle, label.AllocString());
    }

    public void setPipeline(WGPURenderPipeline pipeline) {
        wgpuRenderPassEncoderSetPipeline(Handle, pipeline);
    }

    public void setScissorRect(uint x, uint y, uint width, uint height) {
        wgpuRenderPassEncoderSetScissorRect(Handle, x, y, width, height);
    }

    public void setStencilReference(uint reference) {
        wgpuRenderPassEncoderSetStencilReference(Handle, reference);
    }

    public void setVertexBuffer(uint slot, WGPUBuffer buffer, ulong offset, ulong size) {
        wgpuRenderPassEncoderSetVertexBuffer(Handle, slot, buffer, offset, size);
    }

    public void setViewport(float x, float y, float width, float height, float minDepth, float maxDepth) {
        wgpuRenderPassEncoderSetViewport(Handle, x, y, width, height, minDepth, maxDepth);
    }

    public void reference() {
        wgpuRenderPassEncoderReference(Handle);
        ObjectTracker.IncRef(Handle);
    }

    public void release() {
        ObjectTracker.DecRef(Handle);
        wgpuRenderPassEncoderRelease(Handle);
    }

    public void setPushConstants(WGPUShaderStage stages, uint offset, uint sizeBytes, void* data) {
        wgpuRenderPassEncoderSetPushConstants(Handle, stages, offset, sizeBytes, data);
    }

    public void multiDrawIndirect(WGPUBuffer buffer, ulong offset, uint count) {
        wgpuRenderPassEncoderMultiDrawIndirect(Handle, buffer, offset, count);
    }

    public void multiDrawIndexedIndirect(WGPUBuffer buffer, ulong offset, uint count) {
        wgpuRenderPassEncoderMultiDrawIndexedIndirect(Handle, buffer, offset, count);
    }

    public void multiDrawIndirectCount(WGPUBuffer buffer, ulong offset, WGPUBuffer count_buffer, ulong count_buffer_offset, uint max_count) {
        wgpuRenderPassEncoderMultiDrawIndirectCount(Handle, buffer, offset, count_buffer, count_buffer_offset, max_count);
    }

    public void multiDrawIndexedIndirectCount(WGPUBuffer buffer, ulong offset, WGPUBuffer count_buffer, ulong count_buffer_offset, uint max_count) {
        wgpuRenderPassEncoderMultiDrawIndexedIndirectCount(Handle, buffer, offset, count_buffer, count_buffer_offset, max_count);
    }

    public void beginPipelineStatisticsQuery(WGPUQuerySet querySet, uint queryIndex) {
        wgpuRenderPassEncoderBeginPipelineStatisticsQuery(Handle, querySet, queryIndex);
    }

    public void endPipelineStatisticsQuery() {
        wgpuRenderPassEncoderEndPipelineStatisticsQuery(Handle);
    }

}
