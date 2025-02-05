namespace Evergine.Bindings.WebGPU;

public static unsafe partial class WebGPUNative
{
    public static WGPUInstance wgpuCreateInstance(WGPUInstanceExtras instanceExtras)
    {
        instanceExtras.chain.sType = (WGPUSType)WGPUNativeSType.InstanceExtras;
        WGPUInstanceDescriptor instanceDescriptor = new WGPUInstanceDescriptor {
            nextInChain = &instanceExtras.chain
        };
        return wgpuCreateInstance(&instanceDescriptor);
    }
}