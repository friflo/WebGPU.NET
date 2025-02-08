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
    }

    [Conditional("VALIDATE")]
    private void Validate_destroy() {
        ObjectTracker.ValidateHandle(this);
    }

    public uint count => wgpuQuerySetGetCount(this);

    public WGPUQueryType type => wgpuQuerySetGetType(this);


    public void reference() {
        ObjectTracker.IncRef(this);
        wgpuQuerySetReference(this);
    }

    public void release() {
        ObjectTracker.DecRef(this);
        wgpuQuerySetRelease(this);
    }

    public override string? ToString() => ObjectTracker.GetLabel(Handle);
}
