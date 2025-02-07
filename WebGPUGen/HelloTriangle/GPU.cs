using System;
using System.Diagnostics;
using Evergine.Bindings.WebGPU;

namespace HelloTriangle;

public class GPU
{
    internal WGPUInstance        Instance;
    internal WGPUSurface         Surface;
    internal WGPUAdapter         Adapter;
    internal WGPUDevice          Device;
    internal WGPUTextureFormat   SwapChainFormat;
    internal WGPUQueue           Queue;
    
    private static void UncapturedErrorCallback(WGPUErrorType errorType, Utf8 message) {
        Console.WriteLine($"Uncaptured device error: type: {errorType} ({message.ToString()})");
    }
    
    internal void InitSurface(IntPtr hWnd) {
        Instance = WebGPUNative.wgpuCreateInstance(new WGPUInstanceExtras {
            backends = WGPUInstanceBackend.Vulkan
        });
        Surface = Instance.createSurfaceFromWindowsHWND(new WGPUSurfaceDescriptor(), Process.GetCurrentProcess().Handle, hWnd);
    }

    internal void InitDevice(int width, int height)
    {
        // --- create Adapter
        WGPURequestAdapterOptions options = new WGPURequestAdapterOptions {
            compatibleSurface   = Surface,
            powerPreference     = WGPUPowerPreference.HighPerformance
        };
        Instance.requestAdapter(options, (in RequestAdapterResult result) => {
            if (result.status != WGPURequestAdapterStatus.Success) {
                throw new Exception($"Failed to create adapter: {result.Message.ToString()}");
            }
            Adapter = result.adapter;    
        });
        // --- create Device
        var deviceDescriptor = new WGPUDeviceDescriptor { label = "Device"u8 };
        Adapter.requestDevice(deviceDescriptor, UncapturedErrorCallback, (in RequestDeviceResult result) => {
            if (result.status != WGPURequestDeviceStatus.Success) {
                throw new Exception($"Failed to request device. {result.Message.ToString()}");
            }
            Device = result.device;
        });
        Queue = Device.queue;
        var capabilities = Surface.getCapabilities(Adapter);
        SwapChainFormat = capabilities.formats[0];

        var surfaceConfiguration = new WGPUSurfaceConfiguration {
            device      = Device,
            format      = SwapChainFormat,
            usage       = WGPUTextureUsage.RenderAttachment,
            width       = (uint)width,
            height      = (uint)height,
            presentMode = WGPUPresentMode.Fifo,
        };
        Surface.configure(surfaceConfiguration);
    }
    
    internal void CleanUp()
    {
        // Queue is not released
        Surface.release();
        Device.destroy();
        Device.release();
        Adapter.release();
        Instance.release();
    }
}