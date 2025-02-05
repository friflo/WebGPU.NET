namespace Evergine.Bindings.WebGPU;
using static WebGPUNative;

/// <see href="https://developer.mozilla.org/en-US/docs/Web/API/GPUDevice">MDN documentation</see>           
public unsafe partial struct WGPUDevice
{
    public WGPUCommandEncoder createCommandEncoder() {
        var result = wgpuDeviceCreateCommandEncoder(Handle, null);
        ObjectTracker.CreateRef(result.Handle, HandleType.WGPUCommandEncoder, null);
        return result;
    }

    public WGPUSampler createSampler() {
        var result = wgpuDeviceCreateSampler(Handle, null);
        ObjectTracker.CreateRef(result.Handle, HandleType.WGPUSampler, null);
        return result;
    }
}
