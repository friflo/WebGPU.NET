using System.Diagnostics;

namespace Evergine.Bindings.WebGPU;
using static WebGPUNative;
           
public unsafe partial struct WGPUSurface
{
    public WGPUSurfaceCapabilities getCapabilities(WGPUAdapter adapter)
    {
        ObjectTracker.ValidateHandle(this);
        ObjectTracker.ValidateHandleParam(adapter);
        
        var value = new WGPUSurfaceCapabilities();
        wgpuSurfaceGetCapabilities(this, adapter, &value);
        var result = value;
        result.formats      = new Span<WGPUTextureFormat>       (value._formats,      (int)value._formatCount);
        result.presentModes = new Span<WGPUPresentMode>         (value._presentModes, (int)value._presentModeCount);
        result.alphaModes   = new Span<WGPUCompositeAlphaMode>  (value._alphaModes,   (int)value._alphaModeCount);
        wgpuSurfaceCapabilitiesFreeMembers(value);
        return result;
    }
    
    /// <summary>
    /// The returned <see cref="WGPUSurfaceTexture.texture"/> must be released by caller.
    /// </summary>
    public WGPUSurfaceTexture getCurrentTexture() {
        ObjectTracker.ValidateHandle(this);
        
        var result = new WGPUSurfaceTexture();
        wgpuSurfaceGetCurrentTexture(this, &result);
        ObjectTracker.CreateRef(result.texture, HandleType.WGPUTexture, null);
        return result;
    }
}
