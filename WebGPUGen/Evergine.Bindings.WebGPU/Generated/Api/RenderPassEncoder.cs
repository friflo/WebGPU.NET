using System.Diagnostics;
using static Evergine.Bindings.WebGPU.WebGPUNative;

// ReSharper disable InconsistentNaming
namespace Evergine.Bindings.WebGPU;

/// <see href="https://developer.mozilla.org/en-US/docs/Web/API/GPURenderPassEncoder">MDN documentation</see>           
public unsafe partial struct WGPURenderPassEncoder
{
    public void beginOcclusionQuery(uint queryIndex) {
        Validate_beginOcclusionQuery(Handle, queryIndex);
        wgpuRenderPassEncoderBeginOcclusionQuery(this, queryIndex);
    }

    [Conditional("VALIDATE")]
    private static void Validate_beginOcclusionQuery(IntPtr handle, uint queryIndex) {
        ObjectTracker.ValidateHandle(handle);
    }

    public void draw(uint vertexCount, uint instanceCount, uint firstVertex, uint firstInstance) {
        Validate_draw(Handle, vertexCount, instanceCount, firstVertex, firstInstance);
        wgpuRenderPassEncoderDraw(this, vertexCount, instanceCount, firstVertex, firstInstance);
    }

    [Conditional("VALIDATE")]
    private static void Validate_draw(IntPtr handle, uint vertexCount, uint instanceCount, uint firstVertex, uint firstInstance) {
        ObjectTracker.ValidateHandle(handle);
    }

    public void drawIndexed(uint indexCount, uint instanceCount, uint firstIndex, int baseVertex, uint firstInstance) {
        Validate_drawIndexed(Handle, indexCount, instanceCount, firstIndex, baseVertex, firstInstance);
        wgpuRenderPassEncoderDrawIndexed(this, indexCount, instanceCount, firstIndex, baseVertex, firstInstance);
    }

    [Conditional("VALIDATE")]
    private static void Validate_drawIndexed(IntPtr handle, uint indexCount, uint instanceCount, uint firstIndex, int baseVertex, uint firstInstance) {
        ObjectTracker.ValidateHandle(handle);
    }

    public void drawIndexedIndirect(WGPUBuffer indirectBuffer, ulong indirectOffset) {
        Validate_drawIndexedIndirect(Handle, indirectBuffer, indirectOffset);
        wgpuRenderPassEncoderDrawIndexedIndirect(this, indirectBuffer, indirectOffset);
    }

    [Conditional("VALIDATE")]
    private static void Validate_drawIndexedIndirect(IntPtr handle, WGPUBuffer indirectBuffer, ulong indirectOffset) {
        ObjectTracker.ValidateHandle(handle);
    }

    public void drawIndirect(WGPUBuffer indirectBuffer, ulong indirectOffset) {
        Validate_drawIndirect(Handle, indirectBuffer, indirectOffset);
        wgpuRenderPassEncoderDrawIndirect(this, indirectBuffer, indirectOffset);
    }

    [Conditional("VALIDATE")]
    private static void Validate_drawIndirect(IntPtr handle, WGPUBuffer indirectBuffer, ulong indirectOffset) {
        ObjectTracker.ValidateHandle(handle);
    }

    public void end() {
        Validate_end(Handle);
        wgpuRenderPassEncoderEnd(this);
    }

    [Conditional("VALIDATE")]
    private static void Validate_end(IntPtr handle) {
        ObjectTracker.ValidateHandle(handle);
    }

    public void endOcclusionQuery() {
        Validate_endOcclusionQuery(Handle);
        wgpuRenderPassEncoderEndOcclusionQuery(this);
    }

    [Conditional("VALIDATE")]
    private static void Validate_endOcclusionQuery(IntPtr handle) {
        ObjectTracker.ValidateHandle(handle);
    }

    public void executeBundles(ulong bundleCount, WGPURenderBundle bundles) {
        Validate_executeBundles(Handle, bundleCount, bundles);
        wgpuRenderPassEncoderExecuteBundles(this, bundleCount, &bundles);
    }

    [Conditional("VALIDATE")]
    private static void Validate_executeBundles(IntPtr handle, ulong bundleCount, WGPURenderBundle bundles) {
        ObjectTracker.ValidateHandle(handle);
    }

