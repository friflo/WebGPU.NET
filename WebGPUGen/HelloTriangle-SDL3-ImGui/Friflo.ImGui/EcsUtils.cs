using System;
using System.Text;

namespace HelloTriangle;

internal static class EcsUtils
{
    private static readonly StringBuilder   Sb = new StringBuilder();
    private static          char[]          _chars = [];
    
    
    internal static ReadOnlySpan<char> IntAsSpan(int value) {
        Sb.Clear();
        Sb.Append(value);
        
        if (_chars.Length < Sb.Length) {
            _chars = new char[Sb.Length];
        }
        Sb.CopyTo(0, _chars, 0, Sb.Length);
        return new ReadOnlySpan<char>(_chars, 0 , Sb.Length);
    }
    
    
}