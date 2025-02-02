using System.Text;

namespace Evergine.Bindings.WebGPU;

internal enum Utf8Type
{
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
                    if (!span.IsEmpty) {
                        return span.Length;
                    }
                    break;
                case Utf8Type.Ptr:
                    if (ptr != null)
                        return GetPtrLength(ptr);
                    break;
            }
            throw new NullReferenceException();
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
            case Utf8Type.Span:
                if (span.IsEmpty) {
                    return null;
                }
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