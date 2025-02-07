using System.Windows.Forms;
using Evergine.Bindings.WebGPU;

namespace HelloTriangle
{
    class Program
    {
        static void Main(string[] args)
        {
            HelloTriangle app = new HelloTriangle();
            app.frameArena.Use();
            app.InitWindow();

            app.InitSurface();
            app.InitDevice(app.window.ClientSize.Width, app.window.ClientSize.Height);
            app.window.Text = $"WGPU-Native Triangle ({app.Adapter.info.backendType})";
            
            app.InitResources();

            MainLoop(app);

            app.CleanUp();
        }
        
        private static void MainLoop(HelloTriangle app)
        {
            bool isClosing = false;
            app.window.FormClosing += (s, e) =>
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
