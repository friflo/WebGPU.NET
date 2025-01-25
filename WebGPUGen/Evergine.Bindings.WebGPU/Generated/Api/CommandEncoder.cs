namespace Evergine.Bindings.WebGPU;
using static WebGPUNative;
           
public unsafe partial struct WGPUCommandEncoder
{
    public WGPUComputePassEncoder beginComputePass(WGPUComputePassDescriptor descriptor) {
        var result = wgpuCommandEncoderBeginComputePass(Handle, &descriptor);
        ObjectTracker.CreateRef(result.Handle, HandleType.WGPUComputePassEncoder, descriptor.label);
        return result;
    }

    public WGPURenderPassEncoder beginRenderPass(WGPURenderPassDescriptor descriptor) {
        var result = wgpuCommandEncoderBeginRenderPass(Handle, &descriptor);
        ObjectTracker.CreateRef(result.Handle, HandleType.WGPURenderPassEncoder, descriptor.label);
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
        ObjectTracker.CreateRef(result.Handle, HandleType.WGPUCommandBuffer, descriptor.label);
        return result;
    }

    public void insertDebugMarker(ReadOnlySpan<char> markerLabel) {
        wgpuCommandEncoderInsertDebugMarker(Handle, markerLabel.AllocString());
    }

    public void popDebugGroup() {
        wgpuCommandEncoderPopDebugGroup(Handle);
    }

    public void pushDebugGroup(ReadOnlySpan<char> groupLabel) {
        wgpuCommandEncoderPushDebugGroup(Handle, groupLabel.AllocString());
    }

    public void resolveQuerySet(WGPUQuerySet querySet, uint firstQuery, uint queryCount, WGPUBuffer destination, ulong destinationOffset) {
        wgpuCommandEncoderResolveQuerySet(Handle, querySet, firstQuery, queryCount, destination, destinationOffset);
    }

    public void setLabel(ReadOnlySpan<char> label) {
        wgpuCommandEncoderSetLabel(Handle, label.AllocString());
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

}
