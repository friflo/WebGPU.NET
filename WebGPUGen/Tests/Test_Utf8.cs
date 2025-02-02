using System;
using System.Collections.Generic;
using Evergine.Bindings.WebGPU;
using NUnit.Framework;
using static NUnit.Framework.Assert;

namespace Tests;

public static class Test_Utf8
{
    [Test]
    public static void Test_Utf8_allocations()
    {
        var arena = new Arena();
        arena.Use();
        var str = new Utf8();
        str = "abc"u8;
        IsTrue("abc"u8.SequenceEqual(str.AsSpan()));
        AreEqual("abc", str.ToString());
        AreEqual(3,     str.Length);
        
        str = "wxyz"u8;
        AreEqual("wxyz", str.ToString());
        AreEqual(4,     str.Length);
        
        str = "12345"u8;
        AreEqual("12345", str.ToString());
        AreEqual(5,     str.Length);

        Throws<NullReferenceException>(() => {
            var str2 = new Utf8();            
            _ = str2.Length;
        });
    }
    
    [Test]
    public static void Test_Utf8_struct()
    {
        var arena = new Arena();
        arena.Use();
        var test = new TestStruct();
        test.Str = "abc"u8;
        AreEqual(1, arena.AllocationCount);
        
        var str = test.Str;
        AreEqual("abc", str.ToString());
        
        test.Str = default;
        str = test.Str;
        IsNull(str.ToString());
        AreEqual(1, arena.AllocationCount);
    }
    
    
    unsafe struct TestStruct
    {
        private char* str;

        // --- properties
        public Utf8 Str {
            get => ApiUtils.GetUtf8(str);
            set => ApiUtils.SetUtf8(value, out str);
        }
    }
    
    [Test]
    public static void Test_Utf8_equals()
    {
        var arena = new Arena();
        arena.Use();
        var str1 = new Utf8String();
        var str2 = new Utf8String();
        str1 = "abc"u8;
        str2 = "xyz"u8;
        IsTrue (str1.Equals(str1));
        IsFalse(str1.Equals(str2));
        
        IsTrue (str1.Equals((object)str1));
        IsFalse(str1.Equals((object)str2));
        IsFalse(str1.Equals(null));
        
        var start = GC.GetAllocatedBytesForCurrentThread();
        _ = str1.GetHashCode();
        var diff = GC.GetAllocatedBytesForCurrentThread() - start;
        AreEqual(0, diff);
        
        var map = new Dictionary<Utf8String, byte>();
        map.Add(str1, 1);
        map.Add(str2, 2);
        AreEqual(2, map.Count);
        AreEqual(1, map[str1]);
        AreEqual(2, map[str2]);
    }
    
    [Test]
    public static void Test_Utf8_new() {
        var test = new UseString();
        test.Str = "abc"u8;
    } 
}



public unsafe struct UseString
{
    private char* strPtr;

    // --- properties
    public Utf8 Str {
        get => ApiUtils.GetUtf8(strPtr);
        set => ApiUtils.SetUtf8(value, out strPtr);
    }
}