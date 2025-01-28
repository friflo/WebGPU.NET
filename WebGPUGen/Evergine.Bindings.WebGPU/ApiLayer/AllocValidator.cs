using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Evergine.Bindings.WebGPU;

[StructLayout(LayoutKind.Explicit, Size = 8)]
internal struct AllocHeader
{
    [FieldOffset (0)] internal ushort       allocatorIndex;     //  2
    [FieldOffset (4)] internal ArenaVersion version;            //  4
}

[StructLayout(LayoutKind.Explicit, Size = 4)]
internal struct ArenaVersion
{
    [FieldOffset (0)] internal uint   all;          //  4
    [FieldOffset (0)] internal ushort allocator;    //  2
    [FieldOffset (2)] internal ushort reset;        //  2
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
        ValidateSpan(fragmentStates);
        foreach (var state in fragmentStates) {
            ValidatePtr(state.nextInChain);
            ValidatePtr(state.blend);            
        }
    }
    
    private static unsafe void ValidateSpan<T>(Span<T> span) where T : unmanaged
    {
        void* ptr = Unsafe.AsPointer(ref span.GetPinnableReference());
        ValidatePtr(ptr);
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
        if(arenas[index].all == header.version.all) {
            return;
        }
        throw new InvalidOperationException();
    }
}