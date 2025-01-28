using System.Runtime.InteropServices;
using System.Text;

namespace Evergine.Bindings.WebGPU;

/// <summary>
/// String encoding WGPU: "Strings are represented in UTF-8, using the WGPUStringView struct"
/// See: https://webgpu-native.github.io/webgpu-headers/Strings.html
/// </summary>
/*
Nullable value defining a pointer+length view into a UTF-8 encoded string.
Values passed into the API may use the special length value WGPU_STRLEN to indicate a null-terminated string.
Non-null values passed out of the API (for example as callback arguments) always provide an explicit length and may or may not be null-terminated.

Some inputs to the API accept null values. Those which do not accept null values "default" to the empty string when null values are passed.

Values are encoded as follows:
- {NULL, WGPU_STRLEN}: the null value.
- {non_null_pointer, WGPU_STRLEN}: a null-terminated string view.
- {any, 0}: the empty string.
- {NULL, non_zero_length}: not allowed (null dereference).
- {non_null_pointer, non_zero_length}: an explictly-sized string view with size non_zero_length (in bytes).
 */
public sealed class Arena
{
    private List<nint>      chunks         = new();
    private int             chunkIndex;
    private nint            currentChunk;
    private int             currentPos     = ChunkSize;
    
    private AllocHeader     header;
    
    private const   int  ChunkSize      = 0x10000;
    
    public Arena() {
        var version = AllocValidator.GetArenaVersion();
        header.version = version;
    }
    
    public void Use() {
        ApiUtils.arena = this;
    }

    public void Reset() {
        chunkIndex = 0;
        currentPos = ChunkSize;
        header.version.reset++;
        AllocValidator.UpdateResetVersion(header);
    }
    
    public void FreeGlobalAllocation() {
        foreach (var chunk in chunks) {
            Marshal.FreeHGlobal(chunk);
        }
        chunks.Clear();
        Reset();
    }
    
    public unsafe nint Alloc(int size)
    {
        var ptr = AllocInternal(size + 8);
        *(AllocHeader*)ptr = header;
        return ptr + 8;
    }
    
    private nint AllocInternal(int size)
    {
        size = (size + 7) & 0xffffff8; // add pad bytes for 8 byte alignment
        var pos = currentPos;
        if (pos + size < ChunkSize) {
            currentPos += size;
            return currentChunk + pos;
        }
        if (size > ChunkSize) {
            throw AllocationTooLarge(size);
        }
        if (chunkIndex < chunks.Count) {
            currentPos = size;
            currentChunk = chunks[chunkIndex++];
            return currentChunk;
        }
        var chunk = Marshal.AllocHGlobal(ChunkSize);
        chunks.Add(chunk);
        currentChunk = chunk;
        chunkIndex++;
        currentPos = size;
        return chunk;
    }
    
    private static InvalidOperationException AllocationTooLarge(int size) {
        return new InvalidOperationException($"Allocation too large. max: {ChunkSize} was: {size}");
    }
    
    // WebGPU C API: "Strings are represented in UTF-8, using the WGPUStringView struct"
    // https://webgpu-native.github.io/webgpu-headers/Strings.html
    public unsafe char* AllocString(ReadOnlySpan<char> span)
    {
        var size = Encoding.UTF8.GetMaxByteCount(span.Length) + 1; // +1 for Null terminator
        var ptr = (byte*)Alloc(size);
        var len = Encoding.UTF8.GetBytes(span, new Span<byte>(ptr, size));
        ptr[len] = 0;
        return (char*)ptr;
    }
    
}