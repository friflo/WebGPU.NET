namespace Evergine.Bindings.WebGPU;
using static WebGPUNative;
           
public unsafe partial struct WGPUQuerySet
{
    public void destroy() {
        wgpuQuerySetDestroy(Handle);
    }

    public uint getCount() {
        var result = wgpuQuerySetGetCount(Handle);
        return result;
    }

    public WGPUQueryType getType() {
        var result = wgpuQuerySetGetType(Handle);
        return result;
    }

    public void setLabel(ReadOnlySpan<char> label) {
        wgpuQuerySetSetLabel(Handle, label.AllocString());
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
