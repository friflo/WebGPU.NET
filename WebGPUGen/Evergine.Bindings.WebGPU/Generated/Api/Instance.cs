namespace Evergine.Bindings.WebGPU;
using static WebGPUNative;
           
public static unsafe partial class WebGPUExtensions
{
    public static WGPUSurface createSurface(this WGPUInstance instance, WGPUSurfaceDescriptor descriptor) {
        var result = wgpuInstanceCreateSurface(instance, &descriptor);
        ObjectTracker.CreateRef(result.Handle, descriptor.label);
        return result;
    }

    public static WGPUBool hasWGSLLanguageFeature(this WGPUInstance instance, WGPUWGSLFeatureName feature) {
        var result = wgpuInstanceHasWGSLLanguageFeature(instance, feature);
        return result;
    }

    public static void processEvents(this WGPUInstance instance) {
        wgpuInstanceProcessEvents(instance);
    }

    public static void requestAdapter(this WGPUInstance instance, WGPURequestAdapterOptions options, delegate* unmanaged<WGPURequestAdapterStatus, WGPUAdapter, char*, void*, void> callback, void* userdata) {
        wgpuInstanceRequestAdapter(instance, &options, callback, userdata);
    }

    public static void reference(this WGPUInstance instance) {
        wgpuInstanceReference(instance);
        ObjectTracker.IncRef(instance.Handle);
    }

    public static void release(this WGPUInstance instance) {
        ObjectTracker.DecRef(instance.Handle);
        wgpuInstanceRelease(instance);
    }

    public static void report(this WGPUInstance instance, WGPUGlobalReport report) {
        wgpuGenerateReport(instance, &report);
    }

    public static ulong enumerateAdapters(this WGPUInstance instance, WGPUInstanceEnumerateAdapterOptions options, WGPUAdapter adapters) {
        var result = wgpuInstanceEnumerateAdapters(instance, &options, &adapters);
        return result;
    }

}
