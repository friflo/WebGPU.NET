using System.Collections.Generic;
using Friflo.Engine.ECS;
using ImGuiNET;

namespace Friflo.ImGuiNet;

public class EntityInspector
{
    private QueryExplorer    explorer;
    
    public EntityInspector(QueryExplorer queryExplorer) {
        this.explorer = queryExplorer;
    }
    
    internal void Draw()
    {
        
    }
}