    public void insertDebugMarker(Utf8 markerLabel) {
        Validate_insertDebugMarker(Handle, markerLabel);
        wgpuRenderPassEncoderInsertDebugMarker(this, markerLabel.AllocUtf8());
    }

    [Conditional("VALIDATE")]
    private static void Validate_insertDebugMarker(IntPtr handle, Utf8 markerLabel) {
        ObjectTracker.ValidateHandle(handle);
    }

    public void popDebugGroup() {
        Validate_popDebugGroup(Handle);
        wgpuRenderPassEncoderPopDebugGroup(this);
    }

    [Conditional("VALIDATE")]
    private static void Validate_popDebugGroup(IntPtr handle) {
        ObjectTracker.ValidateHandle(handle);
    }

    public void pushDebugGroup(Utf8 groupLabel) {
        Validate_pushDebugGroup(Handle, groupLabel);
        wgpuRenderPassEncoderPushDebugGroup(this, groupLabel.AllocUtf8());
    }

    [Conditional("VALIDATE")]
    private static void Validate_pushDebugGroup(IntPtr handle, Utf8 groupLabel) {
        ObjectTracker.ValidateHandle(handle);
    }

    public void setBindGroup(uint groupIndex, WGPUBindGroup group, ReadOnlySpan<uint> dynamicOffsets) {
        fixed (uint* ptr = dynamicOffsets) {
            wgpuRenderPassEncoderSetBindGroup(this, groupIndex, group, (ulong)dynamicOffsets.Length, ptr);    
        }
    }

    public void setBlendConstant(WGPUColor color) {
        Validate_setBlendConstant(Handle, color);
        wgpuRenderPassEncoderSetBlendConstant(this, &color);
    }

    [Conditional("VALIDATE")]
    private static void Validate_setBlendConstant(IntPtr handle, WGPUColor color) {
        ObjectTracker.ValidateHandle(handle);
    }

    public void setIndexBuffer(WGPUBuffer buffer, WGPUIndexFormat format, ulong offset, ulong size) {
        Validate_setIndexBuffer(Handle, buffer, format, offset, size);
        wgpuRenderPassEncoderSetIndexBuffer(this, buffer, format, offset, size);
    }

    [Conditional("VALIDATE")]
    private static void Validate_setIndexBuffer(IntPtr handle, WGPUBuffer buffer, WGPUIndexFormat format, ulong offset, ulong size) {
        ObjectTracker.ValidateHandle(handle);
    }


    public void setPipeline(WGPURenderPipeline pipeline) {
        Validate_setPipeline(Handle, pipeline);
        wgpuRenderPassEncoderSetPipeline(this, pipeline);
    }

    [Conditional("VALIDATE")]
    private static void Validate_setPipeline(IntPtr handle, WGPURenderPipeline pipeline) {
        ObjectTracker.ValidateHandle(handle);
    }

    public void setScissorRect(uint x, uint y, uint width, uint height) {
        Validate_setScissorRect(Handle, x, y, width, height);
        wgpuRenderPassEncoderSetScissorRect(this, x, y, width, height);
    }

    [Conditional("VALIDATE")]
    private static void Validate_setScissorRect(IntPtr handle, uint x, uint y, uint width, uint height) {
        ObjectTracker.ValidateHandle(handle);
    }

    public void setStencilReference(uint reference) {
        Validate_setStencilReference(Handle, reference);
        wgpuRenderPassEncoderSetStencilReference(this, reference);
    }

    [Conditional("VALIDATE")]
    private static void Validate_setStencilReference(IntPtr handle, uint reference) {
        ObjectTracker.ValidateHandle(handle);
    }

    public void setVertexBuffer(uint slot, WGPUBuffer buffer, ulong offset, ulong size) {
        Validate_setVertexBuffer(Handle, slot, buffer, offset, size);
        wgpuRenderPassEncoderSetVertexBuffer(this, slot, buffer, offset, size);
    }

    [Conditional("VALIDATE")]
    private static void Validate_setVertexBuffer(IntPtr handle, uint slot, WGPUBuffer buffer, ulong offset, ulong size) {
        ObjectTracker.ValidateHandle(handle);
    }

