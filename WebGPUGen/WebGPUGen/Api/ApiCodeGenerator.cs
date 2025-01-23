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
            sb.Append(param.Name);
            switch (param.Type) {
                case SignatureParamType.Pointer: 
                    sb.Append(".GetOptPtr()");
                    break;
                case SignatureParamType.CharPointer:
                    sb.Append(".AllocString()");
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
                    signature.Append($"Span<{parameter.TypeNamePure}> ");
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
        var fields = structure.Fields;
        foreach (var member in fields)
        {
            string type = Helpers.ConvertToCSharpType(member.Type);
            
            if (type == "char*") {
                var nameUpper = char.ToUpper(member.Name[0]) + member.Name.Substring(1);
                file.WriteLine($"\t\tpublic ReadOnlySpan<char> {nameUpper} {{");
                file.WriteLine($"\t\t\tget => ApiUtils.GetLabel({member.Name});");
                file.WriteLine($"\t\t\tset => ApiUtils.AllocString(value);");
                file.WriteLine($"\t\t}}");
            }
            if (member.Name.EndsWith("Count")) {
                var arrayName = member.Name.Substring(0, member.Name.Length - "Count".Length) + "s";
                
                CppField arrayField = fields.Where(field => field.Name == arrayName).FirstOrDefault();
                if (arrayField != null) {
                    string arrayFieldType = Helpers.ConvertToCSharpType(arrayField.Type);
                    var nameUpper = char.ToUpper(arrayField.Name[0]) + arrayField.Name.Substring(1);
                    arrayFieldType = arrayFieldType.Substring(0, arrayFieldType.Length - 1);
                    file.WriteLine($"\t\tpublic Span<{arrayFieldType}> {nameUpper} {{");
                    file.WriteLine($"\t\t\tget => new ({arrayName}, (int){member.Name});");
                    file.WriteLine($"\t\t\tset => value.SetArr(out {arrayName}, out {member.Name});");
                    file.WriteLine($"\t\t}}");
                }
            }
            // file.WriteLine($"\t\tpublic {type} {member.Name};");
        }
    }
}

