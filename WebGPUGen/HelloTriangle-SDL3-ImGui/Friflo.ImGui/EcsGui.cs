namespace Friflo.ImGuiNet;

public static class EcsGui
{
    // https://github.com/ocornut/imgui/issues/3740
    // https://github.com/ocornut/imgui/blob/master/imgui_demo.cpp
    public static void QueryExplorer(QueryExplorer queryExplorer)
    {
        queryExplorer.Draw();
    }
}