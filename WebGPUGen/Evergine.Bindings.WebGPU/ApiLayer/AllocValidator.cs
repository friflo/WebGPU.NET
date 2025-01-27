using System.Runtime.CompilerServices;

namespace Evergine.Bindings.WebGPU;

internal struct AllocHeader
{
    internal int allocatorIndex;
    internal int revision;
}

internal static class AllocValidator
{
    private static int[] allocators; 
        
    internal static unsafe void ValidateRenderPipelineDescriptor(WGPURenderPipelineDescriptor* descriptor) {
        ValidatePtr(descriptor->label);
        ValidatePtr(descriptor->nextInChain);
        ValidateFragmentState(*descriptor->fragment);
    }
    
    internal static unsafe void ValidateFragmentState(in WGPUFragmentState fragmentState) {
        ValidatePtr(fragmentState.entryPoint);
        ValidateFragmentStates(fragmentState.Targets);
    }
    
    internal static unsafe void ValidateFragmentStates(Span<WGPUColorTargetState> fragmentStates)
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
        if (index < 0 || index >= allocators.Length) {
            throw new InvalidOperationException();
        }
        if (allocators[index] != header.allocatorIndex) {
            throw new InvalidOperationException();
        }
    }
}