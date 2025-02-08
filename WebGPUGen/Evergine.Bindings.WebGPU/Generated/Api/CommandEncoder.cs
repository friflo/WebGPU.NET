using System.Diagnostics;
using static Evergine.Bindings.WebGPU.WebGPUNative;

// ReSharper disable InconsistentNaming
namespace Evergine.Bindings.WebGPU;

/// <see href="https://developer.mozilla.org/en-US/docs/Web/API/GPUCommandEncoder">MDN documentation</see>           
public unsafe partial struct WGPUCommandEncoder
{
    public WGPUComputePassEncoder beginComputePass(WGPUComputePassDescriptor descriptor) {
        Validate_beginComputePass(Handle, descriptor);
        var result = wgpuCommandEncoderBeginComputePass(this, &descriptor);
        ObjectTracker.CreateRef(result, HandleType.WGPUComputePassEncoder, descriptor._label);
        return result;
    }

    [Conditional("VALIDATE")]
    private void Validate_beginComputePass(IntPtr handle, WGPUComputePassDescriptor descriptor) {
        ObjectTracker.ValidateHandle(this);
        descriptor.Validate();
    }

    public WGPURenderPassEncoder beginRenderPass(WGPURenderPassDescriptor descriptor) {
        Validate_beginRenderPass(Handle, descriptor);
        var result = wgpuCommandEncoderBeginRenderPass(this, &descriptor);
        ObjectTracker.CreateRef(result, HandleType.WGPURenderPassEncoder, descriptor._label);
        return result;
    }

    [Conditional("VALIDATE")]
    private void Validate_beginRenderPass(IntPtr handle, WGPURenderPassDescriptor descriptor) {
        ObjectTracker.ValidateHandle(this);
        descriptor.Validate();
    }

    public void clearBuffer(WGPUBuffer buffer, ulong offset, ulong size) {
        Validate_clearBuffer(Handle, buffer, offset, size);
        wgpuCommandEncoderClearBuffer(this, buffer, offset, size);
    }

    [Conditional("VALIDATE")]
    private void Validate_clearBuffer(IntPtr handle, WGPUBuffer buffer, ulong offset, ulong size) {
        ObjectTracker.ValidateHandle(this);
    }

    public void copyBufferToBuffer(WGPUBuffer source, ulong sourceOffset, WGPUBuffer destination, ulong destinationOffset, ulong size) {
        Validate_copyBufferToBuffer(Handle, source, sourceOffset, destination, destinationOffset, size);
        wgpuCommandEncoderCopyBufferToBuffer(this, source, sourceOffset, destination, destinationOffset, size);
    }

    [Conditional("VALIDATE")]
    private void Validate_copyBufferToBuffer(IntPtr handle, WGPUBuffer source, ulong sourceOffset, WGPUBuffer destination, ulong destinationOffset, ulong size) {
        ObjectTracker.ValidateHandle(this);
    }

    public void copyBufferToTexture(WGPUImageCopyBuffer source, WGPUImageCopyTexture destination, WGPUExtent3D copySize) {
        Validate_copyBufferToTexture(Handle, source, destination, copySize);
        wgpuCommandEncoderCopyBufferToTexture(this, &source, &destination, &copySize);
    }

    [Conditional("VALIDATE")]
    private void Validate_copyBufferToTexture(IntPtr handle, WGPUImageCopyBuffer source, WGPUImageCopyTexture destination, WGPUExtent3D copySize) {
        ObjectTracker.ValidateHandle(this);
    }

    public void copyTextureToBuffer(WGPUImageCopyTexture source, WGPUImageCopyBuffer destination, WGPUExtent3D copySize) {
        Validate_copyTextureToBuffer(Handle, source, destination, copySize);
        wgpuCommandEncoderCopyTextureToBuffer(this, &source, &destination, &copySize);
    }

