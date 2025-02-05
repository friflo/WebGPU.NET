using Evergine.Bindings.WebGPU;
using Microsoft.VisualBasic.Logging;
using System;
using System.Diagnostics;
using System.IO;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static Evergine.Bindings.WebGPU.WebGPUNative;

namespace HelloTriangle
{
    public class HelloTriangle
    {
        const uint WIDTH = 800;
        const uint HEIGHT = 600;

        private Form window;
        private WGPUInstance Instance;
        private WGPUSurface Surface;
        private WGPUAdapter Adapter;
        private WGPUAdapterInfo AdapterInfo;
        private WGPUSupportedLimits AdapterLimits;
        private WGPUDevice Device;
        private WGPUTextureFormat SwapChainFormat;
        private WGPUQueue Queue;

        private WGPUPipelineLayout pipelineLayout;
        private WGPURenderPipeline pipeline;
        private WGPUBuffer vertexBuffer;
        private Arena frameArena = new Arena();

        public void Run()
        {
            frameArena.Use();
            this.InitWindow();

            this.InitWebGPU();

            this.InitResources();

            this.MainLoop();

            this.CleanUp();
        }

        private void InitWindow()
        {
            window = new Form();
            window.Size = new System.Drawing.Size((int)WIDTH, (int)HEIGHT);
            window.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            window.Show();
        }

        private unsafe void InitWebGPU()
        {
            WGPUInstanceExtras instanceExtras = new WGPUInstanceExtras()
            {
                chain = new WGPUChainedStruct()
                {
                    sType = (WGPUSType)WGPUNativeSType.InstanceExtras,
                },
                backends = WGPUInstanceBackend.Vulkan,
            };

            WGPUInstanceDescriptor instanceDescriptor = new WGPUInstanceDescriptor()
            {
                nextInChain = &instanceExtras.chain,
            };
            Instance = wgpuCreateInstance(&instanceDescriptor);

            WGPUSurfaceDescriptorFromWindowsHWND windowsSurface = new WGPUSurfaceDescriptorFromWindowsHWND()
            {
                chain = new WGPUChainedStruct()
                {
                    sType = WGPUSType.SurfaceDescriptorFromWindowsHWND,
                },
                hinstance = (void*)Process.GetCurrentProcess().Handle,
                hwnd = (void*)window.Handle,
            };

            WGPUSurfaceDescriptor surfaceDescriptor = new WGPUSurfaceDescriptor()
            {
                nextInChain = &windowsSurface.chain,
            };

            Surface = Instance.createSurface(surfaceDescriptor);

            WGPURequestAdapterOptions options = new WGPURequestAdapterOptions()
            {
                nextInChain = null,
                compatibleSurface = Surface,
                powerPreference = WGPUPowerPreference.HighPerformance
            };

            WGPUAdapter adapter = WGPUAdapter.Null;
            wgpuInstanceRequestAdapter(Instance, &options, &OnAdapterRequestEnded, &adapter);
            WGPUAdapterInfo properties;
            wgpuAdapterGetInfo(adapter, &properties);
            window.Text = $"WGPU-Native Triangle ({properties.backendType})";

            WGPUSupportedLimits limits;
            wgpuAdapterGetLimits(adapter, &limits);
            this.Adapter = adapter;

            AdapterInfo = properties;
            AdapterLimits = limits;


            WGPUDeviceDescriptor deviceDescriptor = new WGPUDeviceDescriptor()
            {
                nextInChain = null,
                label = null,
                requiredFeatures = null,
                requiredFeatureCount = 0,
                requiredLimits = null,
                uncapturedErrorCallbackInfo = new WGPUUncapturedErrorCallbackInfo()
                {
                    callback = &HandleUncapturedErrorCallback
                }
            };

            WGPUDevice device = WGPUDevice.Null;
            wgpuAdapterRequestDevice(Adapter, &deviceDescriptor, &OnDeviceRequestEnded, &device);
            Device = device;

            // Queue = wgpuDeviceGetQueue(Device);
            Queue = Device.queue;

            var capabilities = Surface.getCapabilities(Adapter);
            SwapChainFormat = capabilities.formats[0];

            int width = window.ClientSize.Width;
            int height = window.ClientSize.Height;

            WGPUTextureFormat textureFormat = SwapChainFormat;
            WGPUSurfaceConfiguration surfaceConfiguration = new WGPUSurfaceConfiguration()
            {
                nextInChain = null,
                device = Device,
                format = SwapChainFormat,
                usage = WGPUTextureUsage.RenderAttachment,
                width = (uint)width,
                height = (uint)height,
                presentMode = WGPUPresentMode.Fifo,
            };
            Surface.configure(surfaceConfiguration);
        }

