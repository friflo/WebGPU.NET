using System;
using System.Collections.Generic;
using System.Numerics;
using Friflo.Engine.ECS;
using ImGuiNET;

namespace Friflo.ImGuiNet;

internal struct DrawField {
    internal    EntityContext           entityContext;
    internal    object                  component;
    internal    ComponentFieldDrawer    fieldDrawer;
    
    internal    string                  Name =>  fieldDrawer.fieldInfo.Name;
    
    internal object GetValue() {
        return fieldDrawer.fieldInfo.GetValue(component);
    }
    
    internal void SetValue(object fieldValue) {
        fieldDrawer.fieldInfo.SetValue(component, fieldValue);
        EntityUtils.AddEntityComponentValue(entityContext.entity, fieldDrawer.componentType, component);
    }
}

internal abstract class TypeDrawer
{
    internal static readonly Dictionary<Type, TypeDrawer> Map = new() {
        { typeof(string),       new StringDrawer()     },
        { typeof(bool),         new BoolDrawer()       },
        { typeof(byte),         new ByteDrawer()       },
        { typeof(int),          new IntDrawer()        },
        { typeof(Vector3),      new Vector3Drawer()    } 
    };
    
    public  abstract void DrawField(DrawField context);
}

internal class StringDrawer : TypeDrawer
{
    public  override void DrawField(DrawField context) {
        var value = (string)context.GetValue();
        if (ImGui.InputText("##field", ref value, 100)) {
            context.SetValue(value);
        }
    }
}

internal class BoolDrawer : TypeDrawer
{
    public  override void DrawField(DrawField context) {
        var value = (bool)context.GetValue();
        if (ImGui.Checkbox("##field", ref value)) {
            context.SetValue(value);
        }
    }
}

#region integer

internal class ByteDrawer : TypeDrawer
{
    public  override void DrawField(DrawField context) {
        int value = (byte)context.GetValue();
        if (ImGui.InputInt("##field", ref value, 0, 0)) {
            context.SetValue((byte)value);
        }
    }
}

internal class IntDrawer : TypeDrawer
{
    public  override void DrawField(DrawField context) {
        var value = (int)context.GetValue();
        if (ImGui.InputInt("##field", ref value, 0, 0)) {
            context.SetValue(value);
        }
    }
}

#endregion

internal class Vector3Drawer : TypeDrawer
{
    public  override void DrawField(DrawField context) {
        var value = (Vector3)context.GetValue();
        if (ImGui.InputFloat3("##field", ref value)) {
            context.SetValue(value);
        }
    }
}
