using System;
using System.Collections.Generic;
using Friflo.Engine.ECS;
using ImGuiNET;

namespace Friflo.ImGuiNet;

public class QueryExplorer
{
    private             ArchetypeQuery      query;
    private readonly    HashSet<int>        selections = new ();
    internal            Entity              focusedEntity;
    private readonly    EntityContext       entityContext = new();
    private readonly    List<ColumnDrawer>  columnDrawers = new();
    private readonly    EntityList          entities;
    

    
    
    public QueryExplorer() {
        var store = new EntityStore();
        store.CreateEntity(new EntityName("Hello ECS"));
        store.CreateEntity(new EntityName("add any query with: QueryExplorer.Instance.AddQuery(query);"));
        query       = store.Query();
        entities    = new EntityList(store);
        
        AddComponentDrawer(typeof(EntityName));
        // AddComponentDrawer(typeof(Position));
    }
    
    public void AddQuery(ArchetypeQuery query) {
        this.query = query;
    }
    
    internal void AddComponentDrawer(Type type) {
        ComponentDrawer.Map.TryGetValue(type, out var drawer);
        columnDrawers.Add(new ComponentColumnDrawer(drawer));
    }
    
    internal void AddComponentFieldDrawer(ComponentFieldDrawer fieldDrawer) {
        columnDrawers.Add(new FieldColumnDrawer(fieldDrawer));
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
        var flags = ImGuiTableFlags.Resizable | ImGuiTableFlags.ScrollY | ImGuiTableFlags.ScrollX | ImGuiTableFlags.Reorderable | ImGuiTableFlags.Hideable | ImGuiTableFlags.Sortable;
        if (!ImGui.BeginTable("explorer", columnDrawers.Count + 1, flags)) {
            return;
        }
        var windowFocused = ImGui.IsWindowFocused();
        ImGui.TableSetupColumn("Id", ImGuiTableColumnFlags.WidthFixed, 110);
        foreach (var drawer in columnDrawers) {
            ImGui.TableSetupColumn(drawer.Name, ImGuiTableColumnFlags.WidthFixed, 200);
        }
        ImGui.TableSetupScrollFreeze(0, 1); // fix table header - requires ImGuiTableFlags.ScrollY
        ImGui.TableHeadersRow();
        
        query.Entities.ToEntityList(entities); // Sort always
        
        var sortSpecs = ImGui.TableGetSortSpecs();
        SortTable(sortSpecs.Specs);
            
        // see: [How to use ImGuiListClipper ?](https://github.com/ImGuiNET/ImGui.NET/issues/493)
        ImGuiListClipperPtr clipper = new ImGuiListClipperPtr(ImGuiNative.ImGuiListClipper_ImGuiListClipper());
        
        clipper.Begin(entities.Count, ImGui.GetFrameHeightWithSpacing()); // GetFrameHeightWithSpacing = row height in pixel
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
                    if (!drawer.DrawCell(entityContext)) {
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
    
    struct IntEntry
    {
        internal int id;
    }
    
    private void SortTable(ImGuiTableColumnSortSpecsPtr columnSortSpecs)
    {
        if (columnSortSpecs.ColumnIndex == 0) {
            var sortTable = new int[entities.Count];
            int index = 0;
            foreach (var entity in entities) {
                sortTable[index++] = entity.Id;
            }
            if (columnSortSpecs.SortDirection == ImGuiSortDirection.Ascending) {
                Array.Sort(sortTable, (i1, i2) => i1 - i2);    
            } else {
                Array.Sort(sortTable, (i1, i2) => i2 - i1);
            }
            int count = entities.Count;
            entities.Clear();
            for (int n = 0; n < count; n++) {
                entities.Add(sortTable[n]);
            }
            columnSortSpecs = null;
        }
    }
}