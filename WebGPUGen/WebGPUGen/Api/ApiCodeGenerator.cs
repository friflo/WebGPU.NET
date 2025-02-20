﻿using System.Linq;

namespace WebGPUGen;

using CppAst;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

public static class ApiCodeGenerator
{
    internal static Dictionary<CppTypedef, List<CppFunction>> objects = new();
    
    public static void GetObjects(CppCompilation compilation) {
        var handles = GetHandles(compilation).ToArray();
        foreach (var handle in handles) {
            objects.Add(handle, new List<CppFunction>());
        }
    }
    
    public static void GenerateApiCommands(CppCompilation compilation, string outputPath)
    {
        Debug.WriteLine("Generating API Commands...");
        foreach (var command in compilation.Functions)
        {
            if (command.Parameters.Count == 0) {
                continue;
            }
            var first = command.Parameters[0];
            if (first.Type is CppTypedef handleType) {
                if (objects.ContainsKey(handleType)) {
                    objects[handleType].Add(command);    
                }
            }
        }

        foreach (var (handleType, commands) in objects) {
            var shortName = handleType.Name.Substring(4);
            using (StreamWriter file = File.CreateText(Path.Combine(outputPath, $"Api/{shortName}.cs")))
            {
                var sb = new StringBuilder();
                foreach (var command in commands) {
                    AppendCommand(sb, handleType, command);
                    sb.AppendLine();
                }
                string docs;
                if (handleType.Name == "WGPUInstance" || handleType.Name == "WGPUSurface") {
                    docs = "/// No counterpart in JavaScript WebGPU";
                } else {
                    var name = handleType.Name.Substring(1);
                    docs = $"/// <see href=\"https://developer.mozilla.org/en-US/docs/Web/API/{name}\">MDN documentation</see>";
                }
                file.WriteLine(
                  $$"""
                    using System.Diagnostics;
                    using static Evergine.Bindings.WebGPU.WebGPUNative;
                    
                    // ReSharper disable InconsistentNaming
                    namespace Evergine.Bindings.WebGPU;
                    
                    {{docs}}           
                    public unsafe partial struct {{handleType.Name}}
                    {
                    """);
                file.Write(sb);
                file.WriteLine("    public override string? ToString() => ObjectTracker.GetLabel(Handle);");
                file.WriteLine("}");
            }
        }
    }
    
