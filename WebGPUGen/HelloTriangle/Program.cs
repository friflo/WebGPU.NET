using System.Windows.Forms;

namespace HelloTriangle
{
    static class Program
    {
        private const uint Width  = 800;
        private const uint Height = 600;

        private static void Main()
        {
            var window = InitWindow();
            var gpu = new GPU();
            gpu.CreateSurface(window.Handle);
            gpu.RequestDevice(window.ClientSize.Width, window.ClientSize.Height);
            var triangle = new HelloTriangle(gpu);
            window.Text = $"WGPU-Native Triangle ({gpu.adapter.info.backendType})";
            
            triangle.InitResources();

            MainLoop(triangle, window);

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
        
        private static void MainLoop(HelloTriangle triangle, Form window)
        {
            bool isClosing = false;
            window.FormClosing += (_, e) => {
                isClosing = true;
            };

            while (!isClosing) {
                triangle.DrawFrame();
                Application.DoEvents();
            }
        }
    }
}
