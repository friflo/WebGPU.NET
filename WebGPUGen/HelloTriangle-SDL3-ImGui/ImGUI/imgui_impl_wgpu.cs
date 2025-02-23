using System;
using System.Numerics;
using System.Runtime.InteropServices;
using Evergine.Bindings.WebGPU;
using ImGuiNET;

using static Evergine.Bindings.WebGPU.WebGPUNative;

using ImDrawIdx = System.UInt16;
using ImTextureID = System.IntPtr;

/*
// dear imgui: Renderer for WebGPU
// This needs to be used along with a Platform Binding (e.g. GLFW)
// (Please note that WebGPU is currently experimental, will not run on non-beta browsers, and may break.)

// Implemented features:
//  [X] Renderer: User texture binding. Use 'WGPUTextureView' as ImTextureID. Read the FAQ about ImTextureID!
//  [X] Renderer: Large meshes support (64k+ vertices) even with 16-bit indices (ImGuiBackendFlags_RendererHasVtxOffset).
//  [X] Renderer: Expose selected render state for draw callbacks to use. Access in '(ImGui_ImplXXXX_RenderState*)GetPlatformIO().Renderer_RenderState'.

// You can use unmodified imgui_impl_* files in your project. See examples/ folder for examples of using this.
// Prefer including the entire imgui/ repository into your project (either as a copy or as a submodule), and only build the backends you need.
// Learn about Dear ImGui:
// - FAQ                  https://dearimgui.com/faq
// - Getting Started      https://dearimgui.com/getting-started
// - Documentation        https://dearimgui.com/docs (same as your local docs/ folder).
// - Introduction, links and more at the top of imgui.cpp

// CHANGELOG
// (minor and older changes stripped away, please see git history for details)
//  2024-10-14: Update Dawn support for change of string usages. (#8082, #8083)
//  2024-10-07: Expose selected render state in ImGui_ImplWGPU_RenderState, which you can access in 'void* platform_io.Renderer_RenderState' during draw callbacks.
//  2024-10-07: Changed default texture sampler to Clamp instead of Repeat/Wrap.
//  2024-09-16: Added support for optional IMGUI_IMPL_WEBGPU_BACKEND_DAWN / IMGUI_IMPL_WEBGPU_BACKEND_WGPU define to handle ever-changing native implementations. (#7977)
//  2024-01-22: Added configurable PipelineMultisampleState struct. (#7240)
//  2024-01-22: (Breaking) ImGui_ImplWGPU_Init() now takes a ImGui_ImplWGPU_InitInfo structure instead of variety of parameters, allowing for easier further changes.
//  2024-01-22: Fixed pipeline layout leak. (#7245)
//  2024-01-17: Explicitly fill all of WGPUDepthStencilState since standard removed defaults.
//  2023-07-13: Use WGPUShaderModuleWGSLDescriptor's code instead of source. use WGPUMipmapFilterMode_Linear instead of WGPUFilterMode_Linear. (#6602)
//  2023-04-11: Align buffer sizes. Use WGSL shaders instead of precompiled SPIR-V.
//  2023-04-11: Reorganized backend to pull data from a single structure to facilitate usage with multiple-contexts (all g_XXXX access changed to bd->XXXX).
//  2023-01-25: Revert automatic pipeline layout generation (see https://github.com/gpuweb/gpuweb/issues/2470)
//  2022-11-24: Fixed validation error with default depth buffer settings.
//  2022-11-10: Fixed rendering when a depth buffer is enabled. Added 'WGPUTextureFormat depth_format' parameter to ImGui_ImplWGPU_Init().
//  2022-10-11: Using 'nullptr' instead of 'NULL' as per our switch to C++11.
//  2021-11-29: Passing explicit buffer sizes to wgpuRenderPassEncoderSetVertexBuffer()/wgpuRenderPassEncoderSetIndexBuffer().
//  2021-08-24: Fixed for latest specs.
//  2021-05-24: Add support for draw_data->FramebufferScale.
//  2021-05-19: Replaced direct access to ImDrawCmd::TextureId with a call to ImDrawCmd::GetTexID(). (will become a requirement)
//  2021-05-16: Update to latest WebGPU specs (compatible with Emscripten 2.0.20 and Chrome Canary 92).
//  2021-02-18: Change blending equation to preserve alpha in output buffer.
//  2021-01-28: Initial version.

// When targeting native platforms (i.e. NOT emscripten), one of IMGUI_IMPL_WEBGPU_BACKEND_DAWN
// or IMGUI_IMPL_WEBGPU_BACKEND_WGPU must be provided. See imgui_impl_wgpu.h for more details.
// #ifndef __EMSCRIPTEN__
//     #if defined(IMGUI_IMPL_WEBGPU_BACKEND_DAWN) == defined(IMGUI_IMPL_WEBGPU_BACKEND_WGPU)
//     #error exactly one of IMGUI_IMPL_WEBGPU_BACKEND_DAWN or IMGUI_IMPL_WEBGPU_BACKEND_WGPU must be defined!
//     #endif
// #else
//     #if defined(IMGUI_IMPL_WEBGPU_BACKEND_DAWN) || defined(IMGUI_IMPL_WEBGPU_BACKEND_WGPU)
//     #error neither IMGUI_IMPL_WEBGPU_BACKEND_DAWN nor IMGUI_IMPL_WEBGPU_BACKEND_WGPU may be defined if targeting emscripten!
//     #endif
// #endif
// 
// #include "imgui.h"
// #ifndef IMGUI_DISABLE
// #include "imgui_impl_wgpu.h"
// #include <limits.h>
// #include <webgpu/webgpu.h>
// 
// #ifdef IMGUI_IMPL_WEBGPU_BACKEND_DAWN
// Dawn renamed WGPUProgrammableStageDescriptor to WGPUComputeState (see: https://github.com/webgpu-native/webgpu-headers/pull/413)
// Using type alias until WGPU adopts the same naming convention (#8369)
// using WGPUProgrammableStageDescriptor = WGPUComputeState;
// #endif

// Dear ImGui prototypes from imgui_internal.h
// extern ImGuiID ImHashData(const void* data_p, size_t data_size, ImU32 seed = 0);
// #define MEMALIGN(_SIZE,_ALIGN)        (((_SIZE) + ((_ALIGN) - 1)) & ~((_ALIGN) - 1))    // Memory align (copied from IM_ALIGN() macro).
*/



