using System.Diagnostics;
using static Evergine.Bindings.WebGPU.WebGPUNative;

// ReSharper disable InconsistentNaming
namespace Evergine.Bindings.WebGPU;

/// <see href="https://developer.mozilla.org/en-US/docs/Web/API/GPUDevice">MDN documentation</see>           
public unsafe partial struct WGPUDevice
{
    public delegate* unmanaged<void> cAddress(Utf8 procName) {
        Validate_cAddress(Handle, procName);
        var result = wgpuGetProcAddress(Handle, procName.AllocUtf8());
        return result;
    }

    [Conditional("VALIDATE")]
    private static void Validate_cAddress(IntPtr handle, Utf8 procName) {
        ObjectTracker.ValidateHandle(handle);
    }

    public WGPUBindGroup createBindGroup(WGPUBindGroupDescriptor descriptor) {
        Validate_createBindGroup(Handle, descriptor);
        var result = wgpuDeviceCreateBindGroup(Handle, &descriptor);
        ObjectTracker.CreateRef(result.Handle, HandleType.WGPUBindGroup, descriptor._label);
        return result;
    }

    [Conditional("VALIDATE")]
    private static void Validate_createBindGroup(IntPtr handle, WGPUBindGroupDescriptor descriptor) {
        ObjectTracker.ValidateHandle(handle);
        descriptor.Validate();
    }

    public WGPUBindGroupLayout createBindGroupLayout(WGPUBindGroupLayoutDescriptor descriptor) {
        Validate_createBindGroupLayout(Handle, descriptor);
        var result = wgpuDeviceCreateBindGroupLayout(Handle, &descriptor);
        ObjectTracker.CreateRef(result.Handle, HandleType.WGPUBindGroupLayout, descriptor._label);
        return result;
    }

    [Conditional("VALIDATE")]
    private static void Validate_createBindGroupLayout(IntPtr handle, WGPUBindGroupLayoutDescriptor descriptor) {
        ObjectTracker.ValidateHandle(handle);
        descriptor.Validate();
    }

    public WGPUBuffer createBuffer(WGPUBufferDescriptor descriptor) {
        Validate_createBuffer(Handle, descriptor);
        var result = wgpuDeviceCreateBuffer(Handle, &descriptor);
        ObjectTracker.CreateRef(result.Handle, HandleType.WGPUBuffer, descriptor._label);
        return result;
    }

    [Conditional("VALIDATE")]
    private static void Validate_createBuffer(IntPtr handle, WGPUBufferDescriptor descriptor) {
        ObjectTracker.ValidateHandle(handle);
        descriptor.Validate();
    }

    public WGPUCommandEncoder createCommandEncoder(WGPUCommandEncoderDescriptor descriptor) {
        Validate_createCommandEncoder(Handle, descriptor);
        var result = wgpuDeviceCreateCommandEncoder(Handle, &descriptor);
        ObjectTracker.CreateRef(result.Handle, HandleType.WGPUCommandEncoder, descriptor._label);
        return result;
    }

    [Conditional("VALIDATE")]
    private static void Validate_createCommandEncoder(IntPtr handle, WGPUCommandEncoderDescriptor descriptor) {
        ObjectTracker.ValidateHandle(handle);
        descriptor.Validate();
    }

    public WGPUComputePipeline createComputePipeline(WGPUComputePipelineDescriptor descriptor) {
        Validate_createComputePipeline(Handle, descriptor);
        var result = wgpuDeviceCreateComputePipeline(Handle, &descriptor);
        ObjectTracker.CreateRef(result.Handle, HandleType.WGPUComputePipeline, descriptor._label);
        return result;
    }

    [Conditional("VALIDATE")]
    private static void Validate_createComputePipeline(IntPtr handle, WGPUComputePipelineDescriptor descriptor) {
        ObjectTracker.ValidateHandle(handle);
        descriptor.Validate();
    }

    public void createComputePipelineAsync(WGPUComputePipelineDescriptor descriptor, delegate* unmanaged<WGPUCreatePipelineAsyncStatus, WGPUComputePipeline, char*, void*, void> callback, void* userdata) {
        Validate_createComputePipelineAsync(Handle, descriptor, callback, userdata);
        wgpuDeviceCreateComputePipelineAsync(Handle, &descriptor, callback, userdata);
    }

    [Conditional("VALIDATE")]
    private static void Validate_createComputePipelineAsync(IntPtr handle, WGPUComputePipelineDescriptor descriptor, delegate* unmanaged<WGPUCreatePipelineAsyncStatus, WGPUComputePipeline, char*, void*, void> callback, void* userdata) {
        ObjectTracker.ValidateHandle(handle);
        descriptor.Validate();
    }

    public WGPUPipelineLayout createPipelineLayout(WGPUPipelineLayoutDescriptor descriptor) {
        Validate_createPipelineLayout(Handle, descriptor);
        var result = wgpuDeviceCreatePipelineLayout(Handle, &descriptor);
        ObjectTracker.CreateRef(result.Handle, HandleType.WGPUPipelineLayout, descriptor._label);
        return result;
    }

    [Conditional("VALIDATE")]
    private static void Validate_createPipelineLayout(IntPtr handle, WGPUPipelineLayoutDescriptor descriptor) {
        ObjectTracker.ValidateHandle(handle);
        descriptor.Validate();
    }

    public WGPUQuerySet createQuerySet(WGPUQuerySetDescriptor descriptor) {
        Validate_createQuerySet(Handle, descriptor);
        var result = wgpuDeviceCreateQuerySet(Handle, &descriptor);
        ObjectTracker.CreateRef(result.Handle, HandleType.WGPUQuerySet, descriptor._label);
        return result;
    }

