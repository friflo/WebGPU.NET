namespace Evergine.Bindings.WebGPU;
using static WebGPUNative;
           
public static unsafe partial class WebGPUExtensions
{
    public static void draw(this WGPURenderBundleEncoder renderBundleEncoder, uint vertexCount, uint instanceCount, uint firstVertex, uint firstInstance) {
        wgpuRenderBundleEncoderDraw(renderBundleEncoder, vertexCount, instanceCount, firstVertex, firstInstance);
    }

    public static void drawIndexed(this WGPURenderBundleEncoder renderBundleEncoder, uint indexCount, uint instanceCount, uint firstIndex, int baseVertex, uint firstInstance) {
        wgpuRenderBundleEncoderDrawIndexed(renderBundleEncoder, indexCount, instanceCount, firstIndex, baseVertex, firstInstance);
    }



    public static WGPURenderBundle finish(this WGPURenderBundleEncoder renderBundleEncoder, WGPURenderBundleDescriptor descriptor) {
        var result = wgpuRenderBundleEncoderFinish(renderBundleEncoder, &descriptor);
        ObjectTracker.CreateRef(result.Handle, HandleType.WGPURenderBundle, descriptor.label);
        return result;
    }

    public static void insertDebugMarker(this WGPURenderBundleEncoder renderBundleEncoder, ReadOnlySpan<char> markerLabel) {
        wgpuRenderBundleEncoderInsertDebugMarker(renderBundleEncoder, markerLabel.AllocString());
    }

    public static void popDebugGroup(this WGPURenderBundleEncoder renderBundleEncoder) {
        wgpuRenderBundleEncoderPopDebugGroup(renderBundleEncoder);
    }

    public static void pushDebugGroup(this WGPURenderBundleEncoder renderBundleEncoder, ReadOnlySpan<char> groupLabel) {
        wgpuRenderBundleEncoderPushDebugGroup(renderBundleEncoder, groupLabel.AllocString());
    }

    public static void setBindGroup(this WGPURenderBundleEncoder renderBundleEncoder, uint groupIndex, WGPUBindGroup group, ulong dynamicOffsetCount, uint* dynamicOffsets) {
        wgpuRenderBundleEncoderSetBindGroup(renderBundleEncoder, groupIndex, group, dynamicOffsetCount, dynamicOffsets);
    }

    public static void setIndexBuffer(this WGPURenderBundleEncoder renderBundleEncoder, WGPUBuffer buffer, WGPUIndexFormat format, ulong offset, ulong size) {
        wgpuRenderBundleEncoderSetIndexBuffer(renderBundleEncoder, buffer, format, offset, size);
    }

    public static void setLabel(this WGPURenderBundleEncoder renderBundleEncoder, ReadOnlySpan<char> label) {
        wgpuRenderBundleEncoderSetLabel(renderBundleEncoder, label.AllocString());
    }

    public static void setPipeline(this WGPURenderBundleEncoder renderBundleEncoder, WGPURenderPipeline pipeline) {
        wgpuRenderBundleEncoderSetPipeline(renderBundleEncoder, pipeline);
    }

    public static void setVertexBuffer(this WGPURenderBundleEncoder renderBundleEncoder, uint slot, WGPUBuffer buffer, ulong offset, ulong size) {
        wgpuRenderBundleEncoderSetVertexBuffer(renderBundleEncoder, slot, buffer, offset, size);
    }

    public static void reference(this WGPURenderBundleEncoder renderBundleEncoder) {
        wgpuRenderBundleEncoderReference(renderBundleEncoder);
        ObjectTracker.IncRef(renderBundleEncoder.Handle);
    }

    public static void release(this WGPURenderBundleEncoder renderBundleEncoder) {
        ObjectTracker.DecRef(renderBundleEncoder.Handle);
        wgpuRenderBundleEncoderRelease(renderBundleEncoder);
    }

}
