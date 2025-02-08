using System.Diagnostics;
using static Evergine.Bindings.WebGPU.WebGPUNative;

// ReSharper disable InconsistentNaming
namespace Evergine.Bindings.WebGPU;

/// <see href="https://developer.mozilla.org/en-US/docs/Web/API/GPUCommandEncoder">MDN documentation</see>           
public unsafe partial struct WGPUCommandEncoder
{
    public WGPUComputePassEncoder beginComputePass(WGPUComputePassDescriptor descriptor) {
        Validate_beginComputePass(Handle, descriptor);
        var result = wgpuCommandEncoderBeginComputePass(Handle, &descriptor);
        ObjectTracker.CreateRef(result.Handle, HandleType.WGPUComputePassEncoder, descriptor._label);
        return result;
    }

    [Conditional("VALIDATE")]
    private static void Validate_beginComputePass(IntPtr handle, WGPUComputePassDescriptor descriptor) {
        ObjectTracker.ValidateHandle(handle);
        descriptor.Validate();
    }

    public WGPURenderPassEncoder beginRenderPass(WGPURenderPassDescriptor descriptor) {
        Validate_beginRenderPass(Handle, descriptor);
        var result = wgpuCommandEncoderBeginRenderPass(Handle, &descriptor);
        ObjectTracker.CreateRef(result.Handle, HandleType.WGPURenderPassEncoder, descriptor._label);
        return result;
    }

    [Conditional("VALIDATE")]
    private static void Validate_beginRenderPass(IntPtr handle, WGPURenderPassDescriptor descriptor) {
        ObjectTracker.ValidateHandle(handle);
        descriptor.Validate();
    }

    public void clearBuffer(WGPUBuffer buffer, ulong offset, ulong size) {
        Validate_clearBuffer(Handle, buffer, offset, size);
        wgpuCommandEncoderClearBuffer(Handle, buffer, offset, size);
    }

    [Conditional("VALIDATE")]
    private static void Validate_clearBuffer(IntPtr handle, WGPUBuffer buffer, ulong offset, ulong size) {
        ObjectTracker.ValidateHandle(handle);
    }

    public void copyBufferToBuffer(WGPUBuffer source, ulong sourceOffset, WGPUBuffer destination, ulong destinationOffset, ulong size) {
        Validate_copyBufferToBuffer(Handle, source, sourceOffset, destination, destinationOffset, size);
        wgpuCommandEncoderCopyBufferToBuffer(Handle, source, sourceOffset, destination, destinationOffset, size);
    }

    [Conditional("VALIDATE")]
    private static void Validate_copyBufferToBuffer(IntPtr handle, WGPUBuffer source, ulong sourceOffset, WGPUBuffer destination, ulong destinationOffset, ulong size) {
        ObjectTracker.ValidateHandle(handle);
    }

    public void copyBufferToTexture(WGPUImageCopyBuffer source, WGPUImageCopyTexture destination, WGPUExtent3D copySize) {
        Validate_copyBufferToTexture(Handle, source, destination, copySize);
        wgpuCommandEncoderCopyBufferToTexture(Handle, &source, &destination, &copySize);
    }

    [Conditional("VALIDATE")]
    private static void Validate_copyBufferToTexture(IntPtr handle, WGPUImageCopyBuffer source, WGPUImageCopyTexture destination, WGPUExtent3D copySize) {
        ObjectTracker.ValidateHandle(handle);
    }

    public void copyTextureToBuffer(WGPUImageCopyTexture source, WGPUImageCopyBuffer destination, WGPUExtent3D copySize) {
        Validate_copyTextureToBuffer(Handle, source, destination, copySize);
        wgpuCommandEncoderCopyTextureToBuffer(Handle, &source, &destination, &copySize);
    }

    [Conditional("VALIDATE")]
    private static void Validate_copyTextureToBuffer(IntPtr handle, WGPUImageCopyTexture source, WGPUImageCopyBuffer destination, WGPUExtent3D copySize) {
        ObjectTracker.ValidateHandle(handle);
    }

