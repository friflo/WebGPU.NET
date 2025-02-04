namespace Evergine.Bindings.WebGPU;
using static WebGPUNative;

/// <see href="https://developer.mozilla.org/en-US/docs/Web/API/GPUBindGroupLayout">MDN documentation</see>           
public unsafe partial struct WGPUBindGroupLayout
{
    public void setLabel(Utf8 label) {
        wgpuBindGroupLayoutSetLabel(Handle, label.AllocUtf8());
    }

    public void reference() {
        wgpuBindGroupLayoutReference(Handle);
        ObjectTracker.IncRef(Handle);
    }

    public void release() {
        ObjectTracker.DecRef(Handle);
        wgpuBindGroupLayoutRelease(Handle);
    }

}
