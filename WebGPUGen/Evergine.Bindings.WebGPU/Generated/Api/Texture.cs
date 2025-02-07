namespace Evergine.Bindings.WebGPU;
using static WebGPUNative;

/// <see href="https://developer.mozilla.org/en-US/docs/Web/API/GPUTexture">MDN documentation</see>           
public unsafe partial struct WGPUTexture
{
    public WGPUTextureView createView(WGPUTextureViewDescriptor descriptor) {
        descriptor.Validate();
        var result = wgpuTextureCreateView(Handle, &descriptor);
        ObjectTracker.CreateRef(result.Handle, HandleType.WGPUTextureView, descriptor._label);
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

    public void setLabel(Utf8 label) {
        wgpuTextureSetLabel(Handle, label.AllocUtf8());
    }

    public void reference() {
        wgpuTextureReference(Handle);
        ObjectTracker.IncRef(Handle);
    }

    public void release() {
        ObjectTracker.DecRef(Handle);
        wgpuTextureRelease(Handle);
    }

    public override string? ToString() => ObjectTracker.GetLabel(Handle);
}
