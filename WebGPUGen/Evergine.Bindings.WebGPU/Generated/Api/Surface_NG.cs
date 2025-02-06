namespace Evergine.Bindings.WebGPU;
using static WebGPUNative;
           
public unsafe partial struct WGPUSurface
{
    public WGPUSurfaceCapabilities getCapabilities(WGPUAdapter adapter)
    {
        var value = new WGPUSurfaceCapabilities();
        
        wgpuSurfaceGetCapabilities(Handle, adapter, &value);
        var result = value;
        result.Formats      = new Span<WGPUTextureFormat>       (value._formats,      (int)value._formatCount);
        result.PresentModes = new Span<WGPUPresentMode>         (value._presentModes, (int)value._presentModeCount);
        result.AlphaModes   = new Span<WGPUCompositeAlphaMode>  (value._alphaModes,   (int)value._alphaModeCount);
        wgpuSurfaceCapabilitiesFreeMembers(value);
        return result;
    }
}
