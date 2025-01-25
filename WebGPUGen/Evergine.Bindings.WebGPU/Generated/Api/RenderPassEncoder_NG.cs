namespace Evergine.Bindings.WebGPU;
using static WebGPUNative;
           
public unsafe partial struct WGPURenderPassEncoder
{
    public void setPushConstants<T>(WGPUShaderStage stages, uint offset, Span<T> data) where T : unmanaged {
        fixed (T* ptr = data) {
            wgpuRenderPassEncoderSetPushConstants(Handle, stages, offset, (uint)(data.Length * sizeof(T)), ptr);
        }
    }
}
