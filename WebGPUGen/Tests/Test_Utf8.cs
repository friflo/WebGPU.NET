using System;
using System.Collections.Generic;
using Evergine.Bindings.WebGPU;
using NUnit.Framework;
using static NUnit.Framework.Assert;

namespace Tests;

public static class Test_Utf8
{
    [Test]
    public static unsafe void Test_Utf8_Span()
    {
        Span<byte> span1 = new Span<byte>(null, 0);
        IsTrue(span1.IsEmpty);
        
        
        var arr = new byte[0];
        Span<byte> span2 = new Span<byte>(arr, 0, 0);
        IsTrue(span2.IsEmpty);
    }
    
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


        var str2 = new Utf8();
        AreEqual(0, str2.Length);
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
        AreEqual("", str.ToString());
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
        Utf8 str0 = "abc"u8;
        Utf8 str1 = "abc"u8;
        Utf8 str2 = "xyz"u8;
        Utf8 str3 = default;
        
        IsTrue (str0 == str1);
        IsTrue (str1 != str2);
        IsTrue (str0 != str3);
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