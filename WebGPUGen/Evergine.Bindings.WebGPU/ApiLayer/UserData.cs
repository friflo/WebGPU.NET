using System.Runtime.InteropServices;

namespace Evergine.Bindings.WebGPU;

unsafe struct UserData
{
    internal nuint callbackHandle;
    internal byte* label;
    
    internal static UserData* Create(Utf8 utf8Label)
    {
        var userData = (UserData*)NativeMemory.Alloc((nuint)sizeof(UserData));
        var label = utf8Label.AsSpan();
        if (!label.IsEmpty) {
            var len = label.Length;
            userData->label = (byte*)NativeMemory.Alloc((nuint)len + 1);
            label.CopyTo(new Span<byte>(userData->label, len));
            userData->label[len] = 0;
        }
        return userData;
    }

    internal static void Free(UserData* userData) {
        NativeMemory.Free(userData->label);
        NativeMemory.Free(userData);
    }
}