namespace Evergine.Bindings.WebGPU;
using static WebGPUNative;
           
public static unsafe partial class WebGPUExtensions
{
    public static void setLabel(this WGPUBindGroup bindGroup, ReadOnlySpan<char> label) {
        wgpuBindGroupSetLabel(bindGroup, label.AllocString());
    }

    public static void reference(this WGPUBindGroup bindGroup) {
        wgpuBindGroupReference(bindGroup);
        ObjectTracker.IncRef(bindGroup.Handle);
    }

    public static void release(this WGPUBindGroup bindGroup) {
        ObjectTracker.DecRef(bindGroup.Handle);
        wgpuBindGroupRelease(bindGroup);
    }

}
