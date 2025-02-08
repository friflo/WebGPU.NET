using System.Diagnostics;
using static Evergine.Bindings.WebGPU.WebGPUNative;

// ReSharper disable InconsistentNaming
namespace Evergine.Bindings.WebGPU;

/// <see href="https://developer.mozilla.org/en-US/docs/Web/API/GPUSampler">MDN documentation</see>           
public unsafe partial struct WGPUSampler
{

    public void reference() {
        ObjectTracker.IncRef(Handle);
        wgpuSamplerReference(this);
    }

    public void release() {
        ObjectTracker.DecRef(Handle);
        wgpuSamplerRelease(this);
    }

    public override string? ToString() => ObjectTracker.GetLabel(Handle);
}
