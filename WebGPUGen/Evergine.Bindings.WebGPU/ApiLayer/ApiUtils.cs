using System.Runtime.InteropServices;

namespace Evergine.Bindings.WebGPU;

public static class ApiUtils
{
    internal static Arena arena;
    
    public static unsafe Span<T> GetArr<T>(T* dstPtr, ulong count) where T : unmanaged {
        AllocValidator.ValidatePtr(dstPtr);
        return new Span<T>(dstPtr, (int)count);
    }
    
    public static unsafe void SetArr<T>(Span<T> src, out T* dstPtr, out ulong count) where T : unmanaged {
        // Note:
        // Using GetPinnableReference() is only valid in case Span<T> was created on the stack.
        // When using this approach all struct properties MUST be allocated on the stack. 
        //      dstPtr = (T*)Unsafe.AsPointer(ref src.GetPinnableReference());
        count = (ulong)src.Length;
        dstPtr = (T*)arena.Alloc(sizeof(T) * (int)count);
        src.CopyTo(new Span<T>(dstPtr, (int)count));
    }
    
    public static unsafe void SetArr<T>(Span<T> src, out T* dstPtr, out uint count) where T : unmanaged {
        count = (uint)src.Length;
        dstPtr = (T*)arena.Alloc(sizeof(T) * (int)count);
        src.CopyTo(new Span<T>(dstPtr, (int)count));
    }
    
    public static unsafe T? GetOpt<T>(T* ptr) where T : unmanaged {
        if (ptr == null) {
            return null;
        }
        return *ptr;
    }
    
    public static unsafe void SetOpt<T>(out T* dstPtr, T? value) where T : unmanaged {
        if (value.HasValue) {
            dstPtr = (T*)arena.Alloc(sizeof(T));
            *dstPtr = value.Value;
            return;
        }
        dstPtr = null;
    }
    
    public static unsafe char* AllocString(this ReadOnlySpan<char> span) {
        if (span.Length == 0) {
            return null;
        }
        return arena.AllocString(span);
    }
    
    public static unsafe ReadOnlySpan<char> GetStr(char* label) {
        if (label == null) {
            return default;
        }
        return Marshal.PtrToStringAnsi((IntPtr)label).AsSpan();
    }
    
    public static unsafe void SetStr(ReadOnlySpan<char> span, out char* dst) {
        if (span.Length == 0) {
            dst = null;
            return;
        }
        dst = arena.AllocString(span);
    }
    
    public static unsafe void SetUtf8(Utf8String str, out char* dst) {
        dst = (char*)str.GetPtr();
    }
    
    // ---------- Utf8
    public static unsafe Utf8 GetUtf8(char* ptr) {
        if (ptr == null) {
            return default;
        }
        return new Utf8(ptr);
    }
    
    public static unsafe void SetUtf8(Utf8 utf8, out char* dst) {
        switch (utf8.type)
        {
            case Utf8Type.Null:
                dst = null;
                return;
            case Utf8Type.Span:
                dst = (char*)arena.AllocUtf8(utf8.span);
                return;
            case Utf8Type.Ptr:
                dst = (char*)arena.AllocUtf8(utf8.ptr);
                return;
        }
        throw new InvalidOperationException();
    }
}