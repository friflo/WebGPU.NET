using System.Diagnostics;
using static Evergine.Bindings.WebGPU.WebGPUNative;

// ReSharper disable InconsistentNaming
namespace Evergine.Bindings.WebGPU;

/// <see href="https://developer.mozilla.org/en-US/docs/Web/API/GPUBuffer">MDN documentation</see>           
public unsafe partial struct WGPUBuffer
{
    public void destroy() {
        Validate_destroy();
        wgpuBufferDestroy(this);
    }

    [Conditional("VALIDATE")]
    private void Validate_destroy() {
        ObjectTracker.ValidateHandle(this);
    }

    // getConstMappedRange() - not generated. See: Buffer_NG.cs

    // getMapState() - not generated. See: Buffer_NG.cs

    // getMappedRange() - not generated. See: Buffer_NG.cs

    public ulong size { get {
          ObjectTracker.ValidateHandle(this);
          return wgpuBufferGetSize(this);
    } }

    public WGPUBufferUsage usage { get {
          ObjectTracker.ValidateHandle(this);
          return wgpuBufferGetUsage(this);
    } }

    public void mapAsync(WGPUMapMode mode, ulong offset, ulong size, delegate* unmanaged<WGPUBufferMapAsyncStatus, void*, void> callback, void* userdata) {
        Validate_mapAsync(mode, offset, size, callback, userdata);
        wgpuBufferMapAsync(this, mode, offset, size, callback, userdata);
    }

    [Conditional("VALIDATE")]
    private void Validate_mapAsync(WGPUMapMode mode, ulong offset, ulong size, delegate* unmanaged<WGPUBufferMapAsyncStatus, void*, void> callback, void* userdata) {
        ObjectTracker.ValidateHandle(this);
    }


    public void unmap() {
        Validate_unmap();
        wgpuBufferUnmap(this);
    }

    [Conditional("VALIDATE")]
    private void Validate_unmap() {
        ObjectTracker.ValidateHandle(this);
    }

    public void reference() {
        ObjectTracker.IncRef(this);
        wgpuBufferReference(this);
    }

    public void release() {
        ObjectTracker.DecRef(this);
        wgpuBufferRelease(this);
    }

    public override string? ToString() => ObjectTracker.GetLabel(Handle);
}
