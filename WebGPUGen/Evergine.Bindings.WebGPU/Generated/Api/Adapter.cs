using System.Diagnostics;
using static Evergine.Bindings.WebGPU.WebGPUNative;

// ReSharper disable InconsistentNaming
namespace Evergine.Bindings.WebGPU;

/// <see href="https://developer.mozilla.org/en-US/docs/Web/API/GPUAdapter">MDN documentation</see>           
public unsafe partial struct WGPUAdapter
{
    public ulong enumerateFeatures(WGPUFeatureName features) {
        Validate_enumerateFeatures(Handle, features);
        var result = wgpuAdapterEnumerateFeatures(this, &features);
        return result;
    }

    [Conditional("VALIDATE")]
    private static void Validate_enumerateFeatures(IntPtr handle, WGPUFeatureName features) {
        ObjectTracker.ValidateHandle(handle);
    }

    public WGPUAdapterInfo info { get {
        var result = new WGPUAdapterInfo();
        wgpuAdapterGetInfo(this, &result);
        return result;
    } }

    // getLimits() - not generated. See: Adapter_NG.cs

    public WGPUBool hasFeature(WGPUFeatureName feature) {
        Validate_hasFeature(Handle, feature);
        var result = wgpuAdapterHasFeature(this, feature);
        return result;
    }

    [Conditional("VALIDATE")]
    private static void Validate_hasFeature(IntPtr handle, WGPUFeatureName feature) {
        ObjectTracker.ValidateHandle(handle);
    }

    // requestDevice() - not generated. See: Adapter_NG.cs

    public void reference() {
        ObjectTracker.IncRef(Handle);
        wgpuAdapterReference(this);
    }

    public void release() {
        ObjectTracker.DecRef(Handle);
        wgpuAdapterRelease(this);
    }

    public override string? ToString() => ObjectTracker.GetLabel(Handle);
}