    private static void AppendCommand(StringBuilder sb, CppTypedef handleType, CppFunction command)
    {
        if (CsCodeGenerator.emscriptenUnsupportedCommands.Contains(command.Name)) {
            return;
        }
        string returnType = Helpers.ConvertToCSharpType(command.ReturnType, false);
        var parameters = ApiHelpers.GetSignatureParameters(command);
        var signature = GetParametersSignature(parameters);

        var commandName = command.Name.Substring(handleType.Name.Length);
        commandName =  char.ToLower(commandName[0]) + commandName.Substring(1);
        switch (commandName) {
            case "setLabel":            // not implemented in WGPU - https://github.com/gfx-rs/wgpu-native/blob/trunk/src/unimplemented.rs
                return;
            case "writeBuffer":         // Queue_NG.cs
            case "writeTexture":        // Queue_NG.cs
            case "submit":              // Queue_NG.cs
            case "submitForIndex":      // Queue_NG.cs
            case "getMappedRange":      // Buffer_NG.cs
            case "getConstMappedRange": // Buffer_NG.cs
            case "getMapState":         // Buffer_NG.cs
            case "setPushConstants":    // RenderPassEncoder_NG.cs
            case "getCapabilities":     // Surface_NG.cs
            case "getCurrentTexture":   // Surface_NG.cs
            case "requestDevice":       // Adapter_NG.cs
            case "getInfo":             // Adapter_NG.cs
            case "getLimits":           // Adapter_NG.cs, Device_NG.cs
            case "report":              // Instance_NG.cs
            case "getQueue":            // Device_NG.cs
                // These methods occur only once and implemented manually in *_NG.cs files. 
                sb.AppendLine($"    // {commandName}() - not generated. See: {handleType.Name.Substring(4)}_NG.cs");
                return;
            case "reference":   sb.AppendLine(
                $$"""
                    public void reference() {
                        ObjectTracker.IncRef(this);
                        wgpu{{handleType.Name.Substring(4)}}Reference(this);
                    }
                """);
                return;
            case "release":     sb.AppendLine(
                $$"""
                    public void release() {
                        ObjectTracker.DecRef(this);
                        wgpu{{handleType.Name.Substring(4)}}Release(this);
                    }
                    
                    public void Dispose() {
                        ObjectTracker.DecRef(this);
                        wgpu{{handleType.Name.Substring(4)}}Release(this);
                    }
                """);
                return;
            case "setBindGroup": sb.AppendLine(
                $$"""
                    public void setBindGroup(uint groupIndex, WGPUBindGroup group, ReadOnlySpan<uint> dynamicOffsets) {
                        fixed (uint* ptr = dynamicOffsets) {
                            wgpu{{handleType.Name.Substring(4)}}SetBindGroup(this, groupIndex, group, (ulong)dynamicOffsets.Length, ptr);
                        }
                        WGPUException.ThrowOnError();
                    }
                """);
                return;
        }
        bool hasReturnValue = returnType != "void";
        // create properties
        if (hasReturnValue && commandName.StartsWith("get"))
        {
            if (parameters.Length == 1)
            {
                var propertyName = char.ToLower(commandName[3]) + commandName.Substring(4);
                // sb.AppendLine($"    public {returnType} {propertyName} => {command.Name}(this); // Property");
                sb.AppendLine($$"""
                                  public {{returnType}} {{propertyName}} { get {
                                        ObjectTracker.ValidateHandle(this);
                                        return {{command.Name}}(this);
                                  } }
                              """);
                return;
            }
        }
        /* if (!hasReturnValue && commandName.StartsWith("get") && parameters.Length == 2) {
            var propertyName = char.ToLower(commandName[3]) + commandName.Substring(4);
            var type = parameters[1].TypeNamePure;
            sb.AppendLine(
                $$"""
                    public {{type}} {{propertyName}} { get {
                        var result = new {{type}}();
                        {{command.Name}}(this, &result);
                        return result; // getter
                    } }
                """);
            return;
        } */
                    
        sb.AppendLine($"    public {returnType} {commandName}({signature}) {{");
        AddValidationCall(sb, commandName, parameters);

        if (hasReturnValue) {
            sb.Append("        var result = ");
        } else {
            sb.Append("        ");
        }
        sb.Append(command.Name);
        sb.Append("(this");
        for (int n = 1; n < parameters.Length; n++) {
            var param = parameters[n];
            sb.Append(", ");
            switch (param.Type) {
                case SignatureParamType.Pointer: 
                    sb.Append($"&{param.Name}");
                    break;
                case SignatureParamType.CharPointer:
                    sb.Append($"{param.Name}.AllocUtf8()");
                    break;
                default:
                    sb.Append(param.Name);
                    break;
            }
        }
        sb.AppendLine(");");
        sb.AppendLine("        WGPUException.ThrowOnError();");

        if (hasReturnValue) {
            if (command.ReturnType is CppTypedef handleReturnType) {
                if (objects.ContainsKey(handleReturnType)) {
                    var comment = commandName.StartsWith("create") ? " // ref-create" : " // ref-other";
                    if (parameters.Length >= 2 && parameters[1].Name == "descriptor") {
                        sb.AppendLine($"        ObjectTracker.CreateRefLabel(result, HandleType.{handleReturnType.Name}, descriptor._label);{comment}");
                    } else {
                        sb.AppendLine($"        ObjectTracker.CreateRef(result, HandleType.{handleReturnType.Name}, Handle);{comment}");
                    }
                }
            }
            sb.AppendLine("        return result;");
        }
        sb.AppendLine($"    }}");
        AddValidationMethod(sb, commandName, signature, parameters);
    }
    
    private static void AddValidationCall(StringBuilder sb, string commandName, SignatureParam[] parameters)
    {
        sb.Append($"        Validate_{commandName}");
        sb.Append("(");
        for (int n = 1; n < parameters.Length; n++) {
            var param = parameters[n];
            if (n > 1) {
                sb.Append(", ");
            }
            switch (param.Type) {
                case SignatureParamType.Pointer: 
                    sb.Append($"{param.Name}");
                    break;
                case SignatureParamType.CharPointer:
                    sb.Append($"{param.Name}");
                    break;
                default:
                    sb.Append(param.Name);
                    break;
            }
        }
        sb.AppendLine(");");
    }

    private static void AddValidationMethod(StringBuilder sb, string commandName, string signature, SignatureParam[] parameters)
    {
        sb.AppendLine();
        sb.AppendLine($"    [Conditional(\"VALIDATE\")]");
        sb.AppendLine($"    private void Validate_{commandName}({signature}) {{");
        sb.AppendLine($"        ObjectTracker.ValidateHandle(this);");
        for (int i = 1; i < parameters.Length; i++) {
            var parameter = parameters[i];
            if (parameter.CppParameter.Type is CppPointerType pointerType) {
                var cppType = GetPointerCppType(pointerType);
                if (StructsWithPointers.Contains(cppType)) {
                    sb.AppendLine($"        {parameter.Name}.Validate();");                
                }
            }
            if (parameter.CppParameter.Type is CppTypedef handleType) {
                if (objects.ContainsKey(handleType)) {
                    sb.AppendLine($"        ObjectTracker.ValidateHandleParam({parameter.Name});");
                }
            }
        }
        sb.AppendLine($"    }}");
    }

