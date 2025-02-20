# WebGPU.NET

## Introduction

WebGPU.NET is a lightweight, low-level wrapper built on top of the `wgpu-native` library from Firefox. Our aim is to facilitate swift development of an adapter for Evergine, allowing for rapid testing across Windows, Linux, and Mac platforms using DirectX, Vulkan, and Metal.

[![CI](https://github.com/EvergineTeam/WebGPU.NET/actions/workflows/CI.yml/badge.svg)](https://github.com/EvergineTeam/WebGPU.NET/actions/workflows/CI.yml)
[![CD WebGPU](https://github.com/EvergineTeam/WebGPU.NET/actions/workflows/cd.yml/badge.svg)](https://github.com/EvergineTeam/WebGPU.NET/actions/workflows/cd.yml)
[![Nuget](https://img.shields.io/nuget/v/Evergine.Bindings.WebGPU?logo=nuget)](https://www.nuget.org/packages/Evergine.Bindings.WebGPU)

## Features

- **Low-level Access**: Get closer to the metal with our streamlined API that wraps `wgpu-native`.
  
- **Cross-Platform Support**: Test and deploy your applications seamlessly on Windows, Linux, and Mac.

- **Multiple Graphics API Compatibility**: Designed with DirectX, Vulkan, and Metal in mind.

## Prerequisites

List any dependencies, required libraries, or external factors here.

## Installation

1. Clone the repository: `git clone https://github.com/EvergineTeam/WebGPU.NET.git`
2. Navigate to the project directory.
3. Run the HelloTriangle test project.

## Safe C# API Layer

Provide an **optional** and **lightweight** API Layer on top of the C bindings.  
Features of API Layer
- Provide an OOP like API similar to Javascript/Typescript WebGPU API.
- The API layer enables to write WebGPU applications without **unsafe** C#.
- Entire implementation (all methods & properties) do not allocate objects in C# (CLR) heap.
- Add no additional types. Instead the layer add only extension methods and struct properties.  
- The use of this API can be combined with the vanilla C bindings. So existing projects can be migrated incrementally.
- Track allocations of WebGPU [Objects](https://webgpu-native.github.io/webgpu-headers/group__Objects.html)
  to avoid / find leaks in native WebGPU heap.

### Example: API Layer vs C API

Both implementation provide the same result.

**API Layer**
- no use of unsafe code
- required no local variables
```cs
    vertexBuffer = Device.createBuffer(new WGPUBufferDescriptor {
        nextInChain = null,
        usage = WGPUBufferUsage.Vertex | WGPUBufferUsage.CopyDst,
        size = (ulong)(6 * sizeof(Vector4)),
        mappedAtCreation = false,        
    });
    Queue.writeBuffer(vertexBuffer, 0, [
        new Vector4(0.0f, 0.5f, 0.5f, 1.0f),
        new Vector4(1.0f, 0.0f, 0.0f, 1.0f),
        new Vector4(0.5f, -0.5f, 0.5f, 1.0f),
        new Vector4(0.0f, 1.0f, 0.0f, 1.0f),
        new Vector4(-0.5f, -0.5f, 0.5f, 1.0f),
        new Vector4(0.0f, 0.0f, 1.0f, 1.0f)
    ]);
```

**C API**
- requires unsafe code
- requires local (stack) variables: `bufferDescriptor` & `vertexData`
```cs
    ulong size = (ulong)(6 * sizeof(Vector4));
    WGPUBufferDescriptor bufferDescriptor = new WGPUBufferDescriptor()
    {
        nextInChain = null,
        usage = WGPUBufferUsage.Vertex | WGPUBufferUsage.CopyDst,
        size = size,
        mappedAtCreation = false,
    };
    vertexBuffer = wgpuDeviceCreateBuffer(Device, &bufferDescriptor);
    Vector4* vertexData = stackalloc Vector4[] {
        new Vector4(0.0f, 0.5f, 0.5f, 1.0f),
        new Vector4(1.0f, 0.0f, 0.0f, 1.0f),
        new Vector4(0.5f, -0.5f, 0.5f, 1.0f),
        new Vector4(0.0f, 1.0f, 0.0f, 1.0f),
        new Vector4(-0.5f, -0.5f, 0.5f, 1.0f),
        new Vector4(0.0f, 0.0f, 1.0f, 1.0f)
    };
    wgpuQueueWriteBuffer(Queue, vertexBuffer, 0, vertexData, size);
```



### Usage

To include `Evergine.Bindings.WebGPU` in your project, install the NuGet package:

Install-Package Evergine.Bindings.WebGPU

Or if you use the .NET CLI:

dotnet add package Evergine.Bindings.WebGPU

## License

This project is licensed under the MIT License - see the [LICENSE.md](link_to_license.md) file for details.

## Acknowledgments

- Thanks to the Firefox team for the `wgpu-native` library. Check out the original library on [gfx-rs/wgpu-native](https://github.com/gfx-rs/wgpu-native).
