namespace Evergine.Bindings.WebGPU;
using static WebGPUNative;

        
public unsafe partial struct WGPUTexture
{
    public WGPUTextureView createView() {
        var result = wgpuTextureCreateView(this, null);
        ObjectTracker.CreateRef(result, HandleType.WGPUTextureView, null);
        return result;
    }
}
