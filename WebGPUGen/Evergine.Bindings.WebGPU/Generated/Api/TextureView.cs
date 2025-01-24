namespace Evergine.Bindings.WebGPU;
using static WebGPUNative;
           
public static unsafe partial class WebGPUExtensions
{
    public static void setLabel(this WGPUTextureView textureView, ReadOnlySpan<char> label) {
        wgpuTextureViewSetLabel(textureView, label.AllocString());
    }

    public static void reference(this WGPUTextureView textureView) {
        wgpuTextureViewReference(textureView);
        ObjectTracker.IncRef(textureView.Handle);
    }

    public static void release(this WGPUTextureView textureView) {
        ObjectTracker.DecRef(textureView.Handle);
        wgpuTextureViewRelease(textureView);
    }

}
