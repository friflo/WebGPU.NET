namespace Evergine.Bindings.WebGPU;
using static WebGPUNative;

        
public unsafe partial struct WGPUTexture
{
    public WGPUTextureView createView() {
        var result = wgpuTextureCreateView(Handle, null);
        ObjectTracker.CreateRef(result.Handle, HandleType.WGPUTextureView, null);
        return result;
    }
}
