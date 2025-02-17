using System.Diagnostics;
using static Evergine.Bindings.WebGPU.WebGPUNative;

// ReSharper disable InconsistentNaming
namespace Evergine.Bindings.WebGPU;

/// <see href="https://developer.mozilla.org/en-US/docs/Web/API/GPUTexture">MDN documentation</see>           
public unsafe partial struct WGPUTexture
{
    public WGPUTextureView createView(WGPUTextureViewDescriptor descriptor) {
        Validate_createView(descriptor);
        var result = wgpuTextureCreateView(this, &descriptor);
        WGPUException.ThrowOnError();
        ObjectTracker.CreateRefLabel(result, HandleType.WGPUTextureView, descriptor._label); // ref-create
        return result;
    }

    [Conditional("VALIDATE")]
    private void Validate_createView(WGPUTextureViewDescriptor descriptor) {
        ObjectTracker.ValidateHandle(this);
        descriptor.Validate();
    }

    public void destroy() {
        Validate_destroy();
        wgpuTextureDestroy(this);
        WGPUException.ThrowOnError();
    }

    [Conditional("VALIDATE")]
    private void Validate_destroy() {
        ObjectTracker.ValidateHandle(this);
    }

    public uint depthOrArrayLayers { get {
          ObjectTracker.ValidateHandle(this);
          return wgpuTextureGetDepthOrArrayLayers(this);
    } }

    public WGPUTextureDimension dimension { get {
          ObjectTracker.ValidateHandle(this);
          return wgpuTextureGetDimension(this);
    } }

    public WGPUTextureFormat format { get {
          ObjectTracker.ValidateHandle(this);
          return wgpuTextureGetFormat(this);
    } }

    public uint height { get {
          ObjectTracker.ValidateHandle(this);
          return wgpuTextureGetHeight(this);
    } }

    public uint mipLevelCount { get {
          ObjectTracker.ValidateHandle(this);
          return wgpuTextureGetMipLevelCount(this);
    } }

    public uint sampleCount { get {
          ObjectTracker.ValidateHandle(this);
          return wgpuTextureGetSampleCount(this);
    } }

    public WGPUTextureUsage usage { get {
          ObjectTracker.ValidateHandle(this);
          return wgpuTextureGetUsage(this);
    } }

    public uint width { get {
          ObjectTracker.ValidateHandle(this);
          return wgpuTextureGetWidth(this);
    } }


    public void reference() {
        ObjectTracker.IncRef(this);
        wgpuTextureReference(this);
    }

    public void release() {
        ObjectTracker.DecRef(this);
        wgpuTextureRelease(this);
    }
    
    public void Dispose() {
        ObjectTracker.DecRef(this);
        wgpuTextureRelease(this);
    }

    public override string? ToString() => ObjectTracker.GetLabel(Handle);
}
