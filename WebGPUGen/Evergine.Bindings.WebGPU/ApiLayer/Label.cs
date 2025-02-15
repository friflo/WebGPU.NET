using System.Diagnostics;
using System.Text;

namespace Evergine.Bindings.WebGPU;

[DebuggerDisplay("{DebuggerDisplay(),nq}")]
public readonly struct Label
{
    private readonly    byte[] bytes;
    
    public Label(string? value) {
        if (value == null) {
            bytes = Array.Empty<byte>();
            return;
        }
        bytes = Encoding.UTF8.GetBytes(value);
    }
    
    public ReadOnlySpan<byte> AsSpan() {
        return new ReadOnlySpan<byte>(bytes);
    }
    
    private string? DebuggerDisplay() {
        return $"\"{ToString()}\"";
    }

    public override string? ToString() {
        if (bytes == null) {
            return null;
        }
        return Encoding.UTF8.GetString(bytes);
    }
}