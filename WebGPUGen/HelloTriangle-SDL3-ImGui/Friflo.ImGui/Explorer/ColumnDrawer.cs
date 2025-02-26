using Friflo.Engine.ECS;

namespace Friflo.ImGuiNet;

internal abstract class ColumnDrawer
{
    internal abstract bool DrawCell(EntityContext entityContext);
}

internal class ComponentColumnDrawer : ColumnDrawer
{
    private readonly ComponentDrawer componentDrawer;
    
    internal ComponentColumnDrawer(ComponentDrawer drawer) {
        componentDrawer = drawer;
    }
    
    internal override bool DrawCell(EntityContext entityContext) {
        if (entityContext.entity.Archetype.ComponentTypes.HasAny(componentDrawer.types)) {
            var context = new DrawComponent() { entityContext = entityContext };
            componentDrawer.DrawComponent(context);
            return true;
        }
        return false;
    }
}

internal class FieldColumnDrawer : ColumnDrawer
{
    private readonly ComponentField componentField;
    
    internal FieldColumnDrawer(ComponentField componentField) {
        this.componentField = componentField;
    }
    
    internal override bool DrawCell(EntityContext entityContext) {
        var types = new ComponentTypes(componentField.componentType);
        if (entityContext.entity.Archetype.ComponentTypes.HasAny(types)) {
            var component = EntityUtils.GetEntityComponent(entityContext.entity, componentField.componentType);
            var context = new DrawField {
                entityContext   = entityContext,
                componentField  = componentField,
                component       = component
            };
            componentField.fieldDrawer.DrawField(context);
            return true;
        }
        return false;
    }
}
