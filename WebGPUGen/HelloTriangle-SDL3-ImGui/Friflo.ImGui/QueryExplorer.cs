using System.Collections.Generic;
using Friflo.Engine.ECS;
using ImGuiNET;

namespace Friflo.ImGuiNet;

public class QueryExplorer
{
    private             EntityStore     store;
    private             ArchetypeQuery  query;
    private readonly    HashSet<int>    selections = new ();
    internal            Entity          selectedEntity;
    
    public QueryExplorer(EntityStore store) {
        this.store = store;
        query = store.Query();
    }
    
    // https://github.com/ocornut/imgui/issues/3740
    // https://github.com/ocornut/imgui/blob/master/imgui_demo.cpp
    internal void Draw()
    {
        if (!ImGui.BeginTable("explorer", 2, ImGuiTableFlags.Resizable)) {
            return;
        }
        ImGui.TableSetupColumn("id", ImGuiTableColumnFlags.WidthFixed, 200);
        ImGui.TableSetupColumn("name", ImGuiTableColumnFlags.WidthStretch);
        ImGui.TableHeadersRow();

        
        foreach (var entity in query.Entities)
        {
            ImGui.TableNextRow();
            ImGui.TableSetColumnIndex(0);
            var str = EcsUtils.IntAsSpan(entity.Id);
            // ImGui.SetNextItemWidth(-1);
            
            ImGui.PushID(entity.Id);
            // ImGui.Text(str);
            // ImGui.InputText("##cell", ref str, 20, ImGuiInputTextFlags.ReadOnly);
            bool selected = selections.Contains(entity.Id);
            if (ImGui.Selectable(str, selected)) {
                var ctrlDown = ImGui.IsKeyDown(ImGuiKey.ModCtrl);
                if (!ctrlDown) {
                    selections.Clear();
                }
                if (selected) {
                    selectedEntity = default;
                    selections.Remove(entity.Id);
                } else {
                    selectedEntity = entity;
                    selections.Add(entity.Id);
                }
            }
            ImGui.PopID();
        }
        ImGui.EndTable();
    }
}