namespace Evergine.Bindings.WebGPU;
using static WebGPUNative;
           
public static unsafe partial class WebGPUExtensions
{
    public static WGPUTextureView createView(this WGPUTexture texture, WGPUTextureViewDescriptor descriptor) {
        var result = wgpuTextureCreateView(texture, &descriptor);
        ObjectTracker.CreateRef(result.Handle, HandleType.WGPUTextureView, descriptor.label);
        return result;
    }

    public static void destroy(this WGPUTexture texture) {
        wgpuTextureDestroy(texture);
    }

    public static uint getDepthOrArrayLayers(this WGPUTexture texture) {
        var result = wgpuTextureGetDepthOrArrayLayers(texture);
        return result;
    }

    public static WGPUTextureDimension getDimension(this WGPUTexture texture) {
        var result = wgpuTextureGetDimension(texture);
        return result;
    }

    public static WGPUTextureFormat getFormat(this WGPUTexture texture) {
        var result = wgpuTextureGetFormat(texture);
        return result;
    }

    public static uint getHeight(this WGPUTexture texture) {
        var result = wgpuTextureGetHeight(texture);
        return result;
    }

    public static uint getMipLevelCount(this WGPUTexture texture) {
        var result = wgpuTextureGetMipLevelCount(texture);
        return result;
    }

    public static uint getSampleCount(this WGPUTexture texture) {
        var result = wgpuTextureGetSampleCount(texture);
        return result;
    }

    public static WGPUTextureUsage getUsage(this WGPUTexture texture) {
        var result = wgpuTextureGetUsage(texture);
        return result;
    }

    public static uint getWidth(this WGPUTexture texture) {
        var result = wgpuTextureGetWidth(texture);
        return result;
    }

    public static void setLabel(this WGPUTexture texture, ReadOnlySpan<char> label) {
        wgpuTextureSetLabel(texture, label.AllocString());
    }

    public static void reference(this WGPUTexture texture) {
        wgpuTextureReference(texture);
        ObjectTracker.IncRef(texture.Handle);
    }

    public static void release(this WGPUTexture texture) {
        ObjectTracker.DecRef(texture.Handle);
        wgpuTextureRelease(texture);
    }

}
