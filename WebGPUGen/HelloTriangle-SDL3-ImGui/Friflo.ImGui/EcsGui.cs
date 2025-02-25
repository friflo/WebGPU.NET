using Friflo.Engine.ECS;
using ImGuiNET;

namespace HelloTriangle;


public class QueryExplorer
{
    internal EntityStore store;
    internal ArchetypeQuery query;
    
    public QueryExplorer(EntityStore store) {
        this.store = store;
        query = store.Query();
    }
}

public static class EcsGui
{
    // https://github.com/ocornut/imgui/issues/3740
    public static void QueryExplorer(QueryExplorer queryExplorer)
    {
        ImGui.BeginTable("explorer", 2, ImGuiTableFlags.Resizable);
        // ImGui.TableSetupColumn("id");
        ImGui.TableSetupColumn("id", ImGuiTableColumnFlags.WidthFixed, 100);
        ImGui.TableSetupColumn("name", ImGuiTableColumnFlags.WidthStretch, 200);
        ImGui.TableHeadersRow();
        
        foreach (var entity in queryExplorer.query.Entities) {
            ImGui.TableNextRow();
            ImGui.TableSetColumnIndex(0);
            var str = entity.Id.ToString();
            ImGui.SetNextItemWidth(-1);
            
            ImGui.PushID(entity.Id);
            ImGui.InputText("##cell", ref str, 100, ImGuiInputTextFlags.ReadOnly);
            ImGui.PopID();
        }
        ImGui.EndTable();
    }
}