using static Evergine.Bindings.WebGPU.WebGPUNative;

// ReSharper disable InconsistentNaming
namespace Evergine.Bindings.WebGPU;

/// <see href="https://developer.mozilla.org/en-US/docs/Web/API/GPUTexture">MDN documentation</see>           
public unsafe partial struct WGPUTexture
{
    public WGPUTextureView createView(WGPUTextureViewDescriptor descriptor) {
        Validate_createView(Handle, descriptor);
        var result = wgpuTextureCreateView(Handle, &descriptor);
        ObjectTracker.CreateRef(result.Handle, HandleType.WGPUTextureView, descriptor._label);
        return result;
    }

    private static void Validate_createView(IntPtr handle, WGPUTextureViewDescriptor descriptor) {
        ObjectTracker.ValidateHandle(handle);
        descriptor.Validate();
    }

    public void destroy() {
        Validate_destroy(Handle);
        wgpuTextureDestroy(Handle);
    }

    private static void Validate_destroy(IntPtr handle) {
        ObjectTracker.ValidateHandle(handle);
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
        Validate_setLabel(Handle, label);
        wgpuTextureSetLabel(Handle, label.AllocUtf8());
    }

    private static void Validate_setLabel(IntPtr handle, Utf8 label) {
        ObjectTracker.ValidateHandle(handle);
    }

    public void reference() {
        ObjectTracker.IncRef(Handle);
        wgpuTextureReference(Handle);
    }

    public void release() {
        ObjectTracker.DecRef(Handle);
        wgpuTextureRelease(Handle);
    }

    public override string? ToString() => ObjectTracker.GetLabel(Handle);
}
