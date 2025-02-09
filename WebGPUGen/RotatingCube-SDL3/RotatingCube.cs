using Evergine.Bindings.WebGPU;
using System;
using System.IO;
using System.Numerics;
using System.Runtime.InteropServices;

namespace HelloTriangle
{
    public class RotatingCube
    {
        private readonly    WGPUDevice          device;
        private readonly    WGPUTextureFormat   presentationFormat;
        private readonly    WGPUQueue           queue;

        private             WGPURenderPipeline  pipeline;
        private             WGPUBuffer          vertexBuffer;
        private readonly    Arena               frameArena;

        internal RotatingCube(GPU gpu) {
            device          = gpu.device;
            presentationFormat = gpu.swapChainFormat;
            queue           = gpu.queue;
            frameArena      = gpu.frameArena;
        }
        
        internal void ReleaseResources() {
            vertexBuffer.release();
            pipeline.release();
        }

        internal void InitResources()
        {
            Utf8 basicVertWGSL            = File.ReadAllBytes(Path.Combine(AppContext.BaseDirectory, "Content", "basic.vert.wgsl"));
            Utf8 vertexPositionColorWGSL  = File.ReadAllBytes(Path.Combine(AppContext.BaseDirectory, "Content", "vertexPositionColor.frag.wgsl"));
            
            // Create a vertex buffer from the cube data.
            var verticesBuffer = device.createBuffer(new WGPUBufferDescriptor {
              size = (ulong)(Cube.cubeVertexArray.Length * Marshal.SizeOf<float>()),
              usage = WGPUBufferUsage.Vertex,
              mappedAtCreation = true
            });
            var target = verticesBuffer.getMappedRange<float>(0, (ulong)Cube.cubeVertexArray.Length);
            new Span<float>(Cube.cubeVertexArray).CopyTo(target);
            verticesBuffer.unmap();

            var pipeline = device.createRenderPipeline(new WGPURenderPipelineDescriptor {
                layout = default, 
                vertex = new WGPUVertexState {
                    module= device.createShaderModuleWGSL( new WGPUShaderModuleDescriptor(), basicVertWGSL),
                    buffers = [
                      new WGPUVertexBufferLayout
                        {
                            arrayStride = Cube.cubeVertexSize,
                            attributes= [
                                 new WGPUVertexAttribute {
                                   // position
                                   shaderLocation= 0,
                                   offset= Cube.cubePositionOffset,
                                   format= WGPUVertexFormat.Float32x4
                                },
                                new WGPUVertexAttribute {
                                  // uv
                                  shaderLocation= 1,
                                  offset= Cube.cubeUVOffset,
                                  format= WGPUVertexFormat.Float32x2,
                                },
                            ],
                        },
                    ],
                },
                fragment= new WGPUFragmentState {
                  module= device.createShaderModuleWGSL( new WGPUShaderModuleDescriptor(), vertexPositionColorWGSL),
                  targets= [new WGPUColorTargetState
                  {
                    format= presentationFormat,
                  },
                ],
              },
              primitive = {
                topology= WGPUPrimitiveTopology.TriangleList,
                // Backface culling since the cube is solid piece of geometry.
                // Faces pointing away from the camera will be occluded by faces
                // pointing toward the camera.
                cullMode= WGPUCullMode.Back,
              },

              // Enable depth testing so that the fragment closest to the camera
              // is rendered in front.
              depthStencil= new WGPUDepthStencilState{
                depthWriteEnabled= true,
                depthCompare= WGPUCompareFunction.Less,
                format= WGPUTextureFormat.Depth24Plus,
              }
            });
            /*
            const depthTexture = device.createTexture({
              size: [canvas.width, canvas.height],
              format: 'depth24plus',
              usage: GPUTextureUsage.RENDER_ATTACHMENT,
            });

            const uniformBufferSize = 4 * 16; // 4x4 matrix
            const uniformBuffer = device.createBuffer({
              size: uniformBufferSize,
              usage: GPUBufferUsage.UNIFORM | GPUBufferUsage.COPY_DST,
            });

            const uniformBindGroup = device.createBindGroup({
              layout: pipeline.getBindGroupLayout(0),
              entries: [
                {
                  binding: 0,
                  resource: {
                    buffer: uniformBuffer,
                  },
                },
              ],
            });

            const renderPassDescriptor: GPURenderPassDescriptor = {
              colorAttachments: [
                {
                  view: undefined, // Assigned later

                  clearValue: [0.5, 0.5, 0.5, 1.0],
                  loadOp: 'clear',
                  storeOp: 'store',
                },
              ],
              depthStencilAttachment: {
                view: depthTexture.createView(),

                depthClearValue: 1.0,
                depthLoadOp: 'clear',
                depthStoreOp: 'store',
              },
            };*/
        }

        internal void DrawFrame(WGPUTextureView view)
        {
            frameArena.Use();

            var encoder = device.createCommandEncoder(new WGPUCommandEncoderDescriptor { label = "triangle"u8 });

            WGPURenderPassEncoder renderPass = encoder.beginRenderPass(new WGPURenderPassDescriptor {
                label = "triangle"u8,
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
