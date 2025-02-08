namespace Evergine.Bindings.WebGPU;
using static WebGPUNative;
           
public unsafe partial struct WGPUBuffer
{
    public ReadOnlySpan<T> getConstMappedRange<T>(ulong offset, ulong size) where T : unmanaged
    {
        var result = wgpuBufferGetConstMappedRange(this, offset, size * (ulong)sizeof(T));
        if (result is null) {
            return default;
        }
        return new ReadOnlySpan<T>(result, (int)size);
    }
    
    public Span<T> getMappedRange<T> (ulong offset, ulong size)  where T : unmanaged
    {
        var result = wgpuBufferGetMappedRange(this, offset, size * (ulong)sizeof(T));
        if (result is null) {
            return default;
        }
        return new Span<T>(result, (int)size);
    }
}
