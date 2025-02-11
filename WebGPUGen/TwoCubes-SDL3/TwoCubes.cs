using Evergine.Bindings.WebGPU;
using System;
using System.Diagnostics;
using System.IO;
using System.Numerics;
using System.Runtime.InteropServices;
// ReSharper disable InconsistentNaming

namespace HelloTriangle
{
    public class TwoCubes
    {
        private readonly    WGPUDevice                  device;
        private readonly    WGPUTextureFormat           presentationFormat;
        private readonly    WGPUQueue                   queue;

        private             WGPURenderPipeline          pipeline;
        private readonly    Arena                       frameArena;
        
        private             WGPURenderPassDescriptor    renderPassDescriptor;
        private             WGPUBuffer 					        uniformBuffer;
        private             WGPUBindGroup 				      uniformBindGroup;
        private             WGPUBuffer 					        verticesBuffer;
        private             WGPUBindGroup               uniformBindGroup1;
        private             WGPUBindGroup               uniformBindGroup2;
        
        internal TwoCubes(GPU gpu) {
            device              = gpu.device;
            presentationFormat  = gpu.swapChainFormat;
            queue               = gpu.queue;
            frameArena          = gpu.frameArena;
        }
        
        internal void ReleaseResources() {
            pipeline.release();
        }

        internal void InitResources()
        {
            Utf8 basicVertWGSL            = File.ReadAllBytes(Path.Combine(AppContext.BaseDirectory, "Content", "basic.vert.wgsl"));
            Utf8 vertexPositionColorWGSL  = File.ReadAllBytes(Path.Combine(AppContext.BaseDirectory, "Content", "vertexPositionColor.frag.wgsl"));
            
            
            // Create a vertex buffer from the cube data.
            var verticesBuffer = device.createBuffer(new WGPUBufferDescriptor {
              size= (ulong)(Cube.cubeVertexArray.Length * Marshal.SizeOf<float>()),
              usage= WGPUBufferUsage.Vertex,
              mappedAtCreation = true,
            });
            var target = verticesBuffer.getMappedRange<float>(0, (ulong)Cube.cubeVertexArray.Length);
            new Span<float>(Cube.cubeVertexArray).CopyTo(target);
            verticesBuffer.unmap();

            var pipeline = device.createRenderPipeline(new WGPURenderPipelineDescriptor {
              layout = default,
              vertex = {
                module= device.createShaderModuleWGSL( new WGPUShaderModuleDescriptor(), basicVertWGSL),
                buffers= [new WGPUVertexBufferLayout
                  {
                    arrayStride= Cube.cubeVertexSize,
                    attributes= [new WGPUVertexAttribute {
                        // position
                        shaderLocation= 0,
                        offset= Cube.cubePositionOffset,
                        format= WGPUVertexFormat.Float32x4
                      },
                      new WGPUVertexAttribute {
                        // uv
                        shaderLocation= 1,
                        offset= Cube.cubeUVOffset,
                        format= WGPUVertexFormat.Float32x2
                      },
                    ],
                  },
                ],
              },
              fragment= new WGPUFragmentState {
                module  = device.createShaderModuleWGSL( new WGPUShaderModuleDescriptor(), vertexPositionColorWGSL),
                targets= [new WGPUColorTargetState {
                    format= presentationFormat,
                  },
                ],
              },
              primitive= {
                topology= WGPUPrimitiveTopology.TriangleList,

                // Backface culling since the cube is solid piece of geometry.
                // Faces pointing away from the camera will be occluded by faces
                // pointing toward the camera.
                cullMode= WGPUCullMode.Back,
              },

              // Enable depth testing so that the fragment closest to the camera
              // is rendered in front.
              depthStencil= new WGPUDepthStencilState {
                depthWriteEnabled= true,
                depthCompare= WGPUCompareFunction.Less,
                format= WGPUTextureFormat.Depth24Plus,
              },
            });

            var depthTexture = device.createTexture(new WGPUTextureDescriptor {
              size= new WGPUExtent3D {width = Program.Width, height = Program.Height },
              format= WGPUTextureFormat.Depth24Plus,
              usage= WGPUTextureUsage.RenderAttachment,
            });



            var uniformBuffer = device.createBuffer(new WGPUBufferDescriptor {
              size= (ulong)uniformBufferSize,
              usage= WGPUBufferUsage.Uniform | WGPUBufferUsage.CopyDst
            });

            uniformBindGroup1 = device.createBindGroup(new WGPUBindGroupDescriptor {
              layout= pipeline.getBindGroupLayout(0),
              entries= [new WGPUBindGroupEntry {
                  binding= 0,
                  buffer= uniformBuffer,
                  offset= 0,
                  size= matrixSize,
                },
              ],
            });

            uniformBindGroup2 = device.createBindGroup(new WGPUBindGroupDescriptor {
              layout= pipeline.getBindGroupLayout(0),
              entries= [new WGPUBindGroupEntry {
                  binding= 0,
                    buffer= uniformBuffer,
                    offset= offset,
                    size= matrixSize,
                },
              ],
            });

            var renderPassDescriptor= new WGPURenderPassDescriptor {
              colorAttachments= [new WGPURenderPassColorAttachment {
                  view= default, // Assigned later

                  clearValue=  new WGPUColor {r = 0.5, g = 0.5, b = 0.5, a = 1.0},
                  loadOp= WGPULoadOp.Clear,
                  storeOp= WGPUStoreOp.Store,
                },
              ],
              depthStencilAttachment= new WGPURenderPassDepthStencilAttachment {
                view= depthTexture.createView(),

                depthClearValue= 1.0f,
                depthLoadOp= WGPULoadOp.Clear,
                depthStoreOp= WGPUStoreOp.Store,
              },
            };
            
            const float aspect = Program.Width / Program.Height;
            Matrix4x4 projectionMatrix = Matrix4x4.CreatePerspective((2f * MathF.PI) / 5f, aspect, 1, 100.0f);
            //  Matrix4x4 modelViewProjectionMatrix = new Matrix4x4();
            long startTime = Stopwatch.GetTimestamp();
        }
        