namespace SDLIM;

// ReSharper disable InconsistentNaming
public static unsafe class ImGui_ImplWGPU {
    
// Initialization data, for ImGui_ImplWGPU_Init()
struct ImGui_ImplWGPU_InitInfo
{
    internal WGPUDevice              Device;
    internal int                     NumFramesInFlight = 3;
    internal WGPUTextureFormat       RenderTargetFormat = WGPUTextureFormat.Undefined;
    internal WGPUTextureFormat       DepthStencilFormat = WGPUTextureFormat.Undefined;
    internal WGPUMultisampleState    PipelineMultisampleState = new ();

    public ImGui_ImplWGPU_InitInfo()
    {
        PipelineMultisampleState.count = 1;
        PipelineMultisampleState.mask = 0xFFFFFFFF;
        PipelineMultisampleState.alphaToCoverageEnabled = false;
    }
};

struct ImGui_ImplWGPU_RenderState
{
    internal WGPUDevice                  Device;
    internal WGPURenderPassEncoder       RenderPassEncoder;
};
    
// WebGPU data
struct RenderResources
{
    internal WGPUTexture         FontTexture;          // Font texture
    internal WGPUTextureView     FontTextureView;      // Texture view for font texture
    internal WGPUSampler         Sampler;              // Sampler for the font texture
    internal WGPUBuffer          Uniforms;             // Shader uniforms
    internal WGPUBindGroup       CommonBindGroup;      // Resources bind-group to bind the common resources to pipeline
    internal ImGuiStorage        ImageBindGroups;      // Resources bind-group to bind the font/image resources to pipeline (this is a key->value map)
    internal WGPUBindGroup       ImageBindGroup;       // Default font-resource of Dear ImGui
    internal WGPUBindGroupLayout ImageBindGroupLayout; // Cache layout used for the image bind group. Avoids allocating unnecessary JS objects when working with WebASM
};

struct FrameResources
{
    internal WGPUBuffer  IndexBuffer;
    internal WGPUBuffer  VertexBuffer;
    internal ImDrawIdx*  IndexBufferHost;
    internal ImDrawVert* VertexBufferHost;
    internal int         IndexBufferSize;
    internal int         VertexBufferSize;
};

struct Uniforms
{
    internal Matrix4x4 MVP;
    internal float Gamma;
};

struct ImGui_ImplWGPU_Data
{
    internal ImGui_ImplWGPU_InitInfo initInfo;
    internal WGPUDevice              wgpuDevice;
    internal WGPUQueue               defaultQueue;
    internal WGPUTextureFormat       renderTargetFormat = WGPUTextureFormat.Undefined;
    internal WGPUTextureFormat       depthStencilFormat = WGPUTextureFormat.Undefined;
    internal WGPURenderPipeline      pipelineState;

    internal RenderResources         renderResources;
    internal FrameResources*         pFrameResources;
    internal uint                    numFramesInFlight = 0;
    internal uint                    frameIndex = uint.MaxValue;
    
    public ImGui_ImplWGPU_Data() {}
};

// Backend data stored in io.BackendRendererUserData to allow support for multiple Dear ImGui contexts
// It is STRONGLY preferred that you use docking branch with multi-viewports (== single Dear ImGui context + multiple windows) instead of multiple Dear ImGui contexts.
static ImGui_ImplWGPU_Data* ImGui_ImplWGPU_GetBackendData()
{
    return ImGui.GetCurrentContext() != 0 ? (ImGui_ImplWGPU_Data*)ImGui.GetIO().BackendRendererUserData : null;
}

//-----------------------------------------------------------------------------
// SHADERS
//-----------------------------------------------------------------------------
   
static ReadOnlySpan<byte> __shader_vert_wgsl =>
@"(
struct VertexInput {
    @location(0) position: vec2<f32>,
    @location(1) uv: vec2<f32>,
    @location(2) color: vec4<f32>,
};

struct VertexOutput {
    @builtin(position) position: vec4<f32>,
    @location(0) color: vec4<f32>,
    @location(1) uv: vec2<f32>,
};

struct Uniforms {
    mvp: mat4x4<f32>,
    gamma: f32,
};

@group(0) @binding(0) var<uniform> uniforms: Uniforms;

@vertex
fn main(in: VertexInput) -> VertexOutput {
    var out: VertexOutput;
    out.position = uniforms.mvp * vec4<f32>(in.position, 0.0, 1.0);
    out.color = in.color;
    out.uv = in.uv;
    return out;
}
)"u8;

static ReadOnlySpan<byte> __shader_frag_wgsl =>
@"(
struct VertexOutput {
    @builtin(position) position: vec4<f32>,
    @location(0) color: vec4<f32>,
    @location(1) uv: vec2<f32>,
};

struct Uniforms {
    mvp: mat4x4<f32>,
    gamma: f32,
};

@group(0) @binding(0) var<uniform> uniforms: Uniforms;
@group(0) @binding(1) var s: sampler;
@group(1) @binding(0) var t: texture_2d<f32>;

@fragment
fn main(in: VertexOutput) -> @location(0) vec4<f32> {
    let color = in.color * textureSample(t, s, in.uv);
    let corrected_color = pow(color.rgb, vec3<f32>(uniforms.gamma));
    return vec4<f32>(corrected_color, color.a);
}
)"u8;

