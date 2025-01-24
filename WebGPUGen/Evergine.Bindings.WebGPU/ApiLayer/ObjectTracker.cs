namespace Evergine.Bindings.WebGPU;

public static class ObjectTracker
{
    private static Dictionary<IntPtr, int> handleMap = new Dictionary<IntPtr, int>();
    
    public static unsafe void CreateRef(IntPtr handle, char* descriptorLabel) {
        // var refCount = handleMap[handle];
    }
    
    public static void IncRef(IntPtr handle) {
        // var refCount = handleMap[handle];
        
    }
    
    public static void DecRef(IntPtr handle) {
        // var refCount = handleMap[handle];
        
    }
}