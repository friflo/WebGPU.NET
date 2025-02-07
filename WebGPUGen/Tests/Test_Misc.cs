using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Evergine.Bindings.WebGPU;
using NUnit.Framework;
using static NUnit.Framework.Assert;

namespace Tests;

public static class Test_Misc
{
    private const int Count  = 1_000;
    private const int Repeat = 100_000;
    
    [Test]
    public static unsafe void Test_Misc_Alloc_Free()
    {
        var pointers = new nint[Count];
        for (int i = 0; i < Repeat; ++i)
        {
            for (int n = 0; n < Count; n++) {
                var ptr = Marshal.AllocHGlobal(16);
                pointers[n] = ptr;
                var bytePtr = (byte*)ptr;
            }
            // Array.Sort(pointers); pointer hav a distance of 0x20 
            for (int n = 0; n < Count; n++) {
                Marshal.FreeHGlobal(pointers[n]);
            }
        }
    }
    
    [Test]
    public static void Test_Misc_Arena()
    {
        var arena = new Arena("Test_Misc_Arena");
        for (int i = 0; i < Repeat; ++i) {
            for (int n = 0; n < Count; n++) {
                arena.Alloc(16);
            }
            // Array.Sort(pointers); pointer hav a distance of 0x20
            arena.Reset();
        }
    }
    
    [Test]
    public static void Test_Misc_Dictionary_perf()
    {
        var map = new Dictionary<nint, nint>();
        for (int i = 0; i < Repeat; ++i)
        {
            for (nint n = 0; n < Count; n++) {
                map.Add(n, n);
            }
            for (nint n = 0; n < Count; n++) {
                map.Remove(n);
            }
        }
    }
    
    [Test]
    public static unsafe void Test_Misc_use_after_free_AllocHGlobal()
    {
        var ptr = Marshal.AllocHGlobal(16);
        var bytePtr = (byte*)ptr;
        bytePtr[0] = 1;
        Marshal.FreeHGlobal(ptr);
        // bytePtr[0] = 1; // => undefined behavior. Throws no Exception in this case.
    }
    
    [Test]
    public static unsafe void Test_Misc_use_after_free_NativeMemory()
    {
        var ptr = NativeMemory.Alloc(16);
        var bytePtr = (byte*)ptr;
        bytePtr[0] = 1;
        NativeMemory.Free(ptr);
        // bytePtr[0] = 1; // => undefined behavior. Throws no Exception in this case.
    }
}