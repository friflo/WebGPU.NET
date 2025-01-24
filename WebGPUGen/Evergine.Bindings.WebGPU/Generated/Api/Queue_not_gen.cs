namespace Evergine.Bindings.WebGPU;
using static WebGPUNative;
           
public static unsafe partial class WebGPUExtensions
{
    public static void submit(this WGPUQueue queue, Span<WGPUCommandBuffer> commands) {
        fixed (WGPUCommandBuffer* ptr = commands) {
            wgpuQueueSubmit(queue, (ulong)commands.Length, ptr);
        }
    }

    public static void writeBuffer<T>(this WGPUQueue queue, WGPUBuffer buffer, ulong bufferOffset, ReadOnlySpan<T> data) where T : unmanaged {
        fixed (T* ptr = data) {
            wgpuQueueWriteBuffer(queue, buffer, bufferOffset, ptr, (ulong)(data.Length * sizeof(T)));
        }
    }

    public static ulong submitForIndex(this WGPUQueue queue, Span<WGPUCommandBuffer> commands) {
        fixed (WGPUCommandBuffer* ptr = commands) {
            return wgpuQueueSubmitForIndex(queue, (ulong)commands.Length, ptr);
        }
    }

}
