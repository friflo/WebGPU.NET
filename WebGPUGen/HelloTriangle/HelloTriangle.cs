using Evergine.Bindings.WebGPU;
using System;
using System.Diagnostics;
using System.IO;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace HelloTriangle
{
    public class HelloTriangle
    {
        const uint WIDTH = 800;
        const uint HEIGHT = 600;

        internal Form                window;
        private WGPUInstance        Instance;
        private WGPUSurface         Surface;
        internal WGPUAdapter         Adapter;
        private WGPUDevice          Device;
        private WGPUTextureFormat   SwapChainFormat;
        private WGPUQueue           Queue;

        private WGPURenderPipeline  pipeline;
        private WGPUBuffer          vertexBuffer;
        internal Arena               frameArena = new Arena();

        internal void InitWindow()
        {
            window = new Form();
            window.Size = new System.Drawing.Size((int)WIDTH, (int)HEIGHT);
            window.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            window.Show();
        }
        
        private static void UncapturedErrorCallback(WGPUErrorType errorType, Utf8 message) {
            Console.WriteLine($"Uncaptured device error: type: {errorType} ({message.ToString()})");
        }
        
        internal void InitSurface() {
            Instance = WebGPUNative.wgpuCreateInstance(new WGPUInstanceExtras {
                backends = WGPUInstanceBackend.Vulkan
            });
            Surface = Instance.createSurfaceFromWindowsHWND(new WGPUSurfaceDescriptor(), Process.GetCurrentProcess().Handle, window.Handle);
        }

        internal void InitDevice(int width, int height)
        {
            // --- create Adapter
            WGPURequestAdapterOptions options = new WGPURequestAdapterOptions {
                compatibleSurface   = Surface,
                powerPreference     = WGPUPowerPreference.HighPerformance
            };
            Instance.requestAdapter(options, (in RequestAdapterResult result) => {
                if (result.status != WGPURequestAdapterStatus.Success) {
                    throw new Exception($"Failed to create adapter: {result.Message.ToString()}");
                }
                Adapter = result.adapter;    
            });
            // --- create Device
            var deviceDescriptor = new WGPUDeviceDescriptor { label = "Device"u8 };
            Adapter.requestDevice(deviceDescriptor, UncapturedErrorCallback, (in RequestDeviceResult result) => {
                if (result.status != WGPURequestDeviceStatus.Success) {
                    throw new Exception($"Failed to request device. {result.Message.ToString()}");
                }
                Device = result.device;
            });
            Queue = Device.queue;
            var capabilities = Surface.getCapabilities(Adapter);
            SwapChainFormat = capabilities.formats[0];

            var surfaceConfiguration = new WGPUSurfaceConfiguration {
                device      = Device,
                format      = SwapChainFormat,
                usage       = WGPUTextureUsage.RenderAttachment,
                width       = (uint)width,
                height      = (uint)height,
                presentMode = WGPUPresentMode.Fifo,
            };
            Surface.configure(surfaceConfiguration);
        }

        internal void InitResources()
        {
            var pipelineLayout = Device.createPipelineLayout(new WGPUPipelineLayoutDescriptor());
            // pipelineLayout = wgpuDeviceCreatePipelineLayout(Device, &layoutDescription);


            Utf8 shaderSource = File.ReadAllBytes(Path.Combine(AppContext.BaseDirectory, "Content", $"triangle.wgsl"));

            /*
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
            var shaderModule = Device.createShaderModule(moduleDescriptor);
            */
            var shaderModule = Device.createShaderModuleWGSL(new WGPUShaderModuleDescriptor(), shaderSource);
            /*
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
            */

            var pipelineDescriptor = new WGPURenderPipelineDescriptor {
                layout = pipelineLayout,
                vertex = new WGPUVertexState {
                    buffers = [new WGPUVertexBufferLayout {
                        attributes = [
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
                        arrayStride = (ulong)Marshal.SizeOf<Vector4>() * 2,
                        stepMode = WGPUVertexStepMode.Vertex,
                    }],
                    module = shaderModule,
                    entryPoint = "vertexMain"u8,
                },
                primitive = new WGPUPrimitiveState {
                    topology = WGPUPrimitiveTopology.TriangleList,
                    stripIndexFormat = WGPUIndexFormat.Undefined,
                    frontFace = WGPUFrontFace.CCW,
                    cullMode = WGPUCullMode.None,
                },
                fragment = new WGPUFragmentState {
                    module = shaderModule,
                    entryPoint = "fragmentMain"u8,
                    targets = [new WGPUColorTargetState {
                        format = SwapChainFormat,
                        blend = new WGPUBlendState {
                            color = new WGPUBlendComponent {
                                srcFactor = WGPUBlendFactor.One,
                                dstFactor = WGPUBlendFactor.Zero,
                                operation = WGPUBlendOperation.Add,
                            },
                            alpha = new WGPUBlendComponent {
                                srcFactor = WGPUBlendFactor.One,
                                dstFactor = WGPUBlendFactor.Zero,
                                operation = WGPUBlendOperation.Add,
                            }
                        },
                        writeMask = WGPUColorWriteMask.All,
                    }]
                },
                multisample = new WGPUMultisampleState {
                    count = 1,
                    mask = ~0u,
                    alphaToCoverageEnabled = false,
                }
            };
            pipeline = Device.createRenderPipeline(pipelineDescriptor);
            shaderModule.release();
            pipelineLayout.release();
            

            Span<Vector4> vertexData = [
                new Vector4(0.0f, 0.5f, 0.5f, 1.0f),
                new Vector4(1.0f, 0.0f, 0.0f, 1.0f),
                new Vector4(0.5f, -0.5f, 0.5f, 1.0f),
                new Vector4(0.0f, 1.0f, 0.0f, 1.0f),
                new Vector4(-0.5f, -0.5f, 0.5f, 1.0f),
                new Vector4(0.0f, 0.0f, 1.0f, 1.0f)
            ];

            ulong size = (ulong)(6 * Marshal.SizeOf<Vector4>());
            WGPUBufferDescriptor bufferDescriptor = new WGPUBufferDescriptor {
                usage = WGPUBufferUsage.Vertex | WGPUBufferUsage.CopyDst,
                size = size,
                mappedAtCreation = false,
            };
            vertexBuffer = Device.createBuffer(bufferDescriptor);
            // vertexBuffer = wgpuDeviceCreateBuffer(Device, &bufferDescriptor);
            Queue.writeBuffer(vertexBuffer, 0, vertexData);
        }

        internal void DrawFrame()
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
                label = "123"u8
            });

            WGPURenderPassEncoder renderPass = encoder.beginRenderPass(new WGPURenderPassDescriptor {
                label = "123"u8,
                colorAttachments = [new WGPURenderPassColorAttachment {
                    view = nextView,
                    resolveTarget = WGPUTextureView.Null,
                    loadOp = WGPULoadOp.Clear,
                    storeOp = WGPUStoreOp.Store,
                    clearValue = new WGPUColor() { a = 1.0f },
                }],
            });
            var name = renderPass.ToString();

            renderPass.setPipeline(pipeline);
            renderPass.setVertexBuffer(0, vertexBuffer, 0, WebGPUNative.WGPU_WHOLE_MAP_SIZE);
            renderPass.draw(3, 1, 0, 0);
            renderPass.end();
            renderPass.release();
            nextView.release();

            var command = encoder.finish();
            Queue.submit([command]);

            encoder.release();
            Surface.present();
        }

        internal void CleanUp()
        {
            // Queue is not released
            Surface.release();
            Device.destroy();
            Device.release();
            Adapter.release();
            Instance.release();

            this.window.Dispose();
            this.window.Close();
        }

    }
}
