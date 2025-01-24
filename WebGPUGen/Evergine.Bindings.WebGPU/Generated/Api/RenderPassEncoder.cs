namespace Evergine.Bindings.WebGPU;
using static WebGPUNative;
           
public static unsafe partial class WebGPUExtensions
{
    public static void beginOcclusionQuery(this WGPURenderPassEncoder renderPassEncoder, uint queryIndex) {
        wgpuRenderPassEncoderBeginOcclusionQuery(renderPassEncoder, queryIndex);
    }

    public static void draw(this WGPURenderPassEncoder renderPassEncoder, uint vertexCount, uint instanceCount, uint firstVertex, uint firstInstance) {
        wgpuRenderPassEncoderDraw(renderPassEncoder, vertexCount, instanceCount, firstVertex, firstInstance);
    }

    public static void drawIndexed(this WGPURenderPassEncoder renderPassEncoder, uint indexCount, uint instanceCount, uint firstIndex, int baseVertex, uint firstInstance) {
        wgpuRenderPassEncoderDrawIndexed(renderPassEncoder, indexCount, instanceCount, firstIndex, baseVertex, firstInstance);
    }

    public static void drawIndexedIndirect(this WGPURenderPassEncoder renderPassEncoder, WGPUBuffer indirectBuffer, ulong indirectOffset) {
        wgpuRenderPassEncoderDrawIndexedIndirect(renderPassEncoder, indirectBuffer, indirectOffset);
    }

    public static void drawIndirect(this WGPURenderPassEncoder renderPassEncoder, WGPUBuffer indirectBuffer, ulong indirectOffset) {
        wgpuRenderPassEncoderDrawIndirect(renderPassEncoder, indirectBuffer, indirectOffset);
    }

    public static void end(this WGPURenderPassEncoder renderPassEncoder) {
        wgpuRenderPassEncoderEnd(renderPassEncoder);
    }

    public static void endOcclusionQuery(this WGPURenderPassEncoder renderPassEncoder) {
        wgpuRenderPassEncoderEndOcclusionQuery(renderPassEncoder);
    }

    public static void executeBundles(this WGPURenderPassEncoder renderPassEncoder, ulong bundleCount, WGPURenderBundle bundles) {
        wgpuRenderPassEncoderExecuteBundles(renderPassEncoder, bundleCount, &bundles);
    }

    public static void insertDebugMarker(this WGPURenderPassEncoder renderPassEncoder, ReadOnlySpan<char> markerLabel) {
        wgpuRenderPassEncoderInsertDebugMarker(renderPassEncoder, markerLabel.AllocString());
    }

    public static void popDebugGroup(this WGPURenderPassEncoder renderPassEncoder) {
        wgpuRenderPassEncoderPopDebugGroup(renderPassEncoder);
    }

    public static void pushDebugGroup(this WGPURenderPassEncoder renderPassEncoder, ReadOnlySpan<char> groupLabel) {
        wgpuRenderPassEncoderPushDebugGroup(renderPassEncoder, groupLabel.AllocString());
    }

    public static void setBindGroup(this WGPURenderPassEncoder renderPassEncoder, uint groupIndex, WGPUBindGroup group, ulong dynamicOffsetCount, uint* dynamicOffsets) {
        wgpuRenderPassEncoderSetBindGroup(renderPassEncoder, groupIndex, group, dynamicOffsetCount, dynamicOffsets);
    }

    public static void setBlendConstant(this WGPURenderPassEncoder renderPassEncoder, WGPUColor color) {
        wgpuRenderPassEncoderSetBlendConstant(renderPassEncoder, &color);
    }

    public static void setIndexBuffer(this WGPURenderPassEncoder renderPassEncoder, WGPUBuffer buffer, WGPUIndexFormat format, ulong offset, ulong size) {
        wgpuRenderPassEncoderSetIndexBuffer(renderPassEncoder, buffer, format, offset, size);
    }

