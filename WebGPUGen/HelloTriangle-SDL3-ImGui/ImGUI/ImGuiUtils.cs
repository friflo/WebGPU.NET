using System;
using System.Numerics;
using System.Text;
using ImGuiNET;
using SDL;
using static SDL.SDL3;

namespace SDLIM;

internal static class ImGuiUtils
{
    // --- imgui_impl_sdl3.cpp
    internal static void IM_ASSERT(bool condition, string message) {
        if (!condition) {
            throw new InvalidOperationException(message);
        }
    }
    
    internal static void IMGUI_CHECKVERSION() {}
    
    internal static unsafe byte* SDL_strdup(string value) {
        var len = Encoding.UTF8.GetByteCount(value);
        var ptr = (byte*)SDL_calloc((uint)len + 1, 1);
        Encoding.UTF8.GetBytes(value.AsSpan(), new Span<byte>(ptr, len));
        ptr[len] = 0;
        return ptr;
    }
    
    [System.Runtime.CompilerServices.InlineArray((int)ImGuiMouseCursor.COUNT)]
    internal unsafe struct Cursors
    {
        // structs with the InlineArray attribute must contain EXACTLY one member.
        private IntPtr element;
        
        /* internal SDL_Cursor* this[int i] {
            get { return (SDL_Cursor*)element + i; }
            set { *((IntPtr*)element + i) = (IntPtr)value; }
        }*/
    }
    
    [System.Runtime.CompilerServices.InlineArray(10)]
    internal unsafe struct Gamepads
    {
        // structs with the InlineArray attribute must contain EXACTLY one member.
        private IntPtr element;
        
        // internal SDL_Gamepad* this[int i] => (SDL_Gamepad*)element + i;
    }

    // --- imgui_impl_wgpu.cpp
    // MEMALIGN(_SIZE,_ALIGN)        (((_SIZE) + ((_ALIGN) - 1)) & ~((_ALIGN) - 1))
    internal static ulong MEMALIGN(long size, int align)
    {
        return (ulong)((size + (align - 1)) & ~(align - 1));
    }
    
    internal  static unsafe void memcpy(void* dest, void* src, int size) {
        Buffer.MemoryCopy(src, dest, size, size); 
    }
    
}