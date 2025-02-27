using System.Numerics;
using Friflo.Engine.ECS;
using ImGuiNET;

namespace Friflo.ImGuiNet;

public class EntityContext
{
    public  Entity  entity;
    public  int     widgetId;
    public  float   valueStart;
    public  float   valueWidth;
}

public class EntityInspector
{
    private             QueryExplorer   explorer;
    private readonly    EntityContext   entityContext = new();
    

    
    public EntityInspector(QueryExplorer queryExplorer) {
        this.explorer = queryExplorer;
    }
    
    internal void Draw()
    {
        var entity = explorer.focusedEntity;
        if (entity.IsNull) {
            return;
        }
        entityContext.widgetId = 1;
        entityContext.entity = entity;
        entityContext.valueStart = 300;
        entityContext.valueWidth = ImGui.GetWindowWidth() - 360;
        
        var id = entity.Id; // EcsUtils.IntAsSpan(entity.Id);
        
        ImGui.Text("id");
        ImGui.SameLine(entityContext.valueStart);
        ImGui.SetNextItemWidth(entityContext.valueWidth);

        ImGui.InputInt("##id", ref id, 0, 0, ImGuiInputTextFlags.ReadOnly);

        foreach (var component in entity.Components)
        {
            var type = component.Type.Type;
            if (ComponentDrawer.Map.TryGetValue(type, out var drawer))
            {
                var context = new DrawComponent { entityContext = entityContext };
                ImGui.PushID(entityContext.widgetId++);
                context.ComponentLabel(component.Type.Name);
                drawer.DrawComponent(context);
                if (MorePopup("component_more")) {
                    if (ImGui.MenuItem("Add Explorer column")) {
                        explorer.AddComponentDrawer(type);
                    }
                    ImGui.Separator();
                    if (ImGui.MenuItem("Remove Component")) {
                        EntityUtils.RemoveEntityComponent(entity, component.Type);
                    }
                    ImGui.EndPopup();
                }
                ImGui.PopID();
                continue;
            }
            {
                if (!GenericComponentDrawer.Controls.TryGetValue(type, out var genericDrawer)) {
                    genericDrawer = GenericComponentDrawer.Create(component.Type);
                }
                var context = new DrawComponent { entityContext = entityContext, explorer = explorer };
                genericDrawer.DrawComponent(context);
            }
        }
        ImGui.Text("");
    }
    
    internal static bool MorePopup(string name)
    {
        var morePos = ImGui.GetWindowWidth() - 54;
        ImGui.SameLine(morePos);
        ImGui.PushStyleColor(ImGuiCol.Button, new Vector4(1,0,0,0));
        ImGui.PushStyleColor(ImGuiCol.Text, new Vector4(0.5f,0.5f,0.5f,1));
        if (ImGui.Button("...")) {
            ImGui.OpenPopup(name);
        }
        ImGui.PopStyleColor(2);
        return ImGui.BeginPopup(name, ImGuiWindowFlags.None);
    }
}