    public static void setLabel(this WGPURenderPassEncoder renderPassEncoder, ReadOnlySpan<char> label) {
        wgpuRenderPassEncoderSetLabel(renderPassEncoder, label.AllocString());
    }

    public static void setPipeline(this WGPURenderPassEncoder renderPassEncoder, WGPURenderPipeline pipeline) {
        wgpuRenderPassEncoderSetPipeline(renderPassEncoder, pipeline);
    }

    public static void setScissorRect(this WGPURenderPassEncoder renderPassEncoder, uint x, uint y, uint width, uint height) {
        wgpuRenderPassEncoderSetScissorRect(renderPassEncoder, x, y, width, height);
    }

    public static void setStencilReference(this WGPURenderPassEncoder renderPassEncoder, uint reference) {
        wgpuRenderPassEncoderSetStencilReference(renderPassEncoder, reference);
    }

    public static void setVertexBuffer(this WGPURenderPassEncoder renderPassEncoder, uint slot, WGPUBuffer buffer, ulong offset, ulong size) {
        wgpuRenderPassEncoderSetVertexBuffer(renderPassEncoder, slot, buffer, offset, size);
    }

    public static void setViewport(this WGPURenderPassEncoder renderPassEncoder, float x, float y, float width, float height, float minDepth, float maxDepth) {
        wgpuRenderPassEncoderSetViewport(renderPassEncoder, x, y, width, height, minDepth, maxDepth);
    }

    public static void reference(this WGPURenderPassEncoder renderPassEncoder) {
        wgpuRenderPassEncoderReference(renderPassEncoder);
        ObjectTracker.IncRef(renderPassEncoder.Handle);
    }

    public static void release(this WGPURenderPassEncoder renderPassEncoder) {
        ObjectTracker.DecRef(renderPassEncoder.Handle);
        wgpuRenderPassEncoderRelease(renderPassEncoder);
    }

    public static void setPushConstants(this WGPURenderPassEncoder encoder, WGPUShaderStage stages, uint offset, uint sizeBytes, void* data) {
        wgpuRenderPassEncoderSetPushConstants(encoder, stages, offset, sizeBytes, data);
    }

    public static void multiDrawIndirect(this WGPURenderPassEncoder encoder, WGPUBuffer buffer, ulong offset, uint count) {
        wgpuRenderPassEncoderMultiDrawIndirect(encoder, buffer, offset, count);
    }

    public static void multiDrawIndexedIndirect(this WGPURenderPassEncoder encoder, WGPUBuffer buffer, ulong offset, uint count) {
        wgpuRenderPassEncoderMultiDrawIndexedIndirect(encoder, buffer, offset, count);
    }

    public static void multiDrawIndirectCount(this WGPURenderPassEncoder encoder, WGPUBuffer buffer, ulong offset, WGPUBuffer count_buffer, ulong count_buffer_offset, uint max_count) {
        wgpuRenderPassEncoderMultiDrawIndirectCount(encoder, buffer, offset, count_buffer, count_buffer_offset, max_count);
    }

    public static void multiDrawIndexedIndirectCount(this WGPURenderPassEncoder encoder, WGPUBuffer buffer, ulong offset, WGPUBuffer count_buffer, ulong count_buffer_offset, uint max_count) {
        wgpuRenderPassEncoderMultiDrawIndexedIndirectCount(encoder, buffer, offset, count_buffer, count_buffer_offset, max_count);
    }

    public static void beginPipelineStatisticsQuery(this WGPURenderPassEncoder renderPassEncoder, WGPUQuerySet querySet, uint queryIndex) {
        wgpuRenderPassEncoderBeginPipelineStatisticsQuery(renderPassEncoder, querySet, queryIndex);
    }

    public static void endPipelineStatisticsQuery(this WGPURenderPassEncoder renderPassEncoder) {
        wgpuRenderPassEncoderEndPipelineStatisticsQuery(renderPassEncoder);
    }

}
