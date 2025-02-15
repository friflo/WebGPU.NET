namespace Evergine.Bindings.WebGPU;
using static WebGPUNative;

/// <see href="https://developer.mozilla.org/en-US/docs/Web/API/GPUDevice">MDN documentation</see>           
public unsafe partial struct WGPUDevice
{
    public WGPUCommandEncoder createCommandEncoder() {
        ObjectTracker.ValidateHandle(this);
        
        var result = wgpuDeviceCreateCommandEncoder(this, null);
        ObjectTracker.CreateRef(result, HandleType.WGPUCommandEncoder, Handle);
        return result;
    }

    public WGPUSampler createSampler() {
        ObjectTracker.ValidateHandle(this);
        
        var result = wgpuDeviceCreateSampler(this, null);
        ObjectTracker.CreateRef(result, HandleType.WGPUSampler, Handle);
        return result;
    }
    
    public WGPUShaderModule createShaderModuleWGSL(WGPUShaderModuleDescriptor descriptor, Utf8 code)
    {
        ObjectTracker.ValidateHandle(this);
        
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
        var result = wgpuDeviceCreateShaderModule(this, &descriptor);
        ObjectTracker.CreateRefLabel(result, HandleType.WGPUShaderModule, descriptor._label);
        return result;
    }
    
    public WGPUSupportedLimits getLimits() {
        ObjectTracker.ValidateHandle(this);
        
        WGPUSupportedLimits result;
        var success = wgpuDeviceGetLimits(this, &result);
        return result;
    }
    
    public WGPUQueue queue {
        get {
            ObjectTracker.ValidateHandle(this);
            
            var result = wgpuDeviceGetQueue(this);
            ObjectTracker.CreateRef(result, HandleType.WGPUQueue, Handle);
            return result;
        }
    }
}
