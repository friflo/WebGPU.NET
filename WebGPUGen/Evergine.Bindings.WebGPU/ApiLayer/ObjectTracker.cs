namespace Evergine.Bindings.WebGPU;

using System.Runtime.InteropServices;

public enum HandleType
{
    WGPUAdapter,
	WGPUBindGroup,
	WGPUBindGroupLayout,
	WGPUBuffer,
	WGPUCommandBuffer,
	WGPUCommandEncoder,
	WGPUComputePassEncoder,
	WGPUComputePipeline,
	WGPUDevice,
	WGPUInstance,
	WGPUPipelineLayout,
	WGPUQuerySet,
	WGPUQueue,
	WGPURenderBundle,
	WGPURenderBundleEncoder,
	WGPURenderPassEncoder,
	WGPURenderPipeline,
	WGPUSampler,
	WGPUShaderModule,
	WGPUSurface,
	WGPUTexture,
	WGPUTextureView,
}

internal unsafe struct ObjectLabel
{
    // Store label in a fixed size buffer instead of a string.
    // Using a string (or char[]) would require a heap allocation.
    // Invariant: buffer[63] = 0
    internal fixed byte buffer[64];

    public override string? ToString() {
        fixed (byte* ptr = buffer) {
            return Marshal.PtrToStringAnsi((IntPtr)ptr);    
        }
    }
}

internal struct ObjectEntry
{
    internal ObjectLabel    label;
    internal int            count;
    internal HandleType     type;
    
    public override string? ToString() => label.ToString();
}

public static class ObjectTracker
{
    private static readonly Dictionary<IntPtr, ObjectEntry> HandleMap = new ();
    
    // descriptorLabel encoding: UTF-8 + null terminator, allocated in non movable storage 
    public static unsafe void CreateRef(IntPtr handle, HandleType type, char* descriptorLabel)
    {
        ref var value = ref CollectionsMarshal.GetValueRefOrAddDefault(HandleMap, handle, out bool exists);
        if (!exists) {
            value.count = 1;
            value.type  = type;
            if (descriptorLabel != null) {
                var span = new ReadOnlySpan<byte>(descriptorLabel, 64);
                var len = span.IndexOf((byte)0);
                if (len == -1) {
                    len = 63;
                }
                fixed (byte* ptr = value.label.buffer) {
                    var dest = new Span<byte>(ptr, 63);
                    new ReadOnlySpan<byte>(descriptorLabel, len).CopyTo(dest);
                }
            }
            return;
        }
        throw new InvalidOperationException("WebGPU Object already tracked."); // can occur only in case of bug in API Layer
    }
    
    public static void IncRef(IntPtr handle)
    {
        ref var value = ref CollectionsMarshal.GetValueRefOrAddDefault(HandleMap, handle, out bool exists);
        if (exists) {
            value.count++;
            return;
        }
        throw ObjectNotFoundException();
    }
    
    public static void DecRef(IntPtr handle)
    {
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