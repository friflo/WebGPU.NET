using System.Runtime.InteropServices;

namespace Evergine.Bindings.WebGPU;

using System.Runtime.CompilerServices;

public static class ApiUtils
{
    public static unsafe void SetArr<T>(this Span<T> span, out T* pointer, out ulong count) where T : unmanaged {
        pointer = (T*)Unsafe.AsPointer(ref span.GetPinnableReference());
        count = (ulong)span.Length;    
    }
    
    public static unsafe void SetArr<T>(this Span<T> span, out T* pointer, out uint count) where T : unmanaged {
        pointer = (T*)Unsafe.AsPointer(ref span.GetPinnableReference());
        count = (uint)span.Length;    
    }
    
    public static unsafe T? GetOpt<T>(T* ptr) where T : unmanaged {
        if (ptr == null) {
            return null;
        }
        return *ptr;
    }
    
    public static unsafe void SetOpt<T>(out T* ptr, T? value) where T : unmanaged {
        if (value.HasValue) {
            ptr = null; // TODO allocate on native heap and set value
            return;
        }
        ptr = null;
    }
   
    public static unsafe T* GetArrPtr<T>(this Span<T> span) where T : unmanaged {
        if (span.Length == 0) {
            return null;
        }
        return (T*)Unsafe.AsPointer(ref span.GetPinnableReference());
    }
    
    public static unsafe char* AllocString(this ReadOnlySpan<char> span) {
        if (span.Length == 0) {
            return null;
        }
        // TODO
        // var ptr = (char*)Marshal.AllocHGlobal(2 * (span.Length + 1));
        // Buffer.MemoryCopy();
        return null;
    }
    
    public static unsafe ReadOnlySpan<char> GetLabel(char* label) {
        if (label == null) {
            return default;
        }
        return Marshal.PtrToStringAnsi((IntPtr)label).AsSpan();
    }
    
}