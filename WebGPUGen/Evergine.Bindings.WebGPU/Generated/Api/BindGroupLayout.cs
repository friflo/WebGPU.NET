namespace Evergine.Bindings.WebGPU;
using static WebGPUNative;
           
public static unsafe partial class WebGPUExtensions
{
    public static void setLabel(this WGPUBindGroupLayout bindGroupLayout, ReadOnlySpan<char> label) {
        wgpuBindGroupLayoutSetLabel(bindGroupLayout, label.AllocString());
    }

    public static void reference(this WGPUBindGroupLayout bindGroupLayout) {
        wgpuBindGroupLayoutReference(bindGroupLayout);
        ObjectTracker.IncRef(bindGroupLayout.Handle);
    }

    public static void release(this WGPUBindGroupLayout bindGroupLayout) {
        ObjectTracker.DecRef(bindGroupLayout.Handle);
        wgpuBindGroupLayoutRelease(bindGroupLayout);
    }

}
