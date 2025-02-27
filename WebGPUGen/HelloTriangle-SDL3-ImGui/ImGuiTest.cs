using System.Numerics;
using Friflo.ImGuiNet;
using ImGuiNET;

namespace HelloTriangle;

static class ImGuiTest
{
    private static float _value = 100f;
    private static float _value2 = 100f;
    private static float _value3 = 100f;
    private static bool b1;
    private static bool treeNode = true;
    private static Vector3 color3 = new Vector3(1,0,0);
    


    public static void Draw(QueryExplorer explorer, EntityInspector inspector)
    {
        var bgAlpha = 0.95f;
        ImGui.SetNextWindowPos(new(10, 10), ImGuiCond.Once);
        ImGui.SetNextWindowSize(new(500, 300), ImGuiCond.Once);
        ImGui.SetNextWindowBgAlpha(bgAlpha);
        ImGui.Begin("ImGuiTest");
        
        ImGui.Text("Value 1");
        ImGui.SameLine(250);
        ImGui.InputFloat("##flt1", ref _value, 0, 0, null);
        
        ImGui.ColorEdit3("##color3", ref color3);
        
        ImGui.Text("Value 2");
        ImGui.SameLine(250);
        ImGui.InputFloat("##flt2", ref _value2);
        
        ImGui.SetNextItemOpen(treeNode);
        treeNode = ImGui.TreeNode("tree node");
        if (treeNode) {
            ImGui.Text("Value 3");
            ImGui.SameLine(250);
            ImGui.InputFloat("##flt3", ref _value3);
            
            ImGui.Text("Checkbox");
            ImGui.SameLine(250);
            ImGui.Checkbox("##b1", ref b1);
            ImGui.TreePop();
        }
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
        ImGui.SetNextWindowSize(new(700, 500), ImGuiCond.Once);
        ImGui.SetNextWindowBgAlpha(bgAlpha);
        ImGui.Begin("Inspector");
        EcsGui.EntityInspector(inspector);
        ImGui.End();
    }
}