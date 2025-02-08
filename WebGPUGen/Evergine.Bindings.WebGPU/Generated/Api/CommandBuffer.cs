using System.Diagnostics;
using static Evergine.Bindings.WebGPU.WebGPUNative;

// ReSharper disable InconsistentNaming
namespace Evergine.Bindings.WebGPU;

/// <see href="https://developer.mozilla.org/en-US/docs/Web/API/GPUCommandBuffer">MDN documentation</see>           
public unsafe partial struct WGPUCommandBuffer
{

    public void reference() {
        ObjectTracker.IncRef(Handle);
        wgpuCommandBufferReference(Handle);
    }

    public void release() {
        ObjectTracker.DecRef(Handle);
        wgpuCommandBufferRelease(Handle);
    }

    public override string? ToString() => ObjectTracker.GetLabel(Handle);
}
