using System.Diagnostics;
using System.Text;

namespace Evergine.Bindings.WebGPU;

internal enum Utf8Type
{
    Span,
    Ptr,
}

[DebuggerDisplay("{DebuggerDisplay(),nq}")]
public readonly unsafe ref struct Utf8
{
    internal readonly ReadOnlySpan<byte>  span;
    internal readonly byte*               ptr;
    internal readonly Utf8Type            type;
    
    public Utf8(char* strPtr) {
        type    = Utf8Type.Ptr;
        ptr     = (byte*)strPtr;
    }
    
    private Utf8(ReadOnlySpan<byte> value) {
        span    = value;
        type    = Utf8Type.Span;
    }
    
    public ReadOnlySpan<byte>  AsSpan()
    {
        switch (type) {
            case Utf8Type.Span:
                return span;
            case Utf8Type.Ptr:
                return new ReadOnlySpan<byte>(ptr, GetPtrLength(ptr));
        }
        throw new InvalidOperationException();
    }
    
    public int Length {
        get {
            switch (type) {
                case Utf8Type.Span:
                    return span.Length;

                case Utf8Type.Ptr:
                    if (ptr != null)
                        return GetPtrLength(ptr);
                    return 0;
            }
            throw new NullReferenceException();
        }
    }

    public static implicit operator Utf8(ReadOnlySpan<byte> value) {
        return new Utf8 (value);
    }
    
    public static implicit operator Utf8(in Label value) {
        return new Utf8 (value.AsSpan());
    }
    
    public static implicit operator Utf8(byte[] value) {
        return new Utf8 (value);
    }
    
    
    private static int GetPtrLength(byte* ptr) {
        int len = 0;
        while (ptr[len] != 0) {
            len++;
        }
        return len;
    }

    public override string? ToString()
    {
        var span = AsSpan();
        if (span.Length == 0) {
            return string.Empty;
        }
        return Encoding.UTF8.GetString(span);
    }
    
    private string? DebuggerDisplay() {
        return $"\"{ToString()}\"";
    }
    
    public static bool operator != (Utf8 obj1, Utf8 obj2) => !(obj1 == obj2);
    
    public static bool operator == (Utf8 str1, Utf8 str2) {
        var span1 = str1.AsSpan();
        var span2 = str2.AsSpan();
        if (span1.Length != span2.Length) {
            return false;
        }
        return span1.SequenceEqual(span2);
    }
}