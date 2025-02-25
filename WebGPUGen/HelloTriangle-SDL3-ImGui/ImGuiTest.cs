using Friflo.ImGuiNet;
using ImGuiNET;

namespace HelloTriangle;

static class ImGuiTest
{
    private static float _value = 100f;
    private static float _value2 = 100f;
    private static float _value3 = 100f;
    private static bool b1;
    


    public static void Draw(QueryExplorer explorer, EntityInspector inspector)
    {
        var bgAlpha = 0.95f;
        ImGui.SetNextWindowPos(new(10, 10), ImGuiCond.Once);
        ImGui.SetNextWindowSize(new(500, 300), ImGuiCond.Once);
        ImGui.SetNextWindowBgAlpha(bgAlpha);
        ImGui.Begin("ImGuiTest");
        ImGui.InputFloat("Value", ref _value);
        ImGui.InputFloat("Value2", ref _value2);
        ImGui.InputFloat("Value3", ref _value3);
        ImGui.Checkbox("b1", ref b1);
        ImGui.End();
            
        ImGui.SetNextWindowPos(new(600, 10), ImGuiCond.Once);
        ImGui.SetNextWindowSize(new(500, 200), ImGuiCond.Once);
        ImGui.SetNextWindowBgAlpha(bgAlpha);
        ImGui.Begin("Other");
        ImGui.Checkbox("b1", ref b1);
        ImGui.End();
        
        ImGui.SetNextWindowPos(new(10, 400), ImGuiCond.Once);
        ImGui.SetNextWindowSize(new(500, 600), ImGuiCond.Once);
        ImGui.SetNextWindowBgAlpha(bgAlpha);
        ImGui.Begin("Explorer");
        EcsGui.QueryExplorer(explorer);
        ImGui.End();
        
        ImGui.SetNextWindowPos(new(600, 400), ImGuiCond.Once);
        ImGui.SetNextWindowSize(new(500, 500), ImGuiCond.Once);
        ImGui.SetNextWindowBgAlpha(bgAlpha);
        ImGui.Begin("Inspector");
        EcsGui.EntityInspector(inspector);
        ImGui.End();
    }
}