using CppAst;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace WebGPUGen
{
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
                        /*if (command.ReturnType is CppTypedef typedef) {
                            Console.Write($"  {typedef.Name} : ");
                        } 
                        Console.WriteLine($"  {command.Name}");*/
                        string convertedType = Helpers.ConvertToCSharpType(command.ReturnType, false);

                        if (!CsCodeGenerator.emscriptenUnsupportedCommands.Contains(command.Name))
                        {
                            var (handleName, signature) = ApiHelpers.GetParametersSignature(command);
                            if (signature.Length > 0) {
                                signature = ", " + signature;
                            }
                            var commandName = command.Name.Substring(handleType.Name.Length);
                            commandName =  char.ToLower(commandName[0]) + commandName.Substring(1);
                            
                            sb.AppendLine($"    public static void {commandName}(this {handleType.Name} {handleName}{signature}) {{");
                            sb.AppendLine($"    }}");
                            sb.AppendLine($"");
                            
                            //file.WriteLine($"\n\t\t[DllImport(\"wgpu_native\", CallingConvention = CallingConvention.Cdecl, EntryPoint = \"{command.Name}\")]");
                            //file.WriteLine($"\t\tpublic static extern {convertedType} {command.Name}({Helpers.GetParametersSignature(command)});");
                        }
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
        

    }
}
