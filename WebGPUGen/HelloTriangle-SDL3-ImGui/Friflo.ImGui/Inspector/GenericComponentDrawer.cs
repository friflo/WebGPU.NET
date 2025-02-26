using System;
using System.Collections.Generic;
using System.Reflection;
using Friflo.Engine.ECS;
using ImGuiNET;


namespace Friflo.ImGuiNet;

internal struct ComponentField
{
    internal FieldInfo      fieldInfo;
    internal FieldDrawer    fieldDrawer;
    internal ComponentType  componentType;

    public override string ToString() => fieldInfo.Name;
}

internal class GenericComponentDrawer
{
    private readonly    Type                type;
    private readonly    ComponentField[]    fields;
    private readonly    ComponentType       componentType;
    private             bool                treeNode = true;

    public override string ToString() => type.Name;

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
        var componentFields = new ComponentField[fields.Count];
        for (int n = 0; n < fields.Count; n++) {
            var fieldInfo = fields[n];
            FieldDrawer.Map.TryGetValue(fieldInfo.FieldType, out var componentField);
            componentFields[n] = new ComponentField {
                fieldInfo       = fieldInfo,
                fieldDrawer     = componentField,
                componentType   = componentType
            };
        }
        var drawer = new GenericComponentDrawer(componentType, componentFields);
        Controls.Add(type, drawer);
        return drawer;
    }
    
    private GenericComponentDrawer(ComponentType componentType, ComponentField[] fields) {
        this.componentType  = componentType;
        this.fields         = fields;
    }
    
#pragma warning disable CS0618 // Type or member is obsolete
    internal  void DrawComponent(DrawComponent context)
    {
        ImGui.SetNextItemOpen(treeNode);
        treeNode = ImGui.TreeNode(componentType.Type.Name);
        if (treeNode) {
            var component = EntityUtils.GetEntityComponent(context.entityContext.entity, componentType);
            foreach (var field in fields) {
                ImGui.Text(field.fieldInfo.Name);
                ImGui.SameLine(context.entityContext.valueStart);
                ImGui.SetNextItemWidth(context.entityContext.valueWidth);
                
                var fieldContext = new DrawField {
                    entityContext   = context.entityContext,
                    componentField  = field,
                    component       = component
                };
                ImGui.PushID(context.entityContext.widgetId++);
                field.fieldDrawer.DrawField(fieldContext);
                ImGui.PopID();
            }
            ImGui.TreePop();
        }
    }
#pragma warning restore CS0618 // Type or member is obsolete
}

