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
    
    public WGPUShaderModule createShaderModuleWGSL(WGPUShaderModuleDescriptor descriptor, Utf8 code)
    {
        WGPUShaderModuleWGSLDescriptor wgslDescriptor = new()
        {
            chain = new WGPUChainedStruct()
            {
                next = null,
                sType = WGPUSType.ShaderModuleWGSLDescriptor,
            },
            Code = code,
        };
        descriptor.nextInChain = &wgslDescriptor.chain;
        var result = wgpuDeviceCreateShaderModule(Handle, &descriptor);
        ObjectTracker.CreateRef(result.Handle, HandleType.WGPUShaderModule, descriptor.label);
        return result;
    }
    
    public WGPUSupportedLimits getLimits() {
        WGPUSupportedLimits result;
        var success = wgpuDeviceGetLimits(Handle, &result);
        return result;
    }
}
