namespace Friflo.ImGuiNet;

public static class EcsGui
{
    public static void QueryExplorer(QueryExplorer explorer)
    {
        explorer.Draw();
    }
    
    public static void EntityInspector(EntityInspector inspector)
    {
        inspector.Draw();
    }
}