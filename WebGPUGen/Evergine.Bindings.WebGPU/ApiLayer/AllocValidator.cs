using System.Runtime.CompilerServices;

namespace Evergine.Bindings.WebGPU;

internal struct AllocHeader
{
    internal ushort allocatorIndex;
    internal ushort allocatorRevision;
    
    internal int allocRevision;
}

internal struct AllocatorEntry
{
    internal ushort allocatorRevision;
    internal int    allocRevision;
}

internal static class AllocValidator
{
    private static AllocatorEntry[] allocators;
        
    internal static unsafe void ValidateRenderPipelineDescriptor(in WGPURenderPipelineDescriptor descriptor) {
        ValidatePtr(descriptor.label);
        ValidatePtr(descriptor.nextInChain);
        ValidateFragmentState(*descriptor.fragment);
    }
    
    internal static unsafe void ValidateFragmentState(in WGPUFragmentState fragmentState) {
        ValidatePtr(fragmentState.entryPoint);
        ValidateColorTargetStates(fragmentState.Targets);
    }
    
    internal static unsafe void ValidateColorTargetStates(Span<WGPUColorTargetState> fragmentStates)
    {
        ValidatePtr(Unsafe.AsPointer(ref fragmentStates.GetPinnableReference()));
        foreach (var state in fragmentStates) {
            ValidatePtr(state.nextInChain);
            ValidatePtr(state.blend);            
        }
    }
    
    private static unsafe void ValidatePtr(void* buffer)
    {
        AllocHeader header = *(((AllocHeader*)buffer) - 1);
        var index = header.allocatorIndex;
        if (index >= allocators.Length) {
            throw new InvalidOperationException();
        }
        var entry = allocators[index];
        if (entry.allocatorRevision != header.allocatorRevision) {
            throw new InvalidOperationException();
        }
        if (entry.allocRevision != header.allocRevision) {
            throw new InvalidOperationException();
        }
    }
}