        const ulong matrixSize = 4 * 16; // 4x4 matrix
        const ulong offset = 256; // uniformBindGroup offset must be 256-byte aligned
        const ulong uniformBufferSize = offset + matrixSize;
        
        Matrix4x4 modelViewProjectionMatrix1;
        Matrix4x4 modelViewProjectionMatrix2;
        
        long        startTime         = Stopwatch.GetTimestamp();
        const float aspect            = Program.Width / Program.Height;
        Matrix4x4   projectionMatrix  = Matrix4x4.CreatePerspectiveFieldOfView((float)(2.0 * Math.PI / 5.0), aspect, 1f, 100.0f);
        Matrix4x4   viewMatrix        = Matrix4x4.CreateTranslation(new(0, 0, -7));
        
        private void updateTransformationMatrix()
        {
          float now = (float)(((double)Stopwatch.GetTimestamp() - startTime) / Stopwatch.Frequency);
          modelViewProjectionMatrix1 = Matrix4x4.CreateFromAxisAngle(new(MathF.Sin(now), MathF.Cos(now), 0), 1) with {
            Translation = new(-2, 0, 0)
          };

          modelViewProjectionMatrix2 = Matrix4x4.CreateFromAxisAngle(new(MathF.Cos(now), MathF.Sin(now), 0), 1) with {
            Translation = new(2, 0, 0)
          };
          modelViewProjectionMatrix1 = viewMatrix * projectionMatrix;
          modelViewProjectionMatrix2 = viewMatrix * projectionMatrix;
        }

        internal void DrawFrame(WGPUTextureView view)
        {
            frameArena.Use();
            updateTransformationMatrix();
            queue.writeBuffer(
              uniformBuffer,
              0,
              modelViewProjectionMatrix1
            );
            queue.writeBuffer(
              uniformBuffer,
              offset,
              modelViewProjectionMatrix2
            );

            renderPassDescriptor.colorAttachments[0].view = view;

            var commandEncoder = device.createCommandEncoder();
            var passEncoder = commandEncoder.beginRenderPass(renderPassDescriptor);
            passEncoder.setPipeline(pipeline);
            passEncoder.setVertexBuffer(0, verticesBuffer);

            // Bind the bind group (with the transformation matrix) for
            // each cube, and draw.
            passEncoder.setBindGroup(0, uniformBindGroup1);
            passEncoder.draw(Cube.cubeVertexCount, 1, 0 ,0); // TODO add overload

            passEncoder.setBindGroup(0, uniformBindGroup2);
            passEncoder.draw(Cube.cubeVertexCount, 1, 0 ,0); // TODO add overload

            passEncoder.end();
            queue.submit([commandEncoder.finish()]);
        }
    }
}