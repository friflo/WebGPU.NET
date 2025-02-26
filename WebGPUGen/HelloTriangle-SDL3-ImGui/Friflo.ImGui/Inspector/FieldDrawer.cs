using System;
using System.Collections.Generic;
using System.Numerics;
using Friflo.Engine.ECS;
using ImGuiNET;

namespace Friflo.ImGuiNet;

internal struct FieldContext {
    internal    EntityContext   entityContext;
    internal    ComponentField  componentField;
    internal    object          component;
    
    internal string             Name =>  componentField.fieldInfo.Name;
    
    internal object GetValue() {
        return componentField.fieldInfo.GetValue(component);
    }
    
    internal void SetValue(object fieldValue) {
        componentField.fieldInfo.SetValue(component, fieldValue);
        EntityUtils.AddEntityComponentValue(entityContext.entity, componentField.componentType, component);
    }
}

internal abstract class FieldDrawer
{
    internal static readonly Dictionary<Type, FieldDrawer> Map = new() {
        { typeof(string),       new StringDrawer()     },
        { typeof(byte),         new ByteDrawer()       },
        { typeof(int),          new IntDrawer()        },
        { typeof(Vector3),      new Vector3Drawer()    } 
    };
    
    public  abstract void Draw(FieldContext context);
}

internal class StringDrawer : FieldDrawer
{
    public  override void Draw(FieldContext context) {
        var value = (string)context.GetValue();
        if (ImGui.InputText("##field", ref value, 100)) {
            context.SetValue(value);
        }
    }
}

#region integer

internal class ByteDrawer : FieldDrawer
{
    public  override void Draw(FieldContext context) {
        int value = (byte)context.GetValue();
        if (ImGui.InputInt("##field", ref value, 0, 0)) {
            context.SetValue((byte)value);
        }
    }
}

internal class IntDrawer : FieldDrawer
{
    public  override void Draw(FieldContext context) {
        var value = (int)context.GetValue();
        if (ImGui.InputInt("##field", ref value, 0, 0)) {
            context.SetValue(value);
        }
    }
}

#endregion

internal class Vector3Drawer : FieldDrawer
{
    public  override void Draw(FieldContext context) {
        var value = (Vector3)context.GetValue();
        if (ImGui.InputFloat3("##field", ref value)) {
            context.SetValue(value);
        }
    }
}
