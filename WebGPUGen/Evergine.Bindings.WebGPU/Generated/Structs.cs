using System.Diagnostics;
using System.Runtime.InteropServices;
using static System.Diagnostics.DebuggerBrowsableState;
using Browse = System.Diagnostics.DebuggerBrowsableAttribute;

// ReSharper disable RedundantUnsafeContext;
// ReSharper disable InconsistentNaming;
namespace Evergine.Bindings.WebGPU;
[StructLayout(LayoutKind.Sequential)]
public unsafe struct WGPUAdapterInfo
{
	[Browse(Never)] internal    WGPUChainedStructOut*   _nextInChain;
	[Browse(Never)] internal    char*                   _vendor;
	[Browse(Never)] internal    char*                   _architecture;
	[Browse(Never)] internal    char*                   _device;
	[Browse(Never)] internal    char*                   _description;
	                public      WGPUBackendType         backendType;
	                public      WGPUAdapterType         adapterType;
	                public      uint                    vendorID;
	                public      uint                    deviceID;
	// --- properties
	public Utf8 vendor {
		get => ApiUtils.GetUtf8(_vendor);
		set => ApiUtils.SetUtf8(value, out this._vendor);
	}
	public Utf8 architecture {
		get => ApiUtils.GetUtf8(_architecture);
		set => ApiUtils.SetUtf8(value, out this._architecture);
	}
	public Utf8 device {
		get => ApiUtils.GetUtf8(_device);
		set => ApiUtils.SetUtf8(value, out this._device);
	}
	public Utf8 description {
		get => ApiUtils.GetUtf8(_description);
		set => ApiUtils.SetUtf8(value, out this._description);
	}

