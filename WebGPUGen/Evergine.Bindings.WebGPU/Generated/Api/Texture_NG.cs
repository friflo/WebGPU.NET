namespace Evergine.Bindings.WebGPU;
using static WebGPUNative;

        
public unsafe partial struct WGPUTexture
{
    public WGPUTextureView createView() {
        ObjectTracker.ValidateHandle(this);
        
        var result = wgpuTextureCreateView(this, null);
        WGPUException.ThrowOnError();
        ObjectTracker.CreateRef(result, HandleType.WGPUTextureView, Handle);
        return result;
    }
}