static void SafeRelease(ImDrawIdx*& res)
{
    if (res)
        delete[] res;
    res = nullptr;
}
static void SafeRelease(ImDrawVert*& res)
{
    if (res)
        delete[] res;
    res = nullptr;
}
static void SafeRelease(WGPUBindGroupLayout& res)
{
    if (res)
        wgpuBindGroupLayoutRelease(res);
    res = nullptr;
}
static void SafeRelease(WGPUBindGroup& res)
{
    if (res)
        wgpuBindGroupRelease(res);
    res = nullptr;
}
static void SafeRelease(WGPUBuffer& res)
{
    if (res)
        wgpuBufferRelease(res);
    res = nullptr;
}
static void SafeRelease(WGPUPipelineLayout& res)
{
    if (res)
        wgpuPipelineLayoutRelease(res);
    res = nullptr;
}
static void SafeRelease(WGPURenderPipeline& res)
{
    if (res)
        wgpuRenderPipelineRelease(res);
    res = nullptr;
}
static void SafeRelease(WGPUSampler& res)
{
    if (res)
        wgpuSamplerRelease(res);
    res = nullptr;
}
static void SafeRelease(WGPUShaderModule& res)
{
    if (res)
        wgpuShaderModuleRelease(res);
    res = nullptr;
}
static void SafeRelease(WGPUTextureView& res)
{
    if (res)
        wgpuTextureViewRelease(res);
    res = nullptr;
}
static void SafeRelease(WGPUTexture& res)
{
    if (res)
        wgpuTextureRelease(res);
    res = nullptr;
}

static void SafeRelease(in RenderResources res)
{
    SafeRelease(res.FontTexture);
    SafeRelease(res.FontTextureView);
    SafeRelease(res.Sampler);
    SafeRelease(res.Uniforms);
    SafeRelease(res.CommonBindGroup);
    SafeRelease(res.ImageBindGroup);
    SafeRelease(res.ImageBindGroupLayout);
};

static void SafeRelease(in FrameResources res)
{
    SafeRelease(res.IndexBuffer);
    SafeRelease(res.VertexBuffer);
    SafeRelease(res.IndexBufferHost);
    SafeRelease(res.VertexBufferHost);
}

static WGPUProgrammableStageDescriptor ImGui_ImplWGPU_CreateShaderModule(ReadOnlySpan<byte> wgsl_source)
{
    ImGui_ImplWGPU_Data* bd = ImGui_ImplWGPU_GetBackendData();

#if IMGUI_IMPL_WEBGPU_BACKEND_DAWN
    WGPUShaderSourceWGSL wgsl_desc = {};
    wgsl_desc.chain.sType = WGPUSType.ShaderSourceWGSL;
    wgsl_desc.code = { wgsl_source, WGPU_STRLEN };
#else
    WGPUShaderModuleWGSLDescriptor wgsl_desc = new();
    wgsl_desc.chain.sType = WGPUSType.ShaderModuleWGSLDescriptor;
    wgsl_desc.code = wgsl_source;
#endif

    WGPUShaderModuleDescriptor desc = {};
    desc.nextInChain = reinterpret_cast<WGPUChainedStruct*>(&wgsl_desc);

    WGPUProgrammableStageDescriptor stage_desc = {};
    stage_desc.module = wgpuDeviceCreateShaderModule(bd->wgpuDevice, &desc);
#if IMGUI_IMPL_WEBGPU_BACKEND_DAWN
    stage_desc.entryPoint = { "main", WGPU_STRLEN };
#else
    stage_desc.entryPoint = "main"u8;
#endif
    return stage_desc;
}

static WGPUBindGroup ImGui_ImplWGPU_CreateImageBindGroup(WGPUBindGroupLayout layout, WGPUTextureView texture)
{
    ImGui_ImplWGPU_Data* bd = ImGui_ImplWGPU_GetBackendData();
    WGPUBindGroupEntry image_bg_entries[] = { { nullptr, 0, 0, 0, 0, 0, texture } };

    WGPUBindGroupDescriptor image_bg_descriptor = {};
    image_bg_descriptor.layout = layout;
    image_bg_descriptor.entryCount = sizeof(image_bg_entries) / sizeof(WGPUBindGroupEntry);
    image_bg_descriptor.entries = image_bg_entries;
    return wgpuDeviceCreateBindGroup(bd->wgpuDevice, &image_bg_descriptor);
}

