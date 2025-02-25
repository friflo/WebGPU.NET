using System;
using System.Collections.Generic;
using System.Reflection;


namespace Friflo.ImGuiNet;

internal struct GenericField
{
    internal FieldInfo       fieldInfo;
    internal InspectorField  inspectorField;

    public override string ToString() => fieldInfo.Name;
}

internal class GenericComponent
{
    internal readonly Type              type;
    internal readonly GenericField[]    fields;

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
    
    internal  void Draw(ComponentContext context) {
        
        foreach (var field in fields) {
            var fieldContext = new FieldContext {
                entity          = context.entity,
                genericField    = field,
            };
            field.inspectorField.Draw(fieldContext);    
        }
    }
}

