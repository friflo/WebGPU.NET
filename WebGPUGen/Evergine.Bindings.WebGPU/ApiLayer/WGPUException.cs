namespace Evergine.Bindings.WebGPU;

public class WGPUException : InvalidOperationException
{
    public readonly WGPUErrorType errorType;
        
    public WGPUException(WGPUErrorType errorType, Utf8 message) : base(message.ToString()) {
        this.errorType = errorType;
    }
    
    internal static void DefaultErrorCallback(WGPUErrorType errorType, Utf8 message) {
        throw new WGPUException(errorType, message);
    }
}