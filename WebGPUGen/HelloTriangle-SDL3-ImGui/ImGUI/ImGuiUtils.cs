using System;
using System.Text;
using static SDL.SDL3;

namespace SDLIM;

public static class ImGuiUtils
{
    public static void IM_ASSERT(bool condition, string message) {
        if (!condition) {
            throw new InvalidOperationException(message);
        }
    }
    
    public static void IMGUI_CHECKVERSION() {}
    
    public static unsafe byte* SDL_strdup(string value) {
        var len = Encoding.UTF8.GetByteCount(value);
        var ptr = (byte*)SDL_calloc((uint)len + 1, 1);
        Encoding.UTF8.GetBytes(value.AsSpan(), new Span<byte>(ptr, len));
        ptr[len] = 0;
        return ptr;
    }
}