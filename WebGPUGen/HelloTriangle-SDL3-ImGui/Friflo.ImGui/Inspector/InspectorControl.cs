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

internal abstract class InspectorControl
{
    internal static readonly Dictionary<Type, InspectorControl> Controls = new() {
        { typeof(Position),     new PositionControl()   },
        { typeof(EntityName),   new NameControl()       } 
    };
    
    public  abstract void Draw(ComponentContext context);
}

internal class PositionControl : InspectorControl
{
    public  override void Draw(ComponentContext context) {
        context.ComponentLabel("Position");
        var component = context.entityContext.entity.GetComponent<Position>();
        if (ImGui.InputFloat3("##field", ref component.value)) {
            EntityUtils.AddEntityComponentValue(context.entityContext.entity, context.component.Type, component);
        }
    }
}

internal class NameControl : InspectorControl
{
    public  override void Draw(ComponentContext context) {
        context.ComponentLabel("Name");
        var component = context.entityContext.entity.GetComponent<EntityName>();
        if (ImGui.InputText("##field", ref component.value, 100)) {
            EntityUtils.AddEntityComponentValue(context.entityContext.entity, context.component.Type, component);
        }
    }
}