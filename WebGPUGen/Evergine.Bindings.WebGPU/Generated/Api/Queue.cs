using static Evergine.Bindings.WebGPU.WebGPUNative;

// ReSharper disable InconsistentNaming
namespace Evergine.Bindings.WebGPU;

/// <see href="https://developer.mozilla.org/en-US/docs/Web/API/GPUQueue">MDN documentation</see>           
public unsafe partial struct WGPUQueue
{
    public void onSubmittedWorkDone(delegate* unmanaged<WGPUQueueWorkDoneStatus, void*, void> callback, void* userdata) {
        Validate_onSubmittedWorkDone(Handle, callback, userdata);
        wgpuQueueOnSubmittedWorkDone(Handle, callback, userdata);
    }

    private static void Validate_onSubmittedWorkDone(IntPtr handle, delegate* unmanaged<WGPUQueueWorkDoneStatus, void*, void> callback, void* userdata) {
        ObjectTracker.ValidateHandle(handle);
    }

    public void setLabel(Utf8 label) {
        Validate_setLabel(Handle, label);
        wgpuQueueSetLabel(Handle, label.AllocUtf8());
    }

    private static void Validate_setLabel(IntPtr handle, Utf8 label) {
        ObjectTracker.ValidateHandle(handle);
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
