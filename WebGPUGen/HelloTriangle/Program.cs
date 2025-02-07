using System.Windows.Forms;

namespace HelloTriangle
{
    class Program
    {
        private const uint Width  = 800;
        private const uint Height = 600;
        
        static void Main()
        {
            var window = InitWindow();
            var gpu = new GPU();
            gpu.InitSurface(window.Handle);
            gpu.InitDevice(window.ClientSize.Width, window.ClientSize.Height);
            var app = new HelloTriangle(gpu);
            window.Text = $"WGPU-Native Triangle ({gpu.adapter.info.backendType})";
            
            app.InitResources();

            MainLoop(app, window);

            gpu.CleanUp();
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
        
        private static void MainLoop(HelloTriangle app, Form window)
        {
            bool isClosing = false;
            window.FormClosing += (_, e) => {
                isClosing = true;
            };

            while (!isClosing) {
                app.DrawFrame();
                Application.DoEvents();
            }
        }
    }
}
