namespace Evergine.Bindings.WebGPU;
using static WebGPUNative;
           
public static unsafe partial class WebGPUExtensions
{
    public static delegate* unmanaged<void> cAddress(this WGPUDevice device, ReadOnlySpan<char> procName) {
        var result = wgpuGetProcAddress(device, procName.AllocString());
        return result;
    }

    public static WGPUBindGroup createBindGroup(this WGPUDevice device, WGPUBindGroupDescriptor descriptor) {
        var result = wgpuDeviceCreateBindGroup(device, &descriptor);
        ObjectTracker.CreateRef(result.Handle, descriptor.label);
        return result;
    }

    public static WGPUBindGroupLayout createBindGroupLayout(this WGPUDevice device, WGPUBindGroupLayoutDescriptor descriptor) {
        var result = wgpuDeviceCreateBindGroupLayout(device, &descriptor);
        ObjectTracker.CreateRef(result.Handle, descriptor.label);
        return result;
    }

    public static WGPUBuffer createBuffer(this WGPUDevice device, WGPUBufferDescriptor descriptor) {
        var result = wgpuDeviceCreateBuffer(device, &descriptor);
        ObjectTracker.CreateRef(result.Handle, descriptor.label);
        return result;
    }

    public static WGPUCommandEncoder createCommandEncoder(this WGPUDevice device, WGPUCommandEncoderDescriptor descriptor) {
        var result = wgpuDeviceCreateCommandEncoder(device, &descriptor);
        ObjectTracker.CreateRef(result.Handle, descriptor.label);
        return result;
    }

    public static WGPUComputePipeline createComputePipeline(this WGPUDevice device, WGPUComputePipelineDescriptor descriptor) {
        var result = wgpuDeviceCreateComputePipeline(device, &descriptor);
        ObjectTracker.CreateRef(result.Handle, descriptor.label);
        return result;
    }

    public static void createComputePipelineAsync(this WGPUDevice device, WGPUComputePipelineDescriptor descriptor, delegate* unmanaged<WGPUCreatePipelineAsyncStatus, WGPUComputePipeline, char*, void*, void> callback, void* userdata) {
        wgpuDeviceCreateComputePipelineAsync(device, &descriptor, callback, userdata);
    }

    public static WGPUPipelineLayout createPipelineLayout(this WGPUDevice device, WGPUPipelineLayoutDescriptor descriptor) {
        var result = wgpuDeviceCreatePipelineLayout(device, &descriptor);
        ObjectTracker.CreateRef(result.Handle, descriptor.label);
        return result;
    }

    public static WGPUQuerySet createQuerySet(this WGPUDevice device, WGPUQuerySetDescriptor descriptor) {
        var result = wgpuDeviceCreateQuerySet(device, &descriptor);
        ObjectTracker.CreateRef(result.Handle, descriptor.label);
        return result;
    }

    public static WGPURenderBundleEncoder createRenderBundleEncoder(this WGPUDevice device, WGPURenderBundleEncoderDescriptor descriptor) {
        var result = wgpuDeviceCreateRenderBundleEncoder(device, &descriptor);
        ObjectTracker.CreateRef(result.Handle, descriptor.label);
        return result;
    }

    public static WGPURenderPipeline createRenderPipeline(this WGPUDevice device, WGPURenderPipelineDescriptor descriptor) {
        var result = wgpuDeviceCreateRenderPipeline(device, &descriptor);
        ObjectTracker.CreateRef(result.Handle, descriptor.label);
        return result;
    }

    public static void createRenderPipelineAsync(this WGPUDevice device, WGPURenderPipelineDescriptor descriptor, delegate* unmanaged<WGPUCreatePipelineAsyncStatus, WGPURenderPipeline, char*, void*, void> callback, void* userdata) {
        wgpuDeviceCreateRenderPipelineAsync(device, &descriptor, callback, userdata);
    }

    public static WGPUSampler createSampler(this WGPUDevice device, WGPUSamplerDescriptor descriptor) {
        var result = wgpuDeviceCreateSampler(device, &descriptor);
        ObjectTracker.CreateRef(result.Handle, descriptor.label);
        return result;
    }

    public static WGPUShaderModule createShaderModule(this WGPUDevice device, WGPUShaderModuleDescriptor descriptor) {
        var result = wgpuDeviceCreateShaderModule(device, &descriptor);
        ObjectTracker.CreateRef(result.Handle, descriptor.label);
        return result;
    }

    public static WGPUTexture createTexture(this WGPUDevice device, WGPUTextureDescriptor descriptor) {
        var result = wgpuDeviceCreateTexture(device, &descriptor);
        ObjectTracker.CreateRef(result.Handle, descriptor.label);
        return result;
    }

    public static void destroy(this WGPUDevice device) {
        wgpuDeviceDestroy(device);
    }

    public static ulong enumerateFeatures(this WGPUDevice device, WGPUFeatureName features) {
        var result = wgpuDeviceEnumerateFeatures(device, &features);
        return result;
    }

    public static WGPUBool getLimits(this WGPUDevice device, WGPUSupportedLimits limits) {
        var result = wgpuDeviceGetLimits(device, &limits);
        return result;
    }

    public static WGPUQueue getQueue(this WGPUDevice device) {
        var result = wgpuDeviceGetQueue(device);
        return result;
    }

    public static WGPUBool hasFeature(this WGPUDevice device, WGPUFeatureName feature) {
        var result = wgpuDeviceHasFeature(device, feature);
        return result;
    }



    public static void setLabel(this WGPUDevice device, ReadOnlySpan<char> label) {
        wgpuDeviceSetLabel(device, label.AllocString());
    }

    public static void reference(this WGPUDevice device) {
        wgpuDeviceReference(device);
        ObjectTracker.IncRef(device.Handle);
    }

    public static void release(this WGPUDevice device) {
        ObjectTracker.DecRef(device.Handle);
        wgpuDeviceRelease(device);
    }

    public static WGPUBool poll(this WGPUDevice device, WGPUBool wait, WGPUWrappedSubmissionIndex wrappedSubmissionIndex) {
        var result = wgpuDevicePoll(device, wait, &wrappedSubmissionIndex);
        return result;
    }

}
