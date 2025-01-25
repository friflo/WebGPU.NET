namespace Evergine.Bindings.WebGPU;
using static WebGPUNative;
           
public unsafe partial struct WGPUTextureView
{
    public void setLabel(ReadOnlySpan<char> label) {
        wgpuTextureViewSetLabel(Handle, label.AllocString());
    }

    public void reference() {
        wgpuTextureViewReference(Handle);
        ObjectTracker.IncRef(Handle);
    }

    public void release() {
        ObjectTracker.DecRef(Handle);
        wgpuTextureViewRelease(Handle);
    }

}
