using System;
using Evergine.Bindings.WebGPU;
using SDL;
using static SDL.SDL3;

namespace HelloTriangle;

public class GPU
{
    internal            WGPUInstance        instance;
    internal            WGPUSurface         surface;
    internal            WGPUAdapter         adapter;
    internal            WGPUDevice          device;
    internal            WGPUTextureFormat   swapChainFormat;
    internal            WGPUQueue           queue;
    internal readonly   Arena               frameArena = new Arena("frameArena");
    
    /// platform specific WGPU initialization
    internal unsafe void CreateSurface(SDL_Window* window)
    {
        frameArena.Use();
        if (OperatingSystem.IsWindows()) {
            instance = WebGPUNative.wgpuCreateInstance(new WGPUInstanceExtras {
                backends = WGPUInstanceBackend.Vulkan
            });
            var properties = SDL_GetWindowProperties(window);
            nint hinstance = SDL_GetPointerProperty(properties, SDL_PROP_WINDOW_WIN32_INSTANCE_POINTER, 0);
            nint hwnd      = SDL_GetPointerProperty(properties, SDL_PROP_WINDOW_WIN32_HWND_POINTER,     0);
            surface = instance.createSurfaceFromWindowsHWND(new WGPUSurfaceDescriptor(), hinstance, hwnd);
        }
        else if (OperatingSystem.IsMacOS() || OperatingSystem.IsIOS()) {
            instance = WebGPUNative.wgpuCreateInstance(new WGPUInstanceExtras {
                backends = WGPUInstanceBackend.Metal
            });
            var renderer   = SDL_CreateRenderer(window, (Utf8String)null);
            var metalLayer = SDL_GetRenderMetalLayer(renderer);
            surface = instance.createSurfaceFromMetalLayer(new WGPUSurfaceDescriptor(), metalLayer);
        }
        else if (OperatingSystem.IsLinux()) {
            Console.WriteLine("Untested platform: Linux. Feedback appreciated!");  // Please create GitHub issues if it works or fails
            instance = WebGPUNative.wgpuCreateInstance(new WGPUInstanceExtras {
                backends = WGPUInstanceBackend.Vulkan
            });
            var properties  = SDL_GetWindowProperties(window);
            nint surfacePtr = SDL_GetPointerProperty(properties, SDL_PROP_WINDOW_WAYLAND_SURFACE_POINTER, 0);
            nint displayPtr = SDL_GetPointerProperty(properties, SDL_PROP_WINDOW_WAYLAND_DISPLAY_POINTER, 0);
            if (surfacePtr != 0 && displayPtr != 0) {
                surface = instance.createSurfaceFromWaylandSurface(new WGPUSurfaceDescriptor(), surfacePtr, displayPtr);
                return;
            }
            displayPtr      = SDL_GetPointerProperty(properties, SDL_PROP_WINDOW_X11_DISPLAY_POINTER, 0);
            var windowNum   = SDL_GetNumberProperty (properties, SDL_PROP_WINDOW_X11_WINDOW_NUMBER,   0);
            surface = instance.createSurfaceFromXlibWindow(new WGPUSurfaceDescriptor(), (ulong)windowNum, displayPtr);
        }
        else if (OperatingSystem.IsAndroid()) {
            Console.WriteLine("Untested platform: Android. Feedback appreciated!"); // Please create GitHub issues if it works or fails
            instance = WebGPUNative.wgpuCreateInstance(new WGPUInstanceExtras {
                backends = WGPUInstanceBackend.Vulkan
            });
            var properties  = SDL_GetWindowProperties(window);
            nint windowPtr  = SDL_GetPointerProperty(properties, SDL_PROP_WINDOW_ANDROID_WINDOW_POINTER, 0);
            surface = instance.createSurfaceFromAndroidNativeWindow(new WGPUSurfaceDescriptor(), windowPtr);
        }
        else {
            var platform = Environment.OSVersion.Platform;
            Console.WriteLine($"Platform not implemented. platform: {platform}");
        }
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
        adapter.requestDevice(deviceDescriptor, null, (in RequestDeviceResult result) => {
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