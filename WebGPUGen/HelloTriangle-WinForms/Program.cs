using System;
using System.Windows.Forms;
using Evergine.Bindings.WebGPU;

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
            gpu.CreateSurface(window.Handle);
            gpu.RequestDevice(window.ClientSize.Width, window.ClientSize.Height);
            var triangle = new Triangle(gpu);
            window.Text = $"WGPU-Native Triangle (WinForms - {gpu.adapter.info.backendType})";
            
            triangle.InitResources();

            MainLoop(triangle, window, gpu.frameArena, gpu.surface);

            triangle.ReleaseResources();
            gpu.CleanUp();
            Console.WriteLine($"ObjectTracker: entries: {ObjectTracker.Entries.Count}");
            
            window.Dispose();
            window.Close();
        }
        
        private static Form InitWindow()
        {
            var window = new Form();
            window.Size = new System.Drawing.Size((int)Width, (int)Height);
            window.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            window.Show();
            return window;
        }
        
        private static void MainLoop(Triangle triangle, Form window, Arena frameArena, WGPUSurface surface)
        {
            bool running = true;
            window.FormClosing += (_, _) => {
                running = false;
            };

            while (running) {
                Application.DoEvents();

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
