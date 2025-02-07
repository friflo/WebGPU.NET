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
            HelloTriangle app = new HelloTriangle();
            app.frameArena.Use();
            var window = InitWindow();

            app.InitSurface(window.Handle);
            app.InitDevice(window.ClientSize.Width, window.ClientSize.Height);
            window.Text = $"WGPU-Native Triangle ({app.Adapter.info.backendType})";
            
            app.InitResources();

            MainLoop(app, window);

            app.CleanUp();
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
                for (int n = 0; n < 100_000; n++) {
                    var desc = new WGPURenderPipelineDescriptor() {
                        fragment = new WGPUFragmentState { }
                    };
                    if (n % 10_000 == 0) {
                        app.frameArena.Reset();
                    }
                }

                Application.DoEvents();
            }
        }
        
    }
}
