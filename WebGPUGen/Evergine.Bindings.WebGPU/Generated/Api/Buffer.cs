namespace Evergine.Bindings.WebGPU;
using static WebGPUNative;
           
public static unsafe partial class WebGPUExtensions
{
    public static void destroy(this WGPUBuffer buffer) {
        wgpuBufferDestroy(buffer);
    }

    public static void* getConstMappedRange(this WGPUBuffer buffer, ulong offset, ulong size) {
        var result = wgpuBufferGetConstMappedRange(buffer, offset, size);
        return result;
    }

    public static WGPUBufferMapState getMapState(this WGPUBuffer buffer) {
        var result = wgpuBufferGetMapState(buffer);
        return result;
    }

    public static void* getMappedRange(this WGPUBuffer buffer, ulong offset, ulong size) {
        var result = wgpuBufferGetMappedRange(buffer, offset, size);
        return result;
    }

    public static ulong getSize(this WGPUBuffer buffer) {
        var result = wgpuBufferGetSize(buffer);
        return result;
    }

    public static WGPUBufferUsage getUsage(this WGPUBuffer buffer) {
        var result = wgpuBufferGetUsage(buffer);
        return result;
    }

    public static void mapAsync(this WGPUBuffer buffer, WGPUMapMode mode, ulong offset, ulong size, delegate* unmanaged<WGPUBufferMapAsyncStatus, void*, void> callback, void* userdata) {
        wgpuBufferMapAsync(buffer, mode, offset, size, callback, userdata);
    }

    public static void setLabel(this WGPUBuffer buffer, ReadOnlySpan<char> label) {
        wgpuBufferSetLabel(buffer, label.AllocString());
    }

    public static void unmap(this WGPUBuffer buffer) {
        wgpuBufferUnmap(buffer);
    }

    public static void reference(this WGPUBuffer buffer) {
        wgpuBufferReference(buffer);
        ObjectTracker.IncRef(buffer.Handle);
    }

    public static void release(this WGPUBuffer buffer) {
        ObjectTracker.DecRef(buffer.Handle);
        wgpuBufferRelease(buffer);
    }

}
