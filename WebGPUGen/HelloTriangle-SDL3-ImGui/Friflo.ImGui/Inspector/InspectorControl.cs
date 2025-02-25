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
        { typeof(Position), new InspectorPosition() }
    };
    
    public  abstract void Draw(ComponentContext context);
}

internal class InspectorPosition : InspectorControl
{
    public  override void Draw(ComponentContext context) {
        var position = context.entity.GetComponent<Position>();
        if (ImGui.InputFloat3("Position", ref position.value)) {
            EntityUtils.AddEntityComponentValue(context.entity, context.componentType, position);
        }
    }
}