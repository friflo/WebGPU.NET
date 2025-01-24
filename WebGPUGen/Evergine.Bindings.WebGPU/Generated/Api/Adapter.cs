namespace Evergine.Bindings.WebGPU;
using static WebGPUNative;
           
public static unsafe partial class WebGPUExtensions
{
    public static ulong enumerateFeatures(this WGPUAdapter adapter, WGPUFeatureName features) {
        var result = wgpuAdapterEnumerateFeatures(adapter, &features);
        return result;
    }

    public static void getInfo(this WGPUAdapter adapter, WGPUAdapterInfo info) {
        wgpuAdapterGetInfo(adapter, &info);
    }

    public static WGPUBool getLimits(this WGPUAdapter adapter, WGPUSupportedLimits limits) {
        var result = wgpuAdapterGetLimits(adapter, &limits);
        return result;
    }

    public static WGPUBool hasFeature(this WGPUAdapter adapter, WGPUFeatureName feature) {
        var result = wgpuAdapterHasFeature(adapter, feature);
        return result;
    }

    public static void requestDevice(this WGPUAdapter adapter, WGPUDeviceDescriptor descriptor, delegate* unmanaged<WGPURequestDeviceStatus, WGPUDevice, char*, void*, void> callback, void* userdata) {
        wgpuAdapterRequestDevice(adapter, &descriptor, callback, userdata);
    }

    public static void reference(this WGPUAdapter adapter) {
        wgpuAdapterReference(adapter);
        ObjectTracker.IncRef(adapter.Handle);
    }

    public static void release(this WGPUAdapter adapter) {
        ObjectTracker.DecRef(adapter.Handle);
        wgpuAdapterRelease(adapter);
    }

}
