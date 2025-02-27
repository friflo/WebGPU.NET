using Friflo.Engine.ECS;
using ImGuiNET;

namespace Friflo.ImGuiNet;

internal class ComponentValueDrawer : GenericDrawer
{
    private readonly    ComponentFieldDrawer    fieldDrawer;
    private readonly    ComponentType           componentType;

    public override     string                  ToString() => componentType.Type.Name;

    internal ComponentValueDrawer(ComponentType componentType, ComponentFieldDrawer fieldDrawer) {
        this.componentType = componentType;
        this.fieldDrawer   = fieldDrawer;
    }
    

    protected internal override void DrawComponent(DrawComponent context)
    {
        var command = InspectorACommand.None;
        ImGui.PushID(context.entityContext.widgetId++);
        context.ComponentLabel(componentType.Type.Name);
        var component = EntityUtils.GetEntityComponent(context.entityContext.entity, componentType);
        ImGui.SetNextItemWidth(context.entityContext.valueWidth);
        var fieldContext = new DrawField {
            entityContext   = context.entityContext,
            fieldDrawer     = fieldDrawer,
            component       = component
        };
        fieldDrawer.typeDrawer.DrawField(fieldContext);
        
        if (EntityInspector.MorePopup("single_more")) {
            if (ImGui.MenuItem("Add Explorer column")) {
                context.explorer.AddComponentFieldDrawer(fieldDrawer);
            }
            ImGui.Separator();
            if (ImGui.MenuItem("Remove Component")) {
                command = InspectorACommand.RemoveComponent;
            }
            ImGui.EndPopup();
        }
        ImGui.PopID();
        if (command == InspectorACommand.RemoveComponent) {
            EntityUtils.RemoveEntityComponent(context.entityContext.entity, componentType);   
        }
    }
}

