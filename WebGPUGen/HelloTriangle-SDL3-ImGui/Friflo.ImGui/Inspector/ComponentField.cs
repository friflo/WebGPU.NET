using System;
using System.Collections.Generic;
using System.Numerics;
using Friflo.Engine.ECS;
using ImGuiNET;

namespace Friflo.ImGuiNet;

internal struct FieldContext {
    internal    EntityContext   entityContext;
    internal    GenericField    genericField;
    internal    EntityComponent entityComponent;
    internal    object          component;
    
    internal string             Name =>  genericField.fieldInfo.Name;
    
    internal object GetValue() {
        return genericField.fieldInfo.GetValue(component);
    }
    
    internal void SetValue(object fieldValue) {
        genericField.fieldInfo.SetValue(component, fieldValue);
        EntityUtils.AddEntityComponentValue(entityContext.entity, entityComponent.Type, component);
    }
}

internal abstract class ComponentField
{
    internal static readonly Dictionary<Type, ComponentField> Fields = new() {
        { typeof(string),       new StringField()   },
        { typeof(byte),         new ByteField()      },
        { typeof(int),          new IntField()      },
        { typeof(Vector3),      new Vector3Field()  } 
    };
    
    public  abstract void Draw(FieldContext context);
}

internal class StringField : ComponentField
{
    public  override void Draw(FieldContext context) {
        var value = (string)context.GetValue();
        if (ImGui.InputText("##field", ref value, 100)) {
            context.SetValue(value);
        }
    }
}

#region integer

internal class ByteField : ComponentField
{
    public  override void Draw(FieldContext context) {
        int value = (byte)context.GetValue();
        if (ImGui.InputInt("##field", ref value, 0, 0)) {
            context.SetValue((byte)value);
        }
    }
}

internal class IntField : ComponentField
{
    public  override void Draw(FieldContext context) {
        var value = (int)context.GetValue();
        if (ImGui.InputInt("##field", ref value, 0, 0)) {
            context.SetValue(value);
        }
    }
}

#endregion

internal class Vector3Field : ComponentField
{
    public  override void Draw(FieldContext context) {
        var value = (Vector3)context.GetValue();
        if (ImGui.InputFloat3("##field", ref value)) {
            context.SetValue(value);
        }
    }
}
