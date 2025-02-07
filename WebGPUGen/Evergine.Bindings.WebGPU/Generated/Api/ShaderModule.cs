namespace Evergine.Bindings.WebGPU;
using static WebGPUNative;

/// <see href="https://developer.mozilla.org/en-US/docs/Web/API/GPUShaderModule">MDN documentation</see>           
public unsafe partial struct WGPUShaderModule
{
    public void getCompilationInfo(delegate* unmanaged<WGPUCompilationInfoRequestStatus, WGPUCompilationInfo*, void*, void> callback, void* userdata) {
        wgpuShaderModuleGetCompilationInfo(Handle, callback, userdata);
    }

    public void setLabel(Utf8 label) {
        wgpuShaderModuleSetLabel(Handle, label.AllocUtf8());
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
