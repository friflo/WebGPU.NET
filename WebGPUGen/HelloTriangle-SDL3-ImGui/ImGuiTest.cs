using ImGuiNET;

namespace HelloTriangle;

static class ImGuiTest
{
    private static float _value = 100f;
    private static float _value2 = 100f;
    private static float _value3 = 100f;
    private static bool b1;

    public static void Draw()
    {
        ImGui.SetNextWindowPos(new(10, 10), ImGuiCond.Once);
        ImGui.SetNextWindowSize(new(500, 300), ImGuiCond.Once);
        ImGui.SetNextWindowBgAlpha(0.5f);
        ImGui.Begin("ImGuiTest");
        ImGui.InputFloat("Value", ref _value);
        ImGui.InputFloat("Value2", ref _value2);
        ImGui.InputFloat("Value3", ref _value3);
        ImGui.Checkbox("b1", ref b1);
        ImGui.End();
            
        ImGui.SetNextWindowPos(new(600, 10), ImGuiCond.Once);
        ImGui.SetNextWindowSize(new(500, 200), ImGuiCond.Once);
        ImGui.SetNextWindowBgAlpha(0.5f);
        ImGui.Begin("Other");
        ImGui.Checkbox("b1", ref b1);
        ImGui.End();
    }
}