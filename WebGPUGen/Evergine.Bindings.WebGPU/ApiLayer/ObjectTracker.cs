using System.Diagnostics;
using System.Runtime.InteropServices;

// ReSharper disable InconsistentNaming
namespace Evergine.Bindings.WebGPU;

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
}

public interface IHandle
{
    IntPtr GetHandle();
}

public struct ObjectEntry
{
    internal ObjectLabel    label;
    internal int            count;
    internal HandleType     type;
    
    public HandleType       Type    => type;
    public int              Count   => count;
    public string?          Label   => GetLabel();
    
    private unsafe string? GetLabel() {
        if (label.buffer[0] == 0) {
            return null;
        }
        fixed (byte* ptr = label.buffer) {
            return Marshal.PtrToStringAnsi((IntPtr)ptr);
        }
    }
    
    public override string ToString() {
        var labelStr = GetLabel();
        if (labelStr == null) {
            return $"{type} ref-count: {count}";
        }
        return $"{type} ref-count: {count} label: \"{labelStr}\"";
    }
    
    public string GetShortLabel() {
        var labelStr = GetLabel();
        if (labelStr == null) {
            return $"ref-count: {count}";
        }
        return $"ref-count: {count} label: \"{labelStr}\"";
    }
}

public static class ObjectTracker
{
    public static  Dictionary<nint,ObjectEntry>.ValueCollection Entries => HandleMap.Values;

    private static readonly Dictionary<IntPtr, ObjectEntry> HandleMap = new ();
    
    // descriptorLabel encoding: UTF-8 + null terminator, allocated in non movable storage
    [Conditional("VALIDATE")]
    internal static unsafe void CreateRef<THandle>(THandle handle, HandleType type, char* descriptorLabel)
        where THandle : struct, IHandle
    {
        var handlePtr = handle.GetHandle();
        ref var value = ref CollectionsMarshal.GetValueRefOrAddDefault(HandleMap, handlePtr, out bool exists);
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
        throw new InvalidOperationException("WebGPU Object already tracked."); // can occur only in case API Layer is buggy
    }
    
    [Conditional("VALIDATE")]
    internal static void IncRef<THandle>(THandle handle)
        where THandle : struct, IHandle
    {
        var handlePtr = handle.GetHandle();
        ref var value = ref CollectionsMarshal.GetValueRefOrAddDefault(HandleMap, handlePtr, out bool exists);
        if (exists) {
            value.count++;
            return;
        }
        throw ObjectNotFoundException<THandle>();
    }
    
    [Conditional("VALIDATE")]
    internal static void DecRef<THandle>(THandle handle)
        where THandle : struct, IHandle
    {
        var handlePtr = handle.GetHandle();
        ref var value = ref CollectionsMarshal.GetValueRefOrAddDefault(HandleMap, handlePtr, out bool exists);
        if (exists) {
            value.count--;
            if (value.count > 0) {
                return;
            }
            HandleMap.Remove(handlePtr);
            return;
        }
        throw ObjectNotFoundException<THandle>();
    }
    
    /// Validated handle must not be null
    [Conditional("VALIDATE")]
    internal static void ValidateHandle<THandle>(THandle handle)
        where THandle : struct, IHandle
    {
        var handlePtr = handle.GetHandle();
        if (HandleMap.ContainsKey(handlePtr)) {
            return;
        }
        throw ObjectNotFoundException<THandle>();
    }
    
    /// Validated handle can be null
    [Conditional("VALIDATE")]
    internal static void ValidateHandleParam<THandle>(THandle handle)
        where THandle : struct, IHandle
    {
        var handlePtr = handle.GetHandle();
        if (handlePtr == IntPtr.Zero) {
            return;
        }
        if (HandleMap.ContainsKey(handlePtr)) {
            return;
        }
        throw ObjectNotFoundException<THandle>();
    }
    
    internal static string? GetLabel(IntPtr handle) {
        if (handle == IntPtr.Zero) {
            return "null";
        }
        if (HandleMap.TryGetValue(handle, out var value)) {
            return value.GetShortLabel();
        }
        return "disposed";
    }
    
    private static InvalidOperationException ObjectNotFoundException<THandle>()
        where THandle : struct, IHandle
    {
        return new InvalidOperationException($"{typeof(THandle).Name} already disposed");
    }
}