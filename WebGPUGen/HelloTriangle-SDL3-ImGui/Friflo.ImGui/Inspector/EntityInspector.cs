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
            var type = component.Type.Type;
            if (InspectorControl.Controls.TryGetValue(type, out var control))
            {
                var context = new ComponentContext {entity = entity, component = component };
                control.Draw(context);
                continue;
            }
            {
                if (!GenericComponent.Controls.TryGetValue(type, out var genericControl)) {
                    genericControl = GenericComponent.Create(type);
                }
                var context = new ComponentContext {entity = entity, component = component };
                genericControl.Draw(context);
            }
        }
    }
}