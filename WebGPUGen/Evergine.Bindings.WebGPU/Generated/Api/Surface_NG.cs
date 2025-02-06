namespace Evergine.Bindings.WebGPU;
using static WebGPUNative;
           
public unsafe partial struct WGPUSurface
{
    public WGPUSurfaceCapabilities getCapabilities(WGPUAdapter adapter)
    {
        var value = new WGPUSurfaceCapabilities();
        
        wgpuSurfaceGetCapabilities(Handle, adapter, &value);
        var result = value;
        result.formats      = new Span<WGPUTextureFormat>       (value._formats,      (int)value._formatCount);
        result.presentModes = new Span<WGPUPresentMode>         (value._presentModes, (int)value._presentModeCount);
        result.alphaModes   = new Span<WGPUCompositeAlphaMode>  (value._alphaModes,   (int)value._alphaModeCount);
        wgpuSurfaceCapabilitiesFreeMembers(value);
        return result;
    }
}
