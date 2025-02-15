using Evergine.Bindings.WebGPU;
using System;
using System.IO;
using System.Numerics;
using System.Runtime.InteropServices;

namespace HelloTriangle
{
    public class Triangle
    {
        private static readonly Label           Label = new("triangle");
        private readonly    WGPUDevice          device;
        private readonly    WGPUTextureFormat   swapChainFormat;
        private readonly    WGPUQueue           queue;

        private             WGPURenderPipeline  pipeline;
        private             WGPUBuffer          vertexBuffer;
        private readonly    Arena               frameArena;

        internal Triangle(GPU gpu) {
            device          = gpu.device;
            swapChainFormat = gpu.swapChainFormat;
            queue           = gpu.queue;
            frameArena      = gpu.frameArena;
        }
        
        internal void ReleaseResources() {
            vertexBuffer.release();
            pipeline.release();
        }

        internal void InitResources()
        {
            frameArena.Use();
            var pipelineLayout = device.createPipelineLayout(new WGPUPipelineLayoutDescriptor { label = Label });

            Utf8 shaderSource = File.ReadAllBytes(Path.Combine(AppContext.BaseDirectory, "Content", $"triangle.wgsl"));

            var shaderModule = device.createShaderModuleWGSL(new WGPUShaderModuleDescriptor(), shaderSource);

            var pipelineDescriptor = new WGPURenderPipelineDescriptor {
                label = Label, 
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
                new (0.0f, 0.5f, 0.5f, 1.0f),
                new (1.0f, 0.0f, 0.0f, 1.0f),
                new (0.5f, -0.5f, 0.5f, 1.0f),
                new (0.0f, 1.0f, 0.0f, 1.0f),
                new (-0.5f, -0.5f, 0.5f, 1.0f),
                new (0.0f, 0.0f, 1.0f, 1.0f)
            ];

            ulong size = (ulong)(6 * Marshal.SizeOf<Vector4>());
            WGPUBufferDescriptor bufferDescriptor = new WGPUBufferDescriptor {
                label = Label,
                usage = WGPUBufferUsage.Vertex | WGPUBufferUsage.CopyDst,
                size = size,
                mappedAtCreation = false,
            };
            vertexBuffer = device.createBuffer(bufferDescriptor);
            queue.writeBuffer(vertexBuffer, 0, vertexData);
        }

        internal void DrawFrame(WGPUTextureView view)
        {
            frameArena.Use();

            var encoder = device.createCommandEncoder(new WGPUCommandEncoderDescriptor { label = "triangle"u8 });

            WGPURenderPassEncoder renderPass = encoder.beginRenderPass(new WGPURenderPassDescriptor {
                label = Label,
                colorAttachments = [new WGPURenderPassColorAttachment {
                    view            = view,
                    resolveTarget   = default,
                    loadOp          = WGPULoadOp.Clear,
                    storeOp         = WGPUStoreOp.Store,
                    clearValue      = new WGPUColor() { a = 1.0f },
                }],
            });
            renderPass.setPipeline(pipeline);
            renderPass.setVertexBuffer(0, vertexBuffer, 0, WebGPUNative.WGPU_WHOLE_MAP_SIZE);
            renderPass.draw(3, 1, 0, 0);
            renderPass.end();
            renderPass.release();

            var command = encoder.finish();
            encoder.release();
            queue.submit([command]);
            
            command.release();
        }
    }
}
