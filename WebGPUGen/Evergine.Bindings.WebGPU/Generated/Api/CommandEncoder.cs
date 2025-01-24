namespace Evergine.Bindings.WebGPU;
using static WebGPUNative;
           
public static unsafe partial class WebGPUExtensions
{
    public static WGPUComputePassEncoder beginComputePass(this WGPUCommandEncoder commandEncoder, WGPUComputePassDescriptor descriptor) {
        var result = wgpuCommandEncoderBeginComputePass(commandEncoder, &descriptor);
        ObjectTracker.CreateRef(result.Handle);
        return result;
    }

    public static WGPURenderPassEncoder beginRenderPass(this WGPUCommandEncoder commandEncoder, WGPURenderPassDescriptor descriptor) {
        var result = wgpuCommandEncoderBeginRenderPass(commandEncoder, &descriptor);
        ObjectTracker.CreateRef(result.Handle);
        return result;
    }

    public static void clearBuffer(this WGPUCommandEncoder commandEncoder, WGPUBuffer buffer, ulong offset, ulong size) {
        wgpuCommandEncoderClearBuffer(commandEncoder, buffer, offset, size);
    }

    public static void copyBufferToBuffer(this WGPUCommandEncoder commandEncoder, WGPUBuffer source, ulong sourceOffset, WGPUBuffer destination, ulong destinationOffset, ulong size) {
        wgpuCommandEncoderCopyBufferToBuffer(commandEncoder, source, sourceOffset, destination, destinationOffset, size);
    }

    public static void copyBufferToTexture(this WGPUCommandEncoder commandEncoder, WGPUImageCopyBuffer source, WGPUImageCopyTexture destination, WGPUExtent3D copySize) {
        wgpuCommandEncoderCopyBufferToTexture(commandEncoder, &source, &destination, &copySize);
    }

    public static void copyTextureToBuffer(this WGPUCommandEncoder commandEncoder, WGPUImageCopyTexture source, WGPUImageCopyBuffer destination, WGPUExtent3D copySize) {
        wgpuCommandEncoderCopyTextureToBuffer(commandEncoder, &source, &destination, &copySize);
    }

    public static void copyTextureToTexture(this WGPUCommandEncoder commandEncoder, WGPUImageCopyTexture source, WGPUImageCopyTexture destination, WGPUExtent3D copySize) {
        wgpuCommandEncoderCopyTextureToTexture(commandEncoder, &source, &destination, &copySize);
    }

    public static WGPUCommandBuffer finish(this WGPUCommandEncoder commandEncoder, WGPUCommandBufferDescriptor descriptor) {
        var result = wgpuCommandEncoderFinish(commandEncoder, &descriptor);
        ObjectTracker.CreateRef(result.Handle);
        return result;
    }

    public static void insertDebugMarker(this WGPUCommandEncoder commandEncoder, ReadOnlySpan<char> markerLabel) {
        wgpuCommandEncoderInsertDebugMarker(commandEncoder, markerLabel.AllocString());
    }

    public static void popDebugGroup(this WGPUCommandEncoder commandEncoder) {
        wgpuCommandEncoderPopDebugGroup(commandEncoder);
    }

    public static void pushDebugGroup(this WGPUCommandEncoder commandEncoder, ReadOnlySpan<char> groupLabel) {
        wgpuCommandEncoderPushDebugGroup(commandEncoder, groupLabel.AllocString());
    }

    public static void resolveQuerySet(this WGPUCommandEncoder commandEncoder, WGPUQuerySet querySet, uint firstQuery, uint queryCount, WGPUBuffer destination, ulong destinationOffset) {
        wgpuCommandEncoderResolveQuerySet(commandEncoder, querySet, firstQuery, queryCount, destination, destinationOffset);
    }

    public static void setLabel(this WGPUCommandEncoder commandEncoder, ReadOnlySpan<char> label) {
        wgpuCommandEncoderSetLabel(commandEncoder, label.AllocString());
    }

    public static void writeTimestamp(this WGPUCommandEncoder commandEncoder, WGPUQuerySet querySet, uint queryIndex) {
        wgpuCommandEncoderWriteTimestamp(commandEncoder, querySet, queryIndex);
    }

    public static void reference(this WGPUCommandEncoder commandEncoder) {
        wgpuCommandEncoderReference(commandEncoder);
        ObjectTracker.IncRef(commandEncoder.Handle);
    }

    public static void release(this WGPUCommandEncoder commandEncoder) {
        ObjectTracker.DecRef(commandEncoder.Handle);
        wgpuCommandEncoderRelease(commandEncoder);
    }

}
