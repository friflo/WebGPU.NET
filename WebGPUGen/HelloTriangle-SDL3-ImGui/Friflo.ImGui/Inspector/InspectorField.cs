using System;
using System.Collections.Generic;
using System.Numerics;
using Friflo.Engine.ECS;
using ImGuiNET;

namespace Friflo.ImGuiNet;

internal struct FieldContext {
    internal Entity             entity;
    internal GenericField       genericField;
    internal EntityComponent    entityComponent; 
    internal object             component;
    
    internal string             Name =>  genericField.fieldInfo.Name;
    
    internal object GetValue() {
        return genericField.fieldInfo.GetValue(component);
    }
    
    internal void SetValue(object fieldValue) {
        genericField.fieldInfo.SetValue(component, fieldValue);
        EntityUtils.AddEntityComponentValue(entity, entityComponent.Type, component);
    }
}

internal abstract class InspectorField
{
    internal static readonly Dictionary<Type, InspectorField> Fields = new() {
        { typeof(string),       new StringField()   },
        { typeof(Vector3),      new Vector3Field()  } 
    };
    
    public  abstract void Draw(FieldContext context);
}

internal class StringField : InspectorField
{
    public  override void Draw(FieldContext context) {
        var value = (string)context.GetValue();
        if (ImGui.InputText(context.Name, ref value, 100)) {
            context.SetValue(value);
        }
    }
}

internal class Vector3Field : InspectorField
{
    public  override void Draw(FieldContext context) {
        var value = (Vector3)context.GetValue();
        if (ImGui.InputFloat3(context.Name, ref value)) {
            context.SetValue(value);
        }
    }
}