        [UnmanagedCallersOnly]
        private static unsafe void HandleUncapturedErrorCallback(WGPUErrorType type, char* pMessage, void* pUserData)
        {
            Console.WriteLine($"Uncaptured device error: type: {type} ({Helpers.GetString(pMessage)})");
        }

        [UnmanagedCallersOnly]
        private static unsafe void OnAdapterRequestEnded(WGPURequestAdapterStatus status, WGPUAdapter candidateAdapter, char* message, void* pUserData)
        {
            if (status == WGPURequestAdapterStatus.Success)
            {
                *(WGPUAdapter*)pUserData = candidateAdapter;
            }
            else
            {
                Console.WriteLine($"Could not get WebGPU adapter: {Helpers.GetString(message)}");
            }
        }

        [UnmanagedCallersOnly]
        private static unsafe void OnDeviceRequestEnded(WGPURequestDeviceStatus status, WGPUDevice device, char* message, void* pUserData)
        {
            if (status == WGPURequestDeviceStatus.Success)
            {
                *(WGPUDevice*)pUserData = device;
            }
            else
            {
                Console.WriteLine($"Could not get WebGPU device: {Helpers.GetString(message)}");
            }
        }

        private unsafe void InitResources()
        {
            WGPUPipelineLayoutDescriptor layoutDescription = new()
            {
                nextInChain = null,
                bindGroupLayoutCount = 0,
                bindGroupLayouts = null,
            };

            pipelineLayout = wgpuDeviceCreatePipelineLayout(Device, &layoutDescription);


            Utf8 shaderSource = File.ReadAllBytes(Path.Combine(AppContext.BaseDirectory, "Content", $"triangle.wgsl"));

            WGPUShaderModuleWGSLDescriptor shaderCodeDescriptor = new()
            {
                chain = new WGPUChainedStruct()
                {
                    next = null,
                    sType = WGPUSType.ShaderModuleWGSLDescriptor,
                },
                Code = shaderSource,
            };

            WGPUShaderModuleDescriptor moduleDescriptor = new()
            {
                nextInChain = &shaderCodeDescriptor.chain,
                hintCount = 0,
                hints = null,
            };
            WGPUShaderModule shaderModule = Device.createShaderModule(moduleDescriptor);

            WGPUVertexAttribute* vertexAttributes = stackalloc WGPUVertexAttribute[2]
            {
                new WGPUVertexAttribute()
                {
                    format = WGPUVertexFormat.Float32x4,
                    offset = 0,
                    shaderLocation = 0,
                },
                new WGPUVertexAttribute()
                {
                    format = WGPUVertexFormat.Float32x4,
                    offset = 16,
                    shaderLocation = 1,
                },
            };

            WGPUVertexBufferLayout vertexLayout = new WGPUVertexBufferLayout()
            {
                attributeCount = 2,
                attributes = vertexAttributes,
                arrayStride = (ulong)sizeof(Vector4) * 2,
                stepMode = WGPUVertexStepMode.Vertex,
            };

            WGPUBlendState blendState = new WGPUBlendState()
            {
                color = new WGPUBlendComponent()
                {
                    srcFactor = WGPUBlendFactor.One,
                    dstFactor = WGPUBlendFactor.Zero,
                    operation = WGPUBlendOperation.Add,
                },
                alpha = new WGPUBlendComponent()
                {
                    srcFactor = WGPUBlendFactor.One,
                    dstFactor = WGPUBlendFactor.Zero,
                    operation = WGPUBlendOperation.Add,
                }
            };

            WGPUColorTargetState colorTargetState = new WGPUColorTargetState()
            {
                nextInChain = null,
                format = SwapChainFormat,
                blend = &blendState,
                writeMask = WGPUColorWriteMask.All,
            };

            WGPUFragmentState fragmentState = new WGPUFragmentState()
            {
                nextInChain = null,
                module = shaderModule,
                EntryPoint = "fragmentMain"u8,
                constantCount = 0,
                constants = null,
                targetCount = 1,
                targets = &colorTargetState,
            };


            var pipelineDescriptor = new WGPURenderPipelineDescriptor {
                layout = pipelineLayout,
                vertex = new WGPUVertexState {
                    Buffers = [new WGPUVertexBufferLayout {
                        Attributes = [
                            new WGPUVertexAttribute {
                                format = WGPUVertexFormat.Float32x4,
                                offset = 0,
                                shaderLocation = 0,
                            },
                            new WGPUVertexAttribute {
                                format = WGPUVertexFormat.Float32x4,
                                offset = 16,
                                shaderLocation = 1,
                            },
                        ],
                        arrayStride = (ulong)sizeof(Vector4) * 2,
                        stepMode = WGPUVertexStepMode.Vertex,
                    }],
                    module = shaderModule,
                    EntryPoint = "vertexMain"u8,
                    constantCount = 0,
                    constants = null,
                },
                primitive = new WGPUPrimitiveState {
                    topology = WGPUPrimitiveTopology.TriangleList,
                    stripIndexFormat = WGPUIndexFormat.Undefined,
                    frontFace = WGPUFrontFace.CCW,
                    cullMode = WGPUCullMode.None,
                },
                Fragment = new WGPUFragmentState {
                    nextInChain = null,
                    module = shaderModule,
                    EntryPoint = "fragmentMain"u8,
                    Targets = [new WGPUColorTargetState {
                        nextInChain = null,
                        format = SwapChainFormat,
                        blend = &blendState,
                        writeMask = WGPUColorWriteMask.All,
                    }]
                },
                depthStencil = null,
                multisample = new WGPUMultisampleState {
                    count = 1,
                    mask = ~0u,
                    alphaToCoverageEnabled = false,
                }
            };
            pipeline = Device.createRenderPipeline(pipelineDescriptor);
            shaderModule.release();
            

            Span<Vector4> vertexData = [
                new Vector4(0.0f, 0.5f, 0.5f, 1.0f),
                new Vector4(1.0f, 0.0f, 0.0f, 1.0f),
                new Vector4(0.5f, -0.5f, 0.5f, 1.0f),
                new Vector4(0.0f, 1.0f, 0.0f, 1.0f),
                new Vector4(-0.5f, -0.5f, 0.5f, 1.0f),
                new Vector4(0.0f, 0.0f, 1.0f, 1.0f)
            ];

            ulong size = (ulong)(6 * sizeof(Vector4));
            WGPUBufferDescriptor bufferDescriptor = new WGPUBufferDescriptor()
            {
                nextInChain = null,
                usage = WGPUBufferUsage.Vertex | WGPUBufferUsage.CopyDst,
                size = size,
                mappedAtCreation = false,
            };
            vertexBuffer = wgpuDeviceCreateBuffer(Device, &bufferDescriptor);
            Queue.writeBuffer(vertexBuffer, 0, vertexData);
        }


