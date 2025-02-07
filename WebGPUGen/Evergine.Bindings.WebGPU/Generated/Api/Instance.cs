using static Evergine.Bindings.WebGPU.WebGPUNative;

// ReSharper disable InconsistentNaming
namespace Evergine.Bindings.WebGPU;

/// No counterpart in JavaScript WebGPU           
public unsafe partial struct WGPUInstance
{
    public WGPUSurface createSurface(WGPUSurfaceDescriptor descriptor) {
        Validate_createSurface(Handle, descriptor);
        var result = wgpuInstanceCreateSurface(Handle, &descriptor);
        ObjectTracker.CreateRef(result.Handle, HandleType.WGPUSurface, descriptor._label);
        return result;
    }

    private static void Validate_createSurface(IntPtr handle, WGPUSurfaceDescriptor descriptor) {
        ObjectTracker.ValidateHandle(handle);
        descriptor.Validate();
    }

    public WGPUBool hasWGSLLanguageFeature(WGPUWGSLFeatureName feature) {
        Validate_hasWGSLLanguageFeature(Handle, feature);
        var result = wgpuInstanceHasWGSLLanguageFeature(Handle, feature);
        return result;
    }

    private static void Validate_hasWGSLLanguageFeature(IntPtr handle, WGPUWGSLFeatureName feature) {
        ObjectTracker.ValidateHandle(handle);
    }

    public void processEvents() {
        Validate_processEvents(Handle);
        wgpuInstanceProcessEvents(Handle);
    }

    private static void Validate_processEvents(IntPtr handle) {
        ObjectTracker.ValidateHandle(handle);
    }

    public void requestAdapter(WGPURequestAdapterOptions options, delegate* unmanaged<WGPURequestAdapterStatus, WGPUAdapter, char*, void*, void> callback, void* userdata) {
        Validate_requestAdapter(Handle, options, callback, userdata);
        wgpuInstanceRequestAdapter(Handle, &options, callback, userdata);
    }

    private static void Validate_requestAdapter(IntPtr handle, WGPURequestAdapterOptions options, delegate* unmanaged<WGPURequestAdapterStatus, WGPUAdapter, char*, void*, void> callback, void* userdata) {
        ObjectTracker.ValidateHandle(handle);
    }

    public void reference() {
        ObjectTracker.IncRef(Handle);
        wgpuInstanceReference(Handle);
    }

    public void release() {
        ObjectTracker.DecRef(Handle);
        wgpuInstanceRelease(Handle);
    }

    public void report(WGPUGlobalReport report) {
        Validate_report(Handle, report);
        wgpuGenerateReport(Handle, &report);
    }

    private static void Validate_report(IntPtr handle, WGPUGlobalReport report) {
        ObjectTracker.ValidateHandle(handle);
    }

    public ulong enumerateAdapters(WGPUInstanceEnumerateAdapterOptions options, WGPUAdapter adapters) {
        Validate_enumerateAdapters(Handle, options, adapters);
        var result = wgpuInstanceEnumerateAdapters(Handle, &options, &adapters);
        return result;
    }

    private static void Validate_enumerateAdapters(IntPtr handle, WGPUInstanceEnumerateAdapterOptions options, WGPUAdapter adapters) {
        ObjectTracker.ValidateHandle(handle);
    }

    public override string? ToString() => ObjectTracker.GetLabel(Handle);
}
