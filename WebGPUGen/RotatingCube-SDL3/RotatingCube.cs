﻿using Evergine.Bindings.WebGPU;
using System;
using System.Diagnostics;
using System.IO;
using System.Numerics;
using System.Runtime.InteropServices;

// ReSharper disable InconsistentNaming
namespace HelloTriangle
{
    public class RotatingCube
    {
        private static readonly Label                   Label = new("rotating-cube");
        // --- GPU resources
        private readonly    WGPUDevice                  device;
        private readonly    WGPUTextureFormat           presentationFormat;
        private readonly    WGPUQueue                   queue;
        private readonly    Arena                       frameArena;
        
        // --- handles
        private             WGPURenderPipeline          pipeline;
        private             WGPUBuffer 					uniformBuffer;
        private             WGPUBindGroup 				uniformBindGroup;
        private             WGPUBuffer 					verticesBuffer;
        private             WGPUTextureView             depthTextureView;
        // --- structs              
        private             WGPURenderPassDescriptor    renderPassDescriptor;
        
        internal RotatingCube(GPU gpu) {
            device              = gpu.device;
            presentationFormat  = gpu.swapChainFormat;
            queue               = gpu.queue;
            frameArena          = gpu.frameArena;
        }
        
        internal void ReleaseResources() {
            pipeline.release();
            uniformBuffer.release();
            uniformBindGroup.release();
            verticesBuffer.release();
            depthTextureView.release();
        }

