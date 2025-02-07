using System;
using Evergine.Bindings.WebGPU;
using NUnit.Framework;
using static NUnit.Framework.Assert;

namespace Tests;

public static class Test_Arena
{
    [Test]
    public static unsafe void Test_Arena_allocations()
    {
        var arena = new Arena("Test_Arena_allocations");
        var ptr = arena.AllocUtf8("ABC"u8);
        AllocValidator.ValidatePtr(ptr);
        
        arena.Reset();
        
        var e = Throws<InvalidAllocation>(() => {
            AllocValidator.ValidatePtr(ptr);
        })!;
        AreEqual("pointer expired by Arena.Reset()", e.Message);
    }
    
    [Test]
    public static unsafe void Test_Arena_Span()
    {
        var arena = new Arena("Test_Arena_Span");
        arena.Use();
        var myStruct = new MyStruct {
            Entries = [1,2,3]
        };
        AreEqual(3, myStruct.Entries.Length);
        arena.Reset();
        
        var e = Throws<InvalidAllocation>(() => {
            _ = myStruct.Entries;
        })!;
        AreEqual("pointer expired by Arena.Reset()", e.Message);
    }

    unsafe struct MyStruct
    {
        uint  entryCount;
        int*  entries;
        
        public Span<int> Entries {
            get => ApiUtils.GetArr(entries, entryCount);
            set => ApiUtils.SetArr(value, out entries, out entryCount);
        }
    }
}