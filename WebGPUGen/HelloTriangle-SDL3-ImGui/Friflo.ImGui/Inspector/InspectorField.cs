using System;
using System.Collections.Generic;
using System.Numerics;
using Friflo.Engine.ECS;
using ImGuiNET;

namespace Friflo.ImGuiNet;

internal struct FieldContext {
    internal Entity         entity;
    internal GenericField   genericField;
    internal object         component;
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
        var value = (string)context.genericField.fieldInfo.GetValue(context.component);
        if (ImGui.InputText(context.genericField.fieldInfo.Name, ref value, 100)) {
            context.genericField.fieldInfo.SetValue(context.component, value);
        }
    }
}

internal class Vector3Field : InspectorField
{
    public  override void Draw(FieldContext context) {
        var value = (Vector3)context.genericField.fieldInfo.GetValue(context.component)!;
        if (ImGui.InputFloat3(context.genericField.fieldInfo.Name, ref value)) {
            context.genericField.fieldInfo.SetValue(context.component, value);
        }
    }
}
