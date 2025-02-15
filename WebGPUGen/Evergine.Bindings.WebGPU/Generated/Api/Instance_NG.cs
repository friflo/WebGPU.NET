using System.Runtime.InteropServices;
using static Evergine.Bindings.WebGPU.WebGPUNative;

// ReSharper disable InconsistentNaming
namespace Evergine.Bindings.WebGPU;

public unsafe struct RequestAdapterResult
{
    public      WGPURequestAdapterStatus status;
    public      WGPUAdapter              adapter;
    internal    char*                    message;
    public      Utf8                     Message => ApiUtils.GetUtf8(message);
}

public delegate void RequestAdapterCallback(in RequestAdapterResult result);

/// No counterpart in JavaScript WebGPU           
public unsafe partial struct WGPUInstance
{
#region create Surface
    public WGPUSurface createSurfaceFromWindowsHWND(WGPUSurfaceDescriptor descriptor, IntPtr hInstance, IntPtr hWnd)
    {
        ObjectTracker.ValidateHandle(this);
        descriptor.Validate();
        
        var windowsSurface = new WGPUSurfaceDescriptorFromWindowsHWND {
            chain = new WGPUChainedStruct {
                sType = WGPUSType.SurfaceDescriptorFromWindowsHWND,
            },
            Hinstance = hInstance,
            Hwnd = hWnd
        };
        descriptor._nextInChain = &windowsSurface.chain;
        var result = wgpuInstanceCreateSurface(this, &descriptor);
        ObjectTracker.CreateRefLabel(result, HandleType.WGPUSurface, descriptor._label);
        return result;
    }
    
    public WGPUSurface createSurfaceFromMetalLayer(WGPUSurfaceDescriptor descriptor, IntPtr layer)
    {
        ObjectTracker.ValidateHandle(this);
        descriptor.Validate();
        
        var windowsSurface = new WGPUSurfaceDescriptorFromMetalLayer {
            chain = new WGPUChainedStruct {
                sType = WGPUSType.SurfaceDescriptorFromMetalLayer
            },
            Layer = layer
        };
        descriptor._nextInChain = &windowsSurface.chain;
        var result = wgpuInstanceCreateSurface(this, &descriptor);
        ObjectTracker.CreateRefLabel(result, HandleType.WGPUSurface, descriptor._label);
        return result;
    }
    
    public WGPUSurface createSurfaceFromWaylandSurface(WGPUSurfaceDescriptor descriptor, IntPtr surface, IntPtr display)
    {
        ObjectTracker.ValidateHandle(this);
        descriptor.Validate();
        
        var windowsSurface = new WGPUSurfaceDescriptorFromWaylandSurface {
            chain = new WGPUChainedStruct {
                sType = WGPUSType.SurfaceDescriptorFromWaylandSurface
            },
            Surface = surface,
            Display = display
        };
        descriptor._nextInChain = &windowsSurface.chain;
        var result = wgpuInstanceCreateSurface(this, &descriptor);
        ObjectTracker.CreateRefLabel(result, HandleType.WGPUSurface, descriptor._label);
        return result;
    }
    
    public WGPUSurface createSurfaceFromAndroidNativeWindow(WGPUSurfaceDescriptor descriptor, IntPtr window)
    {
        ObjectTracker.ValidateHandle(this);
        descriptor.Validate();
        
        var windowsSurface = new WGPUSurfaceDescriptorFromAndroidNativeWindow {
            chain = new WGPUChainedStruct {
                sType = WGPUSType.SurfaceDescriptorFromAndroidNativeWindow
            },
            Window = window
        };
        descriptor._nextInChain = &windowsSurface.chain;
        var result = wgpuInstanceCreateSurface(this, &descriptor);
        ObjectTracker.CreateRefLabel(result, HandleType.WGPUSurface, descriptor._label);
        return result;
    }
    
    public WGPUSurface createSurfaceFromXlibWindow(WGPUSurfaceDescriptor descriptor, ulong window, IntPtr display)
    {
        ObjectTracker.ValidateHandle(this);
        descriptor.Validate();
        
        var windowsSurface = new WGPUSurfaceDescriptorFromXlibWindow {
            chain = new WGPUChainedStruct {
                sType = WGPUSType.SurfaceDescriptorFromXlibWindow
            },
            window = window,
            Display = display,
        };
        descriptor._nextInChain = &windowsSurface.chain;
        var result = wgpuInstanceCreateSurface(this, &descriptor);
        ObjectTracker.CreateRefLabel(result, HandleType.WGPUSurface, descriptor._label);
        return result;
    }
    
    public WGPUSurface createSurfaceFromXcbWindow(WGPUSurfaceDescriptor descriptor, uint window, IntPtr connection)
    {
        ObjectTracker.ValidateHandle(this);
        descriptor.Validate();
        
        var windowsSurface = new WGPUSurfaceDescriptorFromXcbWindow {
            chain = new WGPUChainedStruct {
                sType = WGPUSType.SurfaceDescriptorFromXcbWindow
            },
            window = window,
            Connection = connection
        };
        descriptor._nextInChain = &windowsSurface.chain;
        var result = wgpuInstanceCreateSurface(this, &descriptor);
        ObjectTracker.CreateRefLabel(result, HandleType.WGPUSurface, descriptor._label);
        return result;
    }
    #endregion
    
#region request Adapter
    public void requestAdapter(WGPURequestAdapterOptions options, RequestAdapterCallback? callback)
    {
        ObjectTracker.ValidateHandle(this);
        options.Validate();
        
        var userData = UserData.Create(default, callback);
        wgpuInstanceRequestAdapter(this, &options, &requestAdapterCallback, userData);
    }
    
    [UnmanagedCallersOnly]
    // delegate* unmanaged                    <WGPURequestAdapterStatus,        WGPUAdapter,         char*,         void*, void> callback
    private static void requestAdapterCallback(WGPURequestAdapterStatus status, WGPUAdapter adapter, char* message, void* pUserData)
    {
        var userData = (UserData*)pUserData;
        var callbackHandle = userData->callbackHandle;
        try {
            if (!callbackHandle.IsAllocated) {
                return;
            }
            var callback = (RequestAdapterCallback)callbackHandle.Target!;
            ObjectTracker.CreateRefLabel(adapter, HandleType.WGPUAdapter, (char*)userData->label);
            var result = new RequestAdapterResult {
                status = status,
                adapter = adapter,
                message = message
            };
            callback(result);
        }
        finally {
            UserData.Free(userData);
        }
    }
    #endregion
}
