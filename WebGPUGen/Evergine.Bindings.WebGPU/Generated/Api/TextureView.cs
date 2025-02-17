using System.Diagnostics;
using static Evergine.Bindings.WebGPU.WebGPUNative;

// ReSharper disable InconsistentNaming
namespace Evergine.Bindings.WebGPU;

/// <see href="https://developer.mozilla.org/en-US/docs/Web/API/GPUTextureView">MDN documentation</see>           
public unsafe partial struct WGPUTextureView
{

    public void reference() {
        ObjectTracker.IncRef(this);
        wgpuTextureViewReference(this);
    }

    public void release() {
        ObjectTracker.DecRef(this);
        wgpuTextureViewRelease(this);
    }
    
    public void Dispose() {
        ObjectTracker.DecRef(this);
        wgpuTextureViewRelease(this);
    }

    public override string? ToString() => ObjectTracker.GetLabel(Handle);
}
