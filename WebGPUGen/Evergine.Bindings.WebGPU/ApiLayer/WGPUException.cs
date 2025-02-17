using System.Diagnostics;

namespace Evergine.Bindings.WebGPU;

public class WGPUException : InvalidOperationException
{
    public readonly WGPUErrorType errorType;
        
    public WGPUException(WGPUErrorType errorType, Utf8 message) : base(message.ToString()) {
        this.errorType = errorType;
    }
    
    internal static void DefaultErrorCallback(WGPUErrorType errorType, Utf8 message) {
        _lastException = new WGPUException(errorType, message);
    }
    
    private static WGPUException? _lastException;
    
    [Conditional("VALIDATE")]
    internal static void ThrowOnError() {
        var exception = _lastException;
        _lastException = null;
        if (exception != null) {
            throw exception;
        }
    }
}