namespace Evergine.Bindings.WebGPU;
using static WebGPUNative;

/// <see href="https://developer.mozilla.org/en-US/docs/Web/API/GPUComputePassEncoder">MDN documentation</see>           
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

    public void insertDebugMarker(Utf8 markerLabel) {
        wgpuComputePassEncoderInsertDebugMarker(Handle, markerLabel.AllocUtf8());
    }

    public void popDebugGroup() {
        wgpuComputePassEncoderPopDebugGroup(Handle);
    }

    public void pushDebugGroup(Utf8 groupLabel) {
        wgpuComputePassEncoderPushDebugGroup(Handle, groupLabel.AllocUtf8());
    }

    public void setBindGroup(uint groupIndex, WGPUBindGroup group, ReadOnlySpan<uint> dynamicOffsets) {
        fixed (uint* ptr = dynamicOffsets) {
            wgpuComputePassEncoderSetBindGroup(Handle, groupIndex, group, (ulong)dynamicOffsets.Length, ptr);    
        }
    }

    public void setLabel(Utf8 label) {
        wgpuComputePassEncoderSetLabel(Handle, label.AllocUtf8());
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

    public override string? ToString() => ObjectTracker.GetLabel(Handle);
}