    public void copyTextureToTexture(WGPUImageCopyTexture source, WGPUImageCopyTexture destination, WGPUExtent3D copySize) {
        Validate_copyTextureToTexture(Handle, source, destination, copySize);
        wgpuCommandEncoderCopyTextureToTexture(Handle, &source, &destination, &copySize);
    }

    [Conditional("VALIDATE")]
    private static void Validate_copyTextureToTexture(IntPtr handle, WGPUImageCopyTexture source, WGPUImageCopyTexture destination, WGPUExtent3D copySize) {
        ObjectTracker.ValidateHandle(handle);
    }

    public WGPUCommandBuffer finish(WGPUCommandBufferDescriptor descriptor) {
        Validate_finish(Handle, descriptor);
        var result = wgpuCommandEncoderFinish(Handle, &descriptor);
        ObjectTracker.CreateRef(result.Handle, HandleType.WGPUCommandBuffer, descriptor._label);
        return result;
    }

    [Conditional("VALIDATE")]
    private static void Validate_finish(IntPtr handle, WGPUCommandBufferDescriptor descriptor) {
        ObjectTracker.ValidateHandle(handle);
        descriptor.Validate();
    }

    public void insertDebugMarker(Utf8 markerLabel) {
        Validate_insertDebugMarker(Handle, markerLabel);
        wgpuCommandEncoderInsertDebugMarker(Handle, markerLabel.AllocUtf8());
    }

    [Conditional("VALIDATE")]
    private static void Validate_insertDebugMarker(IntPtr handle, Utf8 markerLabel) {
        ObjectTracker.ValidateHandle(handle);
    }

    public void popDebugGroup() {
        Validate_popDebugGroup(Handle);
        wgpuCommandEncoderPopDebugGroup(Handle);
    }

    [Conditional("VALIDATE")]
    private static void Validate_popDebugGroup(IntPtr handle) {
        ObjectTracker.ValidateHandle(handle);
    }

    public void pushDebugGroup(Utf8 groupLabel) {
        Validate_pushDebugGroup(Handle, groupLabel);
        wgpuCommandEncoderPushDebugGroup(Handle, groupLabel.AllocUtf8());
    }

    [Conditional("VALIDATE")]
    private static void Validate_pushDebugGroup(IntPtr handle, Utf8 groupLabel) {
        ObjectTracker.ValidateHandle(handle);
    }

    public void resolveQuerySet(WGPUQuerySet querySet, uint firstQuery, uint queryCount, WGPUBuffer destination, ulong destinationOffset) {
        Validate_resolveQuerySet(Handle, querySet, firstQuery, queryCount, destination, destinationOffset);
        wgpuCommandEncoderResolveQuerySet(Handle, querySet, firstQuery, queryCount, destination, destinationOffset);
    }

    [Conditional("VALIDATE")]
    private static void Validate_resolveQuerySet(IntPtr handle, WGPUQuerySet querySet, uint firstQuery, uint queryCount, WGPUBuffer destination, ulong destinationOffset) {
        ObjectTracker.ValidateHandle(handle);
    }

    public void setLabel(Utf8 label) {
        Validate_setLabel(Handle, label);
        wgpuCommandEncoderSetLabel(Handle, label.AllocUtf8());
    }

    [Conditional("VALIDATE")]
    private static void Validate_setLabel(IntPtr handle, Utf8 label) {
        ObjectTracker.ValidateHandle(handle);
    }

    public void writeTimestamp(WGPUQuerySet querySet, uint queryIndex) {
        Validate_writeTimestamp(Handle, querySet, queryIndex);
        wgpuCommandEncoderWriteTimestamp(Handle, querySet, queryIndex);
    }

    [Conditional("VALIDATE")]
    private static void Validate_writeTimestamp(IntPtr handle, WGPUQuerySet querySet, uint queryIndex) {
        ObjectTracker.ValidateHandle(handle);
    }

    public void reference() {
        ObjectTracker.IncRef(Handle);
        wgpuCommandEncoderReference(Handle);
    }

    public void release() {
        ObjectTracker.DecRef(Handle);
        wgpuCommandEncoderRelease(Handle);
    }

    public override string? ToString() => ObjectTracker.GetLabel(Handle);
}
