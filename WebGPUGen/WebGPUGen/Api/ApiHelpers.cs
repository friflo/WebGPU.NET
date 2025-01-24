using System.Collections.Generic;
using CppAst;

namespace WebGPUGen;

public enum SignatureParamType
{
    VoidPointer = 0,
    CharPointer = 1,
    UIntPointer = 2,
    Pointer     = 3,
    Type        = 4,
}
public struct SignatureParam
{
    public string               Name;
    public string               TypeName;       // may end with * for pointers
    public string               TypeNamePure;   // Name without *
    public SignatureParamType   Type;
    public CppParameter         CppParameter;
}

public static class ApiHelpers
{
    public static SignatureParam[] GetSignatureParameters(CppFunction command)
    {
        var parameters = new List<SignatureParam>();
        foreach (var parameter in command.Parameters)
        {
            string convertedType = Helpers.ConvertToCSharpType(parameter.Type);
            string typeNamePure = convertedType;
            string convertedName = parameter.Name;
            bool isPointer = convertedType.EndsWith("*");
            var type = SignatureParamType.VoidPointer;
            if (isPointer) {
                switch (convertedType) {
                    case "void*":   type = SignatureParamType.VoidPointer;  break;
                    case "char*":   type = SignatureParamType.CharPointer;  break;
                    case "uint*":   type = SignatureParamType.UIntPointer;  break;
                    default:        type = SignatureParamType.Pointer;      break;
                }
                typeNamePure = convertedType.Substring(0, convertedType.Length - 1);
            }
            parameters.Add(new SignatureParam {
                Name = convertedName,
                TypeName = convertedType,
                TypeNamePure = typeNamePure,
                Type = type,
                CppParameter = parameter
            });
        }
        return parameters.ToArray();
    }
}