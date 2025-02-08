using System.Diagnostics;
using static Evergine.Bindings.WebGPU.WebGPUNative;

// ReSharper disable InconsistentNaming
namespace Evergine.Bindings.WebGPU;

/// <see href="https://developer.mozilla.org/en-US/docs/Web/API/GPUDevice">MDN documentation</see>           
public unsafe partial struct WGPUDevice
{
    public delegate* unmanaged<void> cAddress(Utf8 procName) {
        Validate_cAddress(Handle, procName);
        var result = wgpuGetProcAddress(this, procName.AllocUtf8());
        return result;
    }

    [Conditional("VALIDATE")]
    private void Validate_cAddress(IntPtr handle, Utf8 procName) {
        ObjectTracker.ValidateHandle(this);
    }

    public WGPUBindGroup createBindGroup(WGPUBindGroupDescriptor descriptor) {
        Validate_createBindGroup(Handle, descriptor);
        var result = wgpuDeviceCreateBindGroup(this, &descriptor);
        ObjectTracker.CreateRef(result, HandleType.WGPUBindGroup, descriptor._label);
        return result;
    }

    [Conditional("VALIDATE")]
    private void Validate_createBindGroup(IntPtr handle, WGPUBindGroupDescriptor descriptor) {
        ObjectTracker.ValidateHandle(this);
        descriptor.Validate();
    }

    public WGPUBindGroupLayout createBindGroupLayout(WGPUBindGroupLayoutDescriptor descriptor) {
        Validate_createBindGroupLayout(Handle, descriptor);
        var result = wgpuDeviceCreateBindGroupLayout(this, &descriptor);
        ObjectTracker.CreateRef(result, HandleType.WGPUBindGroupLayout, descriptor._label);
        return result;
    }

    [Conditional("VALIDATE")]
    private void Validate_createBindGroupLayout(IntPtr handle, WGPUBindGroupLayoutDescriptor descriptor) {
        ObjectTracker.ValidateHandle(this);
        descriptor.Validate();
    }

    public WGPUBuffer createBuffer(WGPUBufferDescriptor descriptor) {
        Validate_createBuffer(Handle, descriptor);
        var result = wgpuDeviceCreateBuffer(this, &descriptor);
        ObjectTracker.CreateRef(result, HandleType.WGPUBuffer, descriptor._label);
        return result;
    }

    [Conditional("VALIDATE")]
    private void Validate_createBuffer(IntPtr handle, WGPUBufferDescriptor descriptor) {
        ObjectTracker.ValidateHandle(this);
        descriptor.Validate();
    }

    public WGPUCommandEncoder createCommandEncoder(WGPUCommandEncoderDescriptor descriptor) {
        Validate_createCommandEncoder(Handle, descriptor);
        var result = wgpuDeviceCreateCommandEncoder(this, &descriptor);
        ObjectTracker.CreateRef(result, HandleType.WGPUCommandEncoder, descriptor._label);
        return result;
    }

    [Conditional("VALIDATE")]
    private void Validate_createCommandEncoder(IntPtr handle, WGPUCommandEncoderDescriptor descriptor) {
        ObjectTracker.ValidateHandle(this);
        descriptor.Validate();
    }

    public WGPUComputePipeline createComputePipeline(WGPUComputePipelineDescriptor descriptor) {
        Validate_createComputePipeline(Handle, descriptor);
        var result = wgpuDeviceCreateComputePipeline(this, &descriptor);
        ObjectTracker.CreateRef(result, HandleType.WGPUComputePipeline, descriptor._label);
        return result;
    }

    [Conditional("VALIDATE")]
    private void Validate_createComputePipeline(IntPtr handle, WGPUComputePipelineDescriptor descriptor) {
        ObjectTracker.ValidateHandle(this);
        descriptor.Validate();
    }

