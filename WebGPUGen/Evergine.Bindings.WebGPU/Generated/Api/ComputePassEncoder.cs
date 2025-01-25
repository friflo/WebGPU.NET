namespace Evergine.Bindings.WebGPU;
using static WebGPUNative;
           
public unsafe partial struct WGPUComputePassEncoder
{
    public void dispatchWorkgroups(uint workgroupCountX, uint workgroupCountY, uint workgroupCountZ) {
        wgpuComputePassEncoderDispatchWorkgroups(Handle, workgroupCountX, workgroupCountY, workgroupCountZ);
    }

    public void dispatchWorkgroupsIndirect(WGPUBuffer indirectBuffer, ulong indirectOffset) {
        wgpuComputePassEncoderDispatchWorkgroupsIndirect(Handle, indirectBuffer, indirectOffset);
    }

    public void end() {
        wgpuComputePassEncoderEnd(Handle);
    }

    public void insertDebugMarker(ReadOnlySpan<char> markerLabel) {
        wgpuComputePassEncoderInsertDebugMarker(Handle, markerLabel.AllocString());
    }

    public void popDebugGroup() {
        wgpuComputePassEncoderPopDebugGroup(Handle);
    }

    public void pushDebugGroup(ReadOnlySpan<char> groupLabel) {
        wgpuComputePassEncoderPushDebugGroup(Handle, groupLabel.AllocString());
    }

    public void setBindGroup(uint groupIndex, WGPUBindGroup group, ulong dynamicOffsetCount, uint* dynamicOffsets) {
        wgpuComputePassEncoderSetBindGroup(Handle, groupIndex, group, dynamicOffsetCount, dynamicOffsets);
    }

    public void setLabel(ReadOnlySpan<char> label) {
        wgpuComputePassEncoderSetLabel(Handle, label.AllocString());
    }

    public void setPipeline(WGPUComputePipeline pipeline) {
        wgpuComputePassEncoderSetPipeline(Handle, pipeline);
    }

    public void reference() {
        wgpuComputePassEncoderReference(Handle);
        ObjectTracker.IncRef(Handle);
    }

    public void release() {
        ObjectTracker.DecRef(Handle);
        wgpuComputePassEncoderRelease(Handle);
    }

    public void beginPipelineStatisticsQuery(WGPUQuerySet querySet, uint queryIndex) {
        wgpuComputePassEncoderBeginPipelineStatisticsQuery(Handle, querySet, queryIndex);
    }

    public void endPipelineStatisticsQuery() {
        wgpuComputePassEncoderEndPipelineStatisticsQuery(Handle);
    }

}
