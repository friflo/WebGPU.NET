using System;
using System.Collections.Generic;
using System.Reflection;
using Friflo.Engine.ECS;
using ImGuiNET;


namespace Friflo.ImGuiNet;

internal struct ComponentFieldDrawer
{
    internal FieldInfo      fieldInfo;
    internal TypeDrawer     typeDrawer;
    internal ComponentType  componentType;

    public override string ToString() => fieldInfo.Name;
}

enum InspectorACommand
{
    None = 0,
    RemoveComponent
}

internal class GenericComponentDrawer
{
    private readonly    ComponentFieldDrawer[]  fieldDrawers;
    private readonly    ComponentType           componentType;
    private             bool                    treeNode = true;

    public override     string                  ToString() => componentType.Type.Name;

    internal static readonly Dictionary<Type, GenericComponentDrawer> Controls = new();
    
    internal static  GenericComponentDrawer Create (ComponentType componentType)
    {
        var type    = componentType.Type;
        var fields  = new List<FieldInfo>();
        var members = type.GetMembers();
        foreach (var member in members) {
            if (member is FieldInfo field) {
                fields.Add(field);
            }
        }
        var fieldDrawers = new ComponentFieldDrawer[fields.Count];
        for (int n = 0; n < fields.Count; n++) {
            var fieldInfo = fields[n];
            var typeDrawer = TypeDrawer.GetTypeDrawer(fieldInfo.FieldType);
            fieldDrawers[n] = new ComponentFieldDrawer {
                fieldInfo       = fieldInfo,
                typeDrawer      = typeDrawer,
                componentType   = componentType
            };
        }
        var drawer = new GenericComponentDrawer(componentType, fieldDrawers);
        Controls.Add(type, drawer);
        return drawer;
    }
    
    private GenericComponentDrawer(ComponentType componentType, ComponentFieldDrawer[] fieldDrawers) {
        this.componentType  = componentType;
        this.fieldDrawers   = fieldDrawers;
    }
    
#pragma warning disable CS0618 // Type or member is obsolete
    internal  void DrawComponent(DrawComponent context)
    {
        ImGui.SetNextItemOpen(treeNode);
        treeNode = ImGui.TreeNode(componentType.Type.Name);
        var command = InspectorACommand.None;
        if (EntityInspector.MorePopup("generic_more")) {
            if (ImGui.MenuItem("Add Explorer columns")) {
                foreach (var fieldDrawer in fieldDrawers) {
                    context.explorer.AddComponentFieldDrawer(fieldDrawer);
                }
            }
            ImGui.Separator();
            if (ImGui.MenuItem("Remove Component")) {
                command = InspectorACommand.RemoveComponent;
            }
            ImGui.EndPopup();
        }
        if (treeNode) {
            var component = EntityUtils.GetEntityComponent(context.entityContext.entity, componentType);
            foreach (var fieldDrawer in fieldDrawers) {
                ImGui.Text(fieldDrawer.fieldInfo.Name);
                ImGui.SameLine(context.entityContext.valueStart);
                ImGui.SetNextItemWidth(context.entityContext.valueWidth);
                
                var fieldContext = new DrawField {
                    entityContext   = context.entityContext,
                    fieldDrawer     = fieldDrawer,
                    component       = component
                };
                ImGui.PushID(context.entityContext.widgetId++);
                fieldDrawer.typeDrawer.DrawField(fieldContext);

                if (EntityInspector.MorePopup("field_more")) {
                    if (ImGui.MenuItem("Add Explorer column")) {
                        context.explorer.AddComponentFieldDrawer(fieldDrawer);
                    }
                    ImGui.EndPopup();
                }
                ImGui.PopID();
            }
            ImGui.TreePop();
        }
        if (command == InspectorACommand.RemoveComponent) {
            EntityUtils.RemoveEntityComponent(context.entityContext.entity, componentType);   
        }
    }
#pragma warning restore CS0618 // Type or member is obsolete
}

