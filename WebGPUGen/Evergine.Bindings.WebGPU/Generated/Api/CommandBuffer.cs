namespace Evergine.Bindings.WebGPU;
using static WebGPUNative;

/// <see href="https://developer.mozilla.org/en-US/docs/Web/API/GPUCommandBuffer">MDN documentation</see>           
public unsafe partial struct WGPUCommandBuffer
{
    public void setLabel(Utf8 label) {
        wgpuCommandBufferSetLabel(Handle, label.AllocUtf8());
    }

    public void reference() {
        wgpuCommandBufferReference(Handle);
        ObjectTracker.IncRef(Handle);
    }

    public void release() {
        ObjectTracker.DecRef(Handle);
        wgpuCommandBufferRelease(Handle);
    }

}
