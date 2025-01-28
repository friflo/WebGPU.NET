using Evergine.Bindings.WebGPU;
using NUnit.Framework;
using static NUnit.Framework.Assert;

namespace Tests;

public static class Test_Arena
{
    [Test]
    public static unsafe void Test_Arena_allocations() {
        var arena = new Arena();
        var ptr = arena.AllocString("ABC");
        AllocValidator.ValidatePtr(ptr);
        
        arena.Reset();
        
        Throws<InvalidAllocation>(() => {
            AllocValidator.ValidatePtr(ptr);
        });
        
    }
    
}