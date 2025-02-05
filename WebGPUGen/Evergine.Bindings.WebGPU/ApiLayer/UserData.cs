using System.Runtime.InteropServices;

namespace Evergine.Bindings.WebGPU;

unsafe struct UserData
{
    internal GCHandle   callbackHandle;
    internal byte*      label;
    
    internal static UserData* Create(Utf8 utf8Label, Delegate? callback)
    {
        var userData = (UserData*)NativeMemory.Alloc((nuint)sizeof(UserData));
        if (callback is not null) {
            userData->callbackHandle = GCHandle.Alloc(callback);
        }
        var label = utf8Label.AsSpan();
        if (!label.IsEmpty) {
            var len = label.Length;
            userData->label = (byte*)NativeMemory.Alloc((nuint)len + 1);
            label.CopyTo(new Span<byte>(userData->label, len));
            userData->label[len] = 0;
        }
        return userData;
    }

    internal static void Free(UserData* userData)
    {
        userData->callbackHandle.Free();
        NativeMemory.Free(userData->label);
        NativeMemory.Free(userData);
    }
}