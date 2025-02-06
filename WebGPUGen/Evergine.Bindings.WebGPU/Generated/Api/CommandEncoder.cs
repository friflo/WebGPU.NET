namespace Evergine.Bindings.WebGPU;
using static WebGPUNative;

/// <see href="https://developer.mozilla.org/en-US/docs/Web/API/GPUCommandEncoder">MDN documentation</see>           
public unsafe partial struct WGPUCommandEncoder
{
    public WGPUComputePassEncoder beginComputePass(WGPUComputePassDescriptor descriptor) {
        var result = wgpuCommandEncoderBeginComputePass(Handle, &descriptor);
        ObjectTracker.CreateRef(result.Handle, HandleType.WGPUComputePassEncoder, descriptor._label);
        return result;
    }

    public WGPURenderPassEncoder beginRenderPass(WGPURenderPassDescriptor descriptor) {
        var result = wgpuCommandEncoderBeginRenderPass(Handle, &descriptor);
        ObjectTracker.CreateRef(result.Handle, HandleType.WGPURenderPassEncoder, descriptor._label);
        return result;
    }

    public void clearBuffer(WGPUBuffer buffer, ulong offset, ulong size) {
        wgpuCommandEncoderClearBuffer(Handle, buffer, offset, size);
    }

    public void copyBufferToBuffer(WGPUBuffer source, ulong sourceOffset, WGPUBuffer destination, ulong destinationOffset, ulong size) {
        wgpuCommandEncoderCopyBufferToBuffer(Handle, source, sourceOffset, destination, destinationOffset, size);
    }

    public void copyBufferToTexture(WGPUImageCopyBuffer source, WGPUImageCopyTexture destination, WGPUExtent3D copySize) {
        wgpuCommandEncoderCopyBufferToTexture(Handle, &source, &destination, &copySize);
    }

    public void copyTextureToBuffer(WGPUImageCopyTexture source, WGPUImageCopyBuffer destination, WGPUExtent3D copySize) {
        wgpuCommandEncoderCopyTextureToBuffer(Handle, &source, &destination, &copySize);
    }

    public void copyTextureToTexture(WGPUImageCopyTexture source, WGPUImageCopyTexture destination, WGPUExtent3D copySize) {
        wgpuCommandEncoderCopyTextureToTexture(Handle, &source, &destination, &copySize);
    }

    public WGPUCommandBuffer finish(WGPUCommandBufferDescriptor descriptor) {
        var result = wgpuCommandEncoderFinish(Handle, &descriptor);
        ObjectTracker.CreateRef(result.Handle, HandleType.WGPUCommandBuffer, descriptor._label);
        return result;
    }

    public void insertDebugMarker(Utf8 markerLabel) {
        wgpuCommandEncoderInsertDebugMarker(Handle, markerLabel.AllocUtf8());
    }

    public void popDebugGroup() {
        wgpuCommandEncoderPopDebugGroup(Handle);
    }

    public void pushDebugGroup(Utf8 groupLabel) {
        wgpuCommandEncoderPushDebugGroup(Handle, groupLabel.AllocUtf8());
    }

    public void resolveQuerySet(WGPUQuerySet querySet, uint firstQuery, uint queryCount, WGPUBuffer destination, ulong destinationOffset) {
        wgpuCommandEncoderResolveQuerySet(Handle, querySet, firstQuery, queryCount, destination, destinationOffset);
    }

    public void setLabel(Utf8 label) {
        wgpuCommandEncoderSetLabel(Handle, label.AllocUtf8());
    }

    public void writeTimestamp(WGPUQuerySet querySet, uint queryIndex) {
        wgpuCommandEncoderWriteTimestamp(Handle, querySet, queryIndex);
    }

    public void reference() {
        wgpuCommandEncoderReference(Handle);
        ObjectTracker.IncRef(Handle);
    }

    public void release() {
        ObjectTracker.DecRef(Handle);
        wgpuCommandEncoderRelease(Handle);
    }

    public override string? ToString() => ObjectTracker.GetLabel(Handle);
}
