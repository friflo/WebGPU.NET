namespace Evergine.Bindings.WebGPU;
using static WebGPUNative;

/// <see href="https://developer.mozilla.org/en-US/docs/Web/API/GPUQuerySet">MDN documentation</see>           
public unsafe partial struct WGPUQuerySet
{
    public void destroy() {
        wgpuQuerySetDestroy(Handle);
    }

    public uint count => wgpuQuerySetGetCount(Handle);

    public WGPUQueryType type => wgpuQuerySetGetType(Handle);

    public void setLabel(Utf8 label) {
        wgpuQuerySetSetLabel(Handle, label.AllocUtf8());
    }

    public void reference() {
        wgpuQuerySetReference(Handle);
        ObjectTracker.IncRef(Handle);
    }

    public void release() {
        ObjectTracker.DecRef(Handle);
        wgpuQuerySetRelease(Handle);
    }

}
