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
        entityContext.valueWidth = ImGui.GetWindowWidth() - 320;
        
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
                ImGui.PopID();
                continue;
            }
            {
                if (!GenericComponentDrawer.Controls.TryGetValue(type, out var genericDrawer)) {
                    genericDrawer = GenericComponentDrawer.Create(component.Type);
                }
                var context = new DrawComponent {entityContext = entityContext };
                genericDrawer.DrawComponent(context);
            }
        }
    }
}