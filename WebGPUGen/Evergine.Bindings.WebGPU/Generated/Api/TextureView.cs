using System.Diagnostics;
using static Evergine.Bindings.WebGPU.WebGPUNative;

// ReSharper disable InconsistentNaming
namespace Evergine.Bindings.WebGPU;

/// <see href="https://developer.mozilla.org/en-US/docs/Web/API/GPUTextureView">MDN documentation</see>           
public unsafe partial struct WGPUTextureView
{

    public void reference() {
        ObjectTracker.IncRef(Handle);
        wgpuTextureViewReference(Handle);
    }

    public void release() {
        ObjectTracker.DecRef(Handle);
        wgpuTextureViewRelease(Handle);
    }

    public override string? ToString() => ObjectTracker.GetLabel(Handle);
}
