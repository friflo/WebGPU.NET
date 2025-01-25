namespace Evergine.Bindings.WebGPU;
using static WebGPUNative;
           
public unsafe partial struct WGPUBuffer
{
    public void destroy() {
        wgpuBufferDestroy(Handle);
    }

    public void* getConstMappedRange(ulong offset, ulong size) {
        var result = wgpuBufferGetConstMappedRange(Handle, offset, size);
        return result;
    }

    public WGPUBufferMapState mapState => wgpuBufferGetMapState(Handle);

    public void* getMappedRange(ulong offset, ulong size) {
        var result = wgpuBufferGetMappedRange(Handle, offset, size);
        return result;
    }

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
