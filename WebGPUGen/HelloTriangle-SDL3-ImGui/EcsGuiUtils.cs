using System.Numerics;
using Friflo.Engine.ECS;
using Friflo.ImGuiNet;
using ImGuiNET;

namespace HelloTriangle;

#pragma warning disable CS0649 // Field is never assigned to, and will always have its default value

struct MyComponent : IComponent
{
    public string   str;

    public int      int32;
    public byte     uint8;
    public bool     enabled;
    public Vector3  vector3;
    [Domain("color")]
    public Vector3  color3 = new Vector3(1, 0, 0);
        
    public void InstanceMethod() {}
        
    public MyComponent() {}
}
    
struct SingleValue : IComponent
{
    public int      int32;
}

internal static class EcsGuiUtils
{
    internal static EntityStore CreateTestStore() {
        var store = new EntityStore();
        store.CreateEntity();
        store.CreateEntity();
        store.CreateEntity();
        for (int n = 0; n < 10_000; n++) {
            store.CreateEntity(
                new Position(n,n,n),
                new EntityName($"entity {n}"),
                new MyComponent { str = $"str{n}", vector3 = new Vector3(n,n,n)},
                new SingleValue()
            );
        }
        return store;
    }
    
    internal static void DrawEcsWindows() {
        var bgAlpha = 1f;
        ImGui.SetNextWindowPos(new(10, 400), ImGuiCond.Once);
        ImGui.SetNextWindowSize(new(500, 600), ImGuiCond.Once);
        ImGui.SetNextWindowBgAlpha(bgAlpha);
        ImGui.Begin("Explorer");
        EcsGui.QueryExplorer(EcsGui.Explorer);
        ImGui.End();
            
        ImGui.SetNextWindowPos(new(600, 400), ImGuiCond.Once);
        ImGui.SetNextWindowSize(new(700, 500), ImGuiCond.Once);
        ImGui.SetNextWindowBgAlpha(bgAlpha);
        ImGui.Begin("Inspector");
        EcsGui.EntityInspector(EcsGui.Inspector);
        ImGui.End();
    }
}