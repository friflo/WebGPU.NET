using System;
using Evergine.Bindings.WebGPU;
// using SDL2;
using SDL;
using static SDL.SDL3;

namespace HelloTriangle
{
    static class Program
    {
        internal const int Width  = 800;
        internal const int Height = 600;
        
        private static unsafe void Main()
        {
            var window = InitWindow();
            var gpu = new GPU();
            gpu.CreateSurface(window);
            gpu.RequestDevice(Width, Height);
            var cube = new TexturedCube(gpu);
            SDL_SetWindowTitle(window, $"WGPU-Native Triangle (SDL 3 - {gpu.adapter.info.backendType})");
            
            cube.InitResources();

            MainLoop(cube, gpu.frameArena, gpu.surface);

            cube.ReleaseResources();
            gpu.CleanUp();
            Console.WriteLine($"ObjectTracker: entries: {ObjectTracker.Entries.Count}");
            
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
        
        private static unsafe void MainLoop(TexturedCube cubes2, Arena frameArena, WGPUSurface surface)
        {
            bool running = true;
            while (running)
            {
                SDL_Event sdlEvent;
                while (SDL_PollEvent(&sdlEvent)) {
                    switch ((SDL_EventType)sdlEvent.type) {
                        case SDL_EventType.SDL_EVENT_QUIT:
                            running = false;
                            break;
                    }
                }
                var surfaceTexture = surface.currentTexture;
                // Getting the texture may fail, in particular if the window has been resized and thus the target surface changed.
                if (surfaceTexture.status != WGPUSurfaceGetCurrentTextureStatus.Success) {
                    throw new Exception($"Failed to retrieve surface texture. status: {surfaceTexture.status}");
                }
                WGPUTextureView nextView = surfaceTexture.texture.createView();
                
                frameArena.Reset();
                cubes2.DrawFrame(nextView);
                
                nextView.release();
                surface.present();
            }
        }
    }
}
