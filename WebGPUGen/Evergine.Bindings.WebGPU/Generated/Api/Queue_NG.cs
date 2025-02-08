using System.Diagnostics;

namespace Evergine.Bindings.WebGPU;
using static WebGPUNative;
           
public unsafe partial struct WGPUQueue
{
    public void submit(Span<WGPUCommandBuffer> commands) {
        Validate_submit(Handle, commands);
        fixed (WGPUCommandBuffer* ptr = commands) {
            wgpuQueueSubmit(Handle, (ulong)commands.Length, ptr);
        }
    }
    
    [Conditional("VALIDATE")]
    private static void Validate_submit(IntPtr handle, Span<WGPUCommandBuffer> commands) {
        ObjectTracker.ValidateHandle(handle);
        foreach (var command in commands) {
            ObjectTracker.ValidateHandle(command.Handle);
        }
    }

    public void writeBuffer<T>(WGPUBuffer buffer, ulong bufferOffset, Span<T> data) where T : unmanaged {
        fixed (T* ptr = data) {
            wgpuQueueWriteBuffer(Handle, buffer, bufferOffset, ptr, (ulong)(data.Length * sizeof(T)));
        }
    }
    
    public void writeTexture<T>(WGPUImageCopyTexture destination, Span<T> data, WGPUTextureDataLayout dataLayout, WGPUExtent3D writeSize) where T : unmanaged {
        fixed (T* ptr = data) {
            wgpuQueueWriteTexture(Handle, &destination, ptr, (ulong)(data.Length * sizeof(T)), &dataLayout, &writeSize);
        }
    }

    public ulong submitForIndex(Span<WGPUCommandBuffer> commands) {
        fixed (WGPUCommandBuffer* ptr = commands) {
            return wgpuQueueSubmitForIndex(Handle, (ulong)commands.Length, ptr);
        }
    }

}
