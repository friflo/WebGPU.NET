namespace Evergine.Bindings.WebGPU;
using static WebGPUNative;
           
public unsafe partial struct WGPURenderBundleEncoder
{
    public void draw(uint vertexCount, uint instanceCount, uint firstVertex, uint firstInstance) {
        wgpuRenderBundleEncoderDraw(Handle, vertexCount, instanceCount, firstVertex, firstInstance);
    }

    public void drawIndexed(uint indexCount, uint instanceCount, uint firstIndex, int baseVertex, uint firstInstance) {
        wgpuRenderBundleEncoderDrawIndexed(Handle, indexCount, instanceCount, firstIndex, baseVertex, firstInstance);
    }



    public WGPURenderBundle finish(WGPURenderBundleDescriptor descriptor) {
        var result = wgpuRenderBundleEncoderFinish(Handle, &descriptor);
        ObjectTracker.CreateRef(result.Handle, HandleType.WGPURenderBundle, descriptor.label);
        return result;
    }

    public void insertDebugMarker(ReadOnlySpan<char> markerLabel) {
        wgpuRenderBundleEncoderInsertDebugMarker(Handle, markerLabel.AllocString());
    }

    public void popDebugGroup() {
        wgpuRenderBundleEncoderPopDebugGroup(Handle);
    }

    public void pushDebugGroup(ReadOnlySpan<char> groupLabel) {
        wgpuRenderBundleEncoderPushDebugGroup(Handle, groupLabel.AllocString());
    }

    public void setBindGroup(uint groupIndex, WGPUBindGroup group, ulong dynamicOffsetCount, uint* dynamicOffsets) {
        wgpuRenderBundleEncoderSetBindGroup(Handle, groupIndex, group, dynamicOffsetCount, dynamicOffsets);
    }

    public void setIndexBuffer(WGPUBuffer buffer, WGPUIndexFormat format, ulong offset, ulong size) {
        wgpuRenderBundleEncoderSetIndexBuffer(Handle, buffer, format, offset, size);
    }

    public void setLabel(ReadOnlySpan<char> label) {
        wgpuRenderBundleEncoderSetLabel(Handle, label.AllocString());
    }

    public void setPipeline(WGPURenderPipeline pipeline) {
        wgpuRenderBundleEncoderSetPipeline(Handle, pipeline);
    }

    public void setVertexBuffer(uint slot, WGPUBuffer buffer, ulong offset, ulong size) {
        wgpuRenderBundleEncoderSetVertexBuffer(Handle, slot, buffer, offset, size);
    }

    public void reference() {
        wgpuRenderBundleEncoderReference(Handle);
        ObjectTracker.IncRef(Handle);
    }

    public void release() {
        ObjectTracker.DecRef(Handle);
        wgpuRenderBundleEncoderRelease(Handle);
    }

}
