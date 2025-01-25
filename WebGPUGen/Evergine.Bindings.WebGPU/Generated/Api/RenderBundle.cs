namespace Evergine.Bindings.WebGPU;
using static WebGPUNative;
           
public unsafe partial struct WGPURenderBundle
{
    public void setLabel(ReadOnlySpan<char> label) {
        wgpuRenderBundleSetLabel(Handle, label.AllocString());
    }

    public void reference() {
        wgpuRenderBundleReference(Handle);
        ObjectTracker.IncRef(Handle);
    }

    public void release() {
        ObjectTracker.DecRef(Handle);
        wgpuRenderBundleRelease(Handle);
    }

}