static void ImGui_ImplWGPU_SetupRenderState(ImDrawData* draw_data, WGPURenderPassEncoder ctx, FrameResources* fr)
{
    ImGui_ImplWGPU_Data* bd = ImGui_ImplWGPU_GetBackendData();

    // Setup orthographic projection matrix into our constant buffer
    // Our visible imgui space lies from draw_data->DisplayPos (top left) to draw_data->DisplayPos+data_data->DisplaySize (bottom right).
    {
        float L = draw_data->DisplayPos.X;
        float R = draw_data->DisplayPos.X + draw_data->DisplaySize.X;
        float T = draw_data->DisplayPos.Y;
        float B = draw_data->DisplayPos.Y + draw_data->DisplaySize.Y;
        Matrix4x4 mvp = new
        (
            2.0f/(R-L),   0.0f,           0.0f,       0.0f ,
            0.0f,         2.0f/(T-B),     0.0f,       0.0f ,
            0.0f,         0.0f,           0.5f,       0.0f ,
            (R+L)/(L-R),  (T+B)/(B-T),    0.5f,       1.0f
        );
        wgpuQueueWriteBuffer(bd->defaultQueue, bd->renderResources.Uniforms, (ulong)Marshal.OffsetOf<Uniforms>(nameof(Uniforms.MVP)), &mvp, (ulong)sizeof(Matrix4x4));
        float gamma;
        switch (bd->renderTargetFormat)
        {
        case WGPUTextureFormat.ASTC10x10UnormSrgb:
        case WGPUTextureFormat.ASTC10x5UnormSrgb:
        case WGPUTextureFormat.ASTC10x6UnormSrgb:
        case WGPUTextureFormat.ASTC10x8UnormSrgb:
        case WGPUTextureFormat.ASTC12x10UnormSrgb:
        case WGPUTextureFormat.ASTC12x12UnormSrgb:
        case WGPUTextureFormat.ASTC4x4UnormSrgb:
        case WGPUTextureFormat.ASTC5x5UnormSrgb:
        case WGPUTextureFormat.ASTC6x5UnormSrgb:
        case WGPUTextureFormat.ASTC6x6UnormSrgb:
        case WGPUTextureFormat.ASTC8x5UnormSrgb:
        case WGPUTextureFormat.ASTC8x6UnormSrgb:
        case WGPUTextureFormat.ASTC8x8UnormSrgb:
        case WGPUTextureFormat.BC1RGBAUnormSrgb:
        case WGPUTextureFormat.BC2RGBAUnormSrgb:
        case WGPUTextureFormat.BC3RGBAUnormSrgb:
        case WGPUTextureFormat.BC7RGBAUnormSrgb:
        case WGPUTextureFormat.BGRA8UnormSrgb:
        case WGPUTextureFormat.ETC2RGB8A1UnormSrgb:
        case WGPUTextureFormat.ETC2RGB8UnormSrgb:
        case WGPUTextureFormat.ETC2RGBA8UnormSrgb:
        case WGPUTextureFormat.RGBA8UnormSrgb:
            gamma = 2.2f;
            break;
        default:
            gamma = 1.0f;
            break;
        }
        wgpuQueueWriteBuffer(bd->defaultQueue, bd->renderResources.Uniforms, (ulong)Marshal.OffsetOf<Uniforms>(nameof(Uniforms.Gamma)), &gamma, sizeof(float));
    }

    // Setup viewport
    wgpuRenderPassEncoderSetViewport(ctx, 0, 0, draw_data->FramebufferScale.X * draw_data->DisplaySize.X, draw_data->FramebufferScale.Y * draw_data->DisplaySize.Y, 0, 1);

    // Bind shader and vertex buffers
    wgpuRenderPassEncoderSetVertexBuffer(ctx, 0, fr->VertexBuffer, 0, (ulong)(fr->VertexBufferSize * sizeof(ImDrawVert)));
    wgpuRenderPassEncoderSetIndexBuffer(ctx, fr->IndexBuffer, sizeof(ImDrawIdx) == 2 ? WGPUIndexFormat.Uint16 : WGPUIndexFormat.Uint32, 0, (ulong)(fr->IndexBufferSize * sizeof(ImDrawIdx)));
    wgpuRenderPassEncoderSetPipeline(ctx, bd->pipelineState);
    wgpuRenderPassEncoderSetBindGroup(ctx, 0, bd->renderResources.CommonBindGroup, 0, null);

    // Setup blend factor
    WGPUColor blend_color = new WGPUColor();
    wgpuRenderPassEncoderSetBlendConstant(ctx, &blend_color);
}

