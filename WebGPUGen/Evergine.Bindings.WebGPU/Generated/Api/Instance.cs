namespace Evergine.Bindings.WebGPU;
using static WebGPUNative;

/// No counterpart in JavaScript WebGPU           
public unsafe partial struct WGPUInstance
{
    public WGPUSurface createSurface(WGPUSurfaceDescriptor descriptor) {
        var result = wgpuInstanceCreateSurface(Handle, &descriptor);
        ObjectTracker.CreateRef(result.Handle, HandleType.WGPUSurface, descriptor.label);
        return result;
    }

    public WGPUBool hasWGSLLanguageFeature(WGPUWGSLFeatureName feature) {
        var result = wgpuInstanceHasWGSLLanguageFeature(Handle, feature);
        return result;
    }

    public void processEvents() {
        wgpuInstanceProcessEvents(Handle);
    }

    public void requestAdapter(WGPURequestAdapterOptions options, delegate* unmanaged<WGPURequestAdapterStatus, WGPUAdapter, char*, void*, void> callback, void* userdata) {
        wgpuInstanceRequestAdapter(Handle, &options, callback, userdata);
    }

    public void reference() {
        wgpuInstanceReference(Handle);
        ObjectTracker.IncRef(Handle);
    }

    public void release() {
        ObjectTracker.DecRef(Handle);
        wgpuInstanceRelease(Handle);
    }

    public void report(WGPUGlobalReport report) {
        wgpuGenerateReport(Handle, &report);
    }

    public ulong enumerateAdapters(WGPUInstanceEnumerateAdapterOptions options, WGPUAdapter adapters) {
        var result = wgpuInstanceEnumerateAdapters(Handle, &options, &adapters);
        return result;
    }

    public override string? ToString() => ObjectTracker.GetLabel(Handle);
}
