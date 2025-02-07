using static Evergine.Bindings.WebGPU.WebGPUNative;

// ReSharper disable InconsistentNaming
namespace Evergine.Bindings.WebGPU;

/// <see href="https://developer.mozilla.org/en-US/docs/Web/API/GPUComputePassEncoder">MDN documentation</see>           
public unsafe partial struct WGPUComputePassEncoder
{
    public void dispatchWorkgroups(uint workgroupCountX, uint workgroupCountY, uint workgroupCountZ) {
        Validate_dispatchWorkgroups(Handle, workgroupCountX, workgroupCountY, workgroupCountZ);
        wgpuComputePassEncoderDispatchWorkgroups(Handle, workgroupCountX, workgroupCountY, workgroupCountZ);
    }

    private static void Validate_dispatchWorkgroups(IntPtr handle, uint workgroupCountX, uint workgroupCountY, uint workgroupCountZ) {
        ObjectTracker.ValidateHandle(handle);
    }

    public void dispatchWorkgroupsIndirect(WGPUBuffer indirectBuffer, ulong indirectOffset) {
        Validate_dispatchWorkgroupsIndirect(Handle, indirectBuffer, indirectOffset);
        wgpuComputePassEncoderDispatchWorkgroupsIndirect(Handle, indirectBuffer, indirectOffset);
    }

    private static void Validate_dispatchWorkgroupsIndirect(IntPtr handle, WGPUBuffer indirectBuffer, ulong indirectOffset) {
        ObjectTracker.ValidateHandle(handle);
    }

    public void end() {
        Validate_end(Handle);
        wgpuComputePassEncoderEnd(Handle);
    }

    private static void Validate_end(IntPtr handle) {
        ObjectTracker.ValidateHandle(handle);
    }

    public void insertDebugMarker(Utf8 markerLabel) {
        Validate_insertDebugMarker(Handle, markerLabel);
        wgpuComputePassEncoderInsertDebugMarker(Handle, markerLabel.AllocUtf8());
    }

    private static void Validate_insertDebugMarker(IntPtr handle, Utf8 markerLabel) {
        ObjectTracker.ValidateHandle(handle);
    }

    public void popDebugGroup() {
        Validate_popDebugGroup(Handle);
        wgpuComputePassEncoderPopDebugGroup(Handle);
    }

    private static void Validate_popDebugGroup(IntPtr handle) {
        ObjectTracker.ValidateHandle(handle);
    }

    public void pushDebugGroup(Utf8 groupLabel) {
        Validate_pushDebugGroup(Handle, groupLabel);
        wgpuComputePassEncoderPushDebugGroup(Handle, groupLabel.AllocUtf8());
    }

    private static void Validate_pushDebugGroup(IntPtr handle, Utf8 groupLabel) {
        ObjectTracker.ValidateHandle(handle);
    }

    public void setBindGroup(uint groupIndex, WGPUBindGroup group, ReadOnlySpan<uint> dynamicOffsets) {
        fixed (uint* ptr = dynamicOffsets) {
            wgpuComputePassEncoderSetBindGroup(Handle, groupIndex, group, (ulong)dynamicOffsets.Length, ptr);    
        }
    }

    public void setLabel(Utf8 label) {
        Validate_setLabel(Handle, label);
        wgpuComputePassEncoderSetLabel(Handle, label.AllocUtf8());
    }

    private static void Validate_setLabel(IntPtr handle, Utf8 label) {
        ObjectTracker.ValidateHandle(handle);
    }

    public void setPipeline(WGPUComputePipeline pipeline) {
        Validate_setPipeline(Handle, pipeline);
        wgpuComputePassEncoderSetPipeline(Handle, pipeline);
    }

    private static void Validate_setPipeline(IntPtr handle, WGPUComputePipeline pipeline) {
        ObjectTracker.ValidateHandle(handle);
    }

    public void reference() {
        ObjectTracker.IncRef(Handle);
        wgpuComputePassEncoderReference(Handle);
    }

    public void release() {
        ObjectTracker.DecRef(Handle);
        wgpuComputePassEncoderRelease(Handle);
    }

    public void beginPipelineStatisticsQuery(WGPUQuerySet querySet, uint queryIndex) {
        Validate_beginPipelineStatisticsQuery(Handle, querySet, queryIndex);
        wgpuComputePassEncoderBeginPipelineStatisticsQuery(Handle, querySet, queryIndex);
    }

    private static void Validate_beginPipelineStatisticsQuery(IntPtr handle, WGPUQuerySet querySet, uint queryIndex) {
        ObjectTracker.ValidateHandle(handle);
    }

    public void endPipelineStatisticsQuery() {
        Validate_endPipelineStatisticsQuery(Handle);
        wgpuComputePassEncoderEndPipelineStatisticsQuery(Handle);
    }

    private static void Validate_endPipelineStatisticsQuery(IntPtr handle) {
        ObjectTracker.ValidateHandle(handle);
    }

    public override string? ToString() => ObjectTracker.GetLabel(Handle);
}
