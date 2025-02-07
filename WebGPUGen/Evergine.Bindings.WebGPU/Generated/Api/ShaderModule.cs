using static Evergine.Bindings.WebGPU.WebGPUNative;

// ReSharper disable InconsistentNaming
namespace Evergine.Bindings.WebGPU;

/// <see href="https://developer.mozilla.org/en-US/docs/Web/API/GPUShaderModule">MDN documentation</see>           
public unsafe partial struct WGPUShaderModule
{
    public void getCompilationInfo(delegate* unmanaged<WGPUCompilationInfoRequestStatus, WGPUCompilationInfo*, void*, void> callback, void* userdata) {
        Validate_getCompilationInfo(Handle, callback, userdata);
        wgpuShaderModuleGetCompilationInfo(Handle, callback, userdata);
    }

    private static void Validate_getCompilationInfo(IntPtr handle, delegate* unmanaged<WGPUCompilationInfoRequestStatus, WGPUCompilationInfo*, void*, void> callback, void* userdata) {
        ObjectTracker.ValidateHandle(handle);
    }

    public void setLabel(Utf8 label) {
        Validate_setLabel(Handle, label);
        wgpuShaderModuleSetLabel(Handle, label.AllocUtf8());
    }

    private static void Validate_setLabel(IntPtr handle, Utf8 label) {
        ObjectTracker.ValidateHandle(handle);
    }

    public void reference() {
        ObjectTracker.IncRef(Handle);
        wgpuShaderModuleReference(Handle);
    }

    public void release() {
        ObjectTracker.DecRef(Handle);
        wgpuShaderModuleRelease(Handle);
    }

    public override string? ToString() => ObjectTracker.GetLabel(Handle);
}
