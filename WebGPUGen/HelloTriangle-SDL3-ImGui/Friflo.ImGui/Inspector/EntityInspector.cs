using System.Collections.Generic;
using Friflo.Engine.ECS;
using ImGuiNET;

namespace Friflo.ImGuiNet;

public class EntityInspector
{
    private QueryExplorer    explorer;
    
    public EntityInspector(QueryExplorer queryExplorer) {
        this.explorer = queryExplorer;
    }
    
    internal void Draw()
    {
        var id = EcsUtils.IntAsSpan(explorer.focusedEntity.Id);
        ImGui.LabelText(id, "id");
        
        string abc = "abc";
        ImGui.InputText("abc", ref abc, 100);
        
        string xyz = "xyz";
        ImGui.InputText("xyz", ref xyz, 100);
    }
}