    [Conditional("VALIDATE")]
    private void Validate_copyTextureToBuffer(IntPtr handle, WGPUImageCopyTexture source, WGPUImageCopyBuffer destination, WGPUExtent3D copySize) {
        ObjectTracker.ValidateHandle(this);
    }

    public void copyTextureToTexture(WGPUImageCopyTexture source, WGPUImageCopyTexture destination, WGPUExtent3D copySize) {
        Validate_copyTextureToTexture(Handle, source, destination, copySize);
        wgpuCommandEncoderCopyTextureToTexture(this, &source, &destination, &copySize);
    }

    [Conditional("VALIDATE")]
    private void Validate_copyTextureToTexture(IntPtr handle, WGPUImageCopyTexture source, WGPUImageCopyTexture destination, WGPUExtent3D copySize) {
        ObjectTracker.ValidateHandle(this);
    }

    public WGPUCommandBuffer finish(WGPUCommandBufferDescriptor descriptor) {
        Validate_finish(Handle, descriptor);
        var result = wgpuCommandEncoderFinish(this, &descriptor);
        ObjectTracker.CreateRef(result, HandleType.WGPUCommandBuffer, descriptor._label);
        return result;
    }

    [Conditional("VALIDATE")]
    private void Validate_finish(IntPtr handle, WGPUCommandBufferDescriptor descriptor) {
        ObjectTracker.ValidateHandle(this);
        descriptor.Validate();
    }

    public void insertDebugMarker(Utf8 markerLabel) {
        Validate_insertDebugMarker(Handle, markerLabel);
        wgpuCommandEncoderInsertDebugMarker(this, markerLabel.AllocUtf8());
    }

    [Conditional("VALIDATE")]
    private void Validate_insertDebugMarker(IntPtr handle, Utf8 markerLabel) {
        ObjectTracker.ValidateHandle(this);
    }

    public void popDebugGroup() {
        Validate_popDebugGroup(Handle);
        wgpuCommandEncoderPopDebugGroup(this);
    }

    [Conditional("VALIDATE")]
    private void Validate_popDebugGroup(IntPtr handle) {
        ObjectTracker.ValidateHandle(this);
    }

    public void pushDebugGroup(Utf8 groupLabel) {
        Validate_pushDebugGroup(Handle, groupLabel);
        wgpuCommandEncoderPushDebugGroup(this, groupLabel.AllocUtf8());
    }

    [Conditional("VALIDATE")]
    private void Validate_pushDebugGroup(IntPtr handle, Utf8 groupLabel) {
        ObjectTracker.ValidateHandle(this);
    }

    public void resolveQuerySet(WGPUQuerySet querySet, uint firstQuery, uint queryCount, WGPUBuffer destination, ulong destinationOffset) {
        Validate_resolveQuerySet(Handle, querySet, firstQuery, queryCount, destination, destinationOffset);
        wgpuCommandEncoderResolveQuerySet(this, querySet, firstQuery, queryCount, destination, destinationOffset);
    }

    [Conditional("VALIDATE")]
    private void Validate_resolveQuerySet(IntPtr handle, WGPUQuerySet querySet, uint firstQuery, uint queryCount, WGPUBuffer destination, ulong destinationOffset) {
        ObjectTracker.ValidateHandle(this);
    }


    public void writeTimestamp(WGPUQuerySet querySet, uint queryIndex) {
        Validate_writeTimestamp(Handle, querySet, queryIndex);
        wgpuCommandEncoderWriteTimestamp(this, querySet, queryIndex);
    }

    [Conditional("VALIDATE")]
    private void Validate_writeTimestamp(IntPtr handle, WGPUQuerySet querySet, uint queryIndex) {
        ObjectTracker.ValidateHandle(this);
    }

    public void reference() {
        ObjectTracker.IncRef(this);
        wgpuCommandEncoderReference(this);
    }

    public void release() {
        ObjectTracker.DecRef(this);
        wgpuCommandEncoderRelease(this);
    }

    public override string? ToString() => ObjectTracker.GetLabel(Handle);
}
