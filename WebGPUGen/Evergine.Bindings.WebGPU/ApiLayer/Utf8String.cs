using System.Diagnostics;
using System.Text;

namespace Evergine.Bindings.WebGPU;

[DebuggerDisplay("{DebuggerDisplay,nq}")]
public unsafe struct Utf8String
{
    private byte* bytePtr;
    private int   length;
    
    public int Length {
        get {
            if (bytePtr != null) {
                return length;
            }
            throw new NullReferenceException();
        }
    }

    private Utf8String(byte* ptr, int len) {
        bytePtr = ptr;
        length  = len;
    }
    
    public ReadOnlySpan<byte> AsSpan() {
        return new ReadOnlySpan<byte>(bytePtr, length);
    }
    
    public static implicit operator Utf8String(ReadOnlySpan<byte> value) {
        return new Utf8String(ApiUtils.arena.AllocUtf8String(value), value.Length);
    }
    
    public static implicit operator Utf8String(ReadOnlySpan<char> value) {
        return new Utf8String(ApiUtils.arena.AllocUtf8String(value), value.Length);
    }
    
    public static implicit operator Utf8String(string? value) {
        if (value is null) {
            return default;
        }
        return new Utf8String(ApiUtils.arena.AllocUtf8String(value), value.Length);
    }

    public override string? ToString()
    {
        if (bytePtr == null) {
            return null;
        }
        return Encoding.UTF8.GetString(AsSpan());
    }
    
    private string? DebuggerDisplay {
        get {
            if (bytePtr == null) return null;
            return $"\"{ToString()}\"";
        }
    }
}
