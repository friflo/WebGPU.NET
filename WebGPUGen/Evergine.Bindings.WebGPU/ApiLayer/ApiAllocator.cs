using System.Runtime.InteropServices;

namespace Evergine.Bindings.WebGPU;

public static class ApiAllocator
{
    private static List<nint>   _chunks = new();
    private static int          _chunkIndex;
    private static nint         _currentChunk;
    private static int          _currentPos = 0x10000;
    private const int           ChunkSize = 0x10000;

    public static void FreeAllocations() {
        _chunkIndex = 0;
        _currentPos = 0;
    }
    
    public static unsafe nint Alloc(int size)
    {
        var pos = _currentPos;
        if (pos + size < ChunkSize) {
            _currentPos += size;
            return _currentChunk + pos;
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
    
}