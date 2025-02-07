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
        descriptor.Validate();
        WGPUShaderModuleWGSLDescriptor wgslDescriptor = new()
        {
            chain = new WGPUChainedStruct()
            {
                _next = null,
                sType = WGPUSType.ShaderModuleWGSLDescriptor,
            },
            code = code,
        };
        descriptor._nextInChain = &wgslDescriptor.chain;
        var result = wgpuDeviceCreateShaderModule(Handle, &descriptor);
        ObjectTracker.CreateRef(result.Handle, HandleType.WGPUShaderModule, descriptor._label);
        return result;
    }
    
    public WGPUSupportedLimits getLimits() {
        WGPUSupportedLimits result;
        var success = wgpuDeviceGetLimits(Handle, &result);
        return result;
    }
}
