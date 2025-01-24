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

    // submit() - not generated

    // writeBuffer() - not generated

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

    // submitForIndex() - not generated

}
