using System.Diagnostics;
using static Evergine.Bindings.WebGPU.WebGPUNative;

// ReSharper disable InconsistentNaming
namespace Evergine.Bindings.WebGPU;

/// <see href="https://developer.mozilla.org/en-US/docs/Web/API/GPUBindGroupLayout">MDN documentation</see>           
public unsafe partial struct WGPUBindGroupLayout
{

    public void reference() {
        ObjectTracker.IncRef(this);
        wgpuBindGroupLayoutReference(this);
    }

    public void release() {
        ObjectTracker.DecRef(this);
        wgpuBindGroupLayoutRelease(this);
    }

    public override string? ToString() => ObjectTracker.GetLabel(Handle);
}
