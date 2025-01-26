namespace Evergine.Bindings.WebGPU;
using static WebGPUNative;

/// <see href="https://developer.mozilla.org/en-US/docs/Web/API/GPUBuffer">MDN documentation</see>           
public unsafe partial struct WGPUBuffer
{
    public void destroy() {
        wgpuBufferDestroy(Handle);
    }

    // getConstMappedRange() - not generated. See: Buffer_NG.cs

    public WGPUBufferMapState mapState => wgpuBufferGetMapState(Handle);

    // getMappedRange() - not generated. See: Buffer_NG.cs

    public ulong size => wgpuBufferGetSize(Handle);

    public WGPUBufferUsage usage => wgpuBufferGetUsage(Handle);

    public void mapAsync(WGPUMapMode mode, ulong offset, ulong size, delegate* unmanaged<WGPUBufferMapAsyncStatus, void*, void> callback, void* userdata) {
        wgpuBufferMapAsync(Handle, mode, offset, size, callback, userdata);
    }

    public void setLabel(ReadOnlySpan<char> label) {
        wgpuBufferSetLabel(Handle, label.AllocString());
    }

    public void unmap() {
        wgpuBufferUnmap(Handle);
    }

    public void reference() {
        wgpuBufferReference(Handle);
        ObjectTracker.IncRef(Handle);
    }

    public void release() {
        ObjectTracker.DecRef(Handle);
        wgpuBufferRelease(Handle);
    }

}
