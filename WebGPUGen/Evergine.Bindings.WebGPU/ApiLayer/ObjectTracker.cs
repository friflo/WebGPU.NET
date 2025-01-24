using System.Reflection.Emit;
using System.Runtime.InteropServices;

namespace Evergine.Bindings.WebGPU;

internal unsafe struct ObjectLabel
{
    public fixed byte buffer[64];
}

internal struct ObjectEntry
{
    internal ObjectLabel    label;
    internal int            count;
}

public static class ObjectTracker
{
    private static readonly Dictionary<IntPtr, ObjectEntry> HandleMap = new ();
    
    public static unsafe void CreateRef(IntPtr handle, char* descriptorLabel) {
        HandleMap.Add(handle, new ObjectEntry { count = 1 });
    }
    
    public static void IncRef(IntPtr handle) {
        ref var value = ref CollectionsMarshal.GetValueRefOrAddDefault(HandleMap, handle, out bool exists);
        if (exists) {
            value.count++;
            return;
        }
        throw ObjectNotFoundException();
    }
    
    public static void DecRef(IntPtr handle) {
        ref var value = ref CollectionsMarshal.GetValueRefOrAddDefault(HandleMap, handle, out bool exists);
        if (exists) {
            value.count--;
            if (value.count > 0) {
                return;
            }
            HandleMap.Remove(handle);
            return;
        }
        throw ObjectNotFoundException();
    }
    
    private static InvalidOperationException ObjectNotFoundException() => new InvalidOperationException("WebGPU object not found");
}