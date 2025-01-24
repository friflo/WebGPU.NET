using System.Linq;

namespace WebGPUGen;

using CppAst;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;


public static class ApiCodeGenerator
{
    private static Dictionary<CppTypedef, List<CppFunction>> objects = new();
    
    public static void GenerateApiCommands(CppCompilation compilation, string outputPath)
    {
        Debug.WriteLine("Generating API Commands...");
        var handles = GetHandles(compilation).ToArray();
        foreach (var handle in handles) {
            objects.Add(handle, new List<CppFunction>());
        }

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
                file.WriteLine(
                    """
                    namespace Evergine.Bindings.WebGPU;
                    using static WebGPUNative;
                               
                    public static unsafe partial class WebGPUExtensions
                    {
                    """);
                file.Write(sb);
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
        var parameters = Helpers.GetSignatureParameters(command);
        var handleName = parameters[0].Name;
        var signature = GetParametersSignature(parameters);

        var commandName = command.Name.Substring(handleType.Name.Length);
        switch (commandName) {
            case "Reference":
                sb.AppendLine($"    public static void reference(this {handleType.Name} {handleName}) {{");
                sb.AppendLine($"        wgpu{handleType.Name.Substring(4)}Reference({handleName});");
                sb.AppendLine($"        ObjectTracker.IncRef({handleName}.Handle);");
                sb.AppendLine($"    }}");
                return;
            case "Release":
                sb.AppendLine($"    public static void release(this {handleType.Name} {handleName}) {{");
                sb.AppendLine($"        ObjectTracker.DecRef({handleName}.Handle);");
                sb.AppendLine($"        wgpu{handleType.Name.Substring(4)}Release({handleName});");
                sb.AppendLine($"    }}");
                return;
            case "WriteBuffer":
                sb.AppendLine("    public static void writeBuffer<T>(this WGPUQueue queue, WGPUBuffer buffer, ulong bufferOffset, Span<T> data) {");
                sb.AppendLine("        var ptr = System.Runtime.CompilerServices.Unsafe.AsPointer(ref data.GetPinnableReference());");
                sb.AppendLine("        wgpuQueueWriteBuffer(queue, buffer, bufferOffset, ptr, (ulong)(data.Length * sizeof(T)));");
                sb.AppendLine("    }");
                return;
            case "Submit":
                sb.AppendLine("    public static void submit(this WGPUQueue queue, Span<WGPUCommandBuffer> commands) {");
                sb.AppendLine("        wgpuQueueSubmit(queue, (ulong)commands.Length, commands.GetArrPtr());");
                sb.AppendLine("    }");
                return;
            case "SubmitForIndex":
                sb.AppendLine("    public static ulong submitForIndex(this WGPUQueue queue, Span<WGPUCommandBuffer> commands) {");
                sb.AppendLine("        return wgpuQueueSubmitForIndex(queue, (ulong)commands.Length, commands.GetArrPtr());");
                sb.AppendLine("    }");
                return;
        } 
        commandName =  char.ToLower(commandName[0]) + commandName.Substring(1);
                    
        sb.AppendLine($"    public static {returnType} {commandName}(this {handleType.Name} {handleName}{signature}) {{");
        bool hasReturnValue = returnType != "void";
        if (hasReturnValue) {
            sb.Append("        var result = ");
        } else {
            sb.Append("        ");
        }
        sb.Append(command.Name);
        sb.Append("(");
        sb.Append(handleName);
        for (int n = 1; n < parameters.Length; n++) {
            var param = parameters[n];
            sb.Append(", ");
            switch (param.Type) {
                case SignatureParamType.Pointer: 
                    sb.Append($"&{param.Name}");
                    break;
                case SignatureParamType.CharPointer:
                    sb.Append($"{param.Name}.AllocString()");
                    break;
                default:
                    sb.Append(param.Name);
                    break;
            }
        }
        sb.AppendLine(");");
        

        if (hasReturnValue) {
            if (command.ReturnType is CppTypedef handleReturnType) {
                if (objects.ContainsKey(handleReturnType)) {
                    sb.AppendLine("        ObjectTracker.CreateRef(result.Handle);");
                }
            }
            sb.AppendLine("        return result;");
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
        for (int i = 1; i < parameters.Length; i++) {
            var parameter = parameters[i];
            signature.Append(", ");
            switch(parameter.Type) {
                case SignatureParamType.Pointer:
                    signature.Append($"{parameter.TypeNamePure} ");
                    break;
                case SignatureParamType.CharPointer:
                    signature.Append($"ReadOnlySpan<{parameter.TypeNamePure}> ");
                    break;
                default:
                    signature.Append($"{parameter.TypeName} ");
                    break;
            }
            signature.Append($"{parameter.Name}");
        }
        return signature.ToString();
    }

    public static void AddStructProperties(StreamWriter file, CppClass structure)
    {
        var sb = new StringBuilder();
        var fields = structure.Fields;
        foreach (var member in fields)
        {
            string type = Helpers.ConvertToCSharpType(member.Type);
            if (type == "char*") {
                var nameUpper = char.ToUpper(member.Name[0]) + member.Name.Substring(1);
                sb.AppendLine($"\t\tpublic ReadOnlySpan<char> {nameUpper} {{");
                sb.AppendLine($"\t\t\tget => ApiUtils.GetLabel({member.Name});");
                sb.AppendLine($"\t\t\tset => ApiUtils.AllocString(value);");
                sb.AppendLine($"\t\t}}");
                continue;
            }
            if (type.EndsWith("*")) {
                if (member.Name.EndsWith("s")) {
                    // case: Field tuples used for arrays. e.g.
                    //      public WGPUBuffer* buffers;
                    //      public ulong bufferCount;
                    var arrayFieldName = member.Name;
                    var countFieldName = member.Name.Substring(0, arrayFieldName.Length - 1) + "Count";
                    CppField countField = fields.FirstOrDefault(field => field.Name == countFieldName);
                    if (countField != null) {
                        var arrayFieldType = type.Substring(0, type.Length - 1);
                        var nameUpper = char.ToUpper(arrayFieldName[0]) + arrayFieldName.Substring(1);
                        sb.AppendLine($"\t\tpublic Span<{arrayFieldType}> {nameUpper} {{");
                        sb.AppendLine($"\t\t\tget => new ({arrayFieldName}, (int){countFieldName});");
                        sb.AppendLine($"\t\t\tset => value.SetArr(out {arrayFieldName}, out {countFieldName});");
                        sb.AppendLine($"\t\t}}");
                    }
                    continue;
                }
            }
        }
        if (sb.Length > 0) {
            file.WriteLine("\t\t// --- properties");
            file.Write(sb.ToString());
        }
    }
}