    private static List<CppTypedef> GetHandles(CppCompilation compilation)
    {
        var handles = new List<CppTypedef>();
        foreach (CppTypedef typedef in compilation.Typedefs)
        {
            if (typedef.Name.StartsWith("WGPUProc") ||
            typedef.Name.EndsWith("Callback"))
            {
                continue;
            }
            if (typedef.ElementType is not CppPointerType)
            {
                continue;
            }
            handles.Add(typedef);
        }
        return handles;
    }
    
    private static string GetParametersSignature(SignatureParam[] parameters)
    {
        StringBuilder signature = new StringBuilder();
        bool isFirst = true;
        for (int i = 1; i < parameters.Length; i++) {
            var parameter = parameters[i];
            if (isFirst) {
                isFirst = false;
            } else {
                signature.Append(", ");    
            }
            switch(parameter.Type) {
                case SignatureParamType.Pointer:
                    signature.Append($"{parameter.TypeNamePure} ");
                    break;
                case SignatureParamType.CharPointer:
                    signature.Append($"Utf8 ");
                    break;
                default:
                    signature.Append($"{parameter.TypeName} ");
                    break;
            }
            signature.Append($"{parameter.Name}");
        }
        return signature.ToString();
    }
    
    internal static bool IsInternalField(CppField field, CppClass parent)
    {
        if (field.Name == "nextInChain") {
            return true;
        }
        string type = Helpers.ConvertToCSharpType(field.Type);
        if (type == "void*") {
            return false;
        }
        if (type == "char*") {
            return true;
        }
        if (type.EndsWith("*")) {
            if (field.Name.EndsWith("s")) {
                // case: Field tuples used for arrays. e.g.
                //      public WGPUBuffer* buffers;
                //      public ulong bufferCount;
                var arrayFieldName = field.Name;
                string countFieldName;
                if (field.Name == "entries") {
                    countFieldName = "entryCount";
                } else {
                    countFieldName = field.Name.Substring(0, arrayFieldName.Length - 1) + "Count";
                }
                CppField countField = parent.Fields.FirstOrDefault(f => f.Name == countFieldName);
                if (countField != null) {
                    return true;
                }
            }
            // case: Pointer field used for an optional values. 
            return true;
        }
        if (field.Name.EndsWith("Count")) {
            var arrayFieldName = field.Name.Substring(0, field.Name.Length - "Count".Length) + "s";
            if (field.Name == "entryCount") {
                arrayFieldName = "entries";
            }
            CppField arrayField = parent.Fields.FirstOrDefault(f => f.Name == arrayFieldName);
            if (arrayField != null) {
                return true;
            }
        }
        /* if (field.Type is CppTypedef typedef) {
            if (typedef.ElementType is CppPrimitiveType primitiveType) {
                countFieldName = field.Name.Substring(0, arrayFieldName.Length - 1) + "Count";
                switch (primitiveType.Kind) {
                    case CppPrimitiveKind.UnsignedLongLong:
                    case CppPrimitiveKind.LongLong:
                        return true;
                }
            }
        } */
        return false;
    }
    
    private static readonly HashSet<CppClass> StructsWithPointers = new HashSet<CppClass>();
    
    public static void CollectStructsWithPointers(CppCompilation compilation)
    {
        var structs = compilation.Classes.Where(c => c.ClassKind == CppClassKind.Struct && c.IsDefinition);
        foreach (var structure in structs) {
            foreach (var field in structure.Fields) {
                if (field.Name == "nextInChain" || field.Name == "chain" || field.Name == "next") {
                    continue;
                }
                string type = Helpers.ConvertToCSharpType(field.Type);
                if (type == "void*") {
                    continue;
                }
                if (type == "char*" || type.EndsWith("*")) {
                    StructsWithPointers.Add(structure);
                    break;
                }
                if (field.Type is CppTypedef typedef) {
                    if (objects.ContainsKey(typedef)) {
                        StructsWithPointers.Add(structure);
                        break;
                    }
                }
            }
        }
    }

