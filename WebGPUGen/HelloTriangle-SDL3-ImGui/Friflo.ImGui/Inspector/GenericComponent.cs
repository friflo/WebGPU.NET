using System;
using System.Collections.Generic;
using System.Reflection;
using Friflo.Engine.ECS;
using ImGuiNET;


namespace Friflo.ImGuiNet;

internal struct GenericField
{
    internal FieldInfo      fieldInfo;
    internal FieldDrawer fieldDrawer;
    internal ComponentType  componentType;

    public override string ToString() => fieldInfo.Name;
}

internal class GenericComponent
{
    private readonly    Type            type;
    private readonly    GenericField[]  fields;
    private             bool            treeNode = true;

    public override string ToString() => type.Name;

    internal static readonly Dictionary<Type, GenericComponent> Controls = new();
    
    internal static  GenericComponent Create (ComponentType componentType)
    {
        var type    = componentType.Type;
        var fields  = new List<FieldInfo>();
        var members = type.GetMembers();
        foreach (var member in members) {
            if (member is FieldInfo field) {
                fields.Add(field);
            }
        }
        var genericFields = new GenericField[fields.Count];
        for (int n = 0; n < fields.Count; n++) {
            var fieldInfo = fields[n];
            FieldDrawer.Map.TryGetValue(fieldInfo.FieldType, out var componentField);
            genericFields[n] = new GenericField {
                fieldInfo       = fieldInfo,
                fieldDrawer  = componentField,
                componentType   = componentType
            };
        }
        var genericControl = new GenericComponent(type, genericFields);
        Controls.Add(type, genericControl);
        return genericControl;
    }
    
    private GenericComponent(Type type, GenericField[] fields) {
        this.type   = type;
        this.fields = fields;
    }
    
#pragma warning disable CS0618 // Type or member is obsolete
    internal  void InspectorDrawComponent(ComponentContext context)
    {
        ImGui.SetNextItemOpen(treeNode);
        treeNode = ImGui.TreeNode(context.component.Type.Name);
        if (treeNode) {
            var component = context.component.Value;
            foreach (var field in fields) {
                ImGui.Text(field.fieldInfo.Name);
                ImGui.SameLine(context.entityContext.valueStart);
                ImGui.SetNextItemWidth(context.entityContext.valueWidth);
                
                var fieldContext = new FieldContext {
                    entityContext   = context.entityContext,
                    genericField    = field,
                    component       = component
                };
                ImGui.PushID(context.entityContext.widgetId++);
                field.fieldDrawer.Draw(fieldContext);
                ImGui.PopID();
            }
            ImGui.TreePop();
        }
    }
#pragma warning restore CS0618 // Type or member is obsolete
}

