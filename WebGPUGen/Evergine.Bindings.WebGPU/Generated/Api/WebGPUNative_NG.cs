namespace Evergine.Bindings.WebGPU;

public static unsafe partial class WebGPUNative
{
    public static WGPUInstance wgpuCreateInstance(WGPUInstanceExtras instanceExtras)
    {
        instanceExtras.Validate();
        instanceExtras.chain.sType = (WGPUSType)WGPUNativeSType.InstanceExtras;
        WGPUInstanceDescriptor instanceDescriptor = new WGPUInstanceDescriptor {
            nextInChain = &instanceExtras.chain
        };
        var result = wgpuCreateInstance(&instanceDescriptor);
        ObjectTracker.CreateRef(result.Handle, HandleType.WGPUInstance, null);
        return result;
    }
}