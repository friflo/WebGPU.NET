using System;

namespace WebGPUGen;

using CppAst;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;


public static class ApiCodeGenerator
{
    public static void GenerateApiCommands(CppCompilation compilation, string outputPath)
    {
        Debug.WriteLine("Generating API Commands...");
        var handles = GetHandles(compilation).ToArray();
        var objects = new Dictionary<CppTypedef, List<CppFunction>>();
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
        commandName =  char.ToLower(commandName[0]) + commandName.Substring(1);
                    
        sb.AppendLine($"    public static {returnType} {commandName}(this {handleType.Name} {handleName}{signature}) {{");
        if (returnType == "void") {
            sb.Append("        ");
        } else {
            sb.Append("        return ");
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
            if (parameter.Type == SignatureParamType.Pointer ||
                parameter.Type == SignatureParamType.CharPointer) {
                signature.Append($"Span<{parameter.TypeNamePure}> ");
            } else {
                signature.Append($"{parameter.TypeName} ");
            }
            signature.Append($"{parameter.Name}");
        }
        return signature.ToString();
    }

}

