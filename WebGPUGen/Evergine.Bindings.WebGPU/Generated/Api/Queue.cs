namespace Evergine.Bindings.WebGPU;
using static WebGPUNative;
           
public static unsafe partial class WebGPUExtensions
{
    public static void onSubmittedWorkDone(this WGPUQueue queue, delegate* unmanaged<WGPUQueueWorkDoneStatus, void*, void> callback, void* userdata) {
        wgpuQueueOnSubmittedWorkDone(queue, callback, userdata);
    }

    public static void setLabel(this WGPUQueue queue, ReadOnlySpan<char> label) {
        wgpuQueueSetLabel(queue, label.AllocString());
    }

    public static void submit(this WGPUQueue queue, Span<WGPUCommandBuffer> commands) {
        wgpuQueueSubmit(queue, (ulong)commands.Length, commands.GetArrPtr());
    }

    public static void writeBuffer<T>(this WGPUQueue queue, WGPUBuffer buffer, ulong bufferOffset, Span<T> data) {
        var ptr = System.Runtime.CompilerServices.Unsafe.AsPointer(ref data.GetPinnableReference());
        wgpuQueueWriteBuffer(queue, buffer, bufferOffset, ptr, (ulong)(data.Length * sizeof(T)));
    }

    public static void writeTexture(this WGPUQueue queue, WGPUImageCopyTexture destination, void* data, ulong dataSize, WGPUTextureDataLayout dataLayout, WGPUExtent3D writeSize) {
        wgpuQueueWriteTexture(queue, &destination, data, dataSize, &dataLayout, &writeSize);
    }

    public static void reference(this WGPUQueue queue) {
        wgpuQueueReference(queue);
        ObjectTracker.IncRef(queue.Handle);
    }

    public static void release(this WGPUQueue queue) {
        ObjectTracker.DecRef(queue.Handle);
        wgpuQueueRelease(queue);
    }

    public static ulong submitForIndex(this WGPUQueue queue, Span<WGPUCommandBuffer> commands) {
        return wgpuQueueSubmitForIndex(queue, (ulong)commands.Length, commands.GetArrPtr());
    }

}
