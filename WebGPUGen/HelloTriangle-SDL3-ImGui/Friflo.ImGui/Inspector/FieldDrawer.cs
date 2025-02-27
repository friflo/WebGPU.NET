using System;
using System.Collections.Generic;
using System.Numerics;
using Friflo.Engine.ECS;
using ImGuiNET;

namespace Friflo.ImGuiNet;

[AttributeUsage(AttributeTargets.Field)]
public class DomainAttribute : Attribute
{ 
    public DomainAttribute(string domain) { }
}

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

internal struct TypeDrawerDomain
{
    internal string     domain;
    internal TypeDrawer drawer;

    internal TypeDrawerDomain(TypeDrawer drawer) {
        this.drawer = drawer;
    }
    internal TypeDrawerDomain(string domain, TypeDrawer drawer) {
        this.domain = domain;
        this.drawer = drawer;
    }
}

internal abstract class TypeDrawer
{
    private static readonly Dictionary<Type, TypeDrawerDomain[]> Map = new () {
        { typeof(string),   [new (new StringDrawer())]     },
        { typeof(bool),     [new (new BoolDrawer())]       },
        { typeof(byte),     [new (new ByteDrawer())]       },
        { typeof(int),      [new (new IntDrawer())]        },
        { typeof(Vector3),  [new (new Vector3Drawer()), new ("color", new Color3Drawer())]    },
    };
    
    public static TypeDrawer GetTypeDrawer(Type type, string domain) {
        Map.TryGetValue(type, out var typeDrawerDomains);
        if (typeDrawerDomains!.Length == 1) {
            return typeDrawerDomains[0].drawer;
        }
        foreach (var typeDrawerDomain in typeDrawerDomains) {
            if (typeDrawerDomain.domain == domain) {
                return typeDrawerDomain.drawer;
            }
        }
        return typeDrawerDomains![0].drawer;
    }   
    
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

#region vector
internal class Vector3Drawer : TypeDrawer
{
    public  override void DrawField(DrawField context) {
        var value = (Vector3)context.GetValue();
        if (ImGui.InputFloat3("##field", ref value)) {
            context.SetValue(value);
        }
    }
}

internal class Color3Drawer : TypeDrawer
{
    public  override void DrawField(DrawField context) {
        var value = (Vector3)context.GetValue();
        if (ImGui.ColorEdit3("##field", ref value)) {
            context.SetValue(value);
        }
    }
}

#endregion
