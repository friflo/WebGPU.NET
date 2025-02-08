using System;
using Evergine.Bindings.WebGPU;
// using SDL2;
using SDL;
using static SDL.SDL3;

namespace HelloTriangle
{
    static class Program
    {
        private const int Width  = 1600;
        private const int Height = 1200;
        
        private static unsafe void Main()
        {
            var window = InitWindow();
            var gpu = new GPU();
            gpu.CreateSurface(window);
            gpu.RequestDevice(Width, Height);
            var triangle = new Triangle(gpu);
        //  SDL.SDL_SetWindowTitle(window, $"WGPU-Native Triangle (SDL - {gpu.adapter.info.backendType})");
            SDL_SetWindowTitle(window, $"WGPU-Native Triangle (SDL - {gpu.adapter.info.backendType})");
            
            triangle.InitResources();

            MainLoop(triangle, gpu.frameArena, gpu.surface);

            triangle.ReleaseResources();
            gpu.CleanUp();
            Console.WriteLine($"ObjectTracker: entries: {ObjectTracker.Entries.Count}");
            
            SDL_DestroyWindow(window);
            SDL_Quit();
        }
        
        private static unsafe SDL_Window* InitWindow()
        {
            // https://github.com/JeremySayers/SDL2-CS-Tutorial
            if (!SDL_Init(SDL_InitFlags.SDL_INIT_VIDEO)) {
                Console.WriteLine($"There was an issue initializing SDL. {SDL_GetError()}");
            }
            // Create a new window given a title, size, and passes it a flag indicating it should be shown.
            // nint window = SDL.SDL_CreateWindow("SDL .NET 6 Tutorial", SDL.SDL_WINDOWPOS_UNDEFINED, SDL.SDL_WINDOWPOS_UNDEFINED, Width, Height, SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN);
            var window = SDL_CreateWindow("SDL3", Width, Height, 0);
            if (window == null) {
                Console.WriteLine($"There was an issue creating the window. {SDL_GetError()}");
            }
            return window;
        }
        
        private static unsafe void MainLoop(Triangle triangle, Arena frameArena, WGPUSurface surface)
        {
            bool running = true;
            while (running)
            {
                // Check to see if there are any events and continue to do so until the queue is empty.
                /* while (SDL.SDL_PollEvent(out SDL.SDL_Event e) == 1) {
                    switch (e.type) {
                        case SDL.SDL_EventType.SDL_QUIT:
                            running = false;
                            break;
                    }
                } */
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
                triangle.DrawFrame(nextView);
                
                nextView.release();
                surface.present();
            }
        }
    }
}
