using System.Diagnostics;
using static Evergine.Bindings.WebGPU.WebGPUNative;

// ReSharper disable InconsistentNaming
namespace Evergine.Bindings.WebGPU;

/// <see href="https://developer.mozilla.org/en-US/docs/Web/API/GPUComputePassEncoder">MDN documentation</see>           
public unsafe partial struct WGPUComputePassEncoder
{
    public void dispatchWorkgroups(uint workgroupCountX, uint workgroupCountY, uint workgroupCountZ) {
        Validate_dispatchWorkgroups(Handle, workgroupCountX, workgroupCountY, workgroupCountZ);
        wgpuComputePassEncoderDispatchWorkgroups(this, workgroupCountX, workgroupCountY, workgroupCountZ);
    }

    [Conditional("VALIDATE")]
    private void Validate_dispatchWorkgroups(IntPtr handle, uint workgroupCountX, uint workgroupCountY, uint workgroupCountZ) {
        ObjectTracker.ValidateHandle(this);
    }

    public void dispatchWorkgroupsIndirect(WGPUBuffer indirectBuffer, ulong indirectOffset) {
        Validate_dispatchWorkgroupsIndirect(Handle, indirectBuffer, indirectOffset);
        wgpuComputePassEncoderDispatchWorkgroupsIndirect(this, indirectBuffer, indirectOffset);
    }

    [Conditional("VALIDATE")]
    private void Validate_dispatchWorkgroupsIndirect(IntPtr handle, WGPUBuffer indirectBuffer, ulong indirectOffset) {
        ObjectTracker.ValidateHandle(this);
    }

    public void end() {
        Validate_end(Handle);
        wgpuComputePassEncoderEnd(this);
    }

    [Conditional("VALIDATE")]
    private void Validate_end(IntPtr handle) {
        ObjectTracker.ValidateHandle(this);
    }

    public void insertDebugMarker(Utf8 markerLabel) {
        Validate_insertDebugMarker(Handle, markerLabel);
        wgpuComputePassEncoderInsertDebugMarker(this, markerLabel.AllocUtf8());
    }

    [Conditional("VALIDATE")]
    private void Validate_insertDebugMarker(IntPtr handle, Utf8 markerLabel) {
        ObjectTracker.ValidateHandle(this);
    }

    public void popDebugGroup() {
        Validate_popDebugGroup(Handle);
        wgpuComputePassEncoderPopDebugGroup(this);
    }

    [Conditional("VALIDATE")]
    private void Validate_popDebugGroup(IntPtr handle) {
        ObjectTracker.ValidateHandle(this);
    }

    public void pushDebugGroup(Utf8 groupLabel) {
        Validate_pushDebugGroup(Handle, groupLabel);
        wgpuComputePassEncoderPushDebugGroup(this, groupLabel.AllocUtf8());
    }

    [Conditional("VALIDATE")]
    private void Validate_pushDebugGroup(IntPtr handle, Utf8 groupLabel) {
        ObjectTracker.ValidateHandle(this);
    }

    public void setBindGroup(uint groupIndex, WGPUBindGroup group, ReadOnlySpan<uint> dynamicOffsets) {
        fixed (uint* ptr = dynamicOffsets) {
            wgpuComputePassEncoderSetBindGroup(this, groupIndex, group, (ulong)dynamicOffsets.Length, ptr);    
        }
    }


    public void setPipeline(WGPUComputePipeline pipeline) {
        Validate_setPipeline(Handle, pipeline);
        wgpuComputePassEncoderSetPipeline(this, pipeline);
    }

    [Conditional("VALIDATE")]
    private void Validate_setPipeline(IntPtr handle, WGPUComputePipeline pipeline) {
        ObjectTracker.ValidateHandle(this);
    }

    public void reference() {
        ObjectTracker.IncRef(this);
        wgpuComputePassEncoderReference(this);
    }

    public void release() {
        ObjectTracker.DecRef(this);
        wgpuComputePassEncoderRelease(this);
    }

    public void beginPipelineStatisticsQuery(WGPUQuerySet querySet, uint queryIndex) {
        Validate_beginPipelineStatisticsQuery(Handle, querySet, queryIndex);
        wgpuComputePassEncoderBeginPipelineStatisticsQuery(this, querySet, queryIndex);
    }

    [Conditional("VALIDATE")]
    private void Validate_beginPipelineStatisticsQuery(IntPtr handle, WGPUQuerySet querySet, uint queryIndex) {
        ObjectTracker.ValidateHandle(this);
    }

    public void endPipelineStatisticsQuery() {
        Validate_endPipelineStatisticsQuery(Handle);
        wgpuComputePassEncoderEndPipelineStatisticsQuery(this);
    }

    [Conditional("VALIDATE")]
    private void Validate_endPipelineStatisticsQuery(IntPtr handle) {
        ObjectTracker.ValidateHandle(this);
    }

    public override string? ToString() => ObjectTracker.GetLabel(Handle);
}
