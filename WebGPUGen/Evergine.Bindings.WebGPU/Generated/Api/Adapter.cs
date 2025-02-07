namespace Evergine.Bindings.WebGPU;
using static WebGPUNative;

/// <see href="https://developer.mozilla.org/en-US/docs/Web/API/GPUAdapter">MDN documentation</see>           
public unsafe partial struct WGPUAdapter
{
    public ulong enumerateFeatures(WGPUFeatureName features) {
        var result = wgpuAdapterEnumerateFeatures(Handle, &features);
        return result;
    }

    public WGPUAdapterInfo info { get {
        var result = new WGPUAdapterInfo();
        wgpuAdapterGetInfo(Handle, &result);
        return result;
    } }

    // getLimits() - not generated. See: Adapter_NG.cs

    public WGPUBool hasFeature(WGPUFeatureName feature) {
        var result = wgpuAdapterHasFeature(Handle, feature);
        return result;
    }

    // requestDevice() - not generated. See: Adapter_NG.cs

    public void reference() {
        ObjectTracker.IncRef(Handle);
        wgpuAdapterReference(Handle);
    }

    public void release() {
        ObjectTracker.DecRef(Handle);
        wgpuAdapterRelease(Handle);
    }

    public override string? ToString() => ObjectTracker.GetLabel(Handle);
}
