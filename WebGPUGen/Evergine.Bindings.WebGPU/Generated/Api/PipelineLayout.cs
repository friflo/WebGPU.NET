using System.Diagnostics;
using static Evergine.Bindings.WebGPU.WebGPUNative;

// ReSharper disable InconsistentNaming
namespace Evergine.Bindings.WebGPU;

/// <see href="https://developer.mozilla.org/en-US/docs/Web/API/GPUPipelineLayout">MDN documentation</see>           
public unsafe partial struct WGPUPipelineLayout
{

    public void reference() {
        ObjectTracker.IncRef(Handle);
        wgpuPipelineLayoutReference(this);
    }

    public void release() {
        ObjectTracker.DecRef(Handle);
        wgpuPipelineLayoutRelease(this);
    }

    public override string? ToString() => ObjectTracker.GetLabel(Handle);
}
