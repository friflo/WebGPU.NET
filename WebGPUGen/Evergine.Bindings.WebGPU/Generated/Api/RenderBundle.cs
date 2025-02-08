using System.Diagnostics;
using static Evergine.Bindings.WebGPU.WebGPUNative;

// ReSharper disable InconsistentNaming
namespace Evergine.Bindings.WebGPU;

/// <see href="https://developer.mozilla.org/en-US/docs/Web/API/GPURenderBundle">MDN documentation</see>           
public unsafe partial struct WGPURenderBundle
{

    public void reference() {
        ObjectTracker.IncRef(this);
        wgpuRenderBundleReference(this);
    }

    public void release() {
        ObjectTracker.DecRef(this);
        wgpuRenderBundleRelease(this);
    }

    public override string? ToString() => ObjectTracker.GetLabel(Handle);
}
