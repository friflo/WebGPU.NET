using System;
using System.Diagnostics;
using Evergine.Bindings.WebGPU;
using SDL2;

namespace HelloTriangle;

public class GPU
{
    private             WGPUInstance        instance;
    internal            WGPUSurface         surface;
    internal            WGPUAdapter         adapter;
    internal            WGPUDevice          device;
    internal            WGPUTextureFormat   swapChainFormat;
    internal            WGPUQueue           queue;
    internal readonly   Arena               frameArena = new Arena("frameArena");
    
    private static void UncapturedErrorCallback(WGPUErrorType errorType, Utf8 message) {
        Console.WriteLine($"Uncaptured device error: type: {errorType} ({message.ToString()})");
    }
    
    /// platform specific WGPU initialization
    internal void CreateSurface(nint window) {
        frameArena.Use();
        instance = WebGPUNative.wgpuCreateInstance(new WGPUInstanceExtras {
            backends = WGPUInstanceBackend.Vulkan
        });
        var windowWMInfo = new SDL.SDL_SysWMinfo();
        SDL.SDL_VERSION(out windowWMInfo.version);
        SDL.SDL_GetWindowWMInfo(window, ref windowWMInfo);
        var hinstance   = windowWMInfo.info.win.hinstance;
        var hwnd        = windowWMInfo.info.win.window;
        surface = instance.createSurfaceFromWindowsHWND(new WGPUSurfaceDescriptor(), hinstance, hwnd);
    }

    /// general WGPU initialization
    internal void RequestDevice(int width, int height)
    {
        // --- create Adapter
        WGPURequestAdapterOptions options = new WGPURequestAdapterOptions {
            compatibleSurface   = surface,
            powerPreference     = WGPUPowerPreference.HighPerformance
        };
        instance.requestAdapter(options, (in RequestAdapterResult result) => {
            if (result.status != WGPURequestAdapterStatus.Success) {
                throw new Exception($"Failed to create adapter: {result.Message.ToString()}");
            }
            adapter = result.adapter;    
        });
        // --- create Device
        var deviceDescriptor = new WGPUDeviceDescriptor { label = "Device"u8 };
        adapter.requestDevice(deviceDescriptor, UncapturedErrorCallback, (in RequestDeviceResult result) => {
            if (result.status != WGPURequestDeviceStatus.Success) {
                throw new Exception($"Failed to request device. {result.Message.ToString()}");
            }
            device = result.device;
        });
        queue = device.queue;
        var capabilities = surface.getCapabilities(adapter);
        swapChainFormat = capabilities.formats[0];

        var surfaceConfiguration = new WGPUSurfaceConfiguration {
            device      = device,
            format      = swapChainFormat,
            usage       = WGPUTextureUsage.RenderAttachment,
            width       = (uint)width,
            height      = (uint)height,
            presentMode = WGPUPresentMode.Fifo,
        };
        surface.configure(surfaceConfiguration);
    }
    
    internal void CleanUp()
    {
        // Queue is not released
        queue.release();
        surface.release();
        device.destroy();
        device.release();
        adapter.release();
        instance.release();
    }
}