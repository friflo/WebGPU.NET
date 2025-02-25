using System;
using System.Collections.Generic;
using Friflo.Engine.ECS;
using ImGuiNET;

namespace Friflo.ImGuiNet;


internal struct ComponentContext {
    internal Entity         entity;
    internal ComponentType  componentType;
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
        var component = context.entity.GetComponent<Position>();
        if (ImGui.InputFloat3("Position", ref component.value)) {
            EntityUtils.AddEntityComponentValue(context.entity, context.componentType, component);
        }
    }
}

internal class NameControl : InspectorControl
{
    public  override void Draw(ComponentContext context) {
        var component = context.entity.GetComponent<EntityName>();
        if (ImGui.InputText("Name", ref component.value, 100)) {
            EntityUtils.AddEntityComponentValue(context.entity, context.componentType, component);
        }
    }
}