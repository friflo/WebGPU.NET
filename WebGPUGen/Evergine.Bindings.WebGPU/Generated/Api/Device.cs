namespace Evergine.Bindings.WebGPU;
using static WebGPUNative;
           
public unsafe partial struct WGPUDevice
{
    public delegate* unmanaged<void> cAddress(ReadOnlySpan<char> procName) {
        var result = wgpuGetProcAddress(Handle, procName.AllocString());
        return result;
    }

    public WGPUBindGroup createBindGroup(WGPUBindGroupDescriptor descriptor) {
        var result = wgpuDeviceCreateBindGroup(Handle, &descriptor);
        ObjectTracker.CreateRef(result.Handle, HandleType.WGPUBindGroup, descriptor.label);
        return result;
    }

    public WGPUBindGroupLayout createBindGroupLayout(WGPUBindGroupLayoutDescriptor descriptor) {
        var result = wgpuDeviceCreateBindGroupLayout(Handle, &descriptor);
        ObjectTracker.CreateRef(result.Handle, HandleType.WGPUBindGroupLayout, descriptor.label);
        return result;
    }

    public WGPUBuffer createBuffer(WGPUBufferDescriptor descriptor) {
        var result = wgpuDeviceCreateBuffer(Handle, &descriptor);
        ObjectTracker.CreateRef(result.Handle, HandleType.WGPUBuffer, descriptor.label);
        return result;
    }

    public WGPUCommandEncoder createCommandEncoder(WGPUCommandEncoderDescriptor descriptor) {
        var result = wgpuDeviceCreateCommandEncoder(Handle, &descriptor);
        ObjectTracker.CreateRef(result.Handle, HandleType.WGPUCommandEncoder, descriptor.label);
        return result;
    }

    public WGPUComputePipeline createComputePipeline(WGPUComputePipelineDescriptor descriptor) {
        var result = wgpuDeviceCreateComputePipeline(Handle, &descriptor);
        ObjectTracker.CreateRef(result.Handle, HandleType.WGPUComputePipeline, descriptor.label);
        return result;
    }

    public void createComputePipelineAsync(WGPUComputePipelineDescriptor descriptor, delegate* unmanaged<WGPUCreatePipelineAsyncStatus, WGPUComputePipeline, char*, void*, void> callback, void* userdata) {
        wgpuDeviceCreateComputePipelineAsync(Handle, &descriptor, callback, userdata);
    }

    public WGPUPipelineLayout createPipelineLayout(WGPUPipelineLayoutDescriptor descriptor) {
        var result = wgpuDeviceCreatePipelineLayout(Handle, &descriptor);
        ObjectTracker.CreateRef(result.Handle, HandleType.WGPUPipelineLayout, descriptor.label);
        return result;
    }

    public WGPUQuerySet createQuerySet(WGPUQuerySetDescriptor descriptor) {
        var result = wgpuDeviceCreateQuerySet(Handle, &descriptor);
        ObjectTracker.CreateRef(result.Handle, HandleType.WGPUQuerySet, descriptor.label);
        return result;
    }

    public WGPURenderBundleEncoder createRenderBundleEncoder(WGPURenderBundleEncoderDescriptor descriptor) {
        var result = wgpuDeviceCreateRenderBundleEncoder(Handle, &descriptor);
        ObjectTracker.CreateRef(result.Handle, HandleType.WGPURenderBundleEncoder, descriptor.label);
        return result;
    }

    public WGPURenderPipeline createRenderPipeline(WGPURenderPipelineDescriptor descriptor) {
        var result = wgpuDeviceCreateRenderPipeline(Handle, &descriptor);
        ObjectTracker.CreateRef(result.Handle, HandleType.WGPURenderPipeline, descriptor.label);
        return result;
    }

    public void createRenderPipelineAsync(WGPURenderPipelineDescriptor descriptor, delegate* unmanaged<WGPUCreatePipelineAsyncStatus, WGPURenderPipeline, char*, void*, void> callback, void* userdata) {
        wgpuDeviceCreateRenderPipelineAsync(Handle, &descriptor, callback, userdata);
    }

    public WGPUSampler createSampler(WGPUSamplerDescriptor descriptor) {
        var result = wgpuDeviceCreateSampler(Handle, &descriptor);
        ObjectTracker.CreateRef(result.Handle, HandleType.WGPUSampler, descriptor.label);
        return result;
    }

    public WGPUShaderModule createShaderModule(WGPUShaderModuleDescriptor descriptor) {
        var result = wgpuDeviceCreateShaderModule(Handle, &descriptor);
        ObjectTracker.CreateRef(result.Handle, HandleType.WGPUShaderModule, descriptor.label);
        return result;
    }

    public WGPUTexture createTexture(WGPUTextureDescriptor descriptor) {
        var result = wgpuDeviceCreateTexture(Handle, &descriptor);
        ObjectTracker.CreateRef(result.Handle, HandleType.WGPUTexture, descriptor.label);
        return result;
    }

    public void destroy() {
        wgpuDeviceDestroy(Handle);
    }

    public ulong enumerateFeatures(WGPUFeatureName features) {
        var result = wgpuDeviceEnumerateFeatures(Handle, &features);
        return result;
    }

    public WGPUBool getLimits(WGPUSupportedLimits limits) {
        var result = wgpuDeviceGetLimits(Handle, &limits);
        return result;
    }

    public WGPUQueue queue => wgpuDeviceGetQueue(Handle);

    public WGPUBool hasFeature(WGPUFeatureName feature) {
        var result = wgpuDeviceHasFeature(Handle, feature);
        return result;
    }



    public void setLabel(ReadOnlySpan<char> label) {
        wgpuDeviceSetLabel(Handle, label.AllocString());
    }

    public void reference() {
        wgpuDeviceReference(Handle);
        ObjectTracker.IncRef(Handle);
    }

    public void release() {
        ObjectTracker.DecRef(Handle);
        wgpuDeviceRelease(Handle);
    }

    public WGPUBool poll(WGPUBool wait, WGPUWrappedSubmissionIndex wrappedSubmissionIndex) {
        var result = wgpuDevicePoll(Handle, wait, &wrappedSubmissionIndex);
        return result;
    }

}
