namespace Evergine.Bindings.WebGPU;
using static WebGPUNative;

/// <see href="https://developer.mozilla.org/en-US/docs/Web/API/GPUBindGroup">MDN documentation</see>           
public unsafe partial struct WGPUBindGroup
{
    public void setLabel(ReadOnlySpan<char> label) {
        wgpuBindGroupSetLabel(Handle, label.AllocString());
    }

    public void reference() {
        wgpuBindGroupReference(Handle);
        ObjectTracker.IncRef(Handle);
    }

    public void release() {
        ObjectTracker.DecRef(Handle);
        wgpuBindGroupRelease(Handle);
    }

}
