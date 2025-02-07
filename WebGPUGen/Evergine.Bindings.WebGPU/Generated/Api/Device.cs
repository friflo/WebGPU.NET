using static Evergine.Bindings.WebGPU.WebGPUNative;

// ReSharper disable InconsistentNaming
namespace Evergine.Bindings.WebGPU;

/// <see href="https://developer.mozilla.org/en-US/docs/Web/API/GPUDevice">MDN documentation</see>           
public unsafe partial struct WGPUDevice
{
    public delegate* unmanaged<void> cAddress(Utf8 procName) {
        var result = wgpuGetProcAddress(Handle, procName.AllocUtf8());
        return result;
    }

    public WGPUBindGroup createBindGroup(WGPUBindGroupDescriptor descriptor) {
        descriptor.Validate();
        var result = wgpuDeviceCreateBindGroup(Handle, &descriptor);
        ObjectTracker.CreateRef(result.Handle, HandleType.WGPUBindGroup, descriptor._label);
        return result;
    }

    public WGPUBindGroupLayout createBindGroupLayout(WGPUBindGroupLayoutDescriptor descriptor) {
        descriptor.Validate();
        var result = wgpuDeviceCreateBindGroupLayout(Handle, &descriptor);
        ObjectTracker.CreateRef(result.Handle, HandleType.WGPUBindGroupLayout, descriptor._label);
        return result;
    }

    public WGPUBuffer createBuffer(WGPUBufferDescriptor descriptor) {
        descriptor.Validate();
        var result = wgpuDeviceCreateBuffer(Handle, &descriptor);
        ObjectTracker.CreateRef(result.Handle, HandleType.WGPUBuffer, descriptor._label);
        return result;
    }

    public WGPUCommandEncoder createCommandEncoder(WGPUCommandEncoderDescriptor descriptor) {
        descriptor.Validate();
        var result = wgpuDeviceCreateCommandEncoder(Handle, &descriptor);
        ObjectTracker.CreateRef(result.Handle, HandleType.WGPUCommandEncoder, descriptor._label);
        return result;
    }

    public WGPUComputePipeline createComputePipeline(WGPUComputePipelineDescriptor descriptor) {
        descriptor.Validate();
        var result = wgpuDeviceCreateComputePipeline(Handle, &descriptor);
        ObjectTracker.CreateRef(result.Handle, HandleType.WGPUComputePipeline, descriptor._label);
        return result;
    }

    public void createComputePipelineAsync(WGPUComputePipelineDescriptor descriptor, delegate* unmanaged<WGPUCreatePipelineAsyncStatus, WGPUComputePipeline, char*, void*, void> callback, void* userdata) {
        descriptor.Validate();
        wgpuDeviceCreateComputePipelineAsync(Handle, &descriptor, callback, userdata);
    }

    public WGPUPipelineLayout createPipelineLayout(WGPUPipelineLayoutDescriptor descriptor) {
        descriptor.Validate();
        var result = wgpuDeviceCreatePipelineLayout(Handle, &descriptor);
        ObjectTracker.CreateRef(result.Handle, HandleType.WGPUPipelineLayout, descriptor._label);
        return result;
    }

    public WGPUQuerySet createQuerySet(WGPUQuerySetDescriptor descriptor) {
        descriptor.Validate();
        var result = wgpuDeviceCreateQuerySet(Handle, &descriptor);
        ObjectTracker.CreateRef(result.Handle, HandleType.WGPUQuerySet, descriptor._label);
        return result;
    }

    public WGPURenderBundleEncoder createRenderBundleEncoder(WGPURenderBundleEncoderDescriptor descriptor) {
        descriptor.Validate();
        var result = wgpuDeviceCreateRenderBundleEncoder(Handle, &descriptor);
        ObjectTracker.CreateRef(result.Handle, HandleType.WGPURenderBundleEncoder, descriptor._label);
        return result;
    }

    public WGPURenderPipeline createRenderPipeline(WGPURenderPipelineDescriptor descriptor) {
        descriptor.Validate();
        var result = wgpuDeviceCreateRenderPipeline(Handle, &descriptor);
        ObjectTracker.CreateRef(result.Handle, HandleType.WGPURenderPipeline, descriptor._label);
        return result;
    }

    public void createRenderPipelineAsync(WGPURenderPipelineDescriptor descriptor, delegate* unmanaged<WGPUCreatePipelineAsyncStatus, WGPURenderPipeline, char*, void*, void> callback, void* userdata) {
        descriptor.Validate();
        wgpuDeviceCreateRenderPipelineAsync(Handle, &descriptor, callback, userdata);
    }

    public WGPUSampler createSampler(WGPUSamplerDescriptor descriptor) {
        descriptor.Validate();
        var result = wgpuDeviceCreateSampler(Handle, &descriptor);
        ObjectTracker.CreateRef(result.Handle, HandleType.WGPUSampler, descriptor._label);
        return result;
    }

    public WGPUShaderModule createShaderModule(WGPUShaderModuleDescriptor descriptor) {
        descriptor.Validate();
        var result = wgpuDeviceCreateShaderModule(Handle, &descriptor);
        ObjectTracker.CreateRef(result.Handle, HandleType.WGPUShaderModule, descriptor._label);
        return result;
    }

    public WGPUTexture createTexture(WGPUTextureDescriptor descriptor) {
        descriptor.Validate();
        var result = wgpuDeviceCreateTexture(Handle, &descriptor);
        ObjectTracker.CreateRef(result.Handle, HandleType.WGPUTexture, descriptor._label);
        return result;
    }

    public void destroy() {
        wgpuDeviceDestroy(Handle);
    }

    public ulong enumerateFeatures(WGPUFeatureName features) {
        var result = wgpuDeviceEnumerateFeatures(Handle, &features);
        return result;
    }

    // getLimits() - not generated. See: Device_NG.cs

    public WGPUQueue queue => wgpuDeviceGetQueue(Handle);

    public WGPUBool hasFeature(WGPUFeatureName feature) {
        var result = wgpuDeviceHasFeature(Handle, feature);
        return result;
    }



    public void setLabel(Utf8 label) {
        wgpuDeviceSetLabel(Handle, label.AllocUtf8());
    }

    public void reference() {
        ObjectTracker.IncRef(Handle);
        wgpuDeviceReference(Handle);
    }

    public void release() {
        ObjectTracker.DecRef(Handle);
        wgpuDeviceRelease(Handle);
    }

    public WGPUBool poll(WGPUBool wait, WGPUWrappedSubmissionIndex wrappedSubmissionIndex) {
        var result = wgpuDevicePoll(Handle, wait, &wrappedSubmissionIndex);
        return result;
    }

    public override string? ToString() => ObjectTracker.GetLabel(Handle);
}