    public void createComputePipelineAsync(WGPUComputePipelineDescriptor descriptor, delegate* unmanaged<WGPUCreatePipelineAsyncStatus, WGPUComputePipeline, char*, void*, void> callback, void* userdata) {
        Validate_createComputePipelineAsync(Handle, descriptor, callback, userdata);
        wgpuDeviceCreateComputePipelineAsync(this, &descriptor, callback, userdata);
    }

    [Conditional("VALIDATE")]
    private void Validate_createComputePipelineAsync(IntPtr handle, WGPUComputePipelineDescriptor descriptor, delegate* unmanaged<WGPUCreatePipelineAsyncStatus, WGPUComputePipeline, char*, void*, void> callback, void* userdata) {
        ObjectTracker.ValidateHandle(this);
        descriptor.Validate();
    }

    public WGPUPipelineLayout createPipelineLayout(WGPUPipelineLayoutDescriptor descriptor) {
        Validate_createPipelineLayout(Handle, descriptor);
        var result = wgpuDeviceCreatePipelineLayout(this, &descriptor);
        ObjectTracker.CreateRef(result, HandleType.WGPUPipelineLayout, descriptor._label);
        return result;
    }

    [Conditional("VALIDATE")]
    private void Validate_createPipelineLayout(IntPtr handle, WGPUPipelineLayoutDescriptor descriptor) {
        ObjectTracker.ValidateHandle(this);
        descriptor.Validate();
    }

    public WGPUQuerySet createQuerySet(WGPUQuerySetDescriptor descriptor) {
        Validate_createQuerySet(Handle, descriptor);
        var result = wgpuDeviceCreateQuerySet(this, &descriptor);
        ObjectTracker.CreateRef(result, HandleType.WGPUQuerySet, descriptor._label);
        return result;
    }

    [Conditional("VALIDATE")]
    private void Validate_createQuerySet(IntPtr handle, WGPUQuerySetDescriptor descriptor) {
        ObjectTracker.ValidateHandle(this);
        descriptor.Validate();
    }

    public WGPURenderBundleEncoder createRenderBundleEncoder(WGPURenderBundleEncoderDescriptor descriptor) {
        Validate_createRenderBundleEncoder(Handle, descriptor);
        var result = wgpuDeviceCreateRenderBundleEncoder(this, &descriptor);
        ObjectTracker.CreateRef(result, HandleType.WGPURenderBundleEncoder, descriptor._label);
        return result;
    }

    [Conditional("VALIDATE")]
    private void Validate_createRenderBundleEncoder(IntPtr handle, WGPURenderBundleEncoderDescriptor descriptor) {
        ObjectTracker.ValidateHandle(this);
        descriptor.Validate();
    }

    public WGPURenderPipeline createRenderPipeline(WGPURenderPipelineDescriptor descriptor) {
        Validate_createRenderPipeline(Handle, descriptor);
        var result = wgpuDeviceCreateRenderPipeline(this, &descriptor);
        ObjectTracker.CreateRef(result, HandleType.WGPURenderPipeline, descriptor._label);
        return result;
    }

    [Conditional("VALIDATE")]
    private void Validate_createRenderPipeline(IntPtr handle, WGPURenderPipelineDescriptor descriptor) {
        ObjectTracker.ValidateHandle(this);
        descriptor.Validate();
    }

    public void createRenderPipelineAsync(WGPURenderPipelineDescriptor descriptor, delegate* unmanaged<WGPUCreatePipelineAsyncStatus, WGPURenderPipeline, char*, void*, void> callback, void* userdata) {
        Validate_createRenderPipelineAsync(Handle, descriptor, callback, userdata);
        wgpuDeviceCreateRenderPipelineAsync(this, &descriptor, callback, userdata);
    }

    [Conditional("VALIDATE")]
    private void Validate_createRenderPipelineAsync(IntPtr handle, WGPURenderPipelineDescriptor descriptor, delegate* unmanaged<WGPUCreatePipelineAsyncStatus, WGPURenderPipeline, char*, void*, void> callback, void* userdata) {
        ObjectTracker.ValidateHandle(this);
        descriptor.Validate();
    }

