using System;

namespace SDLIM;

public static class ImGuiUtils
{
    public static void IM_ASSERT(bool condition, string message) {
        if (!condition) {
            throw new InvalidOperationException(message);
        }
    }
    
    public static void IMGUI_CHECKVERSION() {}
}