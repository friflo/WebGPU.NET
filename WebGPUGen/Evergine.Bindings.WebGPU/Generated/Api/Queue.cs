using System.Diagnostics;
using static Evergine.Bindings.WebGPU.WebGPUNative;

// ReSharper disable InconsistentNaming
namespace Evergine.Bindings.WebGPU;

/// <see href="https://developer.mozilla.org/en-US/docs/Web/API/GPUQueue">MDN documentation</see>           
public unsafe partial struct WGPUQueue
{
    public void onSubmittedWorkDone(delegate* unmanaged<WGPUQueueWorkDoneStatus, void*, void> callback, void* userdata) {
        Validate_onSubmittedWorkDone(Handle, callback, userdata);
        wgpuQueueOnSubmittedWorkDone(this, callback, userdata);
    }

    [Conditional("VALIDATE")]
    private static void Validate_onSubmittedWorkDone(IntPtr handle, delegate* unmanaged<WGPUQueueWorkDoneStatus, void*, void> callback, void* userdata) {
        ObjectTracker.ValidateHandle(handle);
    }


    // submit() - not generated. See: Queue_NG.cs

    // writeBuffer() - not generated. See: Queue_NG.cs

    // writeTexture() - not generated. See: Queue_NG.cs

    public void reference() {
        ObjectTracker.IncRef(this);
        wgpuQueueReference(this);
    }

    public void release() {
        ObjectTracker.DecRef(this);
        wgpuQueueRelease(this);
    }

    // submitForIndex() - not generated. See: Queue_NG.cs

    public override string? ToString() => ObjectTracker.GetLabel(Handle);
}
