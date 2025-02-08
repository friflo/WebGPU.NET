using System.Diagnostics;
using static Evergine.Bindings.WebGPU.WebGPUNative;

// ReSharper disable InconsistentNaming
namespace Evergine.Bindings.WebGPU;

/// <see href="https://developer.mozilla.org/en-US/docs/Web/API/GPUBindGroupLayout">MDN documentation</see>           
public unsafe partial struct WGPUBindGroupLayout
{

    public void reference() {
        ObjectTracker.IncRef(Handle);
        wgpuBindGroupLayoutReference(this);
    }

    public void release() {
        ObjectTracker.DecRef(Handle);
        wgpuBindGroupLayoutRelease(this);
    }

    public override string? ToString() => ObjectTracker.GetLabel(Handle);
}