// Render function
// (this used to be set in io.RenderDrawListsFn and called by ImGui::Render(), but you can now call this directly from your main loop)
static void ImGui_ImplWGPU_RenderDrawData(ImDrawDataPtr draw_data, WGPURenderPassEncoder pass_encoder)
{
    // Avoid rendering when minimized
    int fb_width = (int)(draw_data.DisplaySize.X * draw_data.FramebufferScale.X);
    int fb_height = (int)(draw_data.DisplaySize.Y * draw_data.FramebufferScale.Y);
    if (fb_width <= 0 || fb_height <= 0 || draw_data.CmdListsCount == 0)
        return;

    // FIXME: Assuming that this only gets called once per frame!
    // If not, we can't just re-allocate the IB or VB, we'll have to do a proper allocator.
    ImGui_ImplWGPU_Data* bd = ImGui_ImplWGPU_GetBackendData();
    bd->frameIndex = bd->frameIndex + 1;
    FrameResources* fr = &bd->pFrameResources[bd->frameIndex % bd->numFramesInFlight];

    // Create and grow vertex/index buffers if needed
    if (fr->VertexBuffer == nullptr || fr->VertexBufferSize < draw_data.TotalVtxCount)
    {
        if (fr->VertexBuffer)
        {
            wgpuBufferDestroy(fr->VertexBuffer);
            wgpuBufferRelease(fr->VertexBuffer);
        }
        SafeRelease(fr->VertexBufferHost);
        fr->VertexBufferSize = draw_data.TotalVtxCount + 5000;

        WGPUBufferDescriptor vb_desc =
        {
            nullptr,
            "Dear ImGui Vertex buffer",
#if IMGUI_IMPL_WEBGPU_BACKEND_DAWN
            WGPU_STRLEN,
#endif
            WGPUBufferUsage.CopyDst | WGPUBufferUsage.Vertex,
            MEMALIGN(fr->VertexBufferSize * sizeof(ImDrawVert), 4),
            false
        };
        fr->VertexBuffer = wgpuDeviceCreateBuffer(bd->wgpuDevice, &vb_desc);
        if (!fr->VertexBuffer)
            return;

        fr->VertexBufferHost = new ImDrawVert[fr->VertexBufferSize];
    }
    if (fr->IndexBuffer == nullptr || fr->IndexBufferSize < draw_data.TotalIdxCount)
    {
        if (fr->IndexBuffer)
        {
            wgpuBufferDestroy(fr->IndexBuffer);
            wgpuBufferRelease(fr->IndexBuffer);
        }
        SafeRelease(fr->IndexBufferHost);
        fr->IndexBufferSize = draw_data.TotalIdxCount + 10000;
        
        WGPUBufferDescriptor ib_desc = new()
        {
//          nullptr,
            label = "Dear ImGui Index buffer"u8,
#if IMGUI_IMPL_WEBGPU_BACKEND_DAWN
            WGPU_STRLEN,
#endif
            usage = WGPUBufferUsage.CopyDst | WGPUBufferUsage.Index,
            size = MEMALIGN(fr->IndexBufferSize * sizeof(ImDrawIdx), 4),
            mappedAtCreation = false
        };
        fr->IndexBuffer = wgpuDeviceCreateBuffer(bd->wgpuDevice, &ib_desc);
        if (!fr->IndexBuffer)
            return;

        fr->IndexBufferHost = new ImDrawIdx[fr->IndexBufferSize];
    }

    // Upload vertex/index data into a single contiguous GPU buffer
    ImDrawVert* vtx_dst = (ImDrawVert*)fr->VertexBufferHost;
    ImDrawIdx* idx_dst = (ImDrawIdx*)fr->IndexBufferHost;
    for (int n = 0; n < draw_data.CmdListsCount; n++)
    {
        var draw_list = draw_data.CmdLists[n];
        memcpy(vtx_dst, draw_list.VtxBuffer.Data, draw_list.VtxBuffer.Size * sizeof(ImDrawVert));
        memcpy(idx_dst, draw_list.IdxBuffer.Data, draw_list.IdxBuffer.Size * sizeof(ImDrawIdx));
        vtx_dst += draw_list.VtxBuffer.Size;
        idx_dst += draw_list.IdxBuffer.Size;
    }
    int64_t vb_write_size = MEMALIGN((char*)vtx_dst - (char*)fr->VertexBufferHost, 4);
    int64_t ib_write_size = MEMALIGN((char*)idx_dst - (char*)fr->IndexBufferHost, 4);
    wgpuQueueWriteBuffer(bd->defaultQueue, fr->VertexBuffer, 0, fr->VertexBufferHost, vb_write_size);
    wgpuQueueWriteBuffer(bd->defaultQueue, fr->IndexBuffer,  0, fr->IndexBufferHost,  ib_write_size);

    // Setup desired render state
    ImGui_ImplWGPU_SetupRenderState(draw_data, pass_encoder, fr);

    // Setup render state structure (for callbacks and custom texture bindings)
    var platform_io = ImGui.GetPlatformIO();
    ImGui_ImplWGPU_RenderState render_state;
    render_state.Device = bd->wgpuDevice;
    render_state.RenderPassEncoder = pass_encoder;
    platform_io.Renderer_RenderState = &render_state;

    // Render command lists
    // (Because we merged all buffers into a single one, we maintain our own offset into them)
    int global_vtx_offset = 0;
    int global_idx_offset = 0;
    var clip_scale = draw_data.FramebufferScale;
    var clip_off = draw_data.DisplayPos;
    for (int n = 0; n < draw_data.CmdListsCount; n++)
    {
        var draw_list = draw_data.CmdLists[n];
        for (int cmd_i = 0; cmd_i < draw_list.CmdBuffer.Size; cmd_i++)
        {
            var pcmd = &draw_list.CmdBuffer[cmd_i];
            if (pcmd->UserCallback != 0)
            {
                // User callback, registered via ImDrawList::AddCallback()
                // (ImDrawCallback_ResetRenderState is a special callback value used by the user to request the renderer to reset render state.)
                if (pcmd->UserCallback == ImDrawCallback_ResetRenderState)
                    ImGui_ImplWGPU_SetupRenderState(draw_data, pass_encoder, fr);
                else
                    pcmd->UserCallback(draw_list, pcmd);
            }
            else
            {
                // Bind custom texture
                ImTextureID tex_id = pcmd->GetTexID();
                ImGuiID tex_id_hash = ImHashData(&tex_id, sizeof(tex_id));
                auto bind_group = bd->renderResources.ImageBindGroups.GetVoidPtr(tex_id_hash);
                if (bind_group)
                {
                    wgpuRenderPassEncoderSetBindGroup(pass_encoder, 1, (WGPUBindGroup)bind_group, 0, nullptr);
                }
                else
                {
                    WGPUBindGroup image_bind_group = ImGui_ImplWGPU_CreateImageBindGroup(bd->renderResources.ImageBindGroupLayout, (WGPUTextureView)tex_id);
                    bd->renderResources.ImageBindGroups.SetVoidPtr(tex_id_hash, image_bind_group);
                    wgpuRenderPassEncoderSetBindGroup(pass_encoder, 1, image_bind_group, 0, nullptr);
                }

                // Project scissor/clipping rectangles into framebuffer space
                var clip_min = new Vector2((pcmd->ClipRect.X - clip_off.X) * clip_scale.X, (pcmd->ClipRect.Y - clip_off.Y) * clip_scale.Y);
                var clip_max = new Vector2((pcmd->ClipRect.X - clip_off.X) * clip_scale.X, (pcmd->ClipRect.W - clip_off.Y) * clip_scale.Y);

                // Clamp to viewport as wgpuRenderPassEncoderSetScissorRect() won't accept values that are off bounds
                if (clip_min.X < 0.0f) { clip_min.X = 0.0f; }
                if (clip_min.Y < 0.0f) { clip_min.Y = 0.0f; }
                if (clip_max.X > fb_width) { clip_max.X = (float)fb_width; }
                if (clip_max.Y > fb_height) { clip_max.Y = (float)fb_height; }
                if (clip_max.X <= clip_min.X || clip_max.Y <= clip_min.Y)
                    continue;

                // Apply scissor/clipping rectangle, Draw
                wgpuRenderPassEncoderSetScissorRect(pass_encoder, (uint)clip_min.X, (uint)clip_min.Y, (uint)(clip_max.X - clip_min.Y), (uint)(clip_max.Y - clip_min.Y));
                wgpuRenderPassEncoderDrawIndexed(pass_encoder, pcmd->ElemCount, 1, (uint)(pcmd->IdxOffset + global_idx_offset), (uint)(pcmd->VtxOffset + global_vtx_offset), 0);
            }
        }
        global_idx_offset += draw_list.IdxBuffer.Size;
        global_vtx_offset += draw_list.VtxBuffer.Size;
    }
    platform_io.Renderer_RenderState = 0;
}

