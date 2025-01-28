using System.Runtime.CompilerServices;

namespace Evergine.Bindings.WebGPU;

internal struct AllocHeader
{
    internal ushort allocatorIndex;
    internal ushort allocatorVersion;
    internal ushort resetVersion;
    internal ushort padding;
}

internal struct ArenaVersion
{
    internal ushort allocatorVersion;
    internal ushort resetVersion;
}

internal static class AllocValidator
{
    private static ArenaVersion[] arenas;
        
    internal static unsafe void ValidateRenderPipelineDescriptor(this in WGPURenderPipelineDescriptor descriptor) {
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
    
    private static unsafe void ValidatePtr(void* ptr)
    {
        if (ptr == null) {
            return;
        }
        AllocHeader header = *(((AllocHeader*)ptr) - 1);
        var index = header.allocatorIndex;
        if (index >= arenas.Length) {
            throw new InvalidOperationException();
        }
        var entry = arenas[index];
        if (entry.allocatorVersion != header.allocatorVersion) {
            throw new InvalidOperationException();
        }
        if (entry.resetVersion != header.resetVersion) {
            throw new InvalidOperationException();
        }
    }
}