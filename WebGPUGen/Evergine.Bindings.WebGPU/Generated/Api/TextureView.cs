namespace Evergine.Bindings.WebGPU;
using static WebGPUNative;

/// <see href="https://developer.mozilla.org/en-US/docs/Web/API/GPUTextureView">MDN documentation</see>           
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