static void ImGui_ImplWGPU_CreateFontsTexture()
{
    // Build texture atlas
    ImGui_ImplWGPU_Data* bd = ImGui_ImplWGPU_GetBackendData();
    ImGuiIOPtr io = ImGui.GetIO();
    byte* pixels;
    int width, height, size_pp;
    io.Fonts.GetTexDataAsRGBA32(out pixels, out width, out height, out size_pp);

    // Upload texture to graphics system
    {
        WGPUTextureDescriptor tex_desc = {};
#if IMGUI_IMPL_WEBGPU_BACKEND_DAWN
        tex_desc.label = { "Dear ImGui Font Texture", WGPU_STRLEN };
#else
        tex_desc.label = "Dear ImGui Font Texture"u8;
#endif
        tex_desc.dimension = WGPUTextureDimension._2D;
        tex_desc.size.width = (uint)width;
        tex_desc.size.height = (uint)height;
        tex_desc.size.depthOrArrayLayers = 1;
        tex_desc.sampleCount = 1;
        tex_desc.format = WGPUTextureFormat.RGBA8Unorm;
        tex_desc.mipLevelCount = 1;
        tex_desc.usage = WGPUTextureUsage.CopyDst | WGPUTextureUsage.TextureBinding;
        bd->renderResources.FontTexture = wgpuDeviceCreateTexture(bd->wgpuDevice, &tex_desc);

        WGPUTextureViewDescriptor tex_view_desc = {};
        tex_view_desc.format = WGPUTextureFormat.RGBA8Unorm;
        tex_view_desc.dimension = WGPUTextureViewDimension._2D;
        tex_view_desc.baseMipLevel = 0;
        tex_view_desc.mipLevelCount = 1;
        tex_view_desc.baseArrayLayer = 0;
        tex_view_desc.arrayLayerCount = 1;
        tex_view_desc.aspect = WGPUTextureAspect.All;
        bd->renderResources.FontTextureView = wgpuTextureCreateView(bd->renderResources.FontTexture, &tex_view_desc);
    }

    // Upload texture data
    {
        WGPUImageCopyTexture dst_view = {};
        dst_view.texture = bd->renderResources.FontTexture;
        dst_view.mipLevel = 0;
        dst_view.origin = new WGPUOrigin3D();
        dst_view.aspect = WGPUTextureAspect.All;
        WGPUTextureDataLayout layout = new WGPUTextureDataLayout();
        layout.offset = 0;
        layout.bytesPerRow = (uint)(width * size_pp);
        layout.rowsPerImage = (uint)height;
        WGPUExtent3D size = new WGPUExtent3D{ width = (uint)width, height = (uint)height, depthOrArrayLayers = 1 }; // { (uint32_t)width, (uint32_t)height, 1 };
        wgpuQueueWriteTexture(bd->defaultQueue, &dst_view, pixels, (uint)(width * size_pp * height), &layout, &size);
    }

    // Create the associated sampler
    // (Bilinear sampling is required by default. Set 'io.Fonts->Flags |= ImFontAtlasFlags_NoBakedLines' or 'style.AntiAliasedLinesUseTex = false' to allow point/nearest sampling)
    {
        WGPUSamplerDescriptor sampler_desc = {};
        sampler_desc.minFilter = WGPUFilterMode.Linear;
        sampler_desc.magFilter = WGPUFilterMode.Linear;
        sampler_desc.mipmapFilter = WGPUMipmapFilterMode.Linear;
        sampler_desc.addressModeU = WGPUAddressMode.ClampToEdge;
        sampler_desc.addressModeV = WGPUAddressMode.ClampToEdge;
        sampler_desc.addressModeW = WGPUAddressMode.ClampToEdge;
        sampler_desc.maxAnisotropy = 1;
        bd->renderResources.Sampler = wgpuDeviceCreateSampler(bd->wgpuDevice, &sampler_desc);
    }

    // Store our identifier
    static_assert(sizeof(ImTextureID) >= sizeof(bd->renderResources.FontTexture), "Can't pack descriptor handle into TexID, 32-bit not supported yet.");
    io.Fonts.SetTexID((ImTextureID)bd->renderResources.FontTextureView);
}

static void ImGui_ImplWGPU_CreateUniformBuffer()
{
    ImGui_ImplWGPU_Data* bd = ImGui_ImplWGPU_GetBackendData();
    WGPUBufferDescriptor ub_desc = new()
    {
//      nullptr,
        label = "Dear ImGui Uniform buffer"u8,
#if IMGUI_IMPL_WEBGPU_BACKEND_DAWN
        WGPU_STRLEN,
#endif
        usage = WGPUBufferUsage.CopyDst | WGPUBufferUsage.Uniform,
        size = MEMALIGN(sizeof(Uniforms), 16),
        mappedAtCreation = false
    };
    bd->renderResources.Uniforms = wgpuDeviceCreateBuffer(bd->wgpuDevice, &ub_desc);
}

