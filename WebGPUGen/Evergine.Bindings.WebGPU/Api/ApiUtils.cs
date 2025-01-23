using System.Runtime.InteropServices;

namespace Evergine.Bindings.WebGPU;

using System.Runtime.CompilerServices;

public static class ApiUtils
{
    public static unsafe void SetArr<T>(this Span<T> span, out T* pointer, out ulong count) where T : unmanaged {
        pointer = (T*)Unsafe.AsPointer(ref span.GetPinnableReference());
        count = (ulong)span.Length;    
    }
    
    public static unsafe Span<T> GetOptSpan<T>(T* ptr) where T : unmanaged {
        if (ptr == null) {
            return default;
        }
        return new Span<T>(ptr, 1);
    }
   
    public static unsafe T* GetOptPtr<T>(this Span<T> span) where T : unmanaged {
        if (span.Length == 0) {
            return null;
        }
        return (T*)Unsafe.AsPointer(ref span.GetPinnableReference());
    }
    
    public static unsafe char* AllocString(this Span<char> span) {
        if (span.Length == 0) {
            return null;
        }
        // TODO
        // var ptr = (char*)Marshal.AllocHGlobal(2 * (span.Length + 1));
        // Buffer.MemoryCopy();
        return null;
    }
    
}