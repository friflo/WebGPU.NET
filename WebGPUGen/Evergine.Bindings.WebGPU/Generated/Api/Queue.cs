namespace Evergine.Bindings.WebGPU;
using static WebGPUNative;

/// <see href="https://developer.mozilla.org/en-US/docs/Web/API/GPUQueue">MDN documentation</see>           
public unsafe partial struct WGPUQueue
{
    public void onSubmittedWorkDone(delegate* unmanaged<WGPUQueueWorkDoneStatus, void*, void> callback, void* userdata) {
        wgpuQueueOnSubmittedWorkDone(Handle, callback, userdata);
    }

    public void setLabel(Utf8 label) {
        wgpuQueueSetLabel(Handle, label.AllocUtf8());
    }

    // submit() - not generated. See: Queue_NG.cs

    // writeBuffer() - not generated. See: Queue_NG.cs

    // writeTexture() - not generated. See: Queue_NG.cs

    public void reference() {
        ObjectTracker.IncRef(Handle);
        wgpuQueueReference(Handle);
    }

    public void release() {
        ObjectTracker.DecRef(Handle);
        wgpuQueueRelease(Handle);
    }

    // submitForIndex() - not generated. See: Queue_NG.cs

    public override string? ToString() => ObjectTracker.GetLabel(Handle);
}
