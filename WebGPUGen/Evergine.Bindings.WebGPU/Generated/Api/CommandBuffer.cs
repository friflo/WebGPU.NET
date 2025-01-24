namespace Evergine.Bindings.WebGPU;
using static WebGPUNative;
           
public static unsafe partial class WebGPUExtensions
{
    public static void setLabel(this WGPUCommandBuffer commandBuffer, ReadOnlySpan<char> label) {
        wgpuCommandBufferSetLabel(commandBuffer, label.AllocString());
    }

    public static void reference(this WGPUCommandBuffer commandBuffer) {
        wgpuCommandBufferReference(commandBuffer);
        ObjectTracker.IncRef(commandBuffer.Handle);
    }

    public static void release(this WGPUCommandBuffer commandBuffer) {
        ObjectTracker.DecRef(commandBuffer.Handle);
        wgpuCommandBufferRelease(commandBuffer);
    }

}
