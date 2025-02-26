using System.Diagnostics;
using static Evergine.Bindings.WebGPU.WebGPUNative;

// ReSharper disable InconsistentNaming
namespace Evergine.Bindings.WebGPU;

/// No counterpart in JavaScript WebGPU           
public unsafe partial struct WGPUInstance
{
    public WGPUSurface createSurface(WGPUSurfaceDescriptor descriptor) {
        Validate_createSurface(descriptor);
        var result = wgpuInstanceCreateSurface(this, &descriptor);
        WGPUException.ThrowOnError();
        ObjectTracker.CreateRefLabel(result, HandleType.WGPUSurface, descriptor._label); // ref-create
        return result;
    }

    [Conditional("VALIDATE")]
    private void Validate_createSurface(WGPUSurfaceDescriptor descriptor) {
        ObjectTracker.ValidateHandle(this);
        descriptor.Validate();
    }

    public WGPUBool hasWGSLLanguageFeature(WGPUWGSLFeatureName feature) {
        Validate_hasWGSLLanguageFeature(feature);
        var result = wgpuInstanceHasWGSLLanguageFeature(this, feature);
        WGPUException.ThrowOnError();
        return result;
    }

    [Conditional("VALIDATE")]
    private void Validate_hasWGSLLanguageFeature(WGPUWGSLFeatureName feature) {
        ObjectTracker.ValidateHandle(this);
    }

    public void processEvents() {
        Validate_processEvents();
        wgpuInstanceProcessEvents(this);
        WGPUException.ThrowOnError();
    }

    [Conditional("VALIDATE")]
    private void Validate_processEvents() {
        ObjectTracker.ValidateHandle(this);
    }

    public void requestAdapter(WGPURequestAdapterOptions options, delegate* unmanaged<WGPURequestAdapterStatus, WGPUAdapter, char*, void*, void> callback, void* userdata) {
        Validate_requestAdapter(options, callback, userdata);
        wgpuInstanceRequestAdapter(this, &options, callback, userdata);
        WGPUException.ThrowOnError();
    }

    [Conditional("VALIDATE")]
    private void Validate_requestAdapter(WGPURequestAdapterOptions options, delegate* unmanaged<WGPURequestAdapterStatus, WGPUAdapter, char*, void*, void> callback, void* userdata) {
        ObjectTracker.ValidateHandle(this);
        options.Validate();
    }

    public void reference() {
        ObjectTracker.IncRef(this);
        wgpuInstanceReference(this);
    }

    public void release() {
        ObjectTracker.DecRef(this);
        wgpuInstanceRelease(this);
    }
    
    public void Dispose() {
        ObjectTracker.DecRef(this);
        wgpuInstanceRelease(this);
    }

    // report() - not generated. See: Instance_NG.cs

    public ulong enumerateAdapters(WGPUInstanceEnumerateAdapterOptions options, WGPUAdapter adapters) {
        Validate_enumerateAdapters(options, adapters);
        var result = wgpuInstanceEnumerateAdapters(this, &options, &adapters);
        WGPUException.ThrowOnError();
        return result;
    }

    [Conditional("VALIDATE")]
    private void Validate_enumerateAdapters(WGPUInstanceEnumerateAdapterOptions options, WGPUAdapter adapters) {
        ObjectTracker.ValidateHandle(this);
    }

    public override string? ToString() => ObjectTracker.GetLabel(Handle);
}
