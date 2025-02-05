namespace Evergine.Bindings.WebGPU;
using static WebGPUNative;

/// <see href="https://developer.mozilla.org/en-US/docs/Web/API/GPUCommandEncoder">MDN documentation</see>           
public unsafe partial struct WGPUCommandEncoder
{
    public WGPUComputePassEncoder beginComputePass() {
        var result = wgpuCommandEncoderBeginComputePass(Handle, null);
        ObjectTracker.CreateRef(result.Handle, HandleType.WGPUComputePassEncoder, null);
        return result;
    }
}
