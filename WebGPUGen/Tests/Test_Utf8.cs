using System;
using System.Collections.Generic;
using System.Text;
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
        var str = new Utf8String();
        str = "abc"u8;
        IsTrue("abc"u8.SequenceEqual(str.AsSpan()));
        AreEqual("abc", str.ToString());
        AreEqual(3,     str.Length);
        
        str = "wxyz"u8;
        AreEqual("wxyz", str.ToString());
        AreEqual(4,     str.Length);
        
        str = "12345";
        AreEqual("12345", str.ToString());
        AreEqual(5,     str.Length);

        str = null;
        Throws<NullReferenceException>(() => {
            _ = str.Length;
        });
    }
    
    [Test]
    public static void Test_Utf8_struct()
    {
        var arena = new Arena();
        arena.Use();
        var test = new TestStruct();
        test.Str = "abc";
        AreEqual(1, arena.AllocationCount);
        
        var str = test.Str;
        AreEqual("abc", str.ToString());
        
        test.Str = null;
        str = test.Str;
        IsNull(str.ToString());
        AreEqual(1, arena.AllocationCount);
    }
    
    
    unsafe struct TestStruct
    {
        private char* strPtr;

        // --- properties
        public Utf8String Str {
            get => ApiUtils.GetUtf8(strPtr);
            set => ApiUtils.SetUtf8(value, out strPtr);
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
    
    public static void Test_Utf8_new() {
        var test = new UseString();
        test.Str = "abc"u8;
    } 
}

internal enum Utf8Type
{
    Null,
    Span,
    Utf8,
} 

public unsafe ref struct Utf8
{
    private ReadOnlySpan<byte>  span;
    private byte*               ptr;
    private Utf8Type            Type;
    
    internal Utf8(char* strPtr) {
        Type        = Utf8Type.Utf8;
        this.ptr = (byte*)strPtr;
    }
    
    private Utf8(ReadOnlySpan<byte> value) {
        span   = value;
        Type    = Utf8Type.Span;
    }
    
    public static implicit operator Utf8(ReadOnlySpan<byte> value) {
        return new Utf8 (value);
    }
    
    
    private int GetPtrLength() {
        throw new NotImplementedException();
        // return new VString (value.AsSpan());
    }
    
    public void AllocUtf8(ref char* strPtr) {
        throw new NotImplementedException();
        // return new VString (value.AsSpan());
    }

    public override string? ToString() {
        switch (Type) {
            case Utf8Type.Null:
                return null;
            case Utf8Type.Span:
                return Encoding.UTF8.GetString(span);
            case Utf8Type.Utf8:
                int length = GetPtrLength();
                return Encoding.UTF8.GetString(ptr, length);
        }
        throw new NotImplementedException();
    }
}

public unsafe struct UseString
{
    private char* strPtr;

    // --- properties
    public Utf8 Str {
        get => new Utf8(strPtr);
        set => value.AllocUtf8(ref strPtr);
    }
}