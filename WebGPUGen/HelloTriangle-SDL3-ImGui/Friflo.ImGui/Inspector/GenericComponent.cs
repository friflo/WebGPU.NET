using System;
using System.Collections.Generic;
using System.Reflection;


namespace Friflo.ImGuiNet;

internal class GenericComponent
{
    internal readonly Type          type;
    internal readonly FieldInfo[]   fields;

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
        var genericControl = new GenericComponent(type, fields.ToArray());
        Controls.Add(type, genericControl);
        return genericControl;

    }
    
    private GenericComponent(Type type, FieldInfo[] fields) {
        this.type   = type;
        this.fields = fields;
    }
    
    internal  void Draw(ComponentContext context) {
        
    }
}

