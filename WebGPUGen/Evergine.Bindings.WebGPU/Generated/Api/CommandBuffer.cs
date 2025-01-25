namespace Evergine.Bindings.WebGPU;
using static WebGPUNative;
           
public unsafe partial struct WGPUCommandBuffer
{
    public void setLabel(ReadOnlySpan<char> label) {
        wgpuCommandBufferSetLabel(Handle, label.AllocString());
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