    public static void AddStructProperties(StreamWriter file, CppClass structure)
    {
        var sb = new StringBuilder();
        var fields = structure.Fields;
        var arrayFields = new List<string>();
        foreach (var member in fields)
        {
            if (member.Name == "nextInChain") {
                continue; // nextInChain fields are not part of WebGPU API
            }
            string type = Helpers.ConvertToCSharpType(member.Type);
            if (type == "void*") {
                var nameUpper = char.ToUpper(member.Name[0]) + member.Name.Substring(1);
                sb.Append($"\tpublic  {"IntPtr",-43} {nameUpper,-20} {{");
                sb.Append($" get => new IntPtr({member.Name});");
                sb.Append($" set => {member.Name} = (void*)value;");
                sb.AppendLine($"\t}}");
                continue;
            }
            if (type == "char*") {
                var propertyName = member.Name;
                sb.Append($"\tpublic  {"Utf8",-43} {propertyName,-20} {{");
                sb.Append($" get => GetUtf8(_{member.Name});");
                sb.Append($" set => SetUtf8(value, out _{member.Name});");
                sb.AppendLine($"\t}}");
                continue;
            }
            if (type.EndsWith("*")) {
                if (member.Name.EndsWith("s")) {
                    // case: Field tuples used for arrays. e.g.
                    //      public WGPUBuffer* buffers;
                    //      public ulong bufferCount;
                    var arrayFieldName = member.Name;
                    string countFieldName;
                    if (member.Name == "entries") {
                        countFieldName = "entryCount";
                    } else {
                        countFieldName = member.Name.Substring(0, arrayFieldName.Length - 1) + "Count";
                    }
                    CppField countField = fields.FirstOrDefault(field => field.Name == countFieldName);
                    if (countField != null) {
                        arrayFields.Add(arrayFieldName);
                        var arrayFieldType = type.Substring(0, type.Length - 1);
                        var propertyName = arrayFieldName;
                        var spanType = $"Span<{arrayFieldType}>";
                        sb.Append($"\tpublic  {spanType,-43} {propertyName,-20} {{");
                        sb.Append($" get => GetArr(_{arrayFieldName}, _{countFieldName});");
                        sb.Append($" set => SetArr(value, out _{arrayFieldName}, out _{countFieldName});");
                        sb.AppendLine($"\t}}");
                        continue;
                    }
                }
                {
                    // case: Pointer field used for an optional values. 
                    var fieldType = type.Substring(0, type.Length - 1) + "?";
                    var propertyName = member.Name;
                    sb.Append($"\tpublic  {fieldType,-43} {propertyName,-20} {{");
                    sb.Append($" get => GetOpt(_{member.Name});");
                    sb.Append($" set => SetOpt(out _{member.Name}, value);");
                    sb.AppendLine($"\t}}");
                    continue;
                }
            }
        }
        if (sb.Length > 0) {
            file.WriteLine();
        }
        AddValidateMethods(sb, structure, arrayFields);
        file.Write(sb.ToString());
    }
    
    private static void AddValidateMethods(StringBuilder sb, CppClass structure, List<string> arrayFields)
    {
        // --- add Validate()
        if (!StructsWithPointers.Contains(structure)) {
            return;
        }
        sb.AppendLine();
        sb.AppendLine("\t[Conditional(\"VALIDATE\")]");
        sb.AppendLine("\tpublic void Validate() {");
        foreach (var field in structure.Fields)
        {
            if (field.Name == "nextInChain" ||
                field.Name == "deviceLostUserdata") {
                continue;
            }
            if (field.Type is CppClass cppClass) {
                if (StructsWithPointers.Contains(cppClass)) {
                    sb.AppendLine($"\t\t{field.Name}.Validate();");
                }
            }
            if (field.Type is CppTypedef handleType) {
                if (objects.ContainsKey(handleType)) {
                    sb.AppendLine($"        ObjectTracker.ValidateHandleParam({field.Name});");
                    continue;
                }
            }
            if (field.Type is CppPointerType pointerType) {
                sb.AppendLine($"\t\tAllocValidator.ValidatePtr(_{field.Name});");
                var cppType = GetPointerCppType(pointerType);
                if (cppType != null) {
                    if (StructsWithPointers.Contains(cppType)) {
                        if (arrayFields.Contains(field.Name)) {
                            sb.AppendLine($"\t\tforeach (var element in {field.Name}) {{");
                            sb.AppendLine($"\t\t    element.Validate();");
                            sb.AppendLine($"\t\t}}");
                        } else {
                            sb.AppendLine($"\t\tif (_{field.Name} != null) {{");
                            sb.AppendLine($"\t\t    _{field.Name}->Validate();");
                            sb.AppendLine($"\t\t}}");
                        }
                    }
                }
                continue;
            }
        }
        sb.AppendLine("\t}");
    }
    
    private static CppType GetPointerCppType(CppPointerType pointerType)
    {
        if (pointerType.ElementType is CppQualifiedType cppQualifiedType) {
            return cppQualifiedType.ElementType;
        }
        if (pointerType.ElementType is CppClass cppClass) {
            return cppClass;
        }
        return null;
    }
    
}

