using Friflo.Engine.ECS;

namespace Friflo.ImGuiNet;

internal abstract class ColumnDrawer
{
    internal abstract bool DrawCell(DrawComponent context);
}

internal class ComponentColumnDrawer : ColumnDrawer
{
    private readonly ComponentDrawer componentDrawer;
    
    internal ComponentColumnDrawer(ComponentDrawer componentDrawer) {
        this.componentDrawer = componentDrawer;
    }
    
    internal override bool DrawCell(DrawComponent context) {
        if (context.entityContext.entity.Archetype.ComponentTypes.HasAny(componentDrawer.types)) {
            componentDrawer.DrawComponent(context);
            return true;
        }
        return false;
    }
}
