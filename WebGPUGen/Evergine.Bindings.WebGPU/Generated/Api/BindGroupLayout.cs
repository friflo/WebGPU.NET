namespace Evergine.Bindings.WebGPU;
using static WebGPUNative;
           
public unsafe partial struct WGPUBindGroupLayout
{
    public void setLabel(ReadOnlySpan<char> label) {
        wgpuBindGroupLayoutSetLabel(Handle, label.AllocString());
    }

    public void reference() {
        wgpuBindGroupLayoutReference(Handle);
        ObjectTracker.IncRef(Handle);
    }

    public void release() {
        ObjectTracker.DecRef(Handle);
        wgpuBindGroupLayoutRelease(Handle);
    }

}
