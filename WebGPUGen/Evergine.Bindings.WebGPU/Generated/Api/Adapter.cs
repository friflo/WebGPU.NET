using System.Diagnostics;
using static Evergine.Bindings.WebGPU.WebGPUNative;

// ReSharper disable InconsistentNaming
namespace Evergine.Bindings.WebGPU;

/// <see href="https://developer.mozilla.org/en-US/docs/Web/API/GPUAdapter">MDN documentation</see>           
public unsafe partial struct WGPUAdapter
{
    public ulong enumerateFeatures(WGPUFeatureName features) {
        Validate_enumerateFeatures(features);
        var result = wgpuAdapterEnumerateFeatures(this, &features);
        return result;
    }

    [Conditional("VALIDATE")]
    private void Validate_enumerateFeatures(WGPUFeatureName features) {
        ObjectTracker.ValidateHandle(this);
    }

    // getInfo() - not generated. See: Adapter_NG.cs

    // getLimits() - not generated. See: Adapter_NG.cs

    public WGPUBool hasFeature(WGPUFeatureName feature) {
        Validate_hasFeature(feature);
        var result = wgpuAdapterHasFeature(this, feature);
        return result;
    }

    [Conditional("VALIDATE")]
    private void Validate_hasFeature(WGPUFeatureName feature) {
        ObjectTracker.ValidateHandle(this);
    }

    // requestDevice() - not generated. See: Adapter_NG.cs

    public void reference() {
        ObjectTracker.IncRef(this);
        wgpuAdapterReference(this);
    }

    public void release() {
        ObjectTracker.DecRef(this);
        wgpuAdapterRelease(this);
    }

    public override string? ToString() => ObjectTracker.GetLabel(Handle);
}
