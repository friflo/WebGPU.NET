namespace Friflo.ImGuiNet;

internal abstract class ColumnDrawer
{
    internal abstract void DrawCell(DrawComponent context);
}

internal class ComponentColumnDrawer : ColumnDrawer
{
    private readonly ComponentDrawer componentDrawer;
    
    internal ComponentColumnDrawer(ComponentDrawer componentDrawer) {
        this.componentDrawer = componentDrawer;
    }
    
    internal override void DrawCell(DrawComponent context) {
        componentDrawer.DrawComponent(context);
    }
}
