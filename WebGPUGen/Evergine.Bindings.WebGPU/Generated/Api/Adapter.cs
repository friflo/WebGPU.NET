namespace Evergine.Bindings.WebGPU;
using static WebGPUNative;
           
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

    public WGPUBool getLimits(WGPUSupportedLimits limits) {
        var result = wgpuAdapterGetLimits(Handle, &limits);
        return result;
    }

    public WGPUBool hasFeature(WGPUFeatureName feature) {
        var result = wgpuAdapterHasFeature(Handle, feature);
        return result;
    }

    public void requestDevice(WGPUDeviceDescriptor descriptor, delegate* unmanaged<WGPURequestDeviceStatus, WGPUDevice, char*, void*, void> callback, void* userdata) {
        wgpuAdapterRequestDevice(Handle, &descriptor, callback, userdata);
    }

    public void reference() {
        wgpuAdapterReference(Handle);
        ObjectTracker.IncRef(Handle);
    }

    public void release() {
        ObjectTracker.DecRef(Handle);
        wgpuAdapterRelease(Handle);
    }

}