        private void MainLoop()
        {
            bool isClosing = false;
            window.FormClosing += (s, e) =>
            {
                isClosing = true;
            };

            while (!isClosing)
            {
                this.DrawFrame();
                for (int n = 0; n < 100_000; n++) {
                    var desc = new WGPURenderPipelineDescriptor() {
                        Fragment = new WGPUFragmentState() {
                            constantCount = (ulong)n
                        }
                    };
                    if (n % 10_000 == 0) {
                        frameArena.Reset();
                    }
                }

                Application.DoEvents();
            }
        }

        private void DrawFrame()
        {
            var surfaceTexture = Surface.currentTexture;

            // Getting the texture may fail, in particular if the window has been resized
            // and thus the target surface changed.
            if (surfaceTexture.status == WGPUSurfaceGetCurrentTextureStatus.Timeout)
            {
                Console.WriteLine("Cannot acquire next swap chain texture");
                return;
            }

            if (surfaceTexture.status == WGPUSurfaceGetCurrentTextureStatus.Outdated)
            {
                Console.WriteLine("Surface texture is outdated, reconfigure the surface!");
                return;
            }
            WGPUTextureView nextView = surfaceTexture.texture.createView();

            var encoder = Device.createCommandEncoder(new WGPUCommandEncoderDescriptor {
                Label = "123"u8
            });

            WGPURenderPassEncoder renderPass = encoder.beginRenderPass(new WGPURenderPassDescriptor {
                Label = "123"u8,
                nextInChain = null,
                ColorAttachments = [new WGPURenderPassColorAttachment {
                    view = nextView,
                    resolveTarget = WGPUTextureView.Null,
                    loadOp = WGPULoadOp.Clear,
                    storeOp = WGPUStoreOp.Store,
                    clearValue = new WGPUColor() { a = 1.0f },
                }],
                depthStencilAttachment = null,
                timestampWrites = null,
            });
            var name = renderPass.ToString();

            renderPass.setPipeline(pipeline);
            renderPass.setVertexBuffer(0, vertexBuffer, 0, WGPU_WHOLE_MAP_SIZE);
            renderPass.draw(3, 1, 0, 0);
            renderPass.end();
            renderPass.release();
            nextView.release();

            var command = encoder.finish();
            Queue.submit([command]);

            // wgpuCommandEncoderRelease(encoder);
            encoder.release();
            Surface.present();
        }

        private void CleanUp()
        {
            // Queue is not released
            Surface.release();
            wgpuDeviceDestroy(Device);
            wgpuDeviceRelease(Device);
            wgpuAdapterRelease(Adapter);
            wgpuInstanceRelease(Instance);

            this.window.Dispose();
            this.window.Close();
        }

    }
}
