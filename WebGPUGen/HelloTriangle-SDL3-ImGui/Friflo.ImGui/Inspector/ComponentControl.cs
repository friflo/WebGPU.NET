using System;
using System.Collections.Generic;
using Friflo.Engine.ECS;
using ImGuiNET;

namespace Friflo.ImGuiNet;


internal struct ComponentContext {
    internal    EntityContext   entityContext;
    internal    EntityComponent component;
    
    public void ComponentLabel(string label) {
        ImGui.Text(label);
        ImGui.SameLine(entityContext.valueStart);
        ImGui.SetNextItemWidth(entityContext.valueWidth); 
    }

}

internal abstract class ComponentControl
{
    internal static readonly Dictionary<Type, ComponentControl> Controls = new() {
        { typeof(Position),     new PositionControl()   },
        { typeof(EntityName),   new NameControl()       }
    };
    
    public  abstract void DrawComponent(ComponentContext context);
}

internal class PositionControl : ComponentControl
{
    public  override void DrawComponent(ComponentContext context) {
        var component = context.entityContext.entity.GetComponent<Position>();
        if (ImGui.InputFloat3("##field", ref component.value)) {
            EntityUtils.AddEntityComponentValue(context.entityContext.entity, context.component.Type, component);
        }
    }
}

internal class NameControl : ComponentControl
{
    public  override void DrawComponent(ComponentContext context) {
        var component = context.entityContext.entity.GetComponent<EntityName>();
        if (ImGui.InputText("##field", ref component.value, 100)) {
            EntityUtils.AddEntityComponentValue(context.entityContext.entity, context.component.Type, component);
        }
    }
}