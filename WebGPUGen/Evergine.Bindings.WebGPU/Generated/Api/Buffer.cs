using System.Diagnostics;
using static Evergine.Bindings.WebGPU.WebGPUNative;

// ReSharper disable InconsistentNaming
namespace Evergine.Bindings.WebGPU;

/// <see href="https://developer.mozilla.org/en-US/docs/Web/API/GPUBuffer">MDN documentation</see>           
public unsafe partial struct WGPUBuffer
{
    public void destroy() {
        Validate_destroy(Handle);
        wgpuBufferDestroy(Handle);
    }

    [Conditional("VALIDATE")]
    private static void Validate_destroy(IntPtr handle) {
        ObjectTracker.ValidateHandle(handle);
    }

    // getConstMappedRange() - not generated. See: Buffer_NG.cs

    public WGPUBufferMapState mapState => wgpuBufferGetMapState(Handle);

    // getMappedRange() - not generated. See: Buffer_NG.cs

    public ulong size => wgpuBufferGetSize(Handle);

    public WGPUBufferUsage usage => wgpuBufferGetUsage(Handle);

    public void mapAsync(WGPUMapMode mode, ulong offset, ulong size, delegate* unmanaged<WGPUBufferMapAsyncStatus, void*, void> callback, void* userdata) {
        Validate_mapAsync(Handle, mode, offset, size, callback, userdata);
        wgpuBufferMapAsync(Handle, mode, offset, size, callback, userdata);
    }

    [Conditional("VALIDATE")]
    private static void Validate_mapAsync(IntPtr handle, WGPUMapMode mode, ulong offset, ulong size, delegate* unmanaged<WGPUBufferMapAsyncStatus, void*, void> callback, void* userdata) {
        ObjectTracker.ValidateHandle(handle);
    }


    public void unmap() {
        Validate_unmap(Handle);
        wgpuBufferUnmap(Handle);
    }

    [Conditional("VALIDATE")]
    private static void Validate_unmap(IntPtr handle) {
        ObjectTracker.ValidateHandle(handle);
    }

    public void reference() {
        ObjectTracker.IncRef(Handle);
        wgpuBufferReference(Handle);
    }

    public void release() {
        ObjectTracker.DecRef(Handle);
        wgpuBufferRelease(Handle);
    }

    public override string? ToString() => ObjectTracker.GetLabel(Handle);
}
