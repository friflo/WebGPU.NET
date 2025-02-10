using System.Diagnostics;
using static Evergine.Bindings.WebGPU.WebGPUNative;

// ReSharper disable InconsistentNaming
namespace Evergine.Bindings.WebGPU;

/// <see href="https://developer.mozilla.org/en-US/docs/Web/API/GPUCommandEncoder">MDN documentation</see>           
public unsafe partial struct WGPUCommandEncoder
{
    public WGPUComputePassEncoder beginComputePass(WGPUComputePassDescriptor descriptor) {
        Validate_beginComputePass(descriptor);
        var result = wgpuCommandEncoderBeginComputePass(this, &descriptor);
        ObjectTracker.CreateRef(result, HandleType.WGPUComputePassEncoder, descriptor._label); // ref-other
        return result;
    }

    [Conditional("VALIDATE")]
    private void Validate_beginComputePass(WGPUComputePassDescriptor descriptor) {
        ObjectTracker.ValidateHandle(this);
        descriptor.Validate();
    }

    public WGPURenderPassEncoder beginRenderPass(WGPURenderPassDescriptor descriptor) {
        Validate_beginRenderPass(descriptor);
        var result = wgpuCommandEncoderBeginRenderPass(this, &descriptor);
        ObjectTracker.CreateRef(result, HandleType.WGPURenderPassEncoder, descriptor._label); // ref-other
        return result;
    }

    [Conditional("VALIDATE")]
    private void Validate_beginRenderPass(WGPURenderPassDescriptor descriptor) {
        ObjectTracker.ValidateHandle(this);
        descriptor.Validate();
    }

    public void clearBuffer(WGPUBuffer buffer, ulong offset, ulong size) {
        Validate_clearBuffer(buffer, offset, size);
        wgpuCommandEncoderClearBuffer(this, buffer, offset, size);
    }

    [Conditional("VALIDATE")]
    private void Validate_clearBuffer(WGPUBuffer buffer, ulong offset, ulong size) {
        ObjectTracker.ValidateHandle(this);
        ObjectTracker.ValidateHandleParam(buffer);
    }

    public void copyBufferToBuffer(WGPUBuffer source, ulong sourceOffset, WGPUBuffer destination, ulong destinationOffset, ulong size) {
        Validate_copyBufferToBuffer(source, sourceOffset, destination, destinationOffset, size);
        wgpuCommandEncoderCopyBufferToBuffer(this, source, sourceOffset, destination, destinationOffset, size);
    }

    [Conditional("VALIDATE")]
    private void Validate_copyBufferToBuffer(WGPUBuffer source, ulong sourceOffset, WGPUBuffer destination, ulong destinationOffset, ulong size) {
        ObjectTracker.ValidateHandle(this);
        ObjectTracker.ValidateHandleParam(source);
        ObjectTracker.ValidateHandleParam(destination);
    }

    public void copyBufferToTexture(WGPUImageCopyBuffer source, WGPUImageCopyTexture destination, WGPUExtent3D copySize) {
        Validate_copyBufferToTexture(source, destination, copySize);
        wgpuCommandEncoderCopyBufferToTexture(this, &source, &destination, &copySize);
    }

    [Conditional("VALIDATE")]
    private void Validate_copyBufferToTexture(WGPUImageCopyBuffer source, WGPUImageCopyTexture destination, WGPUExtent3D copySize) {
        ObjectTracker.ValidateHandle(this);
        source.Validate();
        destination.Validate();
    }

    public void copyTextureToBuffer(WGPUImageCopyTexture source, WGPUImageCopyBuffer destination, WGPUExtent3D copySize) {
        Validate_copyTextureToBuffer(source, destination, copySize);
        wgpuCommandEncoderCopyTextureToBuffer(this, &source, &destination, &copySize);
    }

    [Conditional("VALIDATE")]
    private void Validate_copyTextureToBuffer(WGPUImageCopyTexture source, WGPUImageCopyBuffer destination, WGPUExtent3D copySize) {
        ObjectTracker.ValidateHandle(this);
        source.Validate();
        destination.Validate();
    }

    public void copyTextureToTexture(WGPUImageCopyTexture source, WGPUImageCopyTexture destination, WGPUExtent3D copySize) {
        Validate_copyTextureToTexture(source, destination, copySize);
        wgpuCommandEncoderCopyTextureToTexture(this, &source, &destination, &copySize);
    }

    [Conditional("VALIDATE")]
    private void Validate_copyTextureToTexture(WGPUImageCopyTexture source, WGPUImageCopyTexture destination, WGPUExtent3D copySize) {
        ObjectTracker.ValidateHandle(this);
        source.Validate();
        destination.Validate();
    }

    public WGPUCommandBuffer finish(WGPUCommandBufferDescriptor descriptor) {
        Validate_finish(descriptor);
        var result = wgpuCommandEncoderFinish(this, &descriptor);
        ObjectTracker.CreateRef(result, HandleType.WGPUCommandBuffer, descriptor._label); // ref-other
        return result;
    }

    [Conditional("VALIDATE")]
    private void Validate_finish(WGPUCommandBufferDescriptor descriptor) {
        ObjectTracker.ValidateHandle(this);
        descriptor.Validate();
    }

    public void insertDebugMarker(Utf8 markerLabel) {
        Validate_insertDebugMarker(markerLabel);
        wgpuCommandEncoderInsertDebugMarker(this, markerLabel.AllocUtf8());
    }

    [Conditional("VALIDATE")]
    private void Validate_insertDebugMarker(Utf8 markerLabel) {
        ObjectTracker.ValidateHandle(this);
    }

    public void popDebugGroup() {
        Validate_popDebugGroup();
        wgpuCommandEncoderPopDebugGroup(this);
    }

    [Conditional("VALIDATE")]
    private void Validate_popDebugGroup() {
        ObjectTracker.ValidateHandle(this);
    }

    public void pushDebugGroup(Utf8 groupLabel) {
        Validate_pushDebugGroup(groupLabel);
        wgpuCommandEncoderPushDebugGroup(this, groupLabel.AllocUtf8());
    }

    [Conditional("VALIDATE")]
    private void Validate_pushDebugGroup(Utf8 groupLabel) {
        ObjectTracker.ValidateHandle(this);
    }

    public void resolveQuerySet(WGPUQuerySet querySet, uint firstQuery, uint queryCount, WGPUBuffer destination, ulong destinationOffset) {
        Validate_resolveQuerySet(querySet, firstQuery, queryCount, destination, destinationOffset);
        wgpuCommandEncoderResolveQuerySet(this, querySet, firstQuery, queryCount, destination, destinationOffset);
    }

    [Conditional("VALIDATE")]
    private void Validate_resolveQuerySet(WGPUQuerySet querySet, uint firstQuery, uint queryCount, WGPUBuffer destination, ulong destinationOffset) {
        ObjectTracker.ValidateHandle(this);
        ObjectTracker.ValidateHandleParam(querySet);
        ObjectTracker.ValidateHandleParam(destination);
    }


    public void writeTimestamp(WGPUQuerySet querySet, uint queryIndex) {
        Validate_writeTimestamp(querySet, queryIndex);
        wgpuCommandEncoderWriteTimestamp(this, querySet, queryIndex);
    }

    [Conditional("VALIDATE")]
    private void Validate_writeTimestamp(WGPUQuerySet querySet, uint queryIndex) {
        ObjectTracker.ValidateHandle(this);
        ObjectTracker.ValidateHandleParam(querySet);
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
