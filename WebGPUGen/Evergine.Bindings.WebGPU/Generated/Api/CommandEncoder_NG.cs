namespace Evergine.Bindings.WebGPU;
using static WebGPUNative;

/// <see href="https://developer.mozilla.org/en-US/docs/Web/API/GPUCommandEncoder">MDN documentation</see>           
public unsafe partial struct WGPUCommandEncoder
{
    public WGPUComputePassEncoder beginComputePass() {
        ObjectTracker.ValidateHandle(this);
        
        var result = wgpuCommandEncoderBeginComputePass(this, null);
        ObjectTracker.CreateRef(result, HandleType.WGPUComputePassEncoder, Handle);
        return result;
    }
    
    public WGPUCommandBuffer finish() {
        ObjectTracker.ValidateHandle(this);
        
        var result = wgpuCommandEncoderFinish(this, null);
        ObjectTracker.CreateRef(result, HandleType.WGPUCommandBuffer, Handle);
        return result;
    }
}
