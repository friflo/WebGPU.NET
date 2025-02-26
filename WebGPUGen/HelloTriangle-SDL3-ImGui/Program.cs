﻿using System;
using System.IO;
using Evergine.Bindings.WebGPU;
using Friflo.ImGuiNet;
using ImGuiNET;
// using SDL2;
using SDL;
using SDLIM;
using static SDL.SDL3;


namespace HelloTriangle
{
    static class Program
    {
        private const int Width  = 3200;
        private const int Height = 1600;
        
        private static unsafe void Main()
        {
            var window = InitWindow();
            var gpu = new GPU();
            gpu.CreateSurface(window);
            gpu.RequestDevice(Width, Height);
            
            // ImGui setup
            ImGuiTools.SetupContext(window, gpu.device, gpu.swapChainFormat);
            var io = ImGui.GetIO();
            io.ConfigFlags |=  ImGuiConfigFlags.DockingEnable | ImGuiConfigFlags.NavEnableKeyboard;
            io.Fonts.AddFontFromFileTTF(Path.Combine(AppContext.BaseDirectory, "Content", "Inter-Regular.ttf"), 40); // alternative io.Fonts.AddFontDefault();
            io.Fonts.Build();
            
            var triangle = new Triangle(gpu);
            SDL_SetWindowTitle(window, $"WGPU-Native Triangle (SDL 3 - {gpu.adapter.info.backendType})");
            
            triangle.InitResources();

            MainLoop(triangle, gpu.frameArena, gpu.surface, gpu.queue);

            triangle.ReleaseResources();
            gpu.CleanUp();
            Console.WriteLine($"ObjectTracker - handles: {ObjectTracker.Handles.Count}");
            Console.WriteLine(ObjectTracker.GroupedHandlesLog());
            
            SDL_DestroyWindow(window);
            SDL_Quit();
        }
        
        private static unsafe SDL_Window* InitWindow()
        {
            if (!SDL_Init(SDL_InitFlags.SDL_INIT_VIDEO)) {
                Console.WriteLine($"There was an issue initializing SDL. {SDL_GetError()}");
            }
            var window = SDL_CreateWindow("SDL3", Width, Height, 0);
            if (window == null) {
                Console.WriteLine($"There was an issue creating the window. {SDL_GetError()}");
            }
            return window;
        }
        
        private static unsafe void MainLoop(Triangle triangle, Arena frameArena, WGPUSurface surface, WGPUQueue queue)
        {
            var store = EcsGuiUtils.CreateTestStore();
            var query = store.Query();
            EcsGui.Explorer.AddQuery(query);
            
            bool running = true;
            while (running)
            {
                SDL_Event sdlEvent;
                while (SDL_PollEvent(&sdlEvent)) {
                    ImGui_ImplSDL3.ProcessEvent(&sdlEvent);
                    switch ((SDL_EventType)sdlEvent.type) {
                        case SDL_EventType.SDL_EVENT_QUIT:
                            running = false;
                            break;
                    }
                }
                var surfaceTexture = surface.getCurrentTexture();
                // Getting the texture may fail, in particular if the window has been resized and thus the target surface changed.
                if (surfaceTexture.status != WGPUSurfaceGetCurrentTextureStatus.Success) {
                    throw new Exception($"Failed to retrieve surface texture. status: {surfaceTexture.status}");
                }
                WGPUTextureView nextView = surfaceTexture.texture.createView();
                
                frameArena.Reset();
                
                using var command = triangle.DrawFrame(nextView);
                
                // --- ImGui Draw
                ImGuiTools.NewFrame();
                ImGuiTest.Draw();
                EcsGuiUtils.DrawEcsWindows();
                ImGuiTools.EndFrame();
                using var guiCommand = ImGuiTools.DrawCommands(nextView);
                
                queue.submit([command, guiCommand]);
                
                nextView.release();
                surface.present();
                surfaceTexture.texture.release();
            }
        }
    }
}
