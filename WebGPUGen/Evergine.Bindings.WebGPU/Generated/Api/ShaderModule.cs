namespace Evergine.Bindings.WebGPU;
using static WebGPUNative;
           
public unsafe partial struct WGPUShaderModule
{
    public void getCompilationInfo(delegate* unmanaged<WGPUCompilationInfoRequestStatus, WGPUCompilationInfo*, void*, void> callback, void* userdata) {
        wgpuShaderModuleGetCompilationInfo(Handle, callback, userdata);
    }

    public void setLabel(ReadOnlySpan<char> label) {
        wgpuShaderModuleSetLabel(Handle, label.AllocString());
    }

    public void reference() {
        wgpuShaderModuleReference(Handle);
        ObjectTracker.IncRef(Handle);
    }

    public void release() {
        ObjectTracker.DecRef(Handle);
        wgpuShaderModuleRelease(Handle);
    }

}
