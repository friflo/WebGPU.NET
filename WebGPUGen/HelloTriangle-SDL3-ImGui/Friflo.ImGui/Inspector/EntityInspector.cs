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
    private QueryExplorer   explorer;
    private EntityContext   entityContext = new();
    
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
        entityContext.valueWidth = ImGui.GetWindowWidth() - 320;
        
        var id = entity.Id; // EcsUtils.IntAsSpan(entity.Id);
        
        ImGui.Text("id");
        ImGui.SameLine(entityContext.valueStart);
        ImGui.SetNextItemWidth(entityContext.valueWidth);

        ImGui.InputInt("##id", ref id, 0, 0, ImGuiInputTextFlags.ReadOnly);



        foreach (var component in entity.Components)
        {
            var type = component.Type.Type;
            if (ComponentControl.Controls.TryGetValue(type, out var control))
            {
                var context = new ComponentContext { entityContext = entityContext, component = component };
                ImGui.PushID(entityContext.widgetId++);
                context.ComponentLabel(component.Type.Name);
                control.DrawComponent(context);
                ImGui.PopID();
                continue;
            }
            {
                if (!GenericComponent.Controls.TryGetValue(type, out var genericControl)) {
                    genericControl = GenericComponent.Create(type);
                }
                var context = new ComponentContext {entityContext = entityContext, component = component };
                genericControl.InspectorDrawComponent(context);
            }
        }
    }
}