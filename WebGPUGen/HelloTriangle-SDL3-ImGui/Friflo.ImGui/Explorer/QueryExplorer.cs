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
    internal void Draw()
    {
        ImGui.Text("fixed header");
        ImGui.Text("entities");
        ImGui.BeginChild("dddd");
        DrawTable();
        ImGui.EndChild();
    }
    
    private unsafe void DrawTable()
    {
        // float TEXT_BASE_HEIGHT = ImGui.GetTextLineHeightWithSpacing();
        // ImVec2 outer_size = new Vector2(0.0f, TEXT_BASE_HEIGHT * 8);
        if (!ImGui.BeginTable("explorer", 2, ImGuiTableFlags.Resizable | ImGuiTableFlags.ScrollY)) {
            return;
        }
        var windowFocused = ImGui.IsWindowFocused();
        ImGui.TableSetupColumn("id", ImGuiTableColumnFlags.WidthFixed, 200);
        ImGui.TableSetupColumn("name", ImGuiTableColumnFlags.WidthStretch);
        ImGui.TableSetupScrollFreeze(0, 1); // fix table header - requires ImGuiTableFlags.ScrollY
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
                bool selected           = selections.Contains(entity.Id);
                bool selectionChanged   = ImGui.Selectable(str, selected);
                bool isFocused1         = ImGui.IsItemFocused();
                
                ImGui.TableSetColumnIndex(1);
                selectionChanged       |= ImGui.Selectable("abc", selected);
                bool isFocused2         = ImGui.IsItemFocused();

                ImGui.PopID();
                
                if (selectionChanged) {
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
                if (windowFocused && (isFocused1 || isFocused2)) {
                    focusedEntity = entity;
                }
            }
        }
        ImGui.EndTable();
        clipper.Destroy();
    }
}