static bool ImGui_ImplWGPU_CreateDeviceObjects()
{
    ImGui_ImplWGPU_Data* bd = ImGui_ImplWGPU_GetBackendData();
    if (bd->wgpuDevice.GetHandle() == 0)
        return false;
    if (bd->pipelineState.GetHandle() != 0)
        ImGui_ImplWGPU_InvalidateDeviceObjects();

    // Create render pipeline
    WGPURenderPipelineDescriptor graphics_pipeline_desc = {};
    graphics_pipeline_desc.primitive.topology = WGPUPrimitiveTopology.TriangleList;
    graphics_pipeline_desc.primitive.stripIndexFormat = WGPUIndexFormat.Undefined;
    graphics_pipeline_desc.primitive.frontFace = WGPUFrontFace.CW;
    graphics_pipeline_desc.primitive.cullMode = WGPUCullMode.None;
    graphics_pipeline_desc.multisample = bd->initInfo.PipelineMultisampleState;

    // Bind group layouts
    Span<WGPUBindGroupLayoutEntry> common_bg_layout_entries = stackalloc WGPUBindGroupLayoutEntry[2];
    common_bg_layout_entries[0].binding = 0;
    common_bg_layout_entries[0].visibility = WGPUShaderStage.Vertex | WGPUShaderStage.Fragment;
    common_bg_layout_entries[0].buffer.type = WGPUBufferBindingType.Uniform;
    common_bg_layout_entries[1].binding = 1;
    common_bg_layout_entries[1].visibility = WGPUShaderStage.Fragment;
    common_bg_layout_entries[1].sampler.type = WGPUSamplerBindingType.Filtering;

    Span<WGPUBindGroupLayoutEntry> image_bg_layout_entries = stackalloc WGPUBindGroupLayoutEntry[1];
    image_bg_layout_entries[0].binding = 0;
    image_bg_layout_entries[0].visibility = WGPUShaderStage.Fragment;
    image_bg_layout_entries[0].texture.sampleType = WGPUTextureSampleType.Float;
    image_bg_layout_entries[0].texture.viewDimension = WGPUTextureViewDimension._2D;

    WGPUBindGroupLayoutDescriptor common_bg_layout_desc = new();
//  common_bg_layout_desc.entryCount = 2;
    common_bg_layout_desc.entries = common_bg_layout_entries;

    WGPUBindGroupLayoutDescriptor image_bg_layout_desc = {};
//  image_bg_layout_desc.entryCount = 1;
    image_bg_layout_desc.entries = image_bg_layout_entries;

    Span<WGPUBindGroupLayout> bg_layouts = stackalloc WGPUBindGroupLayout[2];
    bg_layouts[0] = wgpuDeviceCreateBindGroupLayout(bd->wgpuDevice, &common_bg_layout_desc);
    bg_layouts[1] = wgpuDeviceCreateBindGroupLayout(bd->wgpuDevice, &image_bg_layout_desc);

    WGPUPipelineLayoutDescriptor layout_desc = new();
//  layout_desc.bindGroupLayoutCount = 2;
    layout_desc.bindGroupLayouts = bg_layouts;
    graphics_pipeline_desc.layout = wgpuDeviceCreatePipelineLayout(bd->wgpuDevice, &layout_desc);

    // Create the vertex shader
    WGPUProgrammableStageDescriptor vertex_shader_desc = ImGui_ImplWGPU_CreateShaderModule(__shader_vert_wgsl);
    graphics_pipeline_desc.vertex.module = vertex_shader_desc.module;
    graphics_pipeline_desc.vertex.entryPoint = vertex_shader_desc.entryPoint;

    // Vertex input configuration
    Span<WGPUVertexAttribute> attribute_desc = [
        new() { format = WGPUVertexFormat.Float32x2, offset = (uint64_t)offsetof(ImDrawVert, pos), shaderLocation = 0 },
        new() { format = WGPUVertexFormat.Float32x2, offset = (uint64_t)offsetof(ImDrawVert, uv),  shaderLocation = 1 },
        new() { format = WGPUVertexFormat.Unorm8x4,  offset = (uint64_t)offsetof(ImDrawVert, col), shaderLocation = 2 }
    ];

    Span<WGPUVertexBufferLayout> buffer_layouts = stackalloc WGPUVertexBufferLayout[1];
    buffer_layouts[0].arrayStride = (ulong)sizeof(ImDrawVert);
    buffer_layouts[0].stepMode = WGPUVertexStepMode.Vertex;
//  buffer_layouts[0].attributeCount = 3;
    buffer_layouts[0].attributes = attribute_desc;

//  graphics_pipeline_desc.vertex.bufferCount = 1;
    graphics_pipeline_desc.vertex.buffers = buffer_layouts;

    // Create the pixel shader
    WGPUProgrammableStageDescriptor pixel_shader_desc = ImGui_ImplWGPU_CreateShaderModule(__shader_frag_wgsl);

    // Create the blending setup
    WGPUBlendState blend_state = new();
    blend_state.alpha.operation = WGPUBlendOperation.Add;
    blend_state.alpha.srcFactor = WGPUBlendFactor.One;
    blend_state.alpha.dstFactor = WGPUBlendFactor.OneMinusSrcAlpha;
    blend_state.color.operation = WGPUBlendOperation.Add;
    blend_state.color.srcFactor = WGPUBlendFactor.SrcAlpha;
    blend_state.color.dstFactor = WGPUBlendFactor.OneMinusSrcAlpha;

    Span<WGPUColorTargetState> color_state = stackalloc WGPUColorTargetState[1];
    color_state[0].format = bd->renderTargetFormat;
    color_state[0].blend = blend_state;
    color_state[0].writeMask = WGPUColorWriteMask.All;

    WGPUFragmentState fragment_state = new();
    fragment_state.module = pixel_shader_desc.module;
    fragment_state.entryPoint = pixel_shader_desc.entryPoint;
//  fragment_state._targetCount = 1;
    fragment_state.targets = color_state;

    graphics_pipeline_desc.fragment = fragment_state;

    // Create depth-stencil State
    WGPUDepthStencilState depth_stencil_state = new();
    depth_stencil_state.format = bd->depthStencilFormat;
#if IMGUI_IMPL_WEBGPU_BACKEND_DAWN
    depth_stencil_state.depthWriteEnabled = false;
#else
    depth_stencil_state.depthWriteEnabled = false;
#endif
    depth_stencil_state.depthCompare = WGPUCompareFunction.Always;
    depth_stencil_state.stencilFront.compare = WGPUCompareFunction.Always;
    depth_stencil_state.stencilFront.failOp = WGPUStencilOperation.Keep;
    depth_stencil_state.stencilFront.depthFailOp = WGPUStencilOperation.Keep;
    depth_stencil_state.stencilFront.passOp = WGPUStencilOperation.Keep;
    depth_stencil_state.stencilBack.compare = WGPUCompareFunction.Always;
    depth_stencil_state.stencilBack.failOp = WGPUStencilOperation.Keep;
    depth_stencil_state.stencilBack.depthFailOp = WGPUStencilOperation.Keep;
    depth_stencil_state.stencilBack.passOp = WGPUStencilOperation.Keep;

    // Configure disabled depth-stencil state
    graphics_pipeline_desc.depthStencil = (bd->depthStencilFormat == WGPUTextureFormat.Undefined) ? null :  depth_stencil_state;

    bd->pipelineState = wgpuDeviceCreateRenderPipeline(bd->wgpuDevice, &graphics_pipeline_desc);

    ImGui_ImplWGPU_CreateFontsTexture();
    ImGui_ImplWGPU_CreateUniformBuffer();

    // Create resource bind group
    Span<WGPUBindGroupEntry> common_bg_entries = stackalloc WGPUBindGroupEntry[]
    {
        { nullptr, 0, bd->renderResources.Uniforms, 0, MEMALIGN(sizeof(Uniforms), 16), 0, 0 },
        { nullptr, 1, 0, 0, 0, bd->renderResources.Sampler, 0 },
    };

    WGPUBindGroupDescriptor common_bg_descriptor = new();
    common_bg_descriptor.layout = bg_layouts[0];
//  common_bg_descriptor.entryCount = sizeof(common_bg_entries) / sizeof(WGPUBindGroupEntry);
    common_bg_descriptor.entries = common_bg_entries;
    bd->renderResources.CommonBindGroup = wgpuDeviceCreateBindGroup(bd->wgpuDevice, &common_bg_descriptor);

    WGPUBindGroup image_bind_group = ImGui_ImplWGPU_CreateImageBindGroup(bg_layouts[1], bd->renderResources.FontTextureView);
    bd->renderResources.ImageBindGroup = image_bind_group;
    bd->renderResources.ImageBindGroupLayout = bg_layouts[1];
    bd->renderResources.ImageBindGroups.SetVoidPtr(ImHashData(&bd->renderResources.FontTextureView, sizeof(ImTextureID)), image_bind_group);

    SafeRelease(vertex_shader_desc.module);
    SafeRelease(pixel_shader_desc.module);
    SafeRelease(graphics_pipeline_desc.layout);
    SafeRelease(bg_layouts[0]);

    return true;
}

