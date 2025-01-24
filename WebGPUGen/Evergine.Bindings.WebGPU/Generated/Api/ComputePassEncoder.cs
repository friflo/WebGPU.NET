namespace Evergine.Bindings.WebGPU;
using static WebGPUNative;
           
public static unsafe partial class WebGPUExtensions
{
    public static void dispatchWorkgroups(this WGPUComputePassEncoder computePassEncoder, uint workgroupCountX, uint workgroupCountY, uint workgroupCountZ) {
        wgpuComputePassEncoderDispatchWorkgroups(computePassEncoder, workgroupCountX, workgroupCountY, workgroupCountZ);
    }

    public static void dispatchWorkgroupsIndirect(this WGPUComputePassEncoder computePassEncoder, WGPUBuffer indirectBuffer, ulong indirectOffset) {
        wgpuComputePassEncoderDispatchWorkgroupsIndirect(computePassEncoder, indirectBuffer, indirectOffset);
    }

    public static void end(this WGPUComputePassEncoder computePassEncoder) {
        wgpuComputePassEncoderEnd(computePassEncoder);
    }

    public static void insertDebugMarker(this WGPUComputePassEncoder computePassEncoder, ReadOnlySpan<char> markerLabel) {
        wgpuComputePassEncoderInsertDebugMarker(computePassEncoder, markerLabel.AllocString());
    }

    public static void popDebugGroup(this WGPUComputePassEncoder computePassEncoder) {
        wgpuComputePassEncoderPopDebugGroup(computePassEncoder);
    }

    public static void pushDebugGroup(this WGPUComputePassEncoder computePassEncoder, ReadOnlySpan<char> groupLabel) {
        wgpuComputePassEncoderPushDebugGroup(computePassEncoder, groupLabel.AllocString());
    }

    public static void setBindGroup(this WGPUComputePassEncoder computePassEncoder, uint groupIndex, WGPUBindGroup group, ulong dynamicOffsetCount, uint* dynamicOffsets) {
        wgpuComputePassEncoderSetBindGroup(computePassEncoder, groupIndex, group, dynamicOffsetCount, dynamicOffsets);
    }

    public static void setLabel(this WGPUComputePassEncoder computePassEncoder, ReadOnlySpan<char> label) {
        wgpuComputePassEncoderSetLabel(computePassEncoder, label.AllocString());
    }

    public static void setPipeline(this WGPUComputePassEncoder computePassEncoder, WGPUComputePipeline pipeline) {
        wgpuComputePassEncoderSetPipeline(computePassEncoder, pipeline);
    }

    public static void reference(this WGPUComputePassEncoder computePassEncoder) {
        wgpuComputePassEncoderReference(computePassEncoder);
        ObjectTracker.IncRef(computePassEncoder.Handle);
    }

    public static void release(this WGPUComputePassEncoder computePassEncoder) {
        ObjectTracker.DecRef(computePassEncoder.Handle);
        wgpuComputePassEncoderRelease(computePassEncoder);
    }

    public static void beginPipelineStatisticsQuery(this WGPUComputePassEncoder computePassEncoder, WGPUQuerySet querySet, uint queryIndex) {
        wgpuComputePassEncoderBeginPipelineStatisticsQuery(computePassEncoder, querySet, queryIndex);
    }

    public static void endPipelineStatisticsQuery(this WGPUComputePassEncoder computePassEncoder) {
        wgpuComputePassEncoderEndPipelineStatisticsQuery(computePassEncoder);
    }

}
