using System.Diagnostics;
using static Evergine.Bindings.WebGPU.WebGPUNative;

// ReSharper disable InconsistentNaming
namespace Evergine.Bindings.WebGPU;

/// <see href="https://developer.mozilla.org/en-US/docs/Web/API/GPUTexture">MDN documentation</see>           
public unsafe partial struct WGPUTexture
{
    public WGPUTextureView createView(WGPUTextureViewDescriptor descriptor) {
        Validate_createView(Handle, descriptor);
        var result = wgpuTextureCreateView(this, &descriptor);
        ObjectTracker.CreateRef(result.Handle, HandleType.WGPUTextureView, descriptor._label);
        return result;
    }

    [Conditional("VALIDATE")]
    private static void Validate_createView(IntPtr handle, WGPUTextureViewDescriptor descriptor) {
        ObjectTracker.ValidateHandle(handle);
        descriptor.Validate();
    }

    public void destroy() {
        Validate_destroy(Handle);
        wgpuTextureDestroy(this);
    }

    [Conditional("VALIDATE")]
    private static void Validate_destroy(IntPtr handle) {
        ObjectTracker.ValidateHandle(handle);
    }

    public uint depthOrArrayLayers => wgpuTextureGetDepthOrArrayLayers(this);

    public WGPUTextureDimension dimension => wgpuTextureGetDimension(this);

    public WGPUTextureFormat format => wgpuTextureGetFormat(this);

    public uint height => wgpuTextureGetHeight(this);

    public uint mipLevelCount => wgpuTextureGetMipLevelCount(this);

    public uint sampleCount => wgpuTextureGetSampleCount(this);

    public WGPUTextureUsage usage => wgpuTextureGetUsage(this);

    public uint width => wgpuTextureGetWidth(this);


    public void reference() {
        ObjectTracker.IncRef(Handle);
        wgpuTextureReference(this);
    }

    public void release() {
        ObjectTracker.DecRef(Handle);
        wgpuTextureRelease(this);
    }

    public override string? ToString() => ObjectTracker.GetLabel(Handle);
}
