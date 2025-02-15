using System.Runtime.InteropServices;
using System.Text;

namespace Evergine.Bindings.WebGPU;

public unsafe struct Utf8Const
{
    // Store label in a fixed size buffer instead of a string.
    // Using a string (or char[]) would require a heap allocation.
    // Invariant: buffer[63] = 0
    private fixed       byte    buffer[64];
    private readonly    int     len;
    
    
    public Utf8Const(string? value) {
        if (value == null) {
            len = 0;
            return;
        }
        fixed (byte* ptr = buffer) {
            len = Encoding.UTF8.GetBytes(value.AsSpan(), new Span<byte>(ptr, 63));
        }
    }
    
    public ReadOnlySpan<byte> AsSpan() {
        fixed (byte* ptr = buffer) {
            return new ReadOnlySpan<byte>(ptr, len);
        }
    }

    public override string? ToString() {
        if (len == 0) {
            return null;
        }
        fixed (byte* ptr = buffer) {
            return Marshal.PtrToStringAnsi((IntPtr)ptr);
        }
    }
}