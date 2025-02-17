using System.Diagnostics;

namespace Evergine.Bindings.WebGPU;
using static WebGPUNative;
           
public unsafe partial struct WGPUQueue
{
    public void submit(Span<WGPUCommandBuffer> commands) {
        Validate_submit(Handle, commands);
        
        fixed (WGPUCommandBuffer* ptr = commands) {
            wgpuQueueSubmit(this, (ulong)commands.Length, ptr);
        }
        WGPUException.ThrowOnError();
    }
    
    [Conditional("VALIDATE")]
    private void Validate_submit(IntPtr handle, Span<WGPUCommandBuffer> commands) {
        ObjectTracker.ValidateHandle(this);
        
        foreach (var command in commands) {
            ObjectTracker.ValidateHandle(command);
        }
    }

    public void writeBuffer<T>(WGPUBuffer buffer, ulong bufferOffset, Span<T> data) where T : unmanaged {
        ObjectTracker.ValidateHandle(this);
        ObjectTracker.ValidateHandle(buffer);
        
        fixed (T* ptr = data) {
            wgpuQueueWriteBuffer(this, buffer, bufferOffset, ptr, (ulong)(data.Length * sizeof(T)));
        }
        WGPUException.ThrowOnError();
    }
    
    public void writeBuffer<T>(WGPUBuffer buffer, ulong bufferOffset, T data) where T : unmanaged {
        ObjectTracker.ValidateHandle(this);
        ObjectTracker.ValidateHandle(buffer);
        
        wgpuQueueWriteBuffer(this, buffer, bufferOffset, &data, (ulong)sizeof(T));
        WGPUException.ThrowOnError();
    }
    
    public void writeTexture<T>(WGPUImageCopyTexture destination, Span<T> data, WGPUTextureDataLayout dataLayout, WGPUExtent3D writeSize) where T : unmanaged {
        ObjectTracker.ValidateHandle(this);
        destination.Validate();
        
        fixed (T* ptr = data) {
            wgpuQueueWriteTexture(this, &destination, ptr, (ulong)(data.Length * sizeof(T)), &dataLayout, &writeSize);
        }
        WGPUException.ThrowOnError();
    }

    public ulong submitForIndex(Span<WGPUCommandBuffer> commands) {
        Validate_submitForIndex(commands);
        
        fixed (WGPUCommandBuffer* ptr = commands) {
            return wgpuQueueSubmitForIndex(this, (ulong)commands.Length, ptr);
        }
        WGPUException.ThrowOnError();
    }
    
    [Conditional("VALIDATE")]
    private void Validate_submitForIndex(Span<WGPUCommandBuffer> commands) {
        ObjectTracker.ValidateHandle(this);
        foreach (var command in commands) {
            ObjectTracker.ValidateHandle(command);
        }
    }

}
