using static Evergine.Bindings.WebGPU.WebGPUNative;

// ReSharper disable InconsistentNaming
namespace Evergine.Bindings.WebGPU;

/// <see href="https://developer.mozilla.org/en-US/docs/Web/API/GPURenderBundleEncoder">MDN documentation</see>           
public unsafe partial struct WGPURenderBundleEncoder
{
    public void draw(uint vertexCount, uint instanceCount, uint firstVertex, uint firstInstance) {
        Validate_draw(Handle, vertexCount, instanceCount, firstVertex, firstInstance);
        wgpuRenderBundleEncoderDraw(Handle, vertexCount, instanceCount, firstVertex, firstInstance);
    }

    private static void Validate_draw(IntPtr handle, uint vertexCount, uint instanceCount, uint firstVertex, uint firstInstance) {
        ObjectTracker.ValidateHandle(handle);
    }

    public void drawIndexed(uint indexCount, uint instanceCount, uint firstIndex, int baseVertex, uint firstInstance) {
        Validate_drawIndexed(Handle, indexCount, instanceCount, firstIndex, baseVertex, firstInstance);
        wgpuRenderBundleEncoderDrawIndexed(Handle, indexCount, instanceCount, firstIndex, baseVertex, firstInstance);
    }

    private static void Validate_drawIndexed(IntPtr handle, uint indexCount, uint instanceCount, uint firstIndex, int baseVertex, uint firstInstance) {
        ObjectTracker.ValidateHandle(handle);
    }



    public WGPURenderBundle finish(WGPURenderBundleDescriptor descriptor) {
        Validate_finish(Handle, descriptor);
        var result = wgpuRenderBundleEncoderFinish(Handle, &descriptor);
        ObjectTracker.CreateRef(result.Handle, HandleType.WGPURenderBundle, descriptor._label);
        return result;
    }

    private static void Validate_finish(IntPtr handle, WGPURenderBundleDescriptor descriptor) {
        ObjectTracker.ValidateHandle(handle);
        descriptor.Validate();
    }

    public void insertDebugMarker(Utf8 markerLabel) {
        Validate_insertDebugMarker(Handle, markerLabel);
        wgpuRenderBundleEncoderInsertDebugMarker(Handle, markerLabel.AllocUtf8());
    }

    private static void Validate_insertDebugMarker(IntPtr handle, Utf8 markerLabel) {
        ObjectTracker.ValidateHandle(handle);
    }

    public void popDebugGroup() {
        Validate_popDebugGroup(Handle);
        wgpuRenderBundleEncoderPopDebugGroup(Handle);
    }

    private static void Validate_popDebugGroup(IntPtr handle) {
        ObjectTracker.ValidateHandle(handle);
    }

    public void pushDebugGroup(Utf8 groupLabel) {
        Validate_pushDebugGroup(Handle, groupLabel);
        wgpuRenderBundleEncoderPushDebugGroup(Handle, groupLabel.AllocUtf8());
    }

    private static void Validate_pushDebugGroup(IntPtr handle, Utf8 groupLabel) {
        ObjectTracker.ValidateHandle(handle);
    }

    public void setBindGroup(uint groupIndex, WGPUBindGroup group, ReadOnlySpan<uint> dynamicOffsets) {
        fixed (uint* ptr = dynamicOffsets) {
            wgpuRenderBundleEncoderSetBindGroup(Handle, groupIndex, group, (ulong)dynamicOffsets.Length, ptr);    
        }
    }

    public void setIndexBuffer(WGPUBuffer buffer, WGPUIndexFormat format, ulong offset, ulong size) {
        Validate_setIndexBuffer(Handle, buffer, format, offset, size);
        wgpuRenderBundleEncoderSetIndexBuffer(Handle, buffer, format, offset, size);
    }

    private static void Validate_setIndexBuffer(IntPtr handle, WGPUBuffer buffer, WGPUIndexFormat format, ulong offset, ulong size) {
        ObjectTracker.ValidateHandle(handle);
    }

    public void setLabel(Utf8 label) {
        Validate_setLabel(Handle, label);
        wgpuRenderBundleEncoderSetLabel(Handle, label.AllocUtf8());
    }

    private static void Validate_setLabel(IntPtr handle, Utf8 label) {
        ObjectTracker.ValidateHandle(handle);
    }

    public void setPipeline(WGPURenderPipeline pipeline) {
        Validate_setPipeline(Handle, pipeline);
        wgpuRenderBundleEncoderSetPipeline(Handle, pipeline);
    }

    private static void Validate_setPipeline(IntPtr handle, WGPURenderPipeline pipeline) {
        ObjectTracker.ValidateHandle(handle);
    }

    public void setVertexBuffer(uint slot, WGPUBuffer buffer, ulong offset, ulong size) {
        Validate_setVertexBuffer(Handle, slot, buffer, offset, size);
        wgpuRenderBundleEncoderSetVertexBuffer(Handle, slot, buffer, offset, size);
    }

    private static void Validate_setVertexBuffer(IntPtr handle, uint slot, WGPUBuffer buffer, ulong offset, ulong size) {
        ObjectTracker.ValidateHandle(handle);
    }

    public void reference() {
        ObjectTracker.IncRef(Handle);
        wgpuRenderBundleEncoderReference(Handle);
    }

    public void release() {
        ObjectTracker.DecRef(Handle);
        wgpuRenderBundleEncoderRelease(Handle);
    }

    public override string? ToString() => ObjectTracker.GetLabel(Handle);
}
