using Friflo.Engine.ECS;

namespace Friflo.ImGuiNet;

internal abstract class ColumnDrawer
{
    internal abstract bool   DrawCell(EntityContext entityContext);
    internal abstract string Name { get; }
}

internal class ComponentColumnDrawer : ColumnDrawer
{
    internal readonly ComponentDrawer componentDrawer;
    
    internal ComponentColumnDrawer(ComponentDrawer drawer) {
        componentDrawer = drawer;
    }
    
    internal override string Name => componentDrawer.componentType.Name;
    
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
    private readonly ComponentFieldDrawer fieldDrawer;
    
    internal FieldColumnDrawer(ComponentFieldDrawer fieldDrawer) {
        this.fieldDrawer = fieldDrawer;
    }
    
    internal override string Name => fieldDrawer.fieldInfo.Name;
    
    internal override bool DrawCell(EntityContext entityContext)
    {
        var types = new ComponentTypes(fieldDrawer.componentType);
        if (entityContext.entity.Archetype.ComponentTypes.HasAny(types)) {
            var component = EntityUtils.GetEntityComponent(entityContext.entity, fieldDrawer.componentType);
            var context = new DrawField {
                entityContext   = entityContext,
                fieldDrawer     = fieldDrawer,
                component       = component
            };
            fieldDrawer.typeDrawer.DrawField(context);
            return true;
        }
        return false;
    }
}
