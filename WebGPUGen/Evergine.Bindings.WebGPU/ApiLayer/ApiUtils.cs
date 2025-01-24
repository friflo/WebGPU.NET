namespace Evergine.Bindings.WebGPU;

using System.Runtime.InteropServices;

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
public static class ApiUtils
{
    public static unsafe void SetArr<T>(this Span<T> src, out T* dstPtr, out ulong count) where T : unmanaged {
        // Note:
        // Using GetPinnableReference() is only valid in case Span<T> was created on the stack.
        // When using this approach all struct properties MUST be allocated on the stack. 
        //      dstPtr = (T*)Unsafe.AsPointer(ref src.GetPinnableReference());
        count = (ulong)src.Length;
        dstPtr = (T*)ArenaAllocator.Alloc(((sizeof(T) * (int)count) + 7) & 0xffffff8); // add pad bytes for 8 byte alignment
        src.CopyTo(new Span<T>(dstPtr, (int)count));
    }
    
    public static unsafe void SetArr<T>(this Span<T> src, out T* dstPtr, out uint count) where T : unmanaged {
        count = (uint)src.Length;
        dstPtr = (T*)ArenaAllocator.Alloc(((sizeof(T) * (int)count) + 7) & 0xffffff8); // add pad bytes for 8 byte alignment
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
            dstPtr = (T*)ArenaAllocator.Alloc((sizeof(T) + 7) & 0xffffff8); // add pad bytes for 8 byte alignment
            *dstPtr = value.Value;
            return;
        }
        dstPtr = null;
    }
    
    public static unsafe char* AllocString(this ReadOnlySpan<char> span) {
        if (span.Length == 0) {
            return null;
        }
        return ArenaAllocator.AllocString(span);
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
        dst = ArenaAllocator.AllocString(span);
    }
    
}