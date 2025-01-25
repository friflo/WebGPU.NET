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

    public WGPUBufferMapState getMapState() {
        var result = wgpuBufferGetMapState(Handle);
        return result;
    }

    public void* getMappedRange(ulong offset, ulong size) {
        var result = wgpuBufferGetMappedRange(Handle, offset, size);
        return result;
    }

    public ulong getSize() {
        var result = wgpuBufferGetSize(Handle);
        return result;
    }

    public WGPUBufferUsage getUsage() {
        var result = wgpuBufferGetUsage(Handle);
        return result;
    }

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
