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
public sealed class ArenaAllocator
{
    private  List<nint>  _chunks         = new();
    private  int         _chunkIndex;
    private  nint        _currentChunk;
    private  int         _currentPos     = ChunkSize;
    private const   int          ChunkSize      = 0x10000;
    
    public void Use() {
        ApiUtils.arenaAllocator = this;
    }

    public void Reset() {
        _chunkIndex = 0;
        _currentPos = ChunkSize;
    }
    
    public void FreeGlobalAllocation() {
        foreach (var chunk in _chunks) {
            Marshal.FreeHGlobal(chunk);
        }
        _chunks.Clear();
        Reset();
    }
    
    public nint Alloc(int size)
    {
        var pos = _currentPos;
        if (pos + size < ChunkSize) {
            _currentPos += size;
            return _currentChunk + pos;
        }
        if (size > ChunkSize) {
            throw AllocationTooLarge(size);
        }
        if (_chunkIndex < _chunks.Count) {
            _currentPos = size;
            _currentChunk = _chunks[_chunkIndex++];
            return _currentChunk;
        }
        var chunk = Marshal.AllocHGlobal(ChunkSize);
        _chunks.Add(chunk);
        _currentChunk = chunk;
        _chunkIndex++;
        _currentPos = size;
        return chunk;
    }
    
    private static InvalidOperationException AllocationTooLarge(int size) {
        return new InvalidOperationException($"Allocation too large. max: {ChunkSize} was: {size}");
    }
    
    // WebGPU C API: "Strings are represented in UTF-8, using the WGPUStringView struct"
    // https://webgpu-native.github.io/webgpu-headers/Strings.html
    internal unsafe char* AllocString(ReadOnlySpan<char> span)
    {
        var size = Encoding.UTF8.GetMaxByteCount(span.Length) + 1; // +1 for Null terminator
        var ptr = (byte*)Alloc((size + 7) & 0xffffff8); // add pad bytes for 8 byte alignment
        var len = Encoding.UTF8.GetBytes(span, new Span<byte>(ptr, size));
        ptr[len] = 0;
        return (char*)ptr;
    }
    
}