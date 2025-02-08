using System;
using System.Windows.Forms;
using Evergine.Bindings.WebGPU;

namespace HelloTriangle
{
     static partial class Program
    {

        private static void Main()
        {
            var window = InitWindowWinForms();
            var gpu = new GPU();
            gpu.CreateSurface(window.Handle);
            gpu.RequestDevice(window.ClientSize.Width, window.ClientSize.Height);
            var triangle = new Triangle(gpu);
            window.Text = $"WGPU-Native Triangle ({gpu.adapter.info.backendType})";
            
            triangle.InitResources();

            MainLoopWinForms(triangle, window, gpu.frameArena, gpu.surface);

            triangle.ReleaseResources();
            gpu.CleanUp();
            Console.WriteLine($"ObjectTracker: entries: {ObjectTracker.Entries.Count}");
            window.Dispose();
            window.Close();
        }
        
        private static Form InitWindowWinForms()
        {
            var window = new Form();
            window.Size = new System.Drawing.Size((int)Width, (int)Height);
            window.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            window.Show();
            return window;
        }
        
        private static void MainLoopWinForms(Triangle triangle, Form window, Arena frameArena, WGPUSurface surface)
        {
            bool isClosing = false;
            window.FormClosing += (_, _) => {
                isClosing = true;
            };

            while (!isClosing) {
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
                
                Application.DoEvents();
            }
        }
    }
}
