using System;
using System.Collections.Generic;
using Friflo.Engine.ECS;
using ImGuiNET;

namespace Friflo.ImGuiNet;


internal struct DrawComponent {
    internal    EntityContext   entityContext;
    internal    EntityComponent component;
    
    public void ComponentLabel(string label) {
        ImGui.Text(label);
        ImGui.SameLine(entityContext.valueStart);
        ImGui.SetNextItemWidth(entityContext.valueWidth); 
    }
    
    public void UpdateComponent(object value) {
        EntityUtils.AddEntityComponentValue(entityContext.entity, component.Type, value);
    }

}

internal abstract class ComponentDrawer
{
    internal static readonly Dictionary<Type, ComponentDrawer> Map = new() {
        { typeof(Position),     new PositionDrawer()   },
        { typeof(EntityName),   new NameDrawer()       }
    };
    
    public  abstract void DrawComponent(DrawComponent context);
}

internal class PositionDrawer : ComponentDrawer
{
    public  override void DrawComponent(DrawComponent context) {
        var component = context.entityContext.entity.GetComponent<Position>();
        if (ImGui.InputFloat3("##field", ref component.value)) {
            context.UpdateComponent(component);
        }
    }
}

internal class NameDrawer : ComponentDrawer
{
    public  override void DrawComponent(DrawComponent context) {
        var component = context.entityContext.entity.GetComponent<EntityName>();
        if (ImGui.InputText("##field", ref component.value, 100)) {
            context.UpdateComponent(component);
        }
    }
}