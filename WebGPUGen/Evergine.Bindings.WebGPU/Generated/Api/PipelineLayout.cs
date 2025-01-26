namespace Evergine.Bindings.WebGPU;
using static WebGPUNative;

/// <see href="https://developer.mozilla.org/en-US/docs/Web/API/GPUPipelineLayout">MDN documentation</see>           
public unsafe partial struct WGPUPipelineLayout
{
    public void setLabel(ReadOnlySpan<char> label) {
        wgpuPipelineLayoutSetLabel(Handle, label.AllocString());
    }

    public void reference() {
        wgpuPipelineLayoutReference(Handle);
        ObjectTracker.IncRef(Handle);
    }

    public void release() {
        ObjectTracker.DecRef(Handle);
        wgpuPipelineLayoutRelease(Handle);
    }

}
