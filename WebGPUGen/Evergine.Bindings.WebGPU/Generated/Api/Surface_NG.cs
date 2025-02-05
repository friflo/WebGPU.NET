namespace Evergine.Bindings.WebGPU;
using static WebGPUNative;
           
public unsafe partial struct WGPUSurface
{
    public WGPUSurfaceCapabilities getCapabilities(WGPUAdapter adapter)
    {
        var value = new WGPUSurfaceCapabilities();
        
        wgpuSurfaceGetCapabilities(Handle, adapter, &value);
        var result = value;
        result.Formats      = new Span<WGPUTextureFormat>       (value.formats,      (int)value.formatCount);
        result.PresentModes = new Span<WGPUPresentMode>         (value.presentModes, (int)value.presentModeCount);
        result.AlphaModes   = new Span<WGPUCompositeAlphaMode>  (value.alphaModes,   (int)value.alphaModeCount);
        wgpuSurfaceCapabilitiesFreeMembers(value);
        return result;
    }
}
