using System.Diagnostics;
using static Evergine.Bindings.WebGPU.WebGPUNative;

// ReSharper disable InconsistentNaming
namespace Evergine.Bindings.WebGPU;

/// <see href="https://developer.mozilla.org/en-US/docs/Web/API/GPUQuerySet">MDN documentation</see>           
public unsafe partial struct WGPUQuerySet
{
    public void destroy() {
        Validate_destroy();
        wgpuQuerySetDestroy(this);
        WGPUException.ThrowOnError();
    }

    [Conditional("VALIDATE")]
    private void Validate_destroy() {
        ObjectTracker.ValidateHandle(this);
    }

    public uint count { get {
          ObjectTracker.ValidateHandle(this);
          return wgpuQuerySetGetCount(this);
    } }

    public WGPUQueryType type { get {
          ObjectTracker.ValidateHandle(this);
          return wgpuQuerySetGetType(this);
    } }


    public void reference() {
        ObjectTracker.IncRef(this);
        wgpuQuerySetReference(this);
    }

    public void release() {
        ObjectTracker.DecRef(this);
        wgpuQuerySetRelease(this);
    }
    
    public void Dispose() {
        ObjectTracker.DecRef(this);
        wgpuQuerySetRelease(this);
    }

    public override string? ToString() => ObjectTracker.GetLabel(Handle);
}
