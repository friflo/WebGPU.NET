using System.Diagnostics;
using System.Runtime.InteropServices;
using static Evergine.Bindings.WebGPU.ApiUtils;
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
	/** enum   */   public      WGPUBackendType         backendType;
	/** enum   */   public      WGPUAdapterType         adapterType;
	                public      uint                    vendorID;
	                public      uint                    deviceID;

	public Utf8 vendor {
		get => GetUtf8(_vendor);
		set => SetUtf8(value, out this._vendor);
	}
	public Utf8 architecture {
		get => GetUtf8(_architecture);
		set => SetUtf8(value, out this._architecture);
	}
	public Utf8 device {
		get => GetUtf8(_device);
		set => SetUtf8(value, out this._device);
	}
	public Utf8 description {
		get => GetUtf8(_description);
		set => SetUtf8(value, out this._description);
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
	/** handle */   public      WGPUBuffer              buffer;
	                public      ulong                   offset;
	                public      ulong                   size;
	/** handle */   public      WGPUSampler             sampler;
	/** handle */   public      WGPUTextureView         textureView;

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
    public WGPUBlendComponent() {}
	/** enum   */   public      WGPUBlendOperation      operation            = WGPUBlendOperation.Add;
	/** enum   */   public      WGPUBlendFactor         srcFactor            = WGPUBlendFactor.One;
	/** enum   */   public      WGPUBlendFactor         dstFactor            = WGPUBlendFactor.Zero;
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct WGPUBufferBindingLayout
{
    public WGPUBufferBindingLayout() {}
	[Browse(Never)] internal    WGPUChainedStruct*      _nextInChain;
	/** enum   */   public      WGPUBufferBindingType   type                 = WGPUBufferBindingType.Uniform;
	                public      WGPUBool                hasDynamicOffset;
	                public      ulong                   minBindingSize;
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct WGPUBufferDescriptor
{
	[Browse(Never)] internal    WGPUChainedStruct*      _nextInChain;
	[Browse(Never)] internal    char*                   _label;
	/** flags  */   public      WGPUBufferUsage         usage;
	                public      ulong                   size;
	                public      WGPUBool                mappedAtCreation;

	public Utf8 label {
		get => GetUtf8(_label);
		set => SetUtf8(value, out this._label);
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

	public Utf8 label {
		get => GetUtf8(_label);
		set => SetUtf8(value, out this._label);
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

	public Utf8 label {
		get => GetUtf8(_label);
		set => SetUtf8(value, out this._label);
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
	/** enum   */   public      WGPUCompilationMessageType type;
	                public      ulong                   lineNum;
	                public      ulong                   linePos;
	                public      ulong                   offset;
	                public      ulong                   length;
	                public      ulong                   utf16LinePos;
	                public      ulong                   utf16Offset;
	                public      ulong                   utf16Length;

	public Utf8 message {
		get => GetUtf8(_message);
		set => SetUtf8(value, out this._message);
	}

	[Conditional("VALIDATE")]
	public void Validate() {
		AllocValidator.ValidatePtr(_message);
	}
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct WGPUComputePassTimestampWrites
{
	/** handle */   public      WGPUQuerySet            querySet;
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

	public Utf8 key {
		get => GetUtf8(_key);
		set => SetUtf8(value, out this._key);
	}

	[Conditional("VALIDATE")]
	public void Validate() {
		AllocValidator.ValidatePtr(_key);
	}
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct WGPUExtent3D
{
    public WGPUExtent3D() {}
	                public      uint                    width;
	                public      uint                    height               = 1;
	                public      uint                    depthOrArrayLayers   = 1;
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
    public WGPUMultisampleState() {}
	[Browse(Never)] internal    WGPUChainedStruct*      _nextInChain;
	                public      uint                    count                = 1;
	                public      uint                    mask                 = 0xFFFFFFFF;
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

	public Utf8 label {
		get => GetUtf8(_label);
		set => SetUtf8(value, out this._label);
	}
	public Span<WGPUBindGroupLayout> bindGroupLayouts {
		get => GetArr(_bindGroupLayouts, _bindGroupLayoutCount);
		set => SetArr(value, out _bindGroupLayouts, out _bindGroupLayoutCount);
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
    public WGPUPrimitiveState() {}
	[Browse(Never)] internal    WGPUChainedStruct*      _nextInChain;
	/** enum   */   public      WGPUPrimitiveTopology   topology             = WGPUPrimitiveTopology.TriangleList;
	/** enum   */   public      WGPUIndexFormat         stripIndexFormat;
	/** enum   */   public      WGPUFrontFace           frontFace            = WGPUFrontFace.CCW;
	/** enum   */   public      WGPUCullMode            cullMode             = WGPUCullMode.None;
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct WGPUQuerySetDescriptor
{
	[Browse(Never)] internal    WGPUChainedStruct*      _nextInChain;
	[Browse(Never)] internal    char*                   _label;
	/** enum   */   public      WGPUQueryType           type;
	                public      uint                    count;

	public Utf8 label {
		get => GetUtf8(_label);
		set => SetUtf8(value, out this._label);
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

	public Utf8 label {
		get => GetUtf8(_label);
		set => SetUtf8(value, out this._label);
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

	public Utf8 label {
		get => GetUtf8(_label);
		set => SetUtf8(value, out this._label);
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
	/** enum   */   public      WGPUTextureFormat       depthStencilFormat;
	                public      uint                    sampleCount;
	                public      WGPUBool                depthReadOnly;
	                public      WGPUBool                stencilReadOnly;

	public Utf8 label {
		get => GetUtf8(_label);
		set => SetUtf8(value, out this._label);
	}
	public Span<WGPUTextureFormat> colorFormats {
		get => GetArr(_colorFormats, _colorFormatCount);
		set => SetArr(value, out _colorFormats, out _colorFormatCount);
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
	/** handle */   public      WGPUTextureView         view;
	/** enum   */   public      WGPULoadOp              depthLoadOp;
	/** enum   */   public      WGPUStoreOp             depthStoreOp;
	                public      float                   depthClearValue;
	                public      WGPUBool                depthReadOnly;
	/** enum   */   public      WGPULoadOp              stencilLoadOp;
	/** enum   */   public      WGPUStoreOp             stencilStoreOp;
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
	/** handle */   public      WGPUQuerySet            querySet;
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
	/** handle */   public      WGPUSurface             compatibleSurface;
	/** enum   */   public      WGPUPowerPreference     powerPreference;
	/** enum   */   public      WGPUBackendType         backendType;
	                public      WGPUBool                forceFallbackAdapter;

	[Conditional("VALIDATE")]
	public void Validate() {
        ObjectTracker.ValidateHandleParam(compatibleSurface);
	}
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct WGPUSamplerBindingLayout
{
    public WGPUSamplerBindingLayout() {}
	[Browse(Never)] internal    WGPUChainedStruct*      _nextInChain;
	/** enum   */   public      WGPUSamplerBindingType  type                 = WGPUSamplerBindingType.Filtering;
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct WGPUSamplerDescriptor
{
    public WGPUSamplerDescriptor() {}
	[Browse(Never)] internal    WGPUChainedStruct*      _nextInChain;
	[Browse(Never)] internal    char*                   _label;
	/** enum   */   public      WGPUAddressMode         addressModeU         = WGPUAddressMode.ClampToEdge;
	/** enum   */   public      WGPUAddressMode         addressModeV         = WGPUAddressMode.ClampToEdge;
	/** enum   */   public      WGPUAddressMode         addressModeW         = WGPUAddressMode.ClampToEdge;
	/** enum   */   public      WGPUFilterMode          magFilter            = WGPUFilterMode.Nearest;
	/** enum   */   public      WGPUFilterMode          minFilter            = WGPUFilterMode.Nearest;
	/** enum   */   public      WGPUMipmapFilterMode    mipmapFilter;
	                public      float                   lodMinClamp;
	                public      float                   lodMaxClamp          = 32;
	/** enum   */   public      WGPUCompareFunction     compare;
	                public      ushort                  maxAnisotropy        = 1;

	public Utf8 label {
		get => GetUtf8(_label);
		set => SetUtf8(value, out this._label);
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
	/** handle */   public      WGPUPipelineLayout      layout;

	public Utf8 entryPoint {
		get => GetUtf8(_entryPoint);
		set => SetUtf8(value, out this._entryPoint);
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

	public uint? code {
		get => GetOpt(_code);
		set => SetOpt(out _code, value);
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

	public Utf8 code {
		get => GetUtf8(_code);
		set => SetUtf8(value, out this._code);
	}

	[Conditional("VALIDATE")]
	public void Validate() {
		AllocValidator.ValidatePtr(_code);
	}
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct WGPUStencilFaceState
{
    public WGPUStencilFaceState() {}
	/** enum   */   public      WGPUCompareFunction     compare              = WGPUCompareFunction.Always;
	/** enum   */   public      WGPUStencilOperation    failOp               = WGPUStencilOperation.Keep;
	/** enum   */   public      WGPUStencilOperation    depthFailOp          = WGPUStencilOperation.Keep;
	/** enum   */   public      WGPUStencilOperation    passOp               = WGPUStencilOperation.Keep;
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct WGPUStorageTextureBindingLayout
{
    public WGPUStorageTextureBindingLayout() {}
	[Browse(Never)] internal    WGPUChainedStruct*      _nextInChain;
	/** enum   */   public      WGPUStorageTextureAccess access               = WGPUStorageTextureAccess.WriteOnly;
	/** enum   */   public      WGPUTextureFormat       format;
	/** enum   */   public      WGPUTextureViewDimension viewDimension        = WGPUTextureViewDimension._2D;
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct WGPUSurfaceCapabilities
{
	[Browse(Never)] internal    WGPUChainedStructOut*   _nextInChain;
	/** flags  */   public      WGPUTextureUsage        usages;
	[Browse(Never)] internal    ulong                   _formatCount;
	[Browse(Never)] internal    WGPUTextureFormat*      _formats;
	[Browse(Never)] internal    ulong                   _presentModeCount;
	[Browse(Never)] internal    WGPUPresentMode*        _presentModes;
	[Browse(Never)] internal    ulong                   _alphaModeCount;
	[Browse(Never)] internal    WGPUCompositeAlphaMode* _alphaModes;

	public Span<WGPUTextureFormat> formats {
		get => GetArr(_formats, _formatCount);
		set => SetArr(value, out _formats, out _formatCount);
	}
	public Span<WGPUPresentMode> presentModes {
		get => GetArr(_presentModes, _presentModeCount);
		set => SetArr(value, out _presentModes, out _presentModeCount);
	}
	public Span<WGPUCompositeAlphaMode> alphaModes {
		get => GetArr(_alphaModes, _alphaModeCount);
		set => SetArr(value, out _alphaModes, out _alphaModeCount);
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
	/** handle */   public      WGPUDevice              device;
	/** enum   */   public      WGPUTextureFormat       format;
	/** flags  */   public      WGPUTextureUsage        usage;
	[Browse(Never)] internal    ulong                   _viewFormatCount;
	[Browse(Never)] internal    WGPUTextureFormat*      _viewFormats;
	/** enum   */   public      WGPUCompositeAlphaMode  alphaMode;
	                public      uint                    width;
	                public      uint                    height;
	/** enum   */   public      WGPUPresentMode         presentMode;

	public Span<WGPUTextureFormat> viewFormats {
		get => GetArr(_viewFormats, _viewFormatCount);
		set => SetArr(value, out _viewFormats, out _viewFormatCount);
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

	public Utf8 label {
		get => GetUtf8(_label);
		set => SetUtf8(value, out this._label);
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

	public Utf8 selector {
		get => GetUtf8(_selector);
		set => SetUtf8(value, out this._selector);
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

	public IntPtr Display {
		get => new IntPtr(display);
		set => display = (void*)value;
	}
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct WGPUSurfaceTexture
{
	/** handle */   public      WGPUTexture             texture;
	                public      WGPUBool                suboptimal;
	/** enum   */   public      WGPUSurfaceGetCurrentTextureStatus status;

	[Conditional("VALIDATE")]
	public void Validate() {
        ObjectTracker.ValidateHandleParam(texture);
	}
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct WGPUTextureBindingLayout
{
    public WGPUTextureBindingLayout() {}
	[Browse(Never)] internal    WGPUChainedStruct*      _nextInChain;
	/** enum   */   public      WGPUTextureSampleType   sampleType           = WGPUTextureSampleType.Float;
	/** enum   */   public      WGPUTextureViewDimension viewDimension        = WGPUTextureViewDimension._2D;
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
    public WGPUTextureViewDescriptor() {}
	[Browse(Never)] internal    WGPUChainedStruct*      _nextInChain;
	[Browse(Never)] internal    char*                   _label;
	/** enum   */   public      WGPUTextureFormat       format;
	/** enum   */   public      WGPUTextureViewDimension dimension;
	                public      uint                    baseMipLevel;
	                public      uint                    mipLevelCount;
	                public      uint                    baseArrayLayer;
	                public      uint                    arrayLayerCount;
	/** enum   */   public      WGPUTextureAspect       aspect               = WGPUTextureAspect.All;

	public Utf8 label {
		get => GetUtf8(_label);
		set => SetUtf8(value, out this._label);
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

	public IntPtr Userdata {
		get => new IntPtr(userdata);
		set => userdata = (void*)value;
	}
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct WGPUVertexAttribute
{
	/** enum   */   public      WGPUVertexFormat        format;
	                public      ulong                   offset;
	                public      uint                    shaderLocation;
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct WGPUBindGroupDescriptor
{
	[Browse(Never)] internal    WGPUChainedStruct*      _nextInChain;
	[Browse(Never)] internal    char*                   _label;
	/** handle */   public      WGPUBindGroupLayout     layout;
	[Browse(Never)] internal    ulong                   _entryCount;
	[Browse(Never)] internal    WGPUBindGroupEntry*     _entries;

	public Utf8 label {
		get => GetUtf8(_label);
		set => SetUtf8(value, out this._label);
	}
	public Span<WGPUBindGroupEntry> entries {
		get => GetArr(_entries, _entryCount);
		set => SetArr(value, out _entries, out _entryCount);
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
	/** flags  */   public      WGPUShaderStage         visibility;
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

	public Span<WGPUCompilationMessage> messages {
		get => GetArr(_messages, _messageCount);
		set => SetArr(value, out _messages, out _messageCount);
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

	public Utf8 label {
		get => GetUtf8(_label);
		set => SetUtf8(value, out this._label);
	}
	public WGPUComputePassTimestampWrites? timestampWrites {
		get => GetOpt(_timestampWrites);
		set => SetOpt(out _timestampWrites, value);
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
    public WGPUDepthStencilState() {}
	[Browse(Never)] internal    WGPUChainedStruct*      _nextInChain;
	/** enum   */   public      WGPUTextureFormat       format;
	                public      WGPUBool                depthWriteEnabled;
	/** enum   */   public      WGPUCompareFunction     depthCompare;
	                public      WGPUStencilFaceState    stencilFront         = new WGPUStencilFaceState();
	                public      WGPUStencilFaceState    stencilBack          = new WGPUStencilFaceState();
	                public      uint                    stencilReadMask      = 0xFFFFFFFF;
	                public      uint                    stencilWriteMask     = 0xFFFFFFFF;
	                public      int                     depthBias;
	                public      float                   depthBiasSlopeScale;
	                public      float                   depthBiasClamp;
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct WGPUImageCopyBuffer
{
	[Browse(Never)] internal    WGPUChainedStruct*      _nextInChain;
	                public      WGPUTextureDataLayout   layout;
	/** handle */   public      WGPUBuffer              buffer;

	[Conditional("VALIDATE")]
	public void Validate() {
        ObjectTracker.ValidateHandleParam(buffer);
	}
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct WGPUImageCopyTexture
{
	[Browse(Never)] internal    WGPUChainedStruct*      _nextInChain;
	/** handle */   public      WGPUTexture             texture;
	                public      uint                    mipLevel;
	                public      WGPUOrigin3D            origin;
	/** enum   */   public      WGPUTextureAspect       aspect;

	[Conditional("VALIDATE")]
	public void Validate() {
        ObjectTracker.ValidateHandleParam(texture);
	}
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct WGPUProgrammableStageDescriptor
{
	[Browse(Never)] internal    WGPUChainedStruct*      _nextInChain;
	/** handle */   public      WGPUShaderModule        module;
	[Browse(Never)] internal    char*                   _entryPoint;
	[Browse(Never)] internal    ulong                   _constantCount;
	[Browse(Never)] internal    WGPUConstantEntry*      _constants;

	public Utf8 entryPoint {
		get => GetUtf8(_entryPoint);
		set => SetUtf8(value, out this._entryPoint);
	}
	public Span<WGPUConstantEntry> constants {
		get => GetArr(_constants, _constantCount);
		set => SetArr(value, out _constants, out _constantCount);
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
	/** handle */   public      WGPUTextureView         view;
	                public      uint                    depthSlice;
	/** handle */   public      WGPUTextureView         resolveTarget;
	/** enum   */   public      WGPULoadOp              loadOp;
	/** enum   */   public      WGPUStoreOp             storeOp;
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

	public Utf8 label {
		get => GetUtf8(_label);
		set => SetUtf8(value, out this._label);
	}
	public Span<WGPUShaderModuleCompilationHint> hints {
		get => GetArr(_hints, _hintCount);
		set => SetArr(value, out _hints, out _hintCount);
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
    public WGPUTextureDescriptor() {}
	[Browse(Never)] internal    WGPUChainedStruct*      _nextInChain;
	[Browse(Never)] internal    char*                   _label;
	/** flags  */   public      WGPUTextureUsage        usage;
	/** enum   */   public      WGPUTextureDimension    dimension            = WGPUTextureDimension._2D;
	                public      WGPUExtent3D            size;
	/** enum   */   public      WGPUTextureFormat       format;
	                public      uint                    mipLevelCount        = 1;
	                public      uint                    sampleCount          = 1;
	[Browse(Never)] internal    ulong                   _viewFormatCount;
	[Browse(Never)] internal    WGPUTextureFormat*      _viewFormats;

	public Utf8 label {
		get => GetUtf8(_label);
		set => SetUtf8(value, out this._label);
	}
	public Span<WGPUTextureFormat> viewFormats {
		get => GetArr(_viewFormats, _viewFormatCount);
		set => SetArr(value, out _viewFormats, out _viewFormatCount);
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
    public WGPUVertexBufferLayout() {}
	                public      ulong                   arrayStride;
	/** enum   */   public      WGPUVertexStepMode      stepMode             = WGPUVertexStepMode.Vertex;
	[Browse(Never)] internal    ulong                   _attributeCount;
	[Browse(Never)] internal    WGPUVertexAttribute*    _attributes;

	public Span<WGPUVertexAttribute> attributes {
		get => GetArr(_attributes, _attributeCount);
		set => SetArr(value, out _attributes, out _attributeCount);
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

	public Utf8 label {
		get => GetUtf8(_label);
		set => SetUtf8(value, out this._label);
	}
	public Span<WGPUBindGroupLayoutEntry> entries {
		get => GetArr(_entries, _entryCount);
		set => SetArr(value, out _entries, out _entryCount);
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
    public WGPUColorTargetState() {}
	[Browse(Never)] internal    WGPUChainedStruct*      _nextInChain;
	/** enum   */   public      WGPUTextureFormat       format;
	[Browse(Never)] internal    WGPUBlendState*         _blend;
	/** flags  */   public      WGPUColorWriteMask      writeMask            = WGPUColorWriteMask.All;

	public WGPUBlendState? blend {
		get => GetOpt(_blend);
		set => SetOpt(out _blend, value);
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
	/** handle */   public      WGPUPipelineLayout      layout;
	                public      WGPUProgrammableStageDescriptor compute;

	public Utf8 label {
		get => GetUtf8(_label);
		set => SetUtf8(value, out this._label);
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

	public Utf8 label {
		get => GetUtf8(_label);
		set => SetUtf8(value, out this._label);
	}
	public Span<WGPUFeatureName> requiredFeatures {
		get => GetArr(_requiredFeatures, _requiredFeatureCount);
		set => SetArr(value, out _requiredFeatures, out _requiredFeatureCount);
	}
	public WGPURequiredLimits? requiredLimits {
		get => GetOpt(_requiredLimits);
		set => SetOpt(out _requiredLimits, value);
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
	/** handle */   public      WGPUQuerySet            occlusionQuerySet;
	[Browse(Never)] internal    WGPURenderPassTimestampWrites* _timestampWrites;

	public Utf8 label {
		get => GetUtf8(_label);
		set => SetUtf8(value, out this._label);
	}
	public Span<WGPURenderPassColorAttachment> colorAttachments {
		get => GetArr(_colorAttachments, _colorAttachmentCount);
		set => SetArr(value, out _colorAttachments, out _colorAttachmentCount);
	}
	public WGPURenderPassDepthStencilAttachment? depthStencilAttachment {
		get => GetOpt(_depthStencilAttachment);
		set => SetOpt(out _depthStencilAttachment, value);
	}
	public WGPURenderPassTimestampWrites? timestampWrites {
		get => GetOpt(_timestampWrites);
		set => SetOpt(out _timestampWrites, value);
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
	/** handle */   public      WGPUShaderModule        module;
	[Browse(Never)] internal    char*                   _entryPoint;
	[Browse(Never)] internal    ulong                   _constantCount;
	[Browse(Never)] internal    WGPUConstantEntry*      _constants;
	[Browse(Never)] internal    ulong                   _bufferCount;
	[Browse(Never)] internal    WGPUVertexBufferLayout* _buffers;

	public Utf8 entryPoint {
		get => GetUtf8(_entryPoint);
		set => SetUtf8(value, out this._entryPoint);
	}
	public Span<WGPUConstantEntry> constants {
		get => GetArr(_constants, _constantCount);
		set => SetArr(value, out _constants, out _constantCount);
	}
	public Span<WGPUVertexBufferLayout> buffers {
		get => GetArr(_buffers, _bufferCount);
		set => SetArr(value, out _buffers, out _bufferCount);
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
	/** handle */   public      WGPUShaderModule        module;
	[Browse(Never)] internal    char*                   _entryPoint;
	[Browse(Never)] internal    ulong                   _constantCount;
	[Browse(Never)] internal    WGPUConstantEntry*      _constants;
	[Browse(Never)] internal    ulong                   _targetCount;
	[Browse(Never)] internal    WGPUColorTargetState*   _targets;

	public Utf8 entryPoint {
		get => GetUtf8(_entryPoint);
		set => SetUtf8(value, out this._entryPoint);
	}
	public Span<WGPUConstantEntry> constants {
		get => GetArr(_constants, _constantCount);
		set => SetArr(value, out _constants, out _constantCount);
	}
	public Span<WGPUColorTargetState> targets {
		get => GetArr(_targets, _targetCount);
		set => SetArr(value, out _targets, out _targetCount);
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
    public WGPURenderPipelineDescriptor() {}
	[Browse(Never)] internal    WGPUChainedStruct*      _nextInChain;
	[Browse(Never)] internal    char*                   _label;
	/** handle */   public      WGPUPipelineLayout      layout;
	                public      WGPUVertexState         vertex;
	                public      WGPUPrimitiveState      primitive            = new WGPUPrimitiveState();
	[Browse(Never)] internal    WGPUDepthStencilState*  _depthStencil;
	                public      WGPUMultisampleState    multisample          = new WGPUMultisampleState();
	[Browse(Never)] internal    WGPUFragmentState*      _fragment;

	public Utf8 label {
		get => GetUtf8(_label);
		set => SetUtf8(value, out this._label);
	}
	public WGPUDepthStencilState? depthStencil {
		get => GetOpt(_depthStencil);
		set => SetOpt(out _depthStencil, value);
	}
	public WGPUFragmentState? fragment {
		get => GetOpt(_fragment);
		set => SetOpt(out _fragment, value);
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
	/** enum   */   public      WGPUSType               sType;

	public WGPUChainedStruct? next {
		get => GetOpt(_next);
		set => SetOpt(out _next, value);
	}
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct WGPUChainedStructOut
{
	[Browse(Never)] internal    WGPUChainedStructOut*   _next;
	/** enum   */   public      WGPUSType               sType;

	public WGPUChainedStructOut? next {
		get => GetOpt(_next);
		set => SetOpt(out _next, value);
	}
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct WGPUInstanceExtras
{
	                public      WGPUChainedStruct       chain;
	/** flags  */   public      WGPUInstanceBackend     backends;
	/** flags  */   public      WGPUInstance            flags;
	/** enum   */   public      WGPUDx12Compiler        dx12ShaderCompiler;
	/** enum   */   public      WGPUGles3MinorVersion   gles3MinorVersion;
	[Browse(Never)] internal    char*                   _dxilPath;
	[Browse(Never)] internal    char*                   _dxcPath;

	public Utf8 dxilPath {
		get => GetUtf8(_dxilPath);
		set => SetUtf8(value, out this._dxilPath);
	}
	public Utf8 dxcPath {
		get => GetUtf8(_dxcPath);
		set => SetUtf8(value, out this._dxcPath);
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

	public Utf8 tracePath {
		get => GetUtf8(_tracePath);
		set => SetUtf8(value, out this._tracePath);
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
	/** flags  */   public      WGPUShaderStage         stages;
	                public      uint                    start;
	                public      uint                    end;
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct WGPUPipelineLayoutExtras
{
	                public      WGPUChainedStruct       chain;
	[Browse(Never)] internal    ulong                   _pushConstantRangeCount;
	[Browse(Never)] internal    WGPUPushConstantRange*  _pushConstantRanges;

	public Span<WGPUPushConstantRange> pushConstantRanges {
		get => GetArr(_pushConstantRanges, _pushConstantRangeCount);
		set => SetArr(value, out _pushConstantRanges, out _pushConstantRangeCount);
	}

	[Conditional("VALIDATE")]
	public void Validate() {
		AllocValidator.ValidatePtr(_pushConstantRanges);
	}
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct WGPUWrappedSubmissionIndex
{
	/** handle */   public      WGPUQueue               queue;
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

	public Utf8 name {
		get => GetUtf8(_name);
		set => SetUtf8(value, out this._name);
	}
	public Utf8 value {
		get => GetUtf8(_value);
		set => SetUtf8(value, out this._value);
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
	/** enum   */   public      WGPUShaderStage         stage;
	[Browse(Never)] internal    char*                   _code;
	[Browse(Never)] internal    uint                    _defineCount;
	[Browse(Never)] internal    WGPUShaderDefine*       _defines;

	public Utf8 code {
		get => GetUtf8(_code);
		set => SetUtf8(value, out this._code);
	}
	public Span<WGPUShaderDefine> defines {
		get => GetArr(_defines, _defineCount);
		set => SetArr(value, out _defines, out _defineCount);
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
	/** enum   */   public      WGPUBackendType         backendType;
	                public      WGPUHubReport           vulkan;
	                public      WGPUHubReport           metal;
	                public      WGPUHubReport           dx12;
	                public      WGPUHubReport           gl;
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct WGPUInstanceEnumerateAdapterOptions
{
	[Browse(Never)] internal    WGPUChainedStruct*      _nextInChain;
	/** flags  */   public      WGPUInstanceBackend     backends;
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

	public Span<WGPUBuffer> buffers {
		get => GetArr(_buffers, _bufferCount);
		set => SetArr(value, out _buffers, out _bufferCount);
	}
	public Span<WGPUSampler> samplers {
		get => GetArr(_samplers, _samplerCount);
		set => SetArr(value, out _samplers, out _samplerCount);
	}
	public Span<WGPUTextureView> textureViews {
		get => GetArr(_textureViews, _textureViewCount);
		set => SetArr(value, out _textureViews, out _textureViewCount);
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

	public Span<WGPUPipelineStatisticName> pipelineStatistics {
		get => GetArr(_pipelineStatistics, _pipelineStatisticCount);
		set => SetArr(value, out _pipelineStatistics, out _pipelineStatisticCount);
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

