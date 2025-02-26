using System.Diagnostics;
using static Evergine.Bindings.WebGPU.WebGPUNative;

// ReSharper disable InconsistentNaming
namespace Evergine.Bindings.WebGPU;

/// <see href="https://developer.mozilla.org/en-US/docs/Web/API/GPUBindGroup">MDN documentation</see>           
public unsafe partial struct WGPUBindGroup
{

    public void reference() {
        ObjectTracker.IncRef(this);
        wgpuBindGroupReference(this);
    }

    public void release() {
        ObjectTracker.DecRef(this);
        wgpuBindGroupRelease(this);
    }
    
    public void Dispose() {
        ObjectTracker.DecRef(this);
        wgpuBindGroupRelease(this);
    }

    public override string? ToString() => ObjectTracker.GetLabel(Handle);
}
