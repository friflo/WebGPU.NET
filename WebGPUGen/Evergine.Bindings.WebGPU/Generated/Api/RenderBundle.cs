namespace Evergine.Bindings.WebGPU;
using static WebGPUNative;
           
public static unsafe partial class WebGPUExtensions
{
    public static void setLabel(this WGPURenderBundle renderBundle, ReadOnlySpan<char> label) {
        wgpuRenderBundleSetLabel(renderBundle, label.AllocString());
    }

    public static void reference(this WGPURenderBundle renderBundle) {
        wgpuRenderBundleReference(renderBundle);
        ObjectTracker.IncRef(renderBundle.Handle);
    }

    public static void release(this WGPURenderBundle renderBundle) {
        ObjectTracker.DecRef(renderBundle.Handle);
        wgpuRenderBundleRelease(renderBundle);
    }

}
