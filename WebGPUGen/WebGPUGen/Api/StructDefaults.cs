using System;
using System.Collections.Generic;
using CppAst;

namespace WebGPUGen;

public class StructDefault
{
    public class Field
    {
        public readonly string  name;
        public readonly string  value;
    
        public Field(string name, string value) {
            this.name = name;
            this.value = value;
        }
    }
    
    public static bool HasDefaultFields(CppClass structure)
    {
        return Fields.ContainsKey(structure.Name);
    }
    
    public static string GetStructFieldDefault(CppClass structure, CppField field)
    {
        if (Fields.TryGetValue(structure.Name, out var fields)) {
            var pad = new string(' ', Math.Max(20 - field.Name.Length, 0));
            if (fields.TryGetValue(field.Name, out var value)) {
                if (field.Type is CppEnum cppEnum) {
                    return $"{pad} = {cppEnum.Name}.{value}";       
                }
                if (field.Type is CppClass cppClass) {
                    return $"{pad} = new {cppClass.Name}()";
                }
                return $"{pad} = {value}";
            }
        }
        return "";
    }
    
    public static readonly Dictionary<string, Dictionary<string,string>>  Fields = new() {
        { "WGPUDepthStencilState", new() {
                { "stencilFront",       "object" },
                { "stencilBack",        "object" },
                { "stencilReadMask",    "0xFFFFFFFF" },
                { "stencilWriteMask",   "0xFFFFFFFF" }
        } },
        { "WGPUStencilFaceState", new() {
            { "compare",        "Always" },
            { "depthFailOp",    "Keep" },
            { "failOp",         "Keep" },
            { "passOp",         "Keep" }
        } }
    };
/*
        { "XXXX", new() {
            { "xxxx",       "XXXX" },
            { "xxxx",       "XXXX" }
        } },
 */
}