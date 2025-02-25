using System;
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
        var entity = explorer.focusedEntity;
        if (entity.IsNull) {
            return;
        }
        var id = EcsUtils.IntAsSpan(entity.Id);
        ImGui.LabelText(id, "id");

        foreach (var component in entity.Components)
        {
            if (InspectorControl.Controls.TryGetValue(component.Type.Type, out var control))
            {
                var context = new ComponentContext {entity = entity, componentType = component.Type };
                control.Draw(context);
            }
        }
    }
}