using System.Diagnostics;
using static Evergine.Bindings.WebGPU.WebGPUNative;

// ReSharper disable InconsistentNaming
namespace Evergine.Bindings.WebGPU;

/// <see href="https://developer.mozilla.org/en-US/docs/Web/API/GPUComputePassEncoder">MDN documentation</see>           
public unsafe partial struct WGPUComputePassEncoder
{
    public void dispatchWorkgroups(uint workgroupCountX, uint workgroupCountY, uint workgroupCountZ) {
        Validate_dispatchWorkgroups(workgroupCountX, workgroupCountY, workgroupCountZ);
        wgpuComputePassEncoderDispatchWorkgroups(this, workgroupCountX, workgroupCountY, workgroupCountZ);
        WGPUException.ThrowOnError();
    }

    [Conditional("VALIDATE")]
    private void Validate_dispatchWorkgroups(uint workgroupCountX, uint workgroupCountY, uint workgroupCountZ) {
        ObjectTracker.ValidateHandle(this);
    }

    public void dispatchWorkgroupsIndirect(WGPUBuffer indirectBuffer, ulong indirectOffset) {
        Validate_dispatchWorkgroupsIndirect(indirectBuffer, indirectOffset);
        wgpuComputePassEncoderDispatchWorkgroupsIndirect(this, indirectBuffer, indirectOffset);
        WGPUException.ThrowOnError();
    }

    [Conditional("VALIDATE")]
    private void Validate_dispatchWorkgroupsIndirect(WGPUBuffer indirectBuffer, ulong indirectOffset) {
        ObjectTracker.ValidateHandle(this);
        ObjectTracker.ValidateHandleParam(indirectBuffer);
    }

    public void end() {
        Validate_end();
        wgpuComputePassEncoderEnd(this);
        WGPUException.ThrowOnError();
    }

    [Conditional("VALIDATE")]
    private void Validate_end() {
        ObjectTracker.ValidateHandle(this);
    }

    public void insertDebugMarker(Utf8 markerLabel) {
        Validate_insertDebugMarker(markerLabel);
        wgpuComputePassEncoderInsertDebugMarker(this, markerLabel.AllocUtf8());
        WGPUException.ThrowOnError();
    }

    [Conditional("VALIDATE")]
    private void Validate_insertDebugMarker(Utf8 markerLabel) {
        ObjectTracker.ValidateHandle(this);
    }

    public void popDebugGroup() {
        Validate_popDebugGroup();
        wgpuComputePassEncoderPopDebugGroup(this);
        WGPUException.ThrowOnError();
    }

    [Conditional("VALIDATE")]
    private void Validate_popDebugGroup() {
        ObjectTracker.ValidateHandle(this);
    }

    public void pushDebugGroup(Utf8 groupLabel) {
        Validate_pushDebugGroup(groupLabel);
        wgpuComputePassEncoderPushDebugGroup(this, groupLabel.AllocUtf8());
        WGPUException.ThrowOnError();
    }

    [Conditional("VALIDATE")]
    private void Validate_pushDebugGroup(Utf8 groupLabel) {
        ObjectTracker.ValidateHandle(this);
    }

    public void setBindGroup(uint groupIndex, WGPUBindGroup group, ReadOnlySpan<uint> dynamicOffsets) {
        fixed (uint* ptr = dynamicOffsets) {
            wgpuComputePassEncoderSetBindGroup(this, groupIndex, group, (ulong)dynamicOffsets.Length, ptr);
        }
        WGPUException.ThrowOnError();
    }


    public void setPipeline(WGPUComputePipeline pipeline) {
        Validate_setPipeline(pipeline);
        wgpuComputePassEncoderSetPipeline(this, pipeline);
        WGPUException.ThrowOnError();
    }

    [Conditional("VALIDATE")]
    private void Validate_setPipeline(WGPUComputePipeline pipeline) {
        ObjectTracker.ValidateHandle(this);
        ObjectTracker.ValidateHandleParam(pipeline);
    }

    public void reference() {
        ObjectTracker.IncRef(this);
        wgpuComputePassEncoderReference(this);
    }

    public void release() {
        ObjectTracker.DecRef(this);
        wgpuComputePassEncoderRelease(this);
    }
    
    public void Dispose() {
        ObjectTracker.DecRef(this);
        wgpuComputePassEncoderRelease(this);
    }

    public void beginPipelineStatisticsQuery(WGPUQuerySet querySet, uint queryIndex) {
        Validate_beginPipelineStatisticsQuery(querySet, queryIndex);
        wgpuComputePassEncoderBeginPipelineStatisticsQuery(this, querySet, queryIndex);
        WGPUException.ThrowOnError();
    }

    [Conditional("VALIDATE")]
    private void Validate_beginPipelineStatisticsQuery(WGPUQuerySet querySet, uint queryIndex) {
        ObjectTracker.ValidateHandle(this);
        ObjectTracker.ValidateHandleParam(querySet);
    }

    public void endPipelineStatisticsQuery() {
        Validate_endPipelineStatisticsQuery();
        wgpuComputePassEncoderEndPipelineStatisticsQuery(this);
        WGPUException.ThrowOnError();
    }

    [Conditional("VALIDATE")]
    private void Validate_endPipelineStatisticsQuery() {
        ObjectTracker.ValidateHandle(this);
    }

    public override string? ToString() => ObjectTracker.GetLabel(Handle);
}
