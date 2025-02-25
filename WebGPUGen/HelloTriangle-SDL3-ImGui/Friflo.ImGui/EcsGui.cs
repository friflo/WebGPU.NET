using System.Collections.Generic;
using System.Text;
using Friflo.Engine.ECS;
using ImGuiNET;

namespace HelloTriangle;


public class QueryExplorer
{
    internal EntityStore store;
    internal ArchetypeQuery query;
    internal HashSet<int>   selected = new ();
    
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
        ImGui.TableSetupColumn("id", ImGuiTableColumnFlags.WidthFixed, 200);
        ImGui.TableSetupColumn("name", ImGuiTableColumnFlags.WidthStretch);
        ImGui.TableHeadersRow();
        
        foreach (var entity in queryExplorer.query.Entities) {
            ImGui.TableNextRow();
            ImGui.TableSetColumnIndex(0);
            var str = EcsUtils.IntAsSpan(entity.Id);
            ImGui.SetNextItemWidth(-1);
            
            ImGui.PushID(entity.Id);
            // ImGui.Text(str);
            // ImGui.InputText("##cell", ref str, 20, ImGuiInputTextFlags.ReadOnly);
            bool selected = queryExplorer.selected.Contains(entity.Id);
            if (ImGui.Selectable(str, selected)) {
                var ctrlDown = ImGui.IsKeyDown(ImGuiKey.ModCtrl);
                if (!ctrlDown) {
                    queryExplorer.selected.Clear();
                }
                if (selected)
                    queryExplorer.selected.Remove(entity.Id);
                else
                    queryExplorer.selected.Add(entity.Id);
            }
            ImGui.PopID();
        }
        ImGui.EndTable();
    }
}