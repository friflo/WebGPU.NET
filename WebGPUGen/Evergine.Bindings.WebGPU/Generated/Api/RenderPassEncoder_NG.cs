namespace Evergine.Bindings.WebGPU;
using static WebGPUNative;
           
public unsafe partial struct WGPURenderPassEncoder
{
    public void setPushConstants<T>(WGPUShaderStage stages, uint offset, Span<T> data) where T : unmanaged {
        ObjectTracker.ValidateHandle(this);
        
        fixed (T* ptr = data) {
            wgpuRenderPassEncoderSetPushConstants(this, stages, offset, (uint)(data.Length * sizeof(T)), ptr);
        }
    }
    
    public void setVertexBuffer(uint slot, WGPUBuffer buffer, ulong offset = 0) {
        ObjectTracker.ValidateHandle(this);
        ObjectTracker.ValidateHandle(buffer);
        
        Validate_setVertexBuffer(slot, buffer, offset, 0);
        wgpuRenderPassEncoderSetVertexBuffer(this, slot, buffer, offset, buffer.size - offset);
    }
    
    public void setBindGroup(uint groupIndex, WGPUBindGroup group) {
        ObjectTracker.ValidateHandle(this);
        ObjectTracker.ValidateHandle(group);
        
        wgpuRenderPassEncoderSetBindGroup(this, groupIndex, group, 0, null);
    }
}