    public WGPUSampler createSampler(WGPUSamplerDescriptor descriptor) {
        Validate_createSampler(Handle, descriptor);
        var result = wgpuDeviceCreateSampler(this, &descriptor);
        ObjectTracker.CreateRef(result, HandleType.WGPUSampler, descriptor._label);
        return result;
    }

    [Conditional("VALIDATE")]
    private void Validate_createSampler(IntPtr handle, WGPUSamplerDescriptor descriptor) {
        ObjectTracker.ValidateHandle(this);
        descriptor.Validate();
    }

    public WGPUShaderModule createShaderModule(WGPUShaderModuleDescriptor descriptor) {
        Validate_createShaderModule(Handle, descriptor);
        var result = wgpuDeviceCreateShaderModule(this, &descriptor);
        ObjectTracker.CreateRef(result, HandleType.WGPUShaderModule, descriptor._label);
        return result;
    }

    [Conditional("VALIDATE")]
    private void Validate_createShaderModule(IntPtr handle, WGPUShaderModuleDescriptor descriptor) {
        ObjectTracker.ValidateHandle(this);
        descriptor.Validate();
    }

    public WGPUTexture createTexture(WGPUTextureDescriptor descriptor) {
        Validate_createTexture(Handle, descriptor);
        var result = wgpuDeviceCreateTexture(this, &descriptor);
        ObjectTracker.CreateRef(result, HandleType.WGPUTexture, descriptor._label);
        return result;
    }

    [Conditional("VALIDATE")]
    private void Validate_createTexture(IntPtr handle, WGPUTextureDescriptor descriptor) {
        ObjectTracker.ValidateHandle(this);
        descriptor.Validate();
    }

    public void destroy() {
        Validate_destroy(Handle);
        wgpuDeviceDestroy(this);
    }

    [Conditional("VALIDATE")]
    private void Validate_destroy(IntPtr handle) {
        ObjectTracker.ValidateHandle(this);
    }

    public ulong enumerateFeatures(WGPUFeatureName features) {
        Validate_enumerateFeatures(Handle, features);
        var result = wgpuDeviceEnumerateFeatures(this, &features);
        return result;
    }

    [Conditional("VALIDATE")]
    private void Validate_enumerateFeatures(IntPtr handle, WGPUFeatureName features) {
        ObjectTracker.ValidateHandle(this);
    }

    // getLimits() - not generated. See: Device_NG.cs

    // getQueue() - not generated. See: Device_NG.cs

    public WGPUBool hasFeature(WGPUFeatureName feature) {
        Validate_hasFeature(Handle, feature);
        var result = wgpuDeviceHasFeature(this, feature);
        return result;
    }

    [Conditional("VALIDATE")]
    private void Validate_hasFeature(IntPtr handle, WGPUFeatureName feature) {
        ObjectTracker.ValidateHandle(this);
    }




    public void reference() {
        ObjectTracker.IncRef(this);
        wgpuDeviceReference(this);
    }

    public void release() {
        ObjectTracker.DecRef(this);
        wgpuDeviceRelease(this);
    }

    public WGPUBool poll(WGPUBool wait, WGPUWrappedSubmissionIndex wrappedSubmissionIndex) {
        Validate_poll(Handle, wait, wrappedSubmissionIndex);
        var result = wgpuDevicePoll(this, wait, &wrappedSubmissionIndex);
        return result;
    }

    [Conditional("VALIDATE")]
    private void Validate_poll(IntPtr handle, WGPUBool wait, WGPUWrappedSubmissionIndex wrappedSubmissionIndex) {
        ObjectTracker.ValidateHandle(this);
    }

    public override string? ToString() => ObjectTracker.GetLabel(Handle);
}
