namespace Evergine.Bindings.WebGPU;
using static WebGPUNative;

/// <see href="https://developer.mozilla.org/en-US/docs/Web/API/GPURenderBundleEncoder">MDN documentation</see>           
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
        ObjectTracker.CreateRef(result.Handle, HandleType.WGPURenderBundle, descriptor._label);
        return result;
    }

    public void insertDebugMarker(Utf8 markerLabel) {
        wgpuRenderBundleEncoderInsertDebugMarker(Handle, markerLabel.AllocUtf8());
    }

    public void popDebugGroup() {
        wgpuRenderBundleEncoderPopDebugGroup(Handle);
    }

    public void pushDebugGroup(Utf8 groupLabel) {
        wgpuRenderBundleEncoderPushDebugGroup(Handle, groupLabel.AllocUtf8());
    }

    public void setBindGroup(uint groupIndex, WGPUBindGroup group, ReadOnlySpan<uint> dynamicOffsets) {
        fixed (uint* ptr = dynamicOffsets) {
            wgpuRenderBundleEncoderSetBindGroup(Handle, groupIndex, group, (ulong)dynamicOffsets.Length, ptr);    
        }
    }

    public void setIndexBuffer(WGPUBuffer buffer, WGPUIndexFormat format, ulong offset, ulong size) {
        wgpuRenderBundleEncoderSetIndexBuffer(Handle, buffer, format, offset, size);
    }

    public void setLabel(Utf8 label) {
        wgpuRenderBundleEncoderSetLabel(Handle, label.AllocUtf8());
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

    public override string? ToString() => ObjectTracker.GetLabel(Handle);
}
