using System.Windows.Forms;
using Evergine.Bindings.WebGPU;

namespace HelloTriangle
{
    class Program
    {
        const uint WIDTH = 800;
        const uint HEIGHT = 600;
        
        static void Main(string[] args)
        {
            var window = InitWindow();
            var gpu = new GPU();
            gpu.InitSurface(window.Handle);
            gpu.InitDevice(window.ClientSize.Width, window.ClientSize.Height);
            var app = new HelloTriangle(gpu);
            window.Text = $"WGPU-Native Triangle ({gpu.Adapter.info.backendType})";
            
            app.InitResources();

            MainLoop(app, window);

            gpu.CleanUp();
            window.Dispose();
            window.Close();
        }
        
        private static Form InitWindow()
        {
            var window = new Form();
            window.Size = new System.Drawing.Size((int)WIDTH, (int)HEIGHT);
            window.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            window.Show();
            return window;
        }
        
        private static void MainLoop(HelloTriangle app, Form window)
        {
            bool isClosing = false;
            window.FormClosing += (s, e) =>
            {
                isClosing = true;
            };

            while (!isClosing)
            {
                app.DrawFrame();
                Application.DoEvents();
            }
        }
    }
}
