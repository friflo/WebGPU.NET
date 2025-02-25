using System.Collections.Generic;
using Friflo.Engine.ECS;
using ImGuiNET;

namespace Friflo.ImGuiNet;

public class QueryExplorer
{
    private             EntityStore         store;
    private             ArchetypeQuery      query;
    private readonly    HashSet<int>        selections = new ();
    internal            Entity              focusedEntity;
    
    public QueryExplorer(EntityStore store) {
        this.store = store;
        query = store.Query();
    }
    
    // https://github.com/ocornut/imgui/issues/3740
    // https://github.com/ocornut/imgui/blob/master/imgui_demo.cpp
    internal unsafe void Draw()
    {
        if (!ImGui.BeginTable("explorer", 2, ImGuiTableFlags.Resizable)) {
            return;
        }
        var windowFocused = ImGui.IsWindowFocused();
        ImGui.TableSetupColumn("id", ImGuiTableColumnFlags.WidthFixed, 200);
        ImGui.TableSetupColumn("name", ImGuiTableColumnFlags.WidthStretch);
        ImGui.TableHeadersRow();
        
        // see: [How to use ImGuiListClipper ?](https://github.com/ImGuiNET/ImGui.NET/issues/493)
        ImGuiListClipperPtr clipper = new ImGuiListClipperPtr(ImGuiNative.ImGuiListClipper_ImGuiListClipper());
        clipper.Begin(query.Count, ImGui.GetTextLineHeightWithSpacing());
        int index   = -1;
        while (clipper.Step())
        {
            var start   = clipper.DisplayStart;
            var end     = clipper.DisplayEnd;
            foreach (var entity in query.Entities)
            {
                index++;
                if (index < start) {
                    continue;
                }
                if (index >= end) {
                    break;
                }
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
                        selections.Remove(entity.Id);
                    } else {
                        selections.Add(entity.Id);
                    }
                }
                ImGui.PopID();
                if (windowFocused && ImGui.IsItemFocused()) {
                    focusedEntity = entity;
                }
            }
        }
        ImGui.EndTable();
        clipper.Destroy();
    }
}