        internal void InitResources()
        {
            Utf8 basicVertWGSL            = File.ReadAllBytes(Path.Combine(AppContext.BaseDirectory, "Content", "basic.vert.wgsl"));
            Utf8 vertexPositionColorWGSL  = File.ReadAllBytes(Path.Combine(AppContext.BaseDirectory, "Content", "vertexPositionColor.frag.wgsl"));
            
            // Create a vertex buffer from the cube data.
            verticesBuffer = device.createBuffer(new WGPUBufferDescriptor {
                label               = Label,
                size                = (ulong)(Cube.cubeVertexArray.Length * Marshal.SizeOf<float>()),
                usage               = WGPUBufferUsage.Vertex,
                mappedAtCreation    = true
            });
            var target = verticesBuffer.getMappedRange<float>(0, (ulong)Cube.cubeVertexArray.Length);
            new Span<float>(Cube.cubeVertexArray).CopyTo(target);
            verticesBuffer.unmap();
            
            using var shaderModule          = device.createShaderModuleWGSL( new WGPUShaderModuleDescriptor { label = Label }, basicVertWGSL);
            using var positionColorShader   = device.createShaderModuleWGSL( new WGPUShaderModuleDescriptor{ label = Label }, vertexPositionColorWGSL);
            
            pipeline = device.createRenderPipeline(new WGPURenderPipelineDescriptor {
                label   = Label,
                layout  = default, 
                vertex  = new WGPUVertexState {
                    module= shaderModule,
                    buffers = [
                        new WGPUVertexBufferLayout {
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
                    module  = positionColorShader,
                    targets = [new WGPUColorTargetState {
                            format= presentationFormat
                        },
                    ],
                },
                primitive = {
                    topology    = WGPUPrimitiveTopology.TriangleList,
                    // Backface culling since the cube is solid piece of geometry.
                    // Faces pointing away from the camera will be occluded by faces
                    // pointing toward the camera.
                    cullMode    = WGPUCullMode.Back,
                },

                // Enable depth testing so that the fragment closest to the camera
                // is rendered in front.
                depthStencil= new WGPUDepthStencilState{
                    depthWriteEnabled   = true,
                    depthCompare        = WGPUCompareFunction.Less,
                    format              = WGPUTextureFormat.Depth24Plus
                }
            });
            
            using var depthTexture = device.createTexture(new WGPUTextureDescriptor{
                label   = Label,
                size    = new WGPUExtent3D { width  = Program.Width, height = Program.Height },
                format  = WGPUTextureFormat.Depth24Plus,
                usage   = WGPUTextureUsage.RenderAttachment
            });
            
            var uniformBufferSize = 4 * 16; // 4x4 matrix
            uniformBuffer = device.createBuffer( new WGPUBufferDescriptor{
                label   = Label,
                size    = (ulong)uniformBufferSize,
                usage   = WGPUBufferUsage.Uniform | WGPUBufferUsage.CopyDst
            });
            
            using var bindGroupLayout = pipeline.getBindGroupLayout(0);
            uniformBindGroup = device.createBindGroup( new WGPUBindGroupDescriptor {
                label   = Label,
                layout  = bindGroupLayout,
                entries = [new WGPUBindGroupEntry
                    {
                        binding = 0,
                        buffer  = uniformBuffer,
                        size    = (ulong)uniformBufferSize,
                    },
                ],
            });
            var sessionArena = new Arena("sessionArena");
            sessionArena.Use();
            
            depthTextureView = depthTexture.createView();
            renderPassDescriptor = new WGPURenderPassDescriptor  {
                label = Label,
                colorAttachments = [new WGPURenderPassColorAttachment {
                        view        = default, // Assigned later
                        clearValue  = new WGPUColor { r= 0.5, g= 0.5, b = 0.5, a = 1.0 },
                        loadOp      = WGPULoadOp.Clear,
                        storeOp     = WGPUStoreOp.Store
                    },
                ],
                depthStencilAttachment = new WGPURenderPassDepthStencilAttachment {
                    view            = depthTextureView,
                    depthClearValue = 1.0f,
                    depthLoadOp     = WGPULoadOp.Clear,
                    depthStoreOp    = WGPUStoreOp.Store,
                },
            };
            frameArena.Use();
        }
        
        private const    float      aspect = Program.Width / Program.Height;
        private readonly Matrix4x4  projectionMatrix = Matrix4x4.CreatePerspective((2f * MathF.PI) / 5f, aspect, 1, 100.0f);
        //  Matrix4x4 modelViewProjectionMatrix = new Matrix4x4();
        private readonly long       startTime = Stopwatch.GetTimestamp();
        
        Matrix4x4 getTransformationMatrix()
        {
            /*
            const viewMatrix = mat4.identity();
            mat4.translate(viewMatrix, vec3.fromValues(0, 0, -4), viewMatrix);
            const now = Date.now() / 1000;
            mat4.rotate(
              viewMatrix,
              vec3.fromValues(Math.sin(now), Math.cos(now), 0),
              1,
              viewMatrix
            );
            mat4.multiply(projectionMatrix, viewMatrix, modelViewProjectionMatrix);*/
          
            float now = (float)(((double)Stopwatch.GetTimestamp() - startTime) / Stopwatch.Frequency);
            var viewMatrix = Matrix4x4.CreateFromAxisAngle(new(MathF.Sin(now), MathF.Cos(now), 0), 1) with
            {
                Translation = new(0, 0, -4),
            };
            return viewMatrix * projectionMatrix;
        }

        internal void DrawFrame(WGPUTextureView view)
        {
            frameArena.Use();

            var transformationMatrix = getTransformationMatrix();
            queue.writeBuffer(
                uniformBuffer,
                0,
                transformationMatrix
            );
            renderPassDescriptor.colorAttachments[0].view = view;

            using var commandEncoder = device.createCommandEncoder();
            var passEncoder = commandEncoder.beginRenderPass(renderPassDescriptor);
            passEncoder.setPipeline(pipeline);
            passEncoder.setBindGroup(0, uniformBindGroup);
            passEncoder.setVertexBuffer(0, verticesBuffer);
            passEncoder.draw(Cube.cubeVertexCount, 1, 0, 0); // TODO add overload
            passEncoder.end();
            passEncoder.release(); // required: otherwise submit() panics: "CommandBuffer cannot be destroyed because is still in use"
            
            using var command = commandEncoder.finish();

            queue.submit([command]);
        }
    }
}