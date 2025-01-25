namespace Evergine.Bindings.WebGPU;
using static WebGPUNative;
           
public unsafe partial struct WGPUSurface
{
    public WGPUSurfaceCapabilities getCapabilities(WGPUAdapter adapter) {
        var result = new WGPUSurfaceCapabilities();
        wgpuSurfaceGetCapabilities(Handle, adapter, &result);
        return result;
    }
}
