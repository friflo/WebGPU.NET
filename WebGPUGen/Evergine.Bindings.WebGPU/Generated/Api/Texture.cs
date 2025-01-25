namespace Evergine.Bindings.WebGPU;
using static WebGPUNative;
           
public unsafe partial struct WGPUTexture
{
    public WGPUTextureView createView(WGPUTextureViewDescriptor descriptor) {
        var result = wgpuTextureCreateView(Handle, &descriptor);
        ObjectTracker.CreateRef(result.Handle, HandleType.WGPUTextureView, descriptor.label);
        return result;
    }

    public void destroy() {
        wgpuTextureDestroy(Handle);
    }

    public uint depthOrArrayLayers => wgpuTextureGetDepthOrArrayLayers(Handle);

    public WGPUTextureDimension dimension => wgpuTextureGetDimension(Handle);

    public WGPUTextureFormat format => wgpuTextureGetFormat(Handle);

    public uint height => wgpuTextureGetHeight(Handle);

    public uint mipLevelCount => wgpuTextureGetMipLevelCount(Handle);

    public uint sampleCount => wgpuTextureGetSampleCount(Handle);

    public WGPUTextureUsage usage => wgpuTextureGetUsage(Handle);

    public uint width => wgpuTextureGetWidth(Handle);

    public void setLabel(ReadOnlySpan<char> label) {
        wgpuTextureSetLabel(Handle, label.AllocString());
    }

    public void reference() {
        wgpuTextureReference(Handle);
        ObjectTracker.IncRef(Handle);
    }

    public void release() {
        ObjectTracker.DecRef(Handle);
        wgpuTextureRelease(Handle);
    }

}
