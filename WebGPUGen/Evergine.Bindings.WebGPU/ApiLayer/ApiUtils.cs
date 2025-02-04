
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
    
    // ---------- Nullable<>
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
    
    // ---------- Utf8
    public static unsafe char* AllocUtf8(in this Utf8 value) {
        if (value.Length == 0) {
            return null;
        }
        return (char*)arena.AllocUtf8(value.AsSpan());
    }

    public static unsafe Utf8 GetUtf8(char* ptr) {
        if (ptr == null) {
            return default;
        }
        return new Utf8(ptr);
    }
    
    public static unsafe void SetUtf8(in Utf8 utf8, out char* dst) {
        switch (utf8.type)
        {
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