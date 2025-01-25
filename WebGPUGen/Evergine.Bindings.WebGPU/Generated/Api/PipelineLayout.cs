namespace Evergine.Bindings.WebGPU;
using static WebGPUNative;
           
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
