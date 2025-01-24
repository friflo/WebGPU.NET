namespace Evergine.Bindings.WebGPU;
using static WebGPUNative;
           
public static unsafe partial class WebGPUExtensions
{
    public static void destroy(this WGPUQuerySet querySet) {
        wgpuQuerySetDestroy(querySet);
    }

    public static uint getCount(this WGPUQuerySet querySet) {
        var result = wgpuQuerySetGetCount(querySet);
        return result;
    }

    public static WGPUQueryType getType(this WGPUQuerySet querySet) {
        var result = wgpuQuerySetGetType(querySet);
        return result;
    }

    public static void setLabel(this WGPUQuerySet querySet, ReadOnlySpan<char> label) {
        wgpuQuerySetSetLabel(querySet, label.AllocString());
    }

    public static void reference(this WGPUQuerySet querySet) {
        wgpuQuerySetReference(querySet);
        ObjectTracker.IncRef(querySet.Handle);
    }

    public static void release(this WGPUQuerySet querySet) {
        ObjectTracker.DecRef(querySet.Handle);
        wgpuQuerySetRelease(querySet);
    }

}
