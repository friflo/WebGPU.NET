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
    private readonly    EntityContext       entityContext = new();
    private readonly    List<ColumnDrawer>  columnDrawers = new();
    private readonly    EntityList          entities;
    
    
    public QueryExplorer(EntityStore store) {
        this.store  = store;
        query       = store.Query();
        entities    = new EntityList(store);
        
        ComponentDrawer.Map.TryGetValue(typeof(EntityName), out var drawer);
        columnDrawers.Add(new ComponentColumnDrawer(drawer));
       
        ComponentDrawer.Map.TryGetValue(typeof(Position), out drawer);
        columnDrawers.Add(new ComponentColumnDrawer(drawer));
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
        if (!ImGui.BeginTable("explorer", 3, ImGuiTableFlags.Resizable | ImGuiTableFlags.ScrollY)) {
            return;
        }
        var windowFocused = ImGui.IsWindowFocused();
        ImGui.TableSetupColumn("Id", ImGuiTableColumnFlags.WidthFixed, 200);
        ImGui.TableSetupColumn("Name", ImGuiTableColumnFlags.WidthStretch);
        ImGui.TableSetupColumn("Position", ImGuiTableColumnFlags.WidthStretch);
        ImGui.TableSetupScrollFreeze(0, 1); // fix table header - requires ImGuiTableFlags.ScrollY
        ImGui.TableHeadersRow();
        
        // see: [How to use ImGuiListClipper ?](https://github.com/ImGuiNET/ImGui.NET/issues/493)
        ImGuiListClipperPtr clipper = new ImGuiListClipperPtr(ImGuiNative.ImGuiListClipper_ImGuiListClipper());
        
        query.Entities.ToEntityList(entities);
        
        
        clipper.Begin(entities.Count, ImGui.GetTextLineHeightWithSpacing());
        int index   = -1;
        while (clipper.Step())
        {
            var start   = clipper.DisplayStart;
            var end     = clipper.DisplayEnd;
            foreach (var entity in entities)
            {
                index++;
                if (index < start) {
                    continue;
                }
                if (index >= end) {
                    break;
                }
                int widgetId = entity.Id * 100;
                ImGui.TableNextRow();
                ImGui.TableSetColumnIndex(0);
                // var str = EcsUtils.IntAsSpan(entity.Id);
                
                ImGui.PushID(widgetId++);
                bool selected           = selections.Contains(entity.Id);
                int id = entity.Id;
                ImGui.SetNextItemWidth(ImGui.GetColumnWidth());
                ImGui.InputInt("##id", ref id, 0, 0, ImGuiInputTextFlags.ReadOnly | ImGuiInputTextFlags.ElideLeft);
                bool selectionChanged = false; 
                // ImGui.Text(str);
                // ImGui.InputText("##id", ref str, 200);
                // bool selectionChanged = false;
                // bool selectionChanged = ImGui.Selectable(str, selected);
                bool isFocused         = ImGui.IsItemFocused();
                ImGui.PopID();
                
                int columnIndex = 1;
                entityContext.entity = entity;
                foreach (var drawer in columnDrawers) {
                    ImGui.TableSetColumnIndex(columnIndex++);
                    ImGui.PushID(widgetId++);
                    ImGui.SetNextItemWidth(ImGui.GetColumnWidth()); 
                    var context = new DrawComponent { entityContext = entityContext };
                    if (!drawer.DrawCell(context)) {
                        selectionChanged |= ImGui.Selectable("", selected);
                    }
                    isFocused |= ImGui.IsItemFocused(); // Issue: returns false by ImGui.InputFloat3() when navigate with arrow up/down 
                    ImGui.PopID();
                }
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
                if (windowFocused && isFocused) {
                    focusedEntity = entity;
                }
            }
        }
        ImGui.EndTable();
        clipper.Destroy();
    }
}