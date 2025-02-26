namespace Evergine.Bindings.WebGPU;

public static unsafe partial class WebGPUNative
{
    public static WGPUInstance wgpuCreateInstance(WGPUInstanceExtras instanceExtras)
    {
        instanceExtras.Validate();
        instanceExtras.chain.sType = (WGPUSType)WGPUNativeSType.InstanceExtras;
        WGPUInstanceDescriptor instanceDescriptor = new WGPUInstanceDescriptor {
            _nextInChain = &instanceExtras.chain
        };
        var result = wgpuCreateInstance(&instanceDescriptor);
        ObjectTracker.CreateRefLabel(result, HandleType.WGPUInstance, null);
        return result;
    }
}