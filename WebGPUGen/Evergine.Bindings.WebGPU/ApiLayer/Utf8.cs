using System.Text;

namespace Evergine.Bindings.WebGPU;

internal enum Utf8Type
{
    Null,
    Span,
    Ptr,
}

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
            case Utf8Type.Null:
                return default;
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
                case Utf8Type.Null:
                    throw new NullReferenceException();
                case Utf8Type.Span:
                    return span.Length;
                case Utf8Type.Ptr:
                    return GetPtrLength(ptr);
            }
            return 0;
        }
    }

    public static implicit operator Utf8(ReadOnlySpan<byte> value) {
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
        switch (type) {
            case Utf8Type.Null:
                return null;
            case Utf8Type.Span:
                return Encoding.UTF8.GetString(span);
            case Utf8Type.Ptr:
                if (ptr == null) {
                    return null;
                }
                int length = GetPtrLength(ptr);
                return Encoding.UTF8.GetString(ptr, length);
        }
        throw new NotImplementedException();
    }
}