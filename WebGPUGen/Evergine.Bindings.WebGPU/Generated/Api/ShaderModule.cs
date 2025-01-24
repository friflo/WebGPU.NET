namespace Evergine.Bindings.WebGPU;
using static WebGPUNative;
           
public static unsafe partial class WebGPUExtensions
{
    public static void getCompilationInfo(this WGPUShaderModule shaderModule, delegate* unmanaged<WGPUCompilationInfoRequestStatus, WGPUCompilationInfo*, void*, void> callback, void* userdata) {
        wgpuShaderModuleGetCompilationInfo(shaderModule, callback, userdata);
    }

    public static void setLabel(this WGPUShaderModule shaderModule, ReadOnlySpan<char> label) {
        wgpuShaderModuleSetLabel(shaderModule, label.AllocString());
    }

    public static void reference(this WGPUShaderModule shaderModule) {
        wgpuShaderModuleReference(shaderModule);
        ObjectTracker.IncRef(shaderModule.Handle);
    }

    public static void release(this WGPUShaderModule shaderModule) {
        ObjectTracker.DecRef(shaderModule.Handle);
        wgpuShaderModuleRelease(shaderModule);
    }

}
