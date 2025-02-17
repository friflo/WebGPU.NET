using System.Diagnostics;
using static Evergine.Bindings.WebGPU.WebGPUNative;

// ReSharper disable InconsistentNaming
namespace Evergine.Bindings.WebGPU;

/// <see href="https://developer.mozilla.org/en-US/docs/Web/API/GPUShaderModule">MDN documentation</see>           
public unsafe partial struct WGPUShaderModule
{
    public void getCompilationInfo(delegate* unmanaged<WGPUCompilationInfoRequestStatus, WGPUCompilationInfo*, void*, void> callback, void* userdata) {
        Validate_getCompilationInfo(callback, userdata);
        wgpuShaderModuleGetCompilationInfo(this, callback, userdata);
        WGPUException.ThrowOnError();
    }

    [Conditional("VALIDATE")]
    private void Validate_getCompilationInfo(delegate* unmanaged<WGPUCompilationInfoRequestStatus, WGPUCompilationInfo*, void*, void> callback, void* userdata) {
        ObjectTracker.ValidateHandle(this);
    }


    public void reference() {
        ObjectTracker.IncRef(this);
        wgpuShaderModuleReference(this);
    }

    public void release() {
        ObjectTracker.DecRef(this);
        wgpuShaderModuleRelease(this);
    }
    
    public void Dispose() {
        ObjectTracker.DecRef(this);
        wgpuShaderModuleRelease(this);
    }

    public override string? ToString() => ObjectTracker.GetLabel(Handle);
}
