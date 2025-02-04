namespace Evergine.Bindings.WebGPU;
using static WebGPUNative;

/// <see href="https://developer.mozilla.org/en-US/docs/Web/API/GPURenderBundle">MDN documentation</see>           
public unsafe partial struct WGPURenderBundle
{
    public void setLabel(Utf8 label) {
        wgpuRenderBundleSetLabel(Handle, label.AllocUtf8());
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
