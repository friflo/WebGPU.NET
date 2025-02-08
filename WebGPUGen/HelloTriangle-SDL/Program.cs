using System;
using Evergine.Bindings.WebGPU;
using SDL2;

namespace HelloTriangle
{
    static class Program
    {
        private const int Width  = 800;
        private const int Height = 600;
        
        private static void Main()
        {
            var window = InitWindow();
            var gpu = new GPU();
            gpu.CreateSurface(window);
            gpu.RequestDevice(Width, Height);
            var triangle = new Triangle(gpu);
            SDL.SDL_SetWindowTitle(window, $"WGPU-Native Triangle ({gpu.adapter.info.backendType})");
            
            triangle.InitResources();

            MainLoop(triangle, gpu.frameArena, gpu.surface);

            triangle.ReleaseResources();
            gpu.CleanUp();
            Console.WriteLine($"ObjectTracker: entries: {ObjectTracker.Entries.Count}");
            
            SDL.SDL_DestroyWindow(window);
            SDL.SDL_Quit();
        }
        
        private static nint InitWindow()
        {
            // https://github.com/JeremySayers/SDL2-CS-Tutorial
            if (SDL.SDL_Init(SDL.SDL_INIT_VIDEO) < 0) {
                Console.WriteLine($"There was an issue initializing SDL. {SDL.SDL_GetError()}");
            }
            // Create a new window given a title, size, and passes it a flag indicating it should be shown.
            nint window = SDL.SDL_CreateWindow(
                "SDL .NET 6 Tutorial",
                SDL.SDL_WINDOWPOS_UNDEFINED, 
                SDL.SDL_WINDOWPOS_UNDEFINED, 
                Width, 
                Height, 
                SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN);

            if (window == IntPtr.Zero) {
                Console.WriteLine($"There was an issue creating the window. {SDL.SDL_GetError()}");
            }
            return window;
        }
        
        private static void MainLoop(Triangle triangle, Arena frameArena, WGPUSurface surface)
        {
            bool running = true;
            while (running)
            {
                // Check to see if there are any events and continue to do so until the queue is empty.
                while (SDL.SDL_PollEvent(out SDL.SDL_Event e) == 1)
                {
                    switch (e.type)
                    {
                        case SDL.SDL_EventType.SDL_QUIT:
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
