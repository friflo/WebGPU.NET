namespace Evergine.Bindings.WebGPU;
using static WebGPUNative;

public static unsafe partial class WebGPUExtensions
{
    public static WGPUBuffer createBuffer_test(this WGPUDevice device, WGPUBufferDescriptor* descriptor) {
        return wgpuDeviceCreateBuffer(device, descriptor);
    }
}