static void ImGui_ImplWGPU_InvalidateDeviceObjects()
{
    ImGui_ImplWGPU_Data* bd = ImGui_ImplWGPU_GetBackendData();
    if (!bd->wgpuDevice)
        return;

    SafeRelease(bd->pipelineState);
    SafeRelease(bd->renderResources);

    var io = ImGui.GetIO();
    io.Fonts->SetTexID(0); // We copied g_pFontTextureView to io.Fonts->TexID so let's clear that as well.

    for (uint i = 0; i < bd->numFramesInFlight; i++)
        SafeRelease(bd->pFrameResources[i]);
}

static bool ImGui_ImplWGPU_Init(ImGui_ImplWGPU_InitInfo* init_info)
{
    var io = ImGui.GetIO();
    IMGUI_CHECKVERSION();
    IM_ASSERT(io.BackendRendererUserData == 0 && "Already initialized a renderer backend!");

    // Setup backend capabilities flags
    ImGui_ImplWGPU_Data* bd = IM_NEW(ImGui_ImplWGPU_Data)();
    io.BackendRendererUserData = (void*)bd;
#if defined(__EMSCRIPTEN__)
    io.BackendRendererName = "imgui_impl_webgpu_emscripten";
#elif defined(IMGUI_IMPL_WEBGPU_BACKEND_DAWN)
    io.BackendRendererName = "imgui_impl_webgpu_dawn";
#elif defined(IMGUI_IMPL_WEBGPU_BACKEND_WGPU)
    io.BackendRendererName = "imgui_impl_webgpu_wgpu";
#else
    io.BackendRendererName = "imgui_impl_webgpu";
#endif
    io.BackendFlags |= ImGuiBackendFlags.RendererHasVtxOffset;  // We can honor the ImDrawCmd::VtxOffset field, allowing for large meshes.

    bd->initInfo = *init_info;
    bd->wgpuDevice = init_info->Device;
    bd->defaultQueue = wgpuDeviceGetQueue(bd->wgpuDevice);
    bd->renderTargetFormat = init_info->RenderTargetFormat;
    bd->depthStencilFormat = init_info->DepthStencilFormat;
    bd->numFramesInFlight = (uint)init_info->NumFramesInFlight;
    bd->frameIndex = uint.MaxValue;

    bd->renderResources.FontTexture = default;
    bd->renderResources.FontTextureView = default;
    bd->renderResources.Sampler = default;
    bd->renderResources.Uniforms = default;
    bd->renderResources.CommonBindGroup = default;
    bd->renderResources.ImageBindGroups.Data.reserve(100);
    bd->renderResources.ImageBindGroup = default;
    bd->renderResources.ImageBindGroupLayout = default;

    // Create buffers with a default size (they will later be grown as needed)
    bd->pFrameResources = new FrameResources[bd->numFramesInFlight];
    for (uint i = 0; i < bd->numFramesInFlight; i++)
    {
        FrameResources* fr = &bd->pFrameResources[i];
        fr->IndexBuffer = default;
        fr->VertexBuffer = default;
        fr->IndexBufferHost = null;
        fr->VertexBufferHost = null;
        fr->IndexBufferSize = 10000;
        fr->VertexBufferSize = 5000;
    }

    return true;
}

static void ImGui_ImplWGPU_Shutdown()
{
    ImGui_ImplWGPU_Data* bd = ImGui_ImplWGPU_GetBackendData();
    IM_ASSERT(bd != nullptr && "No renderer backend to shutdown, or already shutdown?");
    var io = ImGui.GetIO();

    ImGui_ImplWGPU_InvalidateDeviceObjects();
    delete[] bd->pFrameResources;
    bd->pFrameResources = null;
    wgpuQueueRelease(bd->defaultQueue);
    bd->wgpuDevice = default;
    bd->numFramesInFlight = 0;
    bd->frameIndex = uint.MaxValue;

    io.BackendRendererName = nullptr;
    io.BackendRendererUserData = nullptr;
    io.BackendFlags &= ~ImGuiBackendFlags.RendererHasVtxOffset;
    IM_DELETE(bd);
}

static void ImGui_ImplWGPU_NewFrame()
{
    ImGui_ImplWGPU_Data* bd = ImGui_ImplWGPU_GetBackendData();
    if (!bd->pipelineState)
        ImGui_ImplWGPU_CreateDeviceObjects();
}

//-----------------------------------------------------------------------------

// #endif // #ifndef IMGUI_DISABLE
}

