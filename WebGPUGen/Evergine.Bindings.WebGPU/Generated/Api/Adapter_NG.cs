using System.Runtime.InteropServices;
using System.Text;
using static Evergine.Bindings.WebGPU.WebGPUNative;

namespace Evergine.Bindings.WebGPU;


public unsafe struct RequestDeviceResult
{
    public      WGPURequestDeviceStatus  status;
    public      WGPUDevice               device;
    internal    char*                    message;
    public      Utf8                     Message => ApiUtils.GetUtf8(message);
}

public delegate void RequestDeviceCallback(in RequestDeviceResult result);

public delegate void UncapturedErrorCallback(WGPUErrorType errorType, Utf8 message);


public unsafe partial struct WGPUAdapter
{
    public void requestDevice(WGPUDeviceDescriptor descriptor, UncapturedErrorCallback? errorCallback, RequestDeviceCallback? callback)
    {
        descriptor.Validate();
        if (errorCallback is not null) {
            var errorUserData = UserData.Create(default, errorCallback);
            descriptor.uncapturedErrorCallbackInfo = new WGPUUncapturedErrorCallbackInfo {
                callback = &HandleUncapturedErrorCallback,
                userdata = errorUserData
            };
        }
        var userData = UserData.Create(descriptor.label, callback);
        wgpuAdapterRequestDevice(this, &descriptor, &requestDeviceCallback, userData);
    }
    
    // untested
    public Task<RequestDeviceResult> requestDeviceAsync(WGPUDeviceDescriptor descriptor, UncapturedErrorCallback? errorCallback)
    {
        var tcs = new TaskCompletionSource<RequestDeviceResult>();
        requestDevice(descriptor, errorCallback, (in RequestDeviceResult result) => {
            tcs.SetResult(result);
        });
        return tcs.Task;
    }
    
    [UnmanagedCallersOnly]
    // delegate* unmanaged                   <WGPURequestDeviceStatus,        WGPUDevice,        char*,         void*, void> callback
    private static void requestDeviceCallback(WGPURequestDeviceStatus status, WGPUDevice device, char* message, void* pUserData)
    {
        var userData = (UserData*)pUserData;
        var callbackHandle = userData->callbackHandle;
        try {
            if (!callbackHandle.IsAllocated) {
                return;
            }
            var callback = (RequestDeviceCallback)callbackHandle.Target!;
            ObjectTracker.CreateRef(device, HandleType.WGPUDevice, (char*)userData->label);
            var result = new RequestDeviceResult {
                status = status,
                device = device,
                message = message
            };
            callback(result);
        }
        finally {
            UserData.Free(userData);
        }
    }
    
    [UnmanagedCallersOnly]
    private static void HandleUncapturedErrorCallback(WGPUErrorType type, char* pMessage, void* pUserData)
    {
        var userData = (UserData*)pUserData;
        var callbackHandle = userData->callbackHandle;
        if (!callbackHandle.IsAllocated) {
            throw new InvalidOperationException("error callback is not allocated");
        }
        var callback = (UncapturedErrorCallback)callbackHandle.Target!;
        var message = new Utf8(pMessage);
        callback(type, message);
    }
    
    public WGPUSupportedLimits getLimits() {
        WGPUSupportedLimits result;
        var success = wgpuAdapterGetLimits(this, &result);
        return result;
    }
    
    private static string? PtrToString(char* ptr) {
        if (ptr == null) {
            return null;
        }
        int len = 0;
        var bytePtr = (byte*)ptr;
        while (bytePtr[len] != 0) {
            len++;
        }
        return Encoding.UTF8.GetString(bytePtr, len);
    }
    
    /// Implemented manually as the returned <see cref="WGPUAdapterInfo"/> must be freed 
    public AdapterInfo info {
        get {
            WGPUAdapterInfo wgpuInfo;
            wgpuAdapterGetInfo(this, &wgpuInfo);
            var result = new AdapterInfo {
                vendor          = PtrToString(wgpuInfo._vendor),
                architecture    = PtrToString(wgpuInfo._architecture),
                device          = PtrToString(wgpuInfo._device),
                description     = PtrToString(wgpuInfo._description),
                backendType     = wgpuInfo.backendType,
                adapterType     = wgpuInfo.adapterType,
                vendorID        = wgpuInfo.vendorID,
                deviceID        = wgpuInfo.deviceID,
            };
            wgpuAdapterInfoFreeMembers(wgpuInfo);
            return result;
        }
    }
}

/// <see cref="WGPUAdapterInfo"/>
public struct AdapterInfo
{
    public  string?         vendor;
    public  string?         architecture;
    public  string?         device;
    public  string?         description;
    public  WGPUBackendType backendType;
    public  WGPUAdapterType adapterType;
    public  uint            vendorID;
    public  uint            deviceID;
}
