using System;
using System.Collections.Generic;
using Friflo.Engine.ECS;
using ImGuiNET;

namespace Friflo.ImGuiNet;


internal struct DrawComponent {
    internal    EntityContext   entityContext;
    
    public void ComponentLabel(string label) {
        ImGui.Text(label);
        ImGui.SameLine(entityContext.valueStart);
        ImGui.SetNextItemWidth(entityContext.valueWidth); 
    }
}

internal abstract class ComponentDrawer
{
    internal readonly ComponentType componentType;
    
    internal static readonly Dictionary<Type, ComponentDrawer> Map = CreateMap();
    
    private static Dictionary<Type, ComponentDrawer> CreateMap() {
        var types = EntityStore.GetEntitySchema().ComponentTypeByType;
        var map = new Dictionary<Type, ComponentDrawer> {
            { typeof(Position),     new PositionDrawer  (types[typeof(Position)])     },
            { typeof(EntityName),   new EntityNameDrawer(types[typeof(EntityName)])   }
        };
        return map;
    }
    
    public  abstract void DrawComponent(DrawComponent context);
    
    protected ComponentDrawer(ComponentType componentType) {
        this.componentType = componentType;
    }
    
    public void UpdateComponent(DrawComponent context, object value) {
        EntityUtils.AddEntityComponentValue(context.entityContext.entity, componentType, value);
    }
}

internal class PositionDrawer : ComponentDrawer
{
    internal PositionDrawer(ComponentType componentType) : base(componentType) { }
    
    public  override void DrawComponent(DrawComponent context) {
        var component = context.entityContext.entity.GetComponent<Position>();
        if (ImGui.InputFloat3("##field", ref component.value)) {
            UpdateComponent(context, component);
        }
    }
}

internal class EntityNameDrawer : ComponentDrawer
{
    internal EntityNameDrawer(ComponentType componentType) : base(componentType) { }
    
    public  override void DrawComponent(DrawComponent context) {
        var component = context.entityContext.entity.GetComponent<EntityName>();
        if (ImGui.InputText("##field", ref component.value, 100)) {
            UpdateComponent(context, component);
        }
    }
}