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

public abstract class GenericDrawer
{
    protected internal  abstract void DrawComponent(DrawComponent context);
}

internal class GenericComponentDrawer : GenericDrawer
{
    private readonly    ComponentFieldDrawer[]  fieldDrawers;
    private readonly    ComponentType           componentType;
    private             bool                    treeNode = true;

    public override     string                  ToString() => componentType.Type.Name;

    internal static readonly Dictionary<Type, GenericDrawer> Controls = new();
    
    internal static  GenericDrawer Create (ComponentType componentType)
    {
        var type    = componentType.Type;
        var fields  = new List<FieldInfo>();
        var members = type.GetMembers();
        foreach (var member in members) {
            if (member is FieldInfo field) {
                fields.Add(field);
            }
        }
        GenericDrawer drawer;
        if (fields.Count == 1) {
            var fieldInfo = fields[0];
            var domain      = GetFieldDomain(fieldInfo.CustomAttributes);
            var typeDrawer  = TypeDrawer.GetTypeDrawer(fieldInfo.FieldType, domain);
            var fieldDrawer = new ComponentFieldDrawer {
                fieldInfo       = fieldInfo,
                typeDrawer      = typeDrawer,
                componentType   = componentType
            };
            drawer =  new ComponentValueDrawer(componentType, fieldDrawer);
        } else {
            var fieldDrawers = new ComponentFieldDrawer[fields.Count];
            for (int n = 0; n < fields.Count; n++) {
                var fieldInfo   = fields[n];
                var domain      = GetFieldDomain(fieldInfo.CustomAttributes);
                var typeDrawer  = TypeDrawer.GetTypeDrawer(fieldInfo.FieldType, domain);
                fieldDrawers[n] = new ComponentFieldDrawer {
                    fieldInfo       = fieldInfo,
                    typeDrawer      = typeDrawer,
                    componentType   = componentType
                };
            }
            drawer = new GenericComponentDrawer(componentType, fieldDrawers);
        }
        Controls.Add(type, drawer);
        return drawer;
    }
    
    private static string GetFieldDomain(IEnumerable<CustomAttributeData> attributes) {
        foreach (var attribute in attributes) {
            if (attribute.AttributeType == typeof(DomainAttribute)) {
                return (string)attribute.ConstructorArguments[0].Value;
            }
        }
        return null;
    }
    
    private GenericComponentDrawer(ComponentType componentType, ComponentFieldDrawer[] fieldDrawers) {
        this.componentType  = componentType;
        this.fieldDrawers   = fieldDrawers;
    }
    
    protected internal override void DrawComponent(DrawComponent context)
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
}

