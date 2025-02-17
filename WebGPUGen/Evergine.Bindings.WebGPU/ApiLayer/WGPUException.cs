using System.Diagnostics;

namespace Evergine.Bindings.WebGPU;

public class WGPUException : InvalidOperationException
{
    public readonly WGPUErrorType errorType;
        
    public WGPUException(WGPUErrorType errorType, Utf8 message) : base(message.ToString()) {
        this.errorType = errorType;
    }

    /// <summary>
    /// Store the error as an <see cref="WGPUException"/> and throws its after calling a "wgpu*" method using <see cref="ThrowOnError"/>.<br/>
    /// <br/>
    /// The exception cannot be thrown directly because it has to pass managed -> unmanaged -> managed stack frames.<br/>
    /// This may work on Windows as it supports SEH (Structured Exception Handling) but will fail on other platforms. 
    /// </summary>
    public static void DefaultErrorCallback(WGPUErrorType errorType, Utf8 message) {
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