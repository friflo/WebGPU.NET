using System;
using Evergine.Bindings.WebGPU;
using ImGuiNET;
using SDL;

namespace SDLIM;

public static unsafe class ImGuiTools
{
    private static WGPUDevice  _device;
    
    public static void SetupContext(SDL_Window* window, WGPUDevice device, WGPUTextureFormat textureFormat)
    {
        _device = device;

        IntPtr context = ImGui.CreateContext();
        ImGui.SetCurrentContext(context);

        var io = ImGui.GetIO();
        io.ConfigFlags |= ImGuiConfigFlags.DockingEnable;

        var initInfo = new ImGui_ImplWGPU.ImGui_ImplWGPU_InitInfo {
            Device = device,
            NumFramesInFlight = 3,
            RenderTargetFormat = textureFormat,
            DepthStencilFormat = WGPUTextureFormat.Undefined,
        };

        ImGui_ImplWGPU.Init(initInfo);
        ImGui_ImplSDL3.Init(window, null, null);

        io.Fonts.AddFontDefault();
        io.Fonts.Build();
    }
    
    public static void NewFrame()
    {
        ImGui_ImplSDL3.NewFrame();
        ImGui_ImplWGPU.NewFrame();
        ImGui.NewFrame();
    }
    
    public static void EndFrame()
    {
        ImGui.EndFrame();
    }
    
    public static WGPUCommandBuffer DrawCommands(WGPUTextureView textureView)
    {
        using var commandEncoder = _device.createCommandEncoder(new() { label = "ImGuiTools"u8 });

        Span<WGPURenderPassColorAttachment> colorAttachments = [
            new WGPURenderPassColorAttachment {
                view = textureView,
                resolveTarget = default,
                loadOp = WGPULoadOp.Load,
                storeOp = WGPUStoreOp.Store,
                clearValue = new WGPUColor()
            }
        ];

        WGPURenderPassDescriptor renderPassDesc = new() {
            label = "ImGuiTools"u8,
            colorAttachments = colorAttachments,
            depthStencilAttachment = null
        };
        var renderPass = commandEncoder.beginRenderPass(renderPassDesc);

        ImGui.Render();
        ImGui_ImplWGPU.RenderDrawData(ImGui.GetDrawData(), renderPass);

        renderPass.end();
        renderPass.release();

        return commandEncoder.finish();
    }
}