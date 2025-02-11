using System;
using System.Collections.Generic;
using CppAst;

namespace WebGPUGen;

public class StructDefault
{
#region add default field values    
    public static bool HasDefaultFields(CppClass structure)
    {
        return Fields.ContainsKey(structure.Name);
    }
    
    public static string GetStructFieldDefault(CppClass structure, CppField field)
    {
        if (!Fields.TryGetValue(structure.Name, out var fields)) {
            return "";
        }
        if (structure.Name == "WGPUColorTargetState") {
            int i = 1;
        }
        if (!fields.TryGetValue(field.Name, out var value)) {
            return "";
        }
        if (!FoundFields.TryGetValue(structure.Name, out var found)) {
            found = new HashSet<string>();
            FoundFields.Add(structure.Name, found);
        }
        found.Add(field.Name);
        
        var pad = new string(' ', Math.Max(20 - field.Name.Length, 0));
        
        if (field.Type is CppEnum cppEnum) {
            return $"{pad} = {cppEnum.Name}.{value}";
        }
        if (field.Type is CppClass cppClass) {
            return $"{pad} = new {cppClass.Name}()";
        }
        return $"{pad} = {value}";
    }
    
    public static void UnusedDefaultFields()
    {
        foreach (var (structure, fields) in Fields) {
            if (!FoundFields.TryGetValue(structure, out var foundFields)) {
                Console.WriteLine($"Unused StructDefault: {structure}");
                continue;
            }
            foreach (var (fieldName, _) in fields) {
                if (!foundFields.Contains(fieldName)) {
                    Console.WriteLine($"Unused StructDefault field: {structure}.{fieldName}");
                }
            }
        }
    }
    
    private static readonly Dictionary<string, HashSet<string>>  FoundFields = new();
#endregion
    
    // IDL from: https://www.w3.org/TR/webgpu/#idl-index
    // add fields of dictionary types with an assignment (=)
    private static readonly Dictionary<string, Dictionary<string,string>>  Fields = new() {
        { "WGPUTextureDescriptor", new() {
            { "mipLevelCount",          "1" },
            { "sampleCount",            "1" },
            { "dimension",              "_2D" },
        } },
        { "WGPUTextureViewDescriptor", new() {
            { "aspect",                 "All" }
        } },
        { "WGPUSamplerDescriptor", new() {
            { "addressModeU",           "ClampToEdge" },
            { "addressModeV",           "ClampToEdge" },
            { "addressModeW",           "ClampToEdge" },
            { "magFilter",              "Nearest" },
            { "minFilter",              "Nearest" },
            { "lodMaxClamp",            "32" },
            { "maxAnisotropy",          "1" },
        } },
        { "WGPUBufferBindingLayout", new() {
            { "type",                   "Uniform" },
        } },
        { "WGPUSamplerBindingLayout", new() {
            { "type",                   "Filtering" },
        } },
        { "WGPUTextureBindingLayout", new() {
            { "sampleType",             "Float" },
            { "viewDimension",          "_2D" },
        } },
        { "WGPUStorageTextureBindingLayout", new() {
            { "access",                 "WriteOnly" },
            { "viewDimension",          "_2D" },
        } },
        { "WGPURenderPipelineDescriptor", new() {
            { "primitive",              "object" },
            { "multisample",            "object" },
        } },
        { "WGPUPrimitiveState", new() {
            { "topology",               "TriangleList" },
            { "frontFace",              "CCW" },
            { "cullMode",               "None" },
        } },
        { "WGPUMultisampleState", new() {
            { "count",                  "1" },
            { "mask",                   "0xFFFFFFFF" },
        } },
        { "WGPUColorTargetState", new() {
            { "writeMask",           "0" }, // TODO !!! All
        } },
        { "WGPUBlendComponent", new() {
            { "operation",           "Add" },
            { "srcFactor",           "One" },
            { "dstFactor",           "Zero" },
        } },
        { "WGPUDepthStencilState", new() {
            { "stencilFront",           "object" },
            { "stencilBack",            "object" },
            { "stencilReadMask",        "0xFFFFFFFF" },
            { "stencilWriteMask",       "0xFFFFFFFF" }
        } },
        { "WGPUStencilFaceState", new() {
            { "compare",            "Always" },
            { "depthFailOp",        "Keep" },
            { "failOp",             "Keep" },
            { "passOp",             "Keep" },
        } },
        /*
        { "XXXX", new() {
            { "xxxx",           "XXXX" },
            { "xxxx",           "XXXX" },
        } },
        { "XXXX", new() {
            { "xxxx",           "XXXX" },
            { "xxxx",           "XXXX" },
        } },
        { "XXXX", new() {
            { "xxxx",           "XXXX" },
            { "xxxx",           "XXXX" },
        } },
        { "XXXX", new() {
            { "xxxx",           "XXXX" },
            { "xxxx",           "XXXX" },
        } },
        { "XXXX", new() {
            { "xxxx",           "XXXX" },
            { "xxxx",           "XXXX" },
        } },
        */
        
        


    };
/*
        { "XXXX", new() {
            { "xxxx",           "XXXX" },
            { "xxxx",           "XXXX" },
        } },
 */
}