    [Conditional("VALIDATE")]
    private static void Validate_createQuerySet(IntPtr handle, WGPUQuerySetDescriptor descriptor) {
        ObjectTracker.ValidateHandle(handle);
        descriptor.Validate();
    }

    public WGPURenderBundleEncoder createRenderBundleEncoder(WGPURenderBundleEncoderDescriptor descriptor) {
        Validate_createRenderBundleEncoder(Handle, descriptor);
        var result = wgpuDeviceCreateRenderBundleEncoder(Handle, &descriptor);
        ObjectTracker.CreateRef(result.Handle, HandleType.WGPURenderBundleEncoder, descriptor._label);
        return result;
    }

    [Conditional("VALIDATE")]
    private static void Validate_createRenderBundleEncoder(IntPtr handle, WGPURenderBundleEncoderDescriptor descriptor) {
        ObjectTracker.ValidateHandle(handle);
        descriptor.Validate();
    }

    public WGPURenderPipeline createRenderPipeline(WGPURenderPipelineDescriptor descriptor) {
        Validate_createRenderPipeline(Handle, descriptor);
        var result = wgpuDeviceCreateRenderPipeline(Handle, &descriptor);
        ObjectTracker.CreateRef(result.Handle, HandleType.WGPURenderPipeline, descriptor._label);
        return result;
    }

    [Conditional("VALIDATE")]
    private static void Validate_createRenderPipeline(IntPtr handle, WGPURenderPipelineDescriptor descriptor) {
        ObjectTracker.ValidateHandle(handle);
        descriptor.Validate();
    }

    public void createRenderPipelineAsync(WGPURenderPipelineDescriptor descriptor, delegate* unmanaged<WGPUCreatePipelineAsyncStatus, WGPURenderPipeline, char*, void*, void> callback, void* userdata) {
        Validate_createRenderPipelineAsync(Handle, descriptor, callback, userdata);
        wgpuDeviceCreateRenderPipelineAsync(Handle, &descriptor, callback, userdata);
    }

    [Conditional("VALIDATE")]
    private static void Validate_createRenderPipelineAsync(IntPtr handle, WGPURenderPipelineDescriptor descriptor, delegate* unmanaged<WGPUCreatePipelineAsyncStatus, WGPURenderPipeline, char*, void*, void> callback, void* userdata) {
        ObjectTracker.ValidateHandle(handle);
        descriptor.Validate();
    }

    public WGPUSampler createSampler(WGPUSamplerDescriptor descriptor) {
        Validate_createSampler(Handle, descriptor);
        var result = wgpuDeviceCreateSampler(Handle, &descriptor);
        ObjectTracker.CreateRef(result.Handle, HandleType.WGPUSampler, descriptor._label);
        return result;
    }

    [Conditional("VALIDATE")]
    private static void Validate_createSampler(IntPtr handle, WGPUSamplerDescriptor descriptor) {
        ObjectTracker.ValidateHandle(handle);
        descriptor.Validate();
    }

    public WGPUShaderModule createShaderModule(WGPUShaderModuleDescriptor descriptor) {
        Validate_createShaderModule(Handle, descriptor);
        var result = wgpuDeviceCreateShaderModule(Handle, &descriptor);
        ObjectTracker.CreateRef(result.Handle, HandleType.WGPUShaderModule, descriptor._label);
        return result;
    }

    [Conditional("VALIDATE")]
    private static void Validate_createShaderModule(IntPtr handle, WGPUShaderModuleDescriptor descriptor) {
        ObjectTracker.ValidateHandle(handle);
        descriptor.Validate();
    }

    public WGPUTexture createTexture(WGPUTextureDescriptor descriptor) {
        Validate_createTexture(Handle, descriptor);
        var result = wgpuDeviceCreateTexture(Handle, &descriptor);
        ObjectTracker.CreateRef(result.Handle, HandleType.WGPUTexture, descriptor._label);
        return result;
    }

    [Conditional("VALIDATE")]
    private static void Validate_createTexture(IntPtr handle, WGPUTextureDescriptor descriptor) {
        ObjectTracker.ValidateHandle(handle);
        descriptor.Validate();
    }

    public void destroy() {
        Validate_destroy(Handle);
        wgpuDeviceDestroy(Handle);
    }

    [Conditional("VALIDATE")]
    private static void Validate_destroy(IntPtr handle) {
        ObjectTracker.ValidateHandle(handle);
    }

    public ulong enumerateFeatures(WGPUFeatureName features) {
        Validate_enumerateFeatures(Handle, features);
        var result = wgpuDeviceEnumerateFeatures(Handle, &features);
        return result;
    }

    [Conditional("VALIDATE")]
    private static void Validate_enumerateFeatures(IntPtr handle, WGPUFeatureName features) {
        ObjectTracker.ValidateHandle(handle);
    }

    // getLimits() - not generated. See: Device_NG.cs

    // getQueue() - not generated. See: Device_NG.cs

    public WGPUBool hasFeature(WGPUFeatureName feature) {
        Validate_hasFeature(Handle, feature);
        var result = wgpuDeviceHasFeature(Handle, feature);
        return result;
    }

    [Conditional("VALIDATE")]
    private static void Validate_hasFeature(IntPtr handle, WGPUFeatureName feature) {
        ObjectTracker.ValidateHandle(handle);
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
        Validate_poll(Handle, wait, wrappedSubmissionIndex);
        var result = wgpuDevicePoll(Handle, wait, &wrappedSubmissionIndex);
        return result;
    }

    [Conditional("VALIDATE")]
    private static void Validate_poll(IntPtr handle, WGPUBool wait, WGPUWrappedSubmissionIndex wrappedSubmissionIndex) {
        ObjectTracker.ValidateHandle(handle);
    }

    public override string? ToString() => ObjectTracker.GetLabel(Handle);
}
