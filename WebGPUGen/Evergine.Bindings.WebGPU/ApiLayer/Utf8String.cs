/*

using System.Diagnostics;
using System.Text;

namespace Evergine.Bindings.WebGPU;

[DebuggerDisplay("{DebuggerDisplay,nq}")]
public readonly unsafe struct Utf8String : IEquatable<Utf8String>
{
    private readonly byte* bytePtr;
    private readonly int   length;
    
    public byte* GetPtr() => bytePtr;
    
    public int Length {
        get {
            if (bytePtr != null) {
                return length;
            }
            throw new NullReferenceException();
        }
    }

    internal Utf8String(byte* ptr, int len) {
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
    
    public bool Equals(Utf8String other) {
        if (bytePtr == null) {
            return other.bytePtr == null;
        }
        if (other.bytePtr == null) {
            return false;
        }
        return AsSpan().SequenceEqual(other.AsSpan());
    }

    public override bool Equals(object? obj) {
        if (obj is Utf8String other) {
            return Equals(other);
        }
        return false;
    }
    
    public override int GetHashCode() {
        var ptr = bytePtr;
        if (ptr == null) {
            return -1;
        }
        var hash = new HashCode();
        hash.AddBytes(AsSpan());
        return hash.ToHashCode();
    }

    public static bool operator != (Utf8String obj1, string? obj2) {
        return !(obj1 == obj2);
    }
    
    public static bool operator == (Utf8String obj1, string? obj2) {
        if (obj1.bytePtr == null) {
            return obj2 == null;
        }
        if (obj2 == null) {
            return false;
        }
        var size = Encoding.UTF8.GetMaxByteCount(obj2.Length);
        Span<byte> bytes = stackalloc byte[size];
        var len = Encoding.UTF8.GetBytes(obj2, bytes);
        if (obj1.Length != len) {
            return false;
        }
        return obj1.AsSpan().SequenceEqual(bytes);
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
*/