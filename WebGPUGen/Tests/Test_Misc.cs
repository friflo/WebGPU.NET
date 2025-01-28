using System;
using Evergine.Bindings.WebGPU;
using NUnit.Framework;
using static NUnit.Framework.Assert;

namespace Tests;

public static class Test_Misc
{
    
    [Test]
    public static void Test_Misc_allocations()
    {
        var arena = new Arena();
        arena.Use();
        Utf8String str = new Utf8String();
        str = "abc"u8;
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
}