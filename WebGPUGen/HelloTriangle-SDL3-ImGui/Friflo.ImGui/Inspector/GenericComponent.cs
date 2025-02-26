using System;
using System.Collections.Generic;
using System.Reflection;
using ImGuiNET;


namespace Friflo.ImGuiNet;

internal struct GenericField
{
    internal FieldInfo       fieldInfo;
    internal InspectorField  inspectorField;

    public override string ToString() => fieldInfo.Name;
}

internal class GenericComponent
{
    private readonly    Type            type;
    private readonly    GenericField[]  fields;
    private             bool            treeNode = true;

    public override string ToString() => type.Name;

    internal static readonly Dictionary<Type, GenericComponent> Controls = new();
    
    internal static  GenericComponent Create (Type type)
    {
        var fields = new List<FieldInfo>();
        var members = type.GetMembers();
        foreach (var member in members) {
            if (member is FieldInfo field) {
                fields.Add(field);
            }
        }
        var genericFields = new GenericField[fields.Count];
        for (int n = 0; n < fields.Count; n++) {
            var fieldInfo = fields[n];
            InspectorField.Fields.TryGetValue(fieldInfo.FieldType, out var inspectorField);
            genericFields[n] = new GenericField { fieldInfo = fieldInfo, inspectorField = inspectorField };
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
    internal  void Draw(ComponentContext context)
    {
        ImGui.SetNextItemOpen(treeNode);
        treeNode = ImGui.TreeNode(context.component.Type.Name);
        if (treeNode) {
            var component = context.component.Value;
            int id = 0;
            foreach (var field in fields) {
                id++;
                var fieldContext = new FieldContext {
                    entityContext   = context.entityContext,
                    genericField    = field,
                    component       = component,
                    entityComponent = context.component
                };
                ImGui.Text(field.fieldInfo.Name);
                
                ImGui.SameLine(context.entityContext.valueStart);
                ImGui.PushID(context.entityContext.widgetId++);
                ImGui.SetNextItemWidth(context.entityContext.valueWidth);
                field.inspectorField.Draw(fieldContext);
                ImGui.PopID();
            }
            ImGui.TreePop();
        }
    }
#pragma warning restore CS0618 // Type or member is obsolete
}