    public void setViewport(float x, float y, float width, float height, float minDepth, float maxDepth) {
        Validate_setViewport(Handle, x, y, width, height, minDepth, maxDepth);
        wgpuRenderPassEncoderSetViewport(this, x, y, width, height, minDepth, maxDepth);
    }

    [Conditional("VALIDATE")]
    private static void Validate_setViewport(IntPtr handle, float x, float y, float width, float height, float minDepth, float maxDepth) {
        ObjectTracker.ValidateHandle(handle);
    }

    public void reference() {
        ObjectTracker.IncRef(Handle);
        wgpuRenderPassEncoderReference(this);
    }

    public void release() {
        ObjectTracker.DecRef(Handle);
        wgpuRenderPassEncoderRelease(this);
    }

    // setPushConstants() - not generated. See: RenderPassEncoder_NG.cs

    public void multiDrawIndirect(WGPUBuffer buffer, ulong offset, uint count) {
        Validate_multiDrawIndirect(Handle, buffer, offset, count);
        wgpuRenderPassEncoderMultiDrawIndirect(this, buffer, offset, count);
    }

    [Conditional("VALIDATE")]
    private static void Validate_multiDrawIndirect(IntPtr handle, WGPUBuffer buffer, ulong offset, uint count) {
        ObjectTracker.ValidateHandle(handle);
    }

    public void multiDrawIndexedIndirect(WGPUBuffer buffer, ulong offset, uint count) {
        Validate_multiDrawIndexedIndirect(Handle, buffer, offset, count);
        wgpuRenderPassEncoderMultiDrawIndexedIndirect(this, buffer, offset, count);
    }

    [Conditional("VALIDATE")]
    private static void Validate_multiDrawIndexedIndirect(IntPtr handle, WGPUBuffer buffer, ulong offset, uint count) {
        ObjectTracker.ValidateHandle(handle);
    }

    public void multiDrawIndirectCount(WGPUBuffer buffer, ulong offset, WGPUBuffer count_buffer, ulong count_buffer_offset, uint max_count) {
        Validate_multiDrawIndirectCount(Handle, buffer, offset, count_buffer, count_buffer_offset, max_count);
        wgpuRenderPassEncoderMultiDrawIndirectCount(this, buffer, offset, count_buffer, count_buffer_offset, max_count);
    }

    [Conditional("VALIDATE")]
    private static void Validate_multiDrawIndirectCount(IntPtr handle, WGPUBuffer buffer, ulong offset, WGPUBuffer count_buffer, ulong count_buffer_offset, uint max_count) {
        ObjectTracker.ValidateHandle(handle);
    }

    public void multiDrawIndexedIndirectCount(WGPUBuffer buffer, ulong offset, WGPUBuffer count_buffer, ulong count_buffer_offset, uint max_count) {
        Validate_multiDrawIndexedIndirectCount(Handle, buffer, offset, count_buffer, count_buffer_offset, max_count);
        wgpuRenderPassEncoderMultiDrawIndexedIndirectCount(this, buffer, offset, count_buffer, count_buffer_offset, max_count);
    }

    [Conditional("VALIDATE")]
    private static void Validate_multiDrawIndexedIndirectCount(IntPtr handle, WGPUBuffer buffer, ulong offset, WGPUBuffer count_buffer, ulong count_buffer_offset, uint max_count) {
        ObjectTracker.ValidateHandle(handle);
    }

    public void beginPipelineStatisticsQuery(WGPUQuerySet querySet, uint queryIndex) {
        Validate_beginPipelineStatisticsQuery(Handle, querySet, queryIndex);
        wgpuRenderPassEncoderBeginPipelineStatisticsQuery(this, querySet, queryIndex);
    }

    [Conditional("VALIDATE")]
    private static void Validate_beginPipelineStatisticsQuery(IntPtr handle, WGPUQuerySet querySet, uint queryIndex) {
        ObjectTracker.ValidateHandle(handle);
    }

    public void endPipelineStatisticsQuery() {
        Validate_endPipelineStatisticsQuery(Handle);
        wgpuRenderPassEncoderEndPipelineStatisticsQuery(this);
    }

    [Conditional("VALIDATE")]
    private static void Validate_endPipelineStatisticsQuery(IntPtr handle) {
        ObjectTracker.ValidateHandle(handle);
    }

    public override string? ToString() => ObjectTracker.GetLabel(Handle);
}
