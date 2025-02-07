using Evergine.Bindings.WebGPU;
using System;
using System.IO;
using System.Numerics;
using System.Runtime.InteropServices;

namespace HelloTriangle
{
    public class HelloTriangle
    {
        private readonly    WGPUSurface         surface;
        private readonly    WGPUDevice          device;
        private readonly    WGPUTextureFormat   swapChainFormat;
        private readonly    WGPUQueue           queue;

        private             WGPURenderPipeline  pipeline;
        private             WGPUBuffer          vertexBuffer;
        private readonly    Arena               frameArena = new Arena("frameArena");

        internal HelloTriangle(GPU gpu) {
            surface         = gpu.Surface;
            device          = gpu.Device;
            swapChainFormat = gpu.SwapChainFormat;
            queue           = gpu.Queue;
        }

        internal void InitResources()
        {
            frameArena.Use();
            var pipelineLayout = device.createPipelineLayout(new WGPUPipelineLayoutDescriptor());

            Utf8 shaderSource = File.ReadAllBytes(Path.Combine(AppContext.BaseDirectory, "Content", $"triangle.wgsl"));

            var shaderModule = device.createShaderModuleWGSL(new WGPUShaderModuleDescriptor(), shaderSource);

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
                        format = swapChainFormat,
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
            pipeline = device.createRenderPipeline(pipelineDescriptor);
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
            vertexBuffer = device.createBuffer(bufferDescriptor);
            queue.writeBuffer(vertexBuffer, 0, vertexData);
        }

        internal void DrawFrame()
        {
            for (int n = 0; n < 100_000; n++) {
                var desc = new WGPURenderPipelineDescriptor() {
                    fragment = new WGPUFragmentState { }
                };
                if (n % 10_000 == 0) {
                    frameArena.Reset();
                }
            }
            
            var surfaceTexture = surface.currentTexture;

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

            var encoder = device.createCommandEncoder(new WGPUCommandEncoderDescriptor {
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
            queue.submit([command]);

            encoder.release();
            surface.present();
        }
    }
}