	[Conditional("VALIDATE")]
	public void Validate() {
		AllocValidator.ValidatePtr(_vendor);
		AllocValidator.ValidatePtr(_architecture);
		AllocValidator.ValidatePtr(_device);
		AllocValidator.ValidatePtr(_description);
	}
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct WGPUBindGroupEntry
{
	[Browse(Never)] internal    WGPUChainedStruct*      _nextInChain;
	                public      uint                    binding;
	                public      WGPUBuffer              buffer;
	                public      ulong                   offset;
	                public      ulong                   size;
	                public      WGPUSampler             sampler;
	                public      WGPUTextureView         textureView;

	[Conditional("VALIDATE")]
	public void Validate() {
        ObjectTracker.ValidateHandleParam(buffer);
        ObjectTracker.ValidateHandleParam(sampler);
        ObjectTracker.ValidateHandleParam(textureView);
	}
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct WGPUBlendComponent
{
	                public      WGPUBlendOperation      operation;
	                public      WGPUBlendFactor         srcFactor;
	                public      WGPUBlendFactor         dstFactor;
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct WGPUBufferBindingLayout
{
	[Browse(Never)] internal    WGPUChainedStruct*      _nextInChain;
	                public      WGPUBufferBindingType   type;
	                public      WGPUBool                hasDynamicOffset;
	                public      ulong                   minBindingSize;
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct WGPUBufferDescriptor
{
	[Browse(Never)] internal    WGPUChainedStruct*      _nextInChain;
	[Browse(Never)] internal    char*                   _label;
	                public      WGPUBufferUsage         usage;
	                public      ulong                   size;
	                public      WGPUBool                mappedAtCreation;
	// --- properties
	public Utf8 label {
		get => ApiUtils.GetUtf8(_label);
		set => ApiUtils.SetUtf8(value, out this._label);
	}

	[Conditional("VALIDATE")]
	public void Validate() {
		AllocValidator.ValidatePtr(_label);
	}
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct WGPUColor
{
	                public      double                  r;
	                public      double                  g;
	                public      double                  b;
	                public      double                  a;
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct WGPUCommandBufferDescriptor
{
	[Browse(Never)] internal    WGPUChainedStruct*      _nextInChain;
	[Browse(Never)] internal    char*                   _label;
	// --- properties
	public Utf8 label {
		get => ApiUtils.GetUtf8(_label);
		set => ApiUtils.SetUtf8(value, out this._label);
	}

	[Conditional("VALIDATE")]
	public void Validate() {
		AllocValidator.ValidatePtr(_label);
	}
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct WGPUCommandEncoderDescriptor
{
	[Browse(Never)] internal    WGPUChainedStruct*      _nextInChain;
	[Browse(Never)] internal    char*                   _label;
	// --- properties
	public Utf8 label {
		get => ApiUtils.GetUtf8(_label);
		set => ApiUtils.SetUtf8(value, out this._label);
	}

	[Conditional("VALIDATE")]
	public void Validate() {
		AllocValidator.ValidatePtr(_label);
	}
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct WGPUCompilationMessage
{
	[Browse(Never)] internal    WGPUChainedStruct*      _nextInChain;
	[Browse(Never)] internal    char*                   _message;
	                public      WGPUCompilationMessageType type;
	                public      ulong                   lineNum;
	                public      ulong                   linePos;
	                public      ulong                   offset;
	                public      ulong                   length;
	                public      ulong                   utf16LinePos;
	                public      ulong                   utf16Offset;
	                public      ulong                   utf16Length;
	// --- properties
	public Utf8 message {
		get => ApiUtils.GetUtf8(_message);
		set => ApiUtils.SetUtf8(value, out this._message);
	}

	[Conditional("VALIDATE")]
	public void Validate() {
		AllocValidator.ValidatePtr(_message);
	}
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct WGPUComputePassTimestampWrites
{
	                public      WGPUQuerySet            querySet;
	                public      uint                    beginningOfPassWriteIndex;
	                public      uint                    endOfPassWriteIndex;

	[Conditional("VALIDATE")]
	public void Validate() {
        ObjectTracker.ValidateHandleParam(querySet);
	}
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct WGPUConstantEntry
{
	[Browse(Never)] internal    WGPUChainedStruct*      _nextInChain;
	[Browse(Never)] internal    char*                   _key;
	                public      double                  value;
	// --- properties
	public Utf8 key {
		get => ApiUtils.GetUtf8(_key);
		set => ApiUtils.SetUtf8(value, out this._key);
	}

	[Conditional("VALIDATE")]
	public void Validate() {
		AllocValidator.ValidatePtr(_key);
	}
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct WGPUExtent3D
{
	                public      uint                    width;
	                public      uint                    height;
	                public      uint                    depthOrArrayLayers;
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct WGPUInstanceDescriptor
{
	[Browse(Never)] internal    WGPUChainedStruct*      _nextInChain;
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct WGPULimits
{
	                public      uint                    maxTextureDimension1D;
	                public      uint                    maxTextureDimension2D;
	                public      uint                    maxTextureDimension3D;
	                public      uint                    maxTextureArrayLayers;
	                public      uint                    maxBindGroups;
	                public      uint                    maxBindGroupsPlusVertexBuffers;
	                public      uint                    maxBindingsPerBindGroup;
	                public      uint                    maxDynamicUniformBuffersPerPipelineLayout;
	                public      uint                    maxDynamicStorageBuffersPerPipelineLayout;
	                public      uint                    maxSampledTexturesPerShaderStage;
	                public      uint                    maxSamplersPerShaderStage;
	                public      uint                    maxStorageBuffersPerShaderStage;
	                public      uint                    maxStorageTexturesPerShaderStage;
	                public      uint                    maxUniformBuffersPerShaderStage;
	                public      ulong                   maxUniformBufferBindingSize;
	                public      ulong                   maxStorageBufferBindingSize;
	                public      uint                    minUniformBufferOffsetAlignment;
	                public      uint                    minStorageBufferOffsetAlignment;
	                public      uint                    maxVertexBuffers;
	                public      ulong                   maxBufferSize;
	                public      uint                    maxVertexAttributes;
	                public      uint                    maxVertexBufferArrayStride;
	                public      uint                    maxInterStageShaderComponents;
	                public      uint                    maxInterStageShaderVariables;
	                public      uint                    maxColorAttachments;
	                public      uint                    maxColorAttachmentBytesPerSample;
	                public      uint                    maxComputeWorkgroupStorageSize;
	                public      uint                    maxComputeInvocationsPerWorkgroup;
	                public      uint                    maxComputeWorkgroupSizeX;
	                public      uint                    maxComputeWorkgroupSizeY;
	                public      uint                    maxComputeWorkgroupSizeZ;
	                public      uint                    maxComputeWorkgroupsPerDimension;
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct WGPUMultisampleState
{
	[Browse(Never)] internal    WGPUChainedStruct*      _nextInChain;
	                public      uint                    count;
	                public      uint                    mask;
	                public      WGPUBool                alphaToCoverageEnabled;
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct WGPUOrigin3D
{
	                public      uint                    x;
	                public      uint                    y;
	                public      uint                    z;
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct WGPUPipelineLayoutDescriptor
{
	[Browse(Never)] internal    WGPUChainedStruct*      _nextInChain;
	[Browse(Never)] internal    char*                   _label;
	[Browse(Never)] internal    ulong                   _bindGroupLayoutCount;
	[Browse(Never)] internal    WGPUBindGroupLayout*    _bindGroupLayouts;
	// --- properties
	public Utf8 label {
		get => ApiUtils.GetUtf8(_label);
		set => ApiUtils.SetUtf8(value, out this._label);
	}
	public Span<WGPUBindGroupLayout> bindGroupLayouts {
		get => ApiUtils.GetArr(_bindGroupLayouts, _bindGroupLayoutCount);
		set => ApiUtils.SetArr(value, out _bindGroupLayouts, out _bindGroupLayoutCount);
	}

	[Conditional("VALIDATE")]
	public void Validate() {
		AllocValidator.ValidatePtr(_label);
		AllocValidator.ValidatePtr(_bindGroupLayouts);
	}
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct WGPUPrimitiveDepthClipControl
{
	                public      WGPUChainedStruct       chain;
	                public      WGPUBool                unclippedDepth;
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct WGPUPrimitiveState
{
	[Browse(Never)] internal    WGPUChainedStruct*      _nextInChain;
	                public      WGPUPrimitiveTopology   topology;
	                public      WGPUIndexFormat         stripIndexFormat;
	                public      WGPUFrontFace           frontFace;
	                public      WGPUCullMode            cullMode;
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct WGPUQuerySetDescriptor
{
	[Browse(Never)] internal    WGPUChainedStruct*      _nextInChain;
	[Browse(Never)] internal    char*                   _label;
	                public      WGPUQueryType           type;
	                public      uint                    count;
	// --- properties
	public Utf8 label {
		get => ApiUtils.GetUtf8(_label);
		set => ApiUtils.SetUtf8(value, out this._label);
	}

	[Conditional("VALIDATE")]
	public void Validate() {
		AllocValidator.ValidatePtr(_label);
	}
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct WGPUQueueDescriptor
{
	[Browse(Never)] internal    WGPUChainedStruct*      _nextInChain;
	[Browse(Never)] internal    char*                   _label;
	// --- properties
	public Utf8 label {
		get => ApiUtils.GetUtf8(_label);
		set => ApiUtils.SetUtf8(value, out this._label);
	}

	[Conditional("VALIDATE")]
	public void Validate() {
		AllocValidator.ValidatePtr(_label);
	}
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct WGPURenderBundleDescriptor
{
	[Browse(Never)] internal    WGPUChainedStruct*      _nextInChain;
	[Browse(Never)] internal    char*                   _label;
	// --- properties
	public Utf8 label {
		get => ApiUtils.GetUtf8(_label);
		set => ApiUtils.SetUtf8(value, out this._label);
	}

	[Conditional("VALIDATE")]
	public void Validate() {
		AllocValidator.ValidatePtr(_label);
	}
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct WGPURenderBundleEncoderDescriptor
{
	[Browse(Never)] internal    WGPUChainedStruct*      _nextInChain;
	[Browse(Never)] internal    char*                   _label;
	[Browse(Never)] internal    ulong                   _colorFormatCount;
	[Browse(Never)] internal    WGPUTextureFormat*      _colorFormats;
	                public      WGPUTextureFormat       depthStencilFormat;
	                public      uint                    sampleCount;
	                public      WGPUBool                depthReadOnly;
	                public      WGPUBool                stencilReadOnly;
	// --- properties
	public Utf8 label {
		get => ApiUtils.GetUtf8(_label);
		set => ApiUtils.SetUtf8(value, out this._label);
	}
	public Span<WGPUTextureFormat> colorFormats {
		get => ApiUtils.GetArr(_colorFormats, _colorFormatCount);
		set => ApiUtils.SetArr(value, out _colorFormats, out _colorFormatCount);
	}

	[Conditional("VALIDATE")]
	public void Validate() {
		AllocValidator.ValidatePtr(_label);
		AllocValidator.ValidatePtr(_colorFormats);
	}
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct WGPURenderPassDepthStencilAttachment
{
	                public      WGPUTextureView         view;
	                public      WGPULoadOp              depthLoadOp;
	                public      WGPUStoreOp             depthStoreOp;
	                public      float                   depthClearValue;
	                public      WGPUBool                depthReadOnly;
	                public      WGPULoadOp              stencilLoadOp;
	                public      WGPUStoreOp             stencilStoreOp;
	                public      uint                    stencilClearValue;
	                public      WGPUBool                stencilReadOnly;

	[Conditional("VALIDATE")]
	public void Validate() {
        ObjectTracker.ValidateHandleParam(view);
	}
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct WGPURenderPassDescriptorMaxDrawCount
{
	                public      WGPUChainedStruct       chain;
	                public      ulong                   maxDrawCount;
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct WGPURenderPassTimestampWrites
{
	                public      WGPUQuerySet            querySet;
	                public      uint                    beginningOfPassWriteIndex;
	                public      uint                    endOfPassWriteIndex;

	[Conditional("VALIDATE")]
	public void Validate() {
        ObjectTracker.ValidateHandleParam(querySet);
	}
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct WGPURequestAdapterOptions
{
	[Browse(Never)] internal    WGPUChainedStruct*      _nextInChain;
	                public      WGPUSurface             compatibleSurface;
	                public      WGPUPowerPreference     powerPreference;
	                public      WGPUBackendType         backendType;
	                public      WGPUBool                forceFallbackAdapter;

	[Conditional("VALIDATE")]
	public void Validate() {
        ObjectTracker.ValidateHandleParam(compatibleSurface);
	}
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct WGPUSamplerBindingLayout
{
	[Browse(Never)] internal    WGPUChainedStruct*      _nextInChain;
	                public      WGPUSamplerBindingType  type;
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct WGPUSamplerDescriptor
{
	[Browse(Never)] internal    WGPUChainedStruct*      _nextInChain;
	[Browse(Never)] internal    char*                   _label;
	                public      WGPUAddressMode         addressModeU;
	                public      WGPUAddressMode         addressModeV;
	                public      WGPUAddressMode         addressModeW;
	                public      WGPUFilterMode          magFilter;
	                public      WGPUFilterMode          minFilter;
	                public      WGPUMipmapFilterMode    mipmapFilter;
	                public      float                   lodMinClamp;
	                public      float                   lodMaxClamp;
	                public      WGPUCompareFunction     compare;
	                public      ushort                  maxAnisotropy;
	// --- properties
	public Utf8 label {
		get => ApiUtils.GetUtf8(_label);
		set => ApiUtils.SetUtf8(value, out this._label);
	}

	[Conditional("VALIDATE")]
	public void Validate() {
		AllocValidator.ValidatePtr(_label);
	}
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct WGPUShaderModuleCompilationHint
{
	[Browse(Never)] internal    WGPUChainedStruct*      _nextInChain;
	[Browse(Never)] internal    char*                   _entryPoint;
	                public      WGPUPipelineLayout      layout;
	// --- properties
	public Utf8 entryPoint {
		get => ApiUtils.GetUtf8(_entryPoint);
		set => ApiUtils.SetUtf8(value, out this._entryPoint);
	}

	[Conditional("VALIDATE")]
	public void Validate() {
		AllocValidator.ValidatePtr(_entryPoint);
        ObjectTracker.ValidateHandleParam(layout);
	}
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct WGPUShaderModuleSPIRVDescriptor
{
	                public      WGPUChainedStruct       chain;
	                public      uint                    codeSize;
	[Browse(Never)] internal    uint*                   _code;
	// --- properties
	public uint? code {
		get => ApiUtils.GetOpt(_code);
		set => ApiUtils.SetOpt(out _code, value);
	}

	[Conditional("VALIDATE")]
	public void Validate() {
		AllocValidator.ValidatePtr(_code);
	}
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct WGPUShaderModuleWGSLDescriptor
{
	                public      WGPUChainedStruct       chain;
	[Browse(Never)] internal    char*                   _code;
	// --- properties
	public Utf8 code {
		get => ApiUtils.GetUtf8(_code);
		set => ApiUtils.SetUtf8(value, out this._code);
	}

	[Conditional("VALIDATE")]
	public void Validate() {
		AllocValidator.ValidatePtr(_code);
	}
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct WGPUStencilFaceState
{
	                public      WGPUCompareFunction     compare;
	                public      WGPUStencilOperation    failOp;
	                public      WGPUStencilOperation    depthFailOp;
	                public      WGPUStencilOperation    passOp;
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct WGPUStorageTextureBindingLayout
{
	[Browse(Never)] internal    WGPUChainedStruct*      _nextInChain;
	                public      WGPUStorageTextureAccess access;
	                public      WGPUTextureFormat       format;
	                public      WGPUTextureViewDimension viewDimension;
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct WGPUSurfaceCapabilities
{
	[Browse(Never)] internal    WGPUChainedStructOut*   _nextInChain;
	                public      WGPUTextureUsage        usages;
	[Browse(Never)] internal    ulong                   _formatCount;
	[Browse(Never)] internal    WGPUTextureFormat*      _formats;
	[Browse(Never)] internal    ulong                   _presentModeCount;
	[Browse(Never)] internal    WGPUPresentMode*        _presentModes;
	[Browse(Never)] internal    ulong                   _alphaModeCount;
	[Browse(Never)] internal    WGPUCompositeAlphaMode* _alphaModes;
	// --- properties
	public Span<WGPUTextureFormat> formats {
		get => ApiUtils.GetArr(_formats, _formatCount);
		set => ApiUtils.SetArr(value, out _formats, out _formatCount);
	}
	public Span<WGPUPresentMode> presentModes {
		get => ApiUtils.GetArr(_presentModes, _presentModeCount);
		set => ApiUtils.SetArr(value, out _presentModes, out _presentModeCount);
	}
	public Span<WGPUCompositeAlphaMode> alphaModes {
		get => ApiUtils.GetArr(_alphaModes, _alphaModeCount);
		set => ApiUtils.SetArr(value, out _alphaModes, out _alphaModeCount);
	}

	[Conditional("VALIDATE")]
	public void Validate() {
		AllocValidator.ValidatePtr(_formats);
		AllocValidator.ValidatePtr(_presentModes);
		AllocValidator.ValidatePtr(_alphaModes);
	}
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct WGPUSurfaceConfiguration
{
	[Browse(Never)] internal    WGPUChainedStruct*      _nextInChain;
	                public      WGPUDevice              device;
	                public      WGPUTextureFormat       format;
	                public      WGPUTextureUsage        usage;
	[Browse(Never)] internal    ulong                   _viewFormatCount;
	[Browse(Never)] internal    WGPUTextureFormat*      _viewFormats;
	                public      WGPUCompositeAlphaMode  alphaMode;
	                public      uint                    width;
	                public      uint                    height;
	                public      WGPUPresentMode         presentMode;
	// --- properties
	public Span<WGPUTextureFormat> viewFormats {
		get => ApiUtils.GetArr(_viewFormats, _viewFormatCount);
		set => ApiUtils.SetArr(value, out _viewFormats, out _viewFormatCount);
	}

	[Conditional("VALIDATE")]
	public void Validate() {
        ObjectTracker.ValidateHandleParam(device);
		AllocValidator.ValidatePtr(_viewFormats);
	}
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct WGPUSurfaceDescriptor
{
	[Browse(Never)] internal    WGPUChainedStruct*      _nextInChain;
	[Browse(Never)] internal    char*                   _label;
	// --- properties
	public Utf8 label {
		get => ApiUtils.GetUtf8(_label);
		set => ApiUtils.SetUtf8(value, out this._label);
	}

	[Conditional("VALIDATE")]
	public void Validate() {
		AllocValidator.ValidatePtr(_label);
	}
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct WGPUSurfaceDescriptorFromAndroidNativeWindow
{
	                public      WGPUChainedStruct       chain;
	                public      void*                   window;
	// --- properties
	public IntPtr Window {
		get => new IntPtr(window);
		set => window = (void*)value;
	}
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct WGPUSurfaceDescriptorFromCanvasHTMLSelector
{
	                public      WGPUChainedStruct       chain;
	[Browse(Never)] internal    char*                   _selector;
	// --- properties
	public Utf8 selector {
		get => ApiUtils.GetUtf8(_selector);
		set => ApiUtils.SetUtf8(value, out this._selector);
	}

	[Conditional("VALIDATE")]
	public void Validate() {
		AllocValidator.ValidatePtr(_selector);
	}
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct WGPUSurfaceDescriptorFromMetalLayer
{
	                public      WGPUChainedStruct       chain;
	                public      void*                   layer;
	// --- properties
	public IntPtr Layer {
		get => new IntPtr(layer);
		set => layer = (void*)value;
	}
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct WGPUSurfaceDescriptorFromWaylandSurface
{
	                public      WGPUChainedStruct       chain;
	                public      void*                   display;
	                public      void*                   surface;
	// --- properties
	public IntPtr Display {
		get => new IntPtr(display);
		set => display = (void*)value;
	}
	public IntPtr Surface {
		get => new IntPtr(surface);
		set => surface = (void*)value;
	}
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct WGPUSurfaceDescriptorFromWindowsHWND
{
	                public      WGPUChainedStruct       chain;
	                public      void*                   hinstance;
	                public      void*                   hwnd;
	// --- properties
	public IntPtr Hinstance {
		get => new IntPtr(hinstance);
		set => hinstance = (void*)value;
	}
	public IntPtr Hwnd {
		get => new IntPtr(hwnd);
		set => hwnd = (void*)value;
	}
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct WGPUSurfaceDescriptorFromXcbWindow
{
	                public      WGPUChainedStruct       chain;
	                public      void*                   connection;
	                public      uint                    window;
	// --- properties
	public IntPtr Connection {
		get => new IntPtr(connection);
		set => connection = (void*)value;
	}
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct WGPUSurfaceDescriptorFromXlibWindow
{
	                public      WGPUChainedStruct       chain;
	                public      void*                   display;
	                public      ulong                   window;
	// --- properties
	public IntPtr Display {
		get => new IntPtr(display);
		set => display = (void*)value;
	}
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct WGPUSurfaceTexture
{
	                public      WGPUTexture             texture;
	                public      WGPUBool                suboptimal;
	                public      WGPUSurfaceGetCurrentTextureStatus status;

	[Conditional("VALIDATE")]
	public void Validate() {
        ObjectTracker.ValidateHandleParam(texture);
	}
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct WGPUTextureBindingLayout
{
	[Browse(Never)] internal    WGPUChainedStruct*      _nextInChain;
	                public      WGPUTextureSampleType   sampleType;
	                public      WGPUTextureViewDimension viewDimension;
	                public      WGPUBool                multisampled;
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct WGPUTextureDataLayout
{
	[Browse(Never)] internal    WGPUChainedStruct*      _nextInChain;
	                public      ulong                   offset;
	                public      uint                    bytesPerRow;
	                public      uint                    rowsPerImage;
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct WGPUTextureViewDescriptor
{
	[Browse(Never)] internal    WGPUChainedStruct*      _nextInChain;
	[Browse(Never)] internal    char*                   _label;
	                public      WGPUTextureFormat       format;
	                public      WGPUTextureViewDimension dimension;
	                public      uint                    baseMipLevel;
	                public      uint                    mipLevelCount;
	                public      uint                    baseArrayLayer;
	                public      uint                    arrayLayerCount;
	                public      WGPUTextureAspect       aspect;
	// --- properties
	public Utf8 label {
		get => ApiUtils.GetUtf8(_label);
		set => ApiUtils.SetUtf8(value, out this._label);
	}

	[Conditional("VALIDATE")]
	public void Validate() {
		AllocValidator.ValidatePtr(_label);
	}
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct WGPUUncapturedErrorCallbackInfo
{
	[Browse(Never)] internal    WGPUChainedStruct*      _nextInChain;
	                public      delegate* unmanaged<WGPUErrorType, char*, void*, void> callback;
	                public      void*                   userdata;
	// --- properties
	public IntPtr Userdata {
		get => new IntPtr(userdata);
		set => userdata = (void*)value;
	}
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct WGPUVertexAttribute
{
	                public      WGPUVertexFormat        format;
	                public      ulong                   offset;
	                public      uint                    shaderLocation;
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct WGPUBindGroupDescriptor
{
	[Browse(Never)] internal    WGPUChainedStruct*      _nextInChain;
	[Browse(Never)] internal    char*                   _label;
	                public      WGPUBindGroupLayout     layout;
	[Browse(Never)] internal    ulong                   _entryCount;
	[Browse(Never)] internal    WGPUBindGroupEntry*     _entries;
	// --- properties
	public Utf8 label {
		get => ApiUtils.GetUtf8(_label);
		set => ApiUtils.SetUtf8(value, out this._label);
	}
	public Span<WGPUBindGroupEntry> entries {
		get => ApiUtils.GetArr(_entries, _entryCount);
		set => ApiUtils.SetArr(value, out _entries, out _entryCount);
	}

	[Conditional("VALIDATE")]
	public void Validate() {
		AllocValidator.ValidatePtr(_label);
        ObjectTracker.ValidateHandleParam(layout);
		AllocValidator.ValidatePtr(_entries);
		foreach (var element in entries) {
		    element.Validate();
		}
	}
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct WGPUBindGroupLayoutEntry
{
	[Browse(Never)] internal    WGPUChainedStruct*      _nextInChain;
	                public      uint                    binding;
	                public      WGPUShaderStage         visibility;
	                public      WGPUBufferBindingLayout buffer;
	                public      WGPUSamplerBindingLayout sampler;
	                public      WGPUTextureBindingLayout texture;
	                public      WGPUStorageTextureBindingLayout storageTexture;
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct WGPUBlendState
{
	                public      WGPUBlendComponent      color;
	                public      WGPUBlendComponent      alpha;
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct WGPUCompilationInfo
{
	[Browse(Never)] internal    WGPUChainedStruct*      _nextInChain;
	[Browse(Never)] internal    ulong                   _messageCount;
	[Browse(Never)] internal    WGPUCompilationMessage* _messages;
	// --- properties
	public Span<WGPUCompilationMessage> messages {
		get => ApiUtils.GetArr(_messages, _messageCount);
		set => ApiUtils.SetArr(value, out _messages, out _messageCount);
	}

	[Conditional("VALIDATE")]
	public void Validate() {
		AllocValidator.ValidatePtr(_messages);
		foreach (var element in messages) {
		    element.Validate();
		}
	}
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct WGPUComputePassDescriptor
{
	[Browse(Never)] internal    WGPUChainedStruct*      _nextInChain;
	[Browse(Never)] internal    char*                   _label;
	[Browse(Never)] internal    WGPUComputePassTimestampWrites* _timestampWrites;
	// --- properties
	public Utf8 label {
		get => ApiUtils.GetUtf8(_label);
		set => ApiUtils.SetUtf8(value, out this._label);
	}
	public WGPUComputePassTimestampWrites? timestampWrites {
		get => ApiUtils.GetOpt(_timestampWrites);
		set => ApiUtils.SetOpt(out _timestampWrites, value);
	}

	[Conditional("VALIDATE")]
	public void Validate() {
		AllocValidator.ValidatePtr(_label);
		AllocValidator.ValidatePtr(_timestampWrites);
		if (_timestampWrites != null) {
		    _timestampWrites->Validate();
		}
	}
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct WGPUDepthStencilState
{
	[Browse(Never)] internal    WGPUChainedStruct*      _nextInChain;
	                public      WGPUTextureFormat       format;
	                public      WGPUBool                depthWriteEnabled;
	                public      WGPUCompareFunction     depthCompare;
	                public      WGPUStencilFaceState    stencilFront;
	                public      WGPUStencilFaceState    stencilBack;
	                public      uint                    stencilReadMask;
	                public      uint                    stencilWriteMask;
	                public      int                     depthBias;
	                public      float                   depthBiasSlopeScale;
	                public      float                   depthBiasClamp;
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct WGPUImageCopyBuffer
{
	[Browse(Never)] internal    WGPUChainedStruct*      _nextInChain;
	                public      WGPUTextureDataLayout   layout;
	                public      WGPUBuffer              buffer;

	[Conditional("VALIDATE")]
	public void Validate() {
        ObjectTracker.ValidateHandleParam(buffer);
	}
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct WGPUImageCopyTexture
{
	[Browse(Never)] internal    WGPUChainedStruct*      _nextInChain;
	                public      WGPUTexture             texture;
	                public      uint                    mipLevel;
	                public      WGPUOrigin3D            origin;
	                public      WGPUTextureAspect       aspect;

	[Conditional("VALIDATE")]
	public void Validate() {
        ObjectTracker.ValidateHandleParam(texture);
	}
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct WGPUProgrammableStageDescriptor
{
	[Browse(Never)] internal    WGPUChainedStruct*      _nextInChain;
	                public      WGPUShaderModule        module;
	[Browse(Never)] internal    char*                   _entryPoint;
	[Browse(Never)] internal    ulong                   _constantCount;
	[Browse(Never)] internal    WGPUConstantEntry*      _constants;
	// --- properties
	public Utf8 entryPoint {
		get => ApiUtils.GetUtf8(_entryPoint);
		set => ApiUtils.SetUtf8(value, out this._entryPoint);
	}
	public Span<WGPUConstantEntry> constants {
		get => ApiUtils.GetArr(_constants, _constantCount);
		set => ApiUtils.SetArr(value, out _constants, out _constantCount);
	}

	[Conditional("VALIDATE")]
	public void Validate() {
        ObjectTracker.ValidateHandleParam(module);
		AllocValidator.ValidatePtr(_entryPoint);
		AllocValidator.ValidatePtr(_constants);
		foreach (var element in constants) {
		    element.Validate();
		}
	}
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct WGPURenderPassColorAttachment
{
	[Browse(Never)] internal    WGPUChainedStruct*      _nextInChain;
	                public      WGPUTextureView         view;
	                public      uint                    depthSlice;
	                public      WGPUTextureView         resolveTarget;
	                public      WGPULoadOp              loadOp;
	                public      WGPUStoreOp             storeOp;
	                public      WGPUColor               clearValue;

	[Conditional("VALIDATE")]
	public void Validate() {
        ObjectTracker.ValidateHandleParam(view);
        ObjectTracker.ValidateHandleParam(resolveTarget);
	}
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct WGPURequiredLimits
{
	[Browse(Never)] internal    WGPUChainedStruct*      _nextInChain;
	                public      WGPULimits              limits;
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct WGPUShaderModuleDescriptor
{
	[Browse(Never)] internal    WGPUChainedStruct*      _nextInChain;
	[Browse(Never)] internal    char*                   _label;
	[Browse(Never)] internal    ulong                   _hintCount;
	[Browse(Never)] internal    WGPUShaderModuleCompilationHint* _hints;
	// --- properties
	public Utf8 label {
		get => ApiUtils.GetUtf8(_label);
		set => ApiUtils.SetUtf8(value, out this._label);
	}
	public Span<WGPUShaderModuleCompilationHint> hints {
		get => ApiUtils.GetArr(_hints, _hintCount);
		set => ApiUtils.SetArr(value, out _hints, out _hintCount);
	}

	[Conditional("VALIDATE")]
	public void Validate() {
		AllocValidator.ValidatePtr(_label);
		AllocValidator.ValidatePtr(_hints);
		foreach (var element in hints) {
		    element.Validate();
		}
	}
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct WGPUSupportedLimits
{
	[Browse(Never)] internal    WGPUChainedStructOut*   _nextInChain;
	                public      WGPULimits              limits;
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct WGPUTextureDescriptor
{
	[Browse(Never)] internal    WGPUChainedStruct*      _nextInChain;
	[Browse(Never)] internal    char*                   _label;
	                public      WGPUTextureUsage        usage;
	                public      WGPUTextureDimension    dimension;
	                public      WGPUExtent3D            size;
	                public      WGPUTextureFormat       format;
	                public      uint                    mipLevelCount;
	                public      uint                    sampleCount;
	[Browse(Never)] internal    ulong                   _viewFormatCount;
	[Browse(Never)] internal    WGPUTextureFormat*      _viewFormats;
	// --- properties
	public Utf8 label {
		get => ApiUtils.GetUtf8(_label);
		set => ApiUtils.SetUtf8(value, out this._label);
	}
	public Span<WGPUTextureFormat> viewFormats {
		get => ApiUtils.GetArr(_viewFormats, _viewFormatCount);
		set => ApiUtils.SetArr(value, out _viewFormats, out _viewFormatCount);
	}

	[Conditional("VALIDATE")]
	public void Validate() {
		AllocValidator.ValidatePtr(_label);
		AllocValidator.ValidatePtr(_viewFormats);
	}
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct WGPUVertexBufferLayout
{
	                public      ulong                   arrayStride;
	                public      WGPUVertexStepMode      stepMode;
	[Browse(Never)] internal    ulong                   _attributeCount;
	[Browse(Never)] internal    WGPUVertexAttribute*    _attributes;
	// --- properties
	public Span<WGPUVertexAttribute> attributes {
		get => ApiUtils.GetArr(_attributes, _attributeCount);
		set => ApiUtils.SetArr(value, out _attributes, out _attributeCount);
	}

	[Conditional("VALIDATE")]
	public void Validate() {
		AllocValidator.ValidatePtr(_attributes);
	}
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct WGPUBindGroupLayoutDescriptor
{
	[Browse(Never)] internal    WGPUChainedStruct*      _nextInChain;
	[Browse(Never)] internal    char*                   _label;
	[Browse(Never)] internal    ulong                   _entryCount;
	[Browse(Never)] internal    WGPUBindGroupLayoutEntry* _entries;
	// --- properties
	public Utf8 label {
		get => ApiUtils.GetUtf8(_label);
		set => ApiUtils.SetUtf8(value, out this._label);
	}
	public Span<WGPUBindGroupLayoutEntry> entries {
		get => ApiUtils.GetArr(_entries, _entryCount);
		set => ApiUtils.SetArr(value, out _entries, out _entryCount);
	}

	[Conditional("VALIDATE")]
	public void Validate() {
		AllocValidator.ValidatePtr(_label);
		AllocValidator.ValidatePtr(_entries);
	}
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct WGPUColorTargetState
{
	[Browse(Never)] internal    WGPUChainedStruct*      _nextInChain;
	                public      WGPUTextureFormat       format;
	[Browse(Never)] internal    WGPUBlendState*         _blend;
	                public      WGPUColorWriteMask      writeMask;
	// --- properties
	public WGPUBlendState? blend {
		get => ApiUtils.GetOpt(_blend);
		set => ApiUtils.SetOpt(out _blend, value);
	}

	[Conditional("VALIDATE")]
	public void Validate() {
		AllocValidator.ValidatePtr(_blend);
	}
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct WGPUComputePipelineDescriptor
{
	[Browse(Never)] internal    WGPUChainedStruct*      _nextInChain;
	[Browse(Never)] internal    char*                   _label;
	                public      WGPUPipelineLayout      layout;
	                public      WGPUProgrammableStageDescriptor compute;
	// --- properties
	public Utf8 label {
		get => ApiUtils.GetUtf8(_label);
		set => ApiUtils.SetUtf8(value, out this._label);
	}

	[Conditional("VALIDATE")]
	public void Validate() {
		AllocValidator.ValidatePtr(_label);
        ObjectTracker.ValidateHandleParam(layout);
		compute.Validate();
	}
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct WGPUDeviceDescriptor
{
	[Browse(Never)] internal    WGPUChainedStruct*      _nextInChain;
	[Browse(Never)] internal    char*                   _label;
	[Browse(Never)] internal    ulong                   _requiredFeatureCount;
	[Browse(Never)] internal    WGPUFeatureName*        _requiredFeatures;
	[Browse(Never)] internal    WGPURequiredLimits*     _requiredLimits;
	                public      WGPUQueueDescriptor     defaultQueue;
	                public      delegate* unmanaged<WGPUDeviceLostReason, char*, void*, void> deviceLostCallback;
	                public      void*                   deviceLostUserdata;
	                public      WGPUUncapturedErrorCallbackInfo uncapturedErrorCallbackInfo;
	// --- properties
	public Utf8 label {
		get => ApiUtils.GetUtf8(_label);
		set => ApiUtils.SetUtf8(value, out this._label);
	}
	public Span<WGPUFeatureName> requiredFeatures {
		get => ApiUtils.GetArr(_requiredFeatures, _requiredFeatureCount);
		set => ApiUtils.SetArr(value, out _requiredFeatures, out _requiredFeatureCount);
	}
	public WGPURequiredLimits? requiredLimits {
		get => ApiUtils.GetOpt(_requiredLimits);
		set => ApiUtils.SetOpt(out _requiredLimits, value);
	}
	public IntPtr DeviceLostUserdata {
		get => new IntPtr(deviceLostUserdata);
		set => deviceLostUserdata = (void*)value;
	}

	[Conditional("VALIDATE")]
	public void Validate() {
		AllocValidator.ValidatePtr(_label);
		AllocValidator.ValidatePtr(_requiredFeatures);
		AllocValidator.ValidatePtr(_requiredLimits);
		defaultQueue.Validate();
	}
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct WGPURenderPassDescriptor
{
	[Browse(Never)] internal    WGPUChainedStruct*      _nextInChain;
	[Browse(Never)] internal    char*                   _label;
	[Browse(Never)] internal    ulong                   _colorAttachmentCount;
	[Browse(Never)] internal    WGPURenderPassColorAttachment* _colorAttachments;
	[Browse(Never)] internal    WGPURenderPassDepthStencilAttachment* _depthStencilAttachment;
	                public      WGPUQuerySet            occlusionQuerySet;
	[Browse(Never)] internal    WGPURenderPassTimestampWrites* _timestampWrites;
	// --- properties
	public Utf8 label {
		get => ApiUtils.GetUtf8(_label);
		set => ApiUtils.SetUtf8(value, out this._label);
	}
	public Span<WGPURenderPassColorAttachment> colorAttachments {
		get => ApiUtils.GetArr(_colorAttachments, _colorAttachmentCount);
		set => ApiUtils.SetArr(value, out _colorAttachments, out _colorAttachmentCount);
	}
	public WGPURenderPassDepthStencilAttachment? depthStencilAttachment {
		get => ApiUtils.GetOpt(_depthStencilAttachment);
		set => ApiUtils.SetOpt(out _depthStencilAttachment, value);
	}
	public WGPURenderPassTimestampWrites? timestampWrites {
		get => ApiUtils.GetOpt(_timestampWrites);
		set => ApiUtils.SetOpt(out _timestampWrites, value);
	}

	[Conditional("VALIDATE")]
	public void Validate() {
		AllocValidator.ValidatePtr(_label);
		AllocValidator.ValidatePtr(_colorAttachments);
		foreach (var element in colorAttachments) {
		    element.Validate();
		}
		AllocValidator.ValidatePtr(_depthStencilAttachment);
		if (_depthStencilAttachment != null) {
		    _depthStencilAttachment->Validate();
		}
        ObjectTracker.ValidateHandleParam(occlusionQuerySet);
		AllocValidator.ValidatePtr(_timestampWrites);
		if (_timestampWrites != null) {
		    _timestampWrites->Validate();
		}
	}
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct WGPUVertexState
{
	[Browse(Never)] internal    WGPUChainedStruct*      _nextInChain;
	                public      WGPUShaderModule        module;
	[Browse(Never)] internal    char*                   _entryPoint;
	[Browse(Never)] internal    ulong                   _constantCount;
	[Browse(Never)] internal    WGPUConstantEntry*      _constants;
	[Browse(Never)] internal    ulong                   _bufferCount;
	[Browse(Never)] internal    WGPUVertexBufferLayout* _buffers;
	// --- properties
	public Utf8 entryPoint {
		get => ApiUtils.GetUtf8(_entryPoint);
		set => ApiUtils.SetUtf8(value, out this._entryPoint);
	}
	public Span<WGPUConstantEntry> constants {
		get => ApiUtils.GetArr(_constants, _constantCount);
		set => ApiUtils.SetArr(value, out _constants, out _constantCount);
	}
	public Span<WGPUVertexBufferLayout> buffers {
		get => ApiUtils.GetArr(_buffers, _bufferCount);
		set => ApiUtils.SetArr(value, out _buffers, out _bufferCount);
	}

	[Conditional("VALIDATE")]
	public void Validate() {
        ObjectTracker.ValidateHandleParam(module);
		AllocValidator.ValidatePtr(_entryPoint);
		AllocValidator.ValidatePtr(_constants);
		foreach (var element in constants) {
		    element.Validate();
		}
		AllocValidator.ValidatePtr(_buffers);
		foreach (var element in buffers) {
		    element.Validate();
		}
	}
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct WGPUFragmentState
{
	[Browse(Never)] internal    WGPUChainedStruct*      _nextInChain;
	                public      WGPUShaderModule        module;
	[Browse(Never)] internal    char*                   _entryPoint;
	[Browse(Never)] internal    ulong                   _constantCount;
	[Browse(Never)] internal    WGPUConstantEntry*      _constants;
	[Browse(Never)] internal    ulong                   _targetCount;
	[Browse(Never)] internal    WGPUColorTargetState*   _targets;
	// --- properties
	public Utf8 entryPoint {
		get => ApiUtils.GetUtf8(_entryPoint);
		set => ApiUtils.SetUtf8(value, out this._entryPoint);
	}
	public Span<WGPUConstantEntry> constants {
		get => ApiUtils.GetArr(_constants, _constantCount);
		set => ApiUtils.SetArr(value, out _constants, out _constantCount);
	}
	public Span<WGPUColorTargetState> targets {
		get => ApiUtils.GetArr(_targets, _targetCount);
		set => ApiUtils.SetArr(value, out _targets, out _targetCount);
	}

	[Conditional("VALIDATE")]
	public void Validate() {
        ObjectTracker.ValidateHandleParam(module);
		AllocValidator.ValidatePtr(_entryPoint);
		AllocValidator.ValidatePtr(_constants);
		foreach (var element in constants) {
		    element.Validate();
		}
		AllocValidator.ValidatePtr(_targets);
		foreach (var element in targets) {
		    element.Validate();
		}
	}
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct WGPURenderPipelineDescriptor
{
	[Browse(Never)] internal    WGPUChainedStruct*      _nextInChain;
	[Browse(Never)] internal    char*                   _label;
	                public      WGPUPipelineLayout      layout;
	                public      WGPUVertexState         vertex;
	                public      WGPUPrimitiveState      primitive;
	[Browse(Never)] internal    WGPUDepthStencilState*  _depthStencil;
	                public      WGPUMultisampleState    multisample;
	[Browse(Never)] internal    WGPUFragmentState*      _fragment;
	// --- properties
	public Utf8 label {
		get => ApiUtils.GetUtf8(_label);
		set => ApiUtils.SetUtf8(value, out this._label);
	}
	public WGPUDepthStencilState? depthStencil {
		get => ApiUtils.GetOpt(_depthStencil);
		set => ApiUtils.SetOpt(out _depthStencil, value);
	}
	public WGPUFragmentState? fragment {
		get => ApiUtils.GetOpt(_fragment);
		set => ApiUtils.SetOpt(out _fragment, value);
	}

	[Conditional("VALIDATE")]
	public void Validate() {
		AllocValidator.ValidatePtr(_label);
        ObjectTracker.ValidateHandleParam(layout);
		vertex.Validate();
		AllocValidator.ValidatePtr(_depthStencil);
		AllocValidator.ValidatePtr(_fragment);
		if (_fragment != null) {
		    _fragment->Validate();
		}
	}
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct WGPUChainedStruct
{
	[Browse(Never)] internal    WGPUChainedStruct*      _next;
	                public      WGPUSType               sType;
	// --- properties
	public WGPUChainedStruct? next {
		get => ApiUtils.GetOpt(_next);
		set => ApiUtils.SetOpt(out _next, value);
	}
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct WGPUChainedStructOut
{
	[Browse(Never)] internal    WGPUChainedStructOut*   _next;
	                public      WGPUSType               sType;
	// --- properties
	public WGPUChainedStructOut? next {
		get => ApiUtils.GetOpt(_next);
		set => ApiUtils.SetOpt(out _next, value);
	}
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct WGPUInstanceExtras
{
	                public      WGPUChainedStruct       chain;
	                public      WGPUInstanceBackend     backends;
	                public      WGPUInstance            flags;
	                public      WGPUDx12Compiler        dx12ShaderCompiler;
	                public      WGPUGles3MinorVersion   gles3MinorVersion;
	[Browse(Never)] internal    char*                   _dxilPath;
	[Browse(Never)] internal    char*                   _dxcPath;
	// --- properties
	public Utf8 dxilPath {
		get => ApiUtils.GetUtf8(_dxilPath);
		set => ApiUtils.SetUtf8(value, out this._dxilPath);
	}
	public Utf8 dxcPath {
		get => ApiUtils.GetUtf8(_dxcPath);
		set => ApiUtils.SetUtf8(value, out this._dxcPath);
	}

	[Conditional("VALIDATE")]
	public void Validate() {
		AllocValidator.ValidatePtr(_dxilPath);
		AllocValidator.ValidatePtr(_dxcPath);
	}
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct WGPUDeviceExtras
{
	                public      WGPUChainedStruct       chain;
	[Browse(Never)] internal    char*                   _tracePath;
	// --- properties
	public Utf8 tracePath {
		get => ApiUtils.GetUtf8(_tracePath);
		set => ApiUtils.SetUtf8(value, out this._tracePath);
	}

	[Conditional("VALIDATE")]
	public void Validate() {
		AllocValidator.ValidatePtr(_tracePath);
	}
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct WGPUNativeLimits
{
	                public      uint                    maxPushConstantSize;
	                public      uint                    maxNonSamplerBindings;
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct WGPURequiredLimitsExtras
{
	                public      WGPUChainedStruct       chain;
	                public      WGPUNativeLimits        limits;
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct WGPUSupportedLimitsExtras
{
	                public      WGPUChainedStructOut    chain;
	                public      WGPUNativeLimits        limits;
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct WGPUPushConstantRange
{
	                public      WGPUShaderStage         stages;
	                public      uint                    start;
	                public      uint                    end;
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct WGPUPipelineLayoutExtras
{
	                public      WGPUChainedStruct       chain;
	[Browse(Never)] internal    ulong                   _pushConstantRangeCount;
	[Browse(Never)] internal    WGPUPushConstantRange*  _pushConstantRanges;
	// --- properties
	public Span<WGPUPushConstantRange> pushConstantRanges {
		get => ApiUtils.GetArr(_pushConstantRanges, _pushConstantRangeCount);
		set => ApiUtils.SetArr(value, out _pushConstantRanges, out _pushConstantRangeCount);
	}

	[Conditional("VALIDATE")]
	public void Validate() {
		AllocValidator.ValidatePtr(_pushConstantRanges);
	}
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct WGPUWrappedSubmissionIndex
{
	                public      WGPUQueue               queue;
	                public      ulong                   submissionIndex;

	[Conditional("VALIDATE")]
	public void Validate() {
        ObjectTracker.ValidateHandleParam(queue);
	}
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct WGPUShaderDefine
{
	[Browse(Never)] internal    char*                   _name;
	[Browse(Never)] internal    char*                   _value;
	// --- properties
	public Utf8 name {
		get => ApiUtils.GetUtf8(_name);
		set => ApiUtils.SetUtf8(value, out this._name);
	}
	public Utf8 value {
		get => ApiUtils.GetUtf8(_value);
		set => ApiUtils.SetUtf8(value, out this._value);
	}

	[Conditional("VALIDATE")]
	public void Validate() {
		AllocValidator.ValidatePtr(_name);
		AllocValidator.ValidatePtr(_value);
	}
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct WGPUShaderModuleGLSLDescriptor
{
	                public      WGPUChainedStruct       chain;
	                public      WGPUShaderStage         stage;
	[Browse(Never)] internal    char*                   _code;
	[Browse(Never)] internal    uint                    _defineCount;
	[Browse(Never)] internal    WGPUShaderDefine*       _defines;
	// --- properties
	public Utf8 code {
		get => ApiUtils.GetUtf8(_code);
		set => ApiUtils.SetUtf8(value, out this._code);
	}
	public Span<WGPUShaderDefine> defines {
		get => ApiUtils.GetArr(_defines, _defineCount);
		set => ApiUtils.SetArr(value, out _defines, out _defineCount);
	}

	[Conditional("VALIDATE")]
	public void Validate() {
		AllocValidator.ValidatePtr(_code);
		AllocValidator.ValidatePtr(_defines);
		foreach (var element in defines) {
		    element.Validate();
		}
	}
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct WGPURegistryReport
{
	                public      ulong                   numAllocated;
	                public      ulong                   numKeptFromUser;
	                public      ulong                   numReleasedFromUser;
	                public      ulong                   numError;
	                public      ulong                   elementSize;
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct WGPUHubReport
{
	                public      WGPURegistryReport      adapters;
	                public      WGPURegistryReport      devices;
	                public      WGPURegistryReport      queues;
	                public      WGPURegistryReport      pipelineLayouts;
	                public      WGPURegistryReport      shaderModules;
	                public      WGPURegistryReport      bindGroupLayouts;
	                public      WGPURegistryReport      bindGroups;
	                public      WGPURegistryReport      commandBuffers;
	                public      WGPURegistryReport      renderBundles;
	                public      WGPURegistryReport      renderPipelines;
	                public      WGPURegistryReport      computePipelines;
	                public      WGPURegistryReport      querySets;
	                public      WGPURegistryReport      buffers;
	                public      WGPURegistryReport      textures;
	                public      WGPURegistryReport      textureViews;
	                public      WGPURegistryReport      samplers;
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct WGPUGlobalReport
{
	                public      WGPURegistryReport      surfaces;
	                public      WGPUBackendType         backendType;
	                public      WGPUHubReport           vulkan;
	                public      WGPUHubReport           metal;
	                public      WGPUHubReport           dx12;
	                public      WGPUHubReport           gl;
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct WGPUInstanceEnumerateAdapterOptions
{
	[Browse(Never)] internal    WGPUChainedStruct*      _nextInChain;
	                public      WGPUInstanceBackend     backends;
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct WGPUBindGroupEntryExtras
{
	                public      WGPUChainedStruct       chain;
	[Browse(Never)] internal    WGPUBuffer*             _buffers;
	[Browse(Never)] internal    ulong                   _bufferCount;
	[Browse(Never)] internal    WGPUSampler*            _samplers;
	[Browse(Never)] internal    ulong                   _samplerCount;
	[Browse(Never)] internal    WGPUTextureView*        _textureViews;
	[Browse(Never)] internal    ulong                   _textureViewCount;
	// --- properties
	public Span<WGPUBuffer> buffers {
		get => ApiUtils.GetArr(_buffers, _bufferCount);
		set => ApiUtils.SetArr(value, out _buffers, out _bufferCount);
	}
	public Span<WGPUSampler> samplers {
		get => ApiUtils.GetArr(_samplers, _samplerCount);
		set => ApiUtils.SetArr(value, out _samplers, out _samplerCount);
	}
	public Span<WGPUTextureView> textureViews {
		get => ApiUtils.GetArr(_textureViews, _textureViewCount);
		set => ApiUtils.SetArr(value, out _textureViews, out _textureViewCount);
	}

	[Conditional("VALIDATE")]
	public void Validate() {
		AllocValidator.ValidatePtr(_buffers);
		AllocValidator.ValidatePtr(_samplers);
		AllocValidator.ValidatePtr(_textureViews);
	}
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct WGPUBindGroupLayoutEntryExtras
{
	                public      WGPUChainedStruct       chain;
	                public      uint                    count;
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct WGPUQuerySetDescriptorExtras
{
	                public      WGPUChainedStruct       chain;
	[Browse(Never)] internal    WGPUPipelineStatisticName* _pipelineStatistics;
	[Browse(Never)] internal    ulong                   _pipelineStatisticCount;
	// --- properties
	public Span<WGPUPipelineStatisticName> pipelineStatistics {
		get => ApiUtils.GetArr(_pipelineStatistics, _pipelineStatisticCount);
		set => ApiUtils.SetArr(value, out _pipelineStatistics, out _pipelineStatisticCount);
	}

	[Conditional("VALIDATE")]
	public void Validate() {
		AllocValidator.ValidatePtr(_pipelineStatistics);
	}
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct WGPUSurfaceConfigurationExtras
{
	                public      WGPUChainedStruct       chain;
	                public      uint                    desiredMaximumFrameLatency;
}

