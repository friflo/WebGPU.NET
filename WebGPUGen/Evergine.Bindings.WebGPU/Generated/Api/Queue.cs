namespace Evergine.Bindings.WebGPU;
using static WebGPUNative;
           
public unsafe partial struct WGPUQueue
{
    public void onSubmittedWorkDone(delegate* unmanaged<WGPUQueueWorkDoneStatus, void*, void> callback, void* userdata) {
        wgpuQueueOnSubmittedWorkDone(Handle, callback, userdata);
    }

    public void setLabel(ReadOnlySpan<char> label) {
        wgpuQueueSetLabel(Handle, label.AllocString());
    }

    // submit() - not generated

    // writeBuffer() - not generated

    public void writeTexture(WGPUImageCopyTexture destination, void* data, ulong dataSize, WGPUTextureDataLayout dataLayout, WGPUExtent3D writeSize) {
        wgpuQueueWriteTexture(Handle, &destination, data, dataSize, &dataLayout, &writeSize);
    }

    public void reference() {
        wgpuQueueReference(Handle);
        ObjectTracker.IncRef(Handle);
    }

    public void release() {
        ObjectTracker.DecRef(Handle);
        wgpuQueueRelease(Handle);
    }

    // submitForIndex() - not generated

}
