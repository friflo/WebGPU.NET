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

    public uint getDepthOrArrayLayers() {
        var result = wgpuTextureGetDepthOrArrayLayers(Handle);
        return result;
    }

    public WGPUTextureDimension getDimension() {
        var result = wgpuTextureGetDimension(Handle);
        return result;
    }

    public WGPUTextureFormat getFormat() {
        var result = wgpuTextureGetFormat(Handle);
        return result;
    }

    public uint getHeight() {
        var result = wgpuTextureGetHeight(Handle);
        return result;
    }

    public uint getMipLevelCount() {
        var result = wgpuTextureGetMipLevelCount(Handle);
        return result;
    }

    public uint getSampleCount() {
        var result = wgpuTextureGetSampleCount(Handle);
        return result;
    }

    public WGPUTextureUsage getUsage() {
        var result = wgpuTextureGetUsage(Handle);
        return result;
    }

    public uint getWidth() {
        var result = wgpuTextureGetWidth(Handle);
        return result;
    }

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
