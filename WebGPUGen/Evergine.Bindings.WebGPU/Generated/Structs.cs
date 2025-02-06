using System.Runtime.InteropServices;


// ReSharper disable RedundantUnsafeContext;
// ReSharper disable InconsistentNaming;
namespace Evergine.Bindings.WebGPU
{
	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPUAdapterInfo
	{
		public      WGPUChainedStructOut*   nextInChain;
		internal    char*                   _vendor;
		internal    char*                   _architecture;
		internal    char*                   _device;
		internal    char*                   _description;
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
		public      WGPUChainedStruct*      nextInChain;
		public      uint                    binding;
		public      WGPUBuffer              buffer;
		public      ulong                   offset;
		public      ulong                   size;
		public      WGPUSampler             sampler;
		public      WGPUTextureView         textureView;
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
		public      WGPUChainedStruct*      nextInChain;
		public      WGPUBufferBindingType   type;
		public      WGPUBool                hasDynamicOffset;
		public      ulong                   minBindingSize;
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPUBufferDescriptor
	{
		public      WGPUChainedStruct*      nextInChain;
		internal    char*                   _label;
		public      WGPUBufferUsage         usage;
		public      ulong                   size;
		public      WGPUBool                mappedAtCreation;
		// --- properties
		public Utf8 label {
			get => ApiUtils.GetUtf8(_label);
			set => ApiUtils.SetUtf8(value, out this._label);
		}

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
		public      WGPUChainedStruct*      nextInChain;
		internal    char*                   _label;
		// --- properties
		public Utf8 label {
			get => ApiUtils.GetUtf8(_label);
			set => ApiUtils.SetUtf8(value, out this._label);
		}

		public void Validate() {
			AllocValidator.ValidatePtr(_label);
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPUCommandEncoderDescriptor
	{
		public      WGPUChainedStruct*      nextInChain;
		internal    char*                   _label;
		// --- properties
		public Utf8 label {
			get => ApiUtils.GetUtf8(_label);
			set => ApiUtils.SetUtf8(value, out this._label);
		}

		public void Validate() {
			AllocValidator.ValidatePtr(_label);
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPUCompilationMessage
	{
		public      WGPUChainedStruct*      nextInChain;
		internal    char*                   _message;
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
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPUConstantEntry
	{
		public      WGPUChainedStruct*      nextInChain;
		internal    char*                   _key;
		public      double                  value;
		// --- properties
		public Utf8 key {
			get => ApiUtils.GetUtf8(_key);
			set => ApiUtils.SetUtf8(value, out this._key);
		}

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
		public      WGPUChainedStruct*      nextInChain;
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
		public      WGPUChainedStruct*      nextInChain;
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
		public      WGPUChainedStruct*      nextInChain;
		internal    char*                   _label;
		internal    ulong                   _bindGroupLayoutCount;
		internal    WGPUBindGroupLayout*    _bindGroupLayouts;
		// --- properties
		public Utf8 label {
			get => ApiUtils.GetUtf8(_label);
			set => ApiUtils.SetUtf8(value, out this._label);
		}
		public Span<WGPUBindGroupLayout> bindGroupLayouts {
			get => ApiUtils.GetArr(_bindGroupLayouts, _bindGroupLayoutCount);
			set => ApiUtils.SetArr(value, out _bindGroupLayouts, out _bindGroupLayoutCount);
		}

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
		public      WGPUChainedStruct*      nextInChain;
		public      WGPUPrimitiveTopology   topology;
		public      WGPUIndexFormat         stripIndexFormat;
		public      WGPUFrontFace           frontFace;
		public      WGPUCullMode            cullMode;
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPUQuerySetDescriptor
	{
		public      WGPUChainedStruct*      nextInChain;
		internal    char*                   _label;
		public      WGPUQueryType           type;
		public      uint                    count;
		// --- properties
		public Utf8 label {
			get => ApiUtils.GetUtf8(_label);
			set => ApiUtils.SetUtf8(value, out this._label);
		}

		public void Validate() {
			AllocValidator.ValidatePtr(_label);
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPUQueueDescriptor
	{
		public      WGPUChainedStruct*      nextInChain;
		internal    char*                   _label;
		// --- properties
		public Utf8 label {
			get => ApiUtils.GetUtf8(_label);
			set => ApiUtils.SetUtf8(value, out this._label);
		}

		public void Validate() {
			AllocValidator.ValidatePtr(_label);
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPURenderBundleDescriptor
	{
		public      WGPUChainedStruct*      nextInChain;
		internal    char*                   _label;
		// --- properties
		public Utf8 label {
			get => ApiUtils.GetUtf8(_label);
			set => ApiUtils.SetUtf8(value, out this._label);
		}

		public void Validate() {
			AllocValidator.ValidatePtr(_label);
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPURenderBundleEncoderDescriptor
	{
		public      WGPUChainedStruct*      nextInChain;
		internal    char*                   _label;
		internal    ulong                   _colorFormatCount;
		internal    WGPUTextureFormat*      _colorFormats;
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
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPURequestAdapterOptions
	{
		public      WGPUChainedStruct*      nextInChain;
		public      WGPUSurface             compatibleSurface;
		public      WGPUPowerPreference     powerPreference;
		public      WGPUBackendType         backendType;
		public      WGPUBool                forceFallbackAdapter;
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPUSamplerBindingLayout
	{
		public      WGPUChainedStruct*      nextInChain;
		public      WGPUSamplerBindingType  type;
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPUSamplerDescriptor
	{
		public      WGPUChainedStruct*      nextInChain;
		internal    char*                   _label;
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

		public void Validate() {
			AllocValidator.ValidatePtr(_label);
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPUShaderModuleCompilationHint
	{
		public      WGPUChainedStruct*      nextInChain;
		internal    char*                   _entryPoint;
		public      WGPUPipelineLayout      layout;
		// --- properties
		public Utf8 entryPoint {
			get => ApiUtils.GetUtf8(_entryPoint);
			set => ApiUtils.SetUtf8(value, out this._entryPoint);
		}

		public void Validate() {
			AllocValidator.ValidatePtr(_entryPoint);
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPUShaderModuleSPIRVDescriptor
	{
		public      WGPUChainedStruct       chain;
		public      uint                    codeSize;
		internal    uint*                   _code;
		// --- properties
		public uint? code {
			get => ApiUtils.GetOpt(_code);
			set => ApiUtils.SetOpt(out _code, value);
		}

		public void Validate() {
			AllocValidator.ValidatePtr(_code);
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPUShaderModuleWGSLDescriptor
	{
		public      WGPUChainedStruct       chain;
		internal    char*                   _code;
		// --- properties
		public Utf8 code {
			get => ApiUtils.GetUtf8(_code);
			set => ApiUtils.SetUtf8(value, out this._code);
		}

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
		public      WGPUChainedStruct*      nextInChain;
		public      WGPUStorageTextureAccess access;
		public      WGPUTextureFormat       format;
		public      WGPUTextureViewDimension viewDimension;
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPUSurfaceCapabilities
	{
		public      WGPUChainedStructOut*   nextInChain;
		public      WGPUTextureUsage        usages;
		internal    ulong                   _formatCount;
		internal    WGPUTextureFormat*      _formats;
		internal    ulong                   _presentModeCount;
		internal    WGPUPresentMode*        _presentModes;
		internal    ulong                   _alphaModeCount;
		internal    WGPUCompositeAlphaMode* _alphaModes;
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

		public void Validate() {
			AllocValidator.ValidatePtr(_formats);
			AllocValidator.ValidatePtr(_presentModes);
			AllocValidator.ValidatePtr(_alphaModes);
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPUSurfaceConfiguration
	{
		public      WGPUChainedStruct*      nextInChain;
		public      WGPUDevice              device;
		public      WGPUTextureFormat       format;
		public      WGPUTextureUsage        usage;
		internal    ulong                   _viewFormatCount;
		internal    WGPUTextureFormat*      _viewFormats;
		public      WGPUCompositeAlphaMode  alphaMode;
		public      uint                    width;
		public      uint                    height;
		public      WGPUPresentMode         presentMode;
		// --- properties
		public Span<WGPUTextureFormat> viewFormats {
			get => ApiUtils.GetArr(_viewFormats, _viewFormatCount);
			set => ApiUtils.SetArr(value, out _viewFormats, out _viewFormatCount);
		}

		public void Validate() {
			AllocValidator.ValidatePtr(_viewFormats);
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPUSurfaceDescriptor
	{
		public      WGPUChainedStruct*      nextInChain;
		internal    char*                   _label;
		// --- properties
		public Utf8 label {
			get => ApiUtils.GetUtf8(_label);
			set => ApiUtils.SetUtf8(value, out this._label);
		}

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
		internal    char*                   _selector;
		// --- properties
		public Utf8 selector {
			get => ApiUtils.GetUtf8(_selector);
			set => ApiUtils.SetUtf8(value, out this._selector);
		}

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
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPUTextureBindingLayout
	{
		public      WGPUChainedStruct*      nextInChain;
		public      WGPUTextureSampleType   sampleType;
		public      WGPUTextureViewDimension viewDimension;
		public      WGPUBool                multisampled;
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPUTextureDataLayout
	{
		public      WGPUChainedStruct*      nextInChain;
		public      ulong                   offset;
		public      uint                    bytesPerRow;
		public      uint                    rowsPerImage;
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPUTextureViewDescriptor
	{
		public      WGPUChainedStruct*      nextInChain;
		internal    char*                   _label;
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

		public void Validate() {
			AllocValidator.ValidatePtr(_label);
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPUUncapturedErrorCallbackInfo
	{
		public      WGPUChainedStruct*      nextInChain;
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
		public      WGPUChainedStruct*      nextInChain;
		internal    char*                   _label;
		public      WGPUBindGroupLayout     layout;
		internal    ulong                   _entryCount;
		internal    WGPUBindGroupEntry*     _entries;
		// --- properties
		public Utf8 label {
			get => ApiUtils.GetUtf8(_label);
			set => ApiUtils.SetUtf8(value, out this._label);
		}
		public Span<WGPUBindGroupEntry> entries {
			get => ApiUtils.GetArr(_entries, _entryCount);
			set => ApiUtils.SetArr(value, out _entries, out _entryCount);
		}

		public void Validate() {
			AllocValidator.ValidatePtr(_label);
			AllocValidator.ValidatePtr(_entries);
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPUBindGroupLayoutEntry
	{
		public      WGPUChainedStruct*      nextInChain;
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
		public      WGPUChainedStruct*      nextInChain;
		internal    ulong                   _messageCount;
		internal    WGPUCompilationMessage* _messages;
		// --- properties
		public Span<WGPUCompilationMessage> messages {
			get => ApiUtils.GetArr(_messages, _messageCount);
			set => ApiUtils.SetArr(value, out _messages, out _messageCount);
		}

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
		public      WGPUChainedStruct*      nextInChain;
		internal    char*                   _label;
		internal    WGPUComputePassTimestampWrites* _timestampWrites;
		// --- properties
		public Utf8 label {
			get => ApiUtils.GetUtf8(_label);
			set => ApiUtils.SetUtf8(value, out this._label);
		}
		public WGPUComputePassTimestampWrites? timestampWrites {
			get => ApiUtils.GetOpt(_timestampWrites);
			set => ApiUtils.SetOpt(out _timestampWrites, value);
		}

		public void Validate() {
			AllocValidator.ValidatePtr(_label);
			AllocValidator.ValidatePtr(_timestampWrites);
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPUDepthStencilState
	{
		public      WGPUChainedStruct*      nextInChain;
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
		public      WGPUChainedStruct*      nextInChain;
		public      WGPUTextureDataLayout   layout;
		public      WGPUBuffer              buffer;
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPUImageCopyTexture
	{
		public      WGPUChainedStruct*      nextInChain;
		public      WGPUTexture             texture;
		public      uint                    mipLevel;
		public      WGPUOrigin3D            origin;
		public      WGPUTextureAspect       aspect;
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPUProgrammableStageDescriptor
	{
		public      WGPUChainedStruct*      nextInChain;
		public      WGPUShaderModule        module;
		internal    char*                   _entryPoint;
		internal    ulong                   _constantCount;
		internal    WGPUConstantEntry*      _constants;
		// --- properties
		public Utf8 entryPoint {
			get => ApiUtils.GetUtf8(_entryPoint);
			set => ApiUtils.SetUtf8(value, out this._entryPoint);
		}
		public Span<WGPUConstantEntry> constants {
			get => ApiUtils.GetArr(_constants, _constantCount);
			set => ApiUtils.SetArr(value, out _constants, out _constantCount);
		}

		public void Validate() {
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
		public      WGPUChainedStruct*      nextInChain;
		public      WGPUTextureView         view;
		public      uint                    depthSlice;
		public      WGPUTextureView         resolveTarget;
		public      WGPULoadOp              loadOp;
		public      WGPUStoreOp             storeOp;
		public      WGPUColor               clearValue;
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPURequiredLimits
	{
		public      WGPUChainedStruct*      nextInChain;
		public      WGPULimits              limits;
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPUShaderModuleDescriptor
	{
		public      WGPUChainedStruct*      nextInChain;
		internal    char*                   _label;
		internal    ulong                   _hintCount;
		internal    WGPUShaderModuleCompilationHint* _hints;
		// --- properties
		public Utf8 label {
			get => ApiUtils.GetUtf8(_label);
			set => ApiUtils.SetUtf8(value, out this._label);
		}
		public Span<WGPUShaderModuleCompilationHint> hints {
			get => ApiUtils.GetArr(_hints, _hintCount);
			set => ApiUtils.SetArr(value, out _hints, out _hintCount);
		}

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
		public      WGPUChainedStructOut*   nextInChain;
		public      WGPULimits              limits;
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPUTextureDescriptor
	{
		public      WGPUChainedStruct*      nextInChain;
		internal    char*                   _label;
		public      WGPUTextureUsage        usage;
		public      WGPUTextureDimension    dimension;
		public      WGPUExtent3D            size;
		public      WGPUTextureFormat       format;
		public      uint                    mipLevelCount;
		public      uint                    sampleCount;
		internal    ulong                   _viewFormatCount;
		internal    WGPUTextureFormat*      _viewFormats;
		// --- properties
		public Utf8 label {
			get => ApiUtils.GetUtf8(_label);
			set => ApiUtils.SetUtf8(value, out this._label);
		}
		public Span<WGPUTextureFormat> viewFormats {
			get => ApiUtils.GetArr(_viewFormats, _viewFormatCount);
			set => ApiUtils.SetArr(value, out _viewFormats, out _viewFormatCount);
		}

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
		internal    ulong                   _attributeCount;
		internal    WGPUVertexAttribute*    _attributes;
		// --- properties
		public Span<WGPUVertexAttribute> attributes {
			get => ApiUtils.GetArr(_attributes, _attributeCount);
			set => ApiUtils.SetArr(value, out _attributes, out _attributeCount);
		}

		public void Validate() {
			AllocValidator.ValidatePtr(_attributes);
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPUBindGroupLayoutDescriptor
	{
		public      WGPUChainedStruct*      nextInChain;
		internal    char*                   _label;
		internal    ulong                   _entryCount;
		internal    WGPUBindGroupLayoutEntry* _entries;
		// --- properties
		public Utf8 label {
			get => ApiUtils.GetUtf8(_label);
			set => ApiUtils.SetUtf8(value, out this._label);
		}
		public Span<WGPUBindGroupLayoutEntry> entries {
			get => ApiUtils.GetArr(_entries, _entryCount);
			set => ApiUtils.SetArr(value, out _entries, out _entryCount);
		}

		public void Validate() {
			AllocValidator.ValidatePtr(_label);
			AllocValidator.ValidatePtr(_entries);
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPUColorTargetState
	{
		public      WGPUChainedStruct*      nextInChain;
		public      WGPUTextureFormat       format;
		internal    WGPUBlendState*         _blend;
		public      WGPUColorWriteMask      writeMask;
		// --- properties
		public WGPUBlendState? blend {
			get => ApiUtils.GetOpt(_blend);
			set => ApiUtils.SetOpt(out _blend, value);
		}

		public void Validate() {
			AllocValidator.ValidatePtr(_blend);
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPUComputePipelineDescriptor
	{
		public      WGPUChainedStruct*      nextInChain;
		internal    char*                   _label;
		public      WGPUPipelineLayout      layout;
		public      WGPUProgrammableStageDescriptor compute;
		// --- properties
		public Utf8 label {
			get => ApiUtils.GetUtf8(_label);
			set => ApiUtils.SetUtf8(value, out this._label);
		}

		public void Validate() {
			AllocValidator.ValidatePtr(_label);
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPUDeviceDescriptor
	{
		public      WGPUChainedStruct*      nextInChain;
		internal    char*                   _label;
		internal    ulong                   _requiredFeatureCount;
		internal    WGPUFeatureName*        _requiredFeatures;
		internal    WGPURequiredLimits*     _requiredLimits;
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

		public void Validate() {
			AllocValidator.ValidatePtr(_label);
			AllocValidator.ValidatePtr(_requiredFeatures);
			AllocValidator.ValidatePtr(_requiredLimits);
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPURenderPassDescriptor
	{
		public      WGPUChainedStruct*      nextInChain;
		internal    char*                   _label;
		internal    ulong                   _colorAttachmentCount;
		internal    WGPURenderPassColorAttachment* _colorAttachments;
		internal    WGPURenderPassDepthStencilAttachment* _depthStencilAttachment;
		public      WGPUQuerySet            occlusionQuerySet;
		internal    WGPURenderPassTimestampWrites* _timestampWrites;
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

		public void Validate() {
			AllocValidator.ValidatePtr(_label);
			AllocValidator.ValidatePtr(_colorAttachments);
			AllocValidator.ValidatePtr(_depthStencilAttachment);
			AllocValidator.ValidatePtr(_timestampWrites);
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPUVertexState
	{
		public      WGPUChainedStruct*      nextInChain;
		public      WGPUShaderModule        module;
		internal    char*                   _entryPoint;
		internal    ulong                   _constantCount;
		internal    WGPUConstantEntry*      _constants;
		internal    ulong                   _bufferCount;
		internal    WGPUVertexBufferLayout* _buffers;
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

		public void Validate() {
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
		public      WGPUChainedStruct*      nextInChain;
		public      WGPUShaderModule        module;
		internal    char*                   _entryPoint;
		internal    ulong                   _constantCount;
		internal    WGPUConstantEntry*      _constants;
		internal    ulong                   _targetCount;
		internal    WGPUColorTargetState*   _targets;
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

		public void Validate() {
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
		public      WGPUChainedStruct*      nextInChain;
		internal    char*                   _label;
		public      WGPUPipelineLayout      layout;
		public      WGPUVertexState         vertex;
		public      WGPUPrimitiveState      primitive;
		internal    WGPUDepthStencilState*  _depthStencil;
		public      WGPUMultisampleState    multisample;
		internal    WGPUFragmentState*      _fragment;
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

		public void Validate() {
			AllocValidator.ValidatePtr(_label);
			AllocValidator.ValidatePtr(_depthStencil);
			AllocValidator.ValidatePtr(_fragment);
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPUChainedStruct
	{
		internal    WGPUChainedStruct*      _next;
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
		internal    WGPUChainedStructOut*   _next;
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
		internal    char*                   _dxilPath;
		internal    char*                   _dxcPath;
		// --- properties
		public Utf8 dxilPath {
			get => ApiUtils.GetUtf8(_dxilPath);
			set => ApiUtils.SetUtf8(value, out this._dxilPath);
		}
		public Utf8 dxcPath {
			get => ApiUtils.GetUtf8(_dxcPath);
			set => ApiUtils.SetUtf8(value, out this._dxcPath);
		}

		public void Validate() {
			AllocValidator.ValidatePtr(_dxilPath);
			AllocValidator.ValidatePtr(_dxcPath);
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPUDeviceExtras
	{
		public      WGPUChainedStruct       chain;
		internal    char*                   _tracePath;
		// --- properties
		public Utf8 tracePath {
			get => ApiUtils.GetUtf8(_tracePath);
			set => ApiUtils.SetUtf8(value, out this._tracePath);
		}

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
		internal    ulong                   _pushConstantRangeCount;
		internal    WGPUPushConstantRange*  _pushConstantRanges;
		// --- properties
		public Span<WGPUPushConstantRange> pushConstantRanges {
			get => ApiUtils.GetArr(_pushConstantRanges, _pushConstantRangeCount);
			set => ApiUtils.SetArr(value, out _pushConstantRanges, out _pushConstantRangeCount);
		}

		public void Validate() {
			AllocValidator.ValidatePtr(_pushConstantRanges);
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPUWrappedSubmissionIndex
	{
		public      WGPUQueue               queue;
		public      ulong                   submissionIndex;
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPUShaderDefine
	{
		internal    char*                   _name;
		internal    char*                   _value;
		// --- properties
		public Utf8 name {
			get => ApiUtils.GetUtf8(_name);
			set => ApiUtils.SetUtf8(value, out this._name);
		}
		public Utf8 value {
			get => ApiUtils.GetUtf8(_value);
			set => ApiUtils.SetUtf8(value, out this._value);
		}

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
		internal    char*                   _code;
		internal    uint                    _defineCount;
		internal    WGPUShaderDefine*       _defines;
		// --- properties
		public Utf8 code {
			get => ApiUtils.GetUtf8(_code);
			set => ApiUtils.SetUtf8(value, out this._code);
		}
		public Span<WGPUShaderDefine> defines {
			get => ApiUtils.GetArr(_defines, _defineCount);
			set => ApiUtils.SetArr(value, out _defines, out _defineCount);
		}

		public void Validate() {
			AllocValidator.ValidatePtr(_code);
			AllocValidator.ValidatePtr(_defines);
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
		public      WGPUChainedStruct*      nextInChain;
		public      WGPUInstanceBackend     backends;
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPUBindGroupEntryExtras
	{
		public      WGPUChainedStruct       chain;
		internal    WGPUBuffer*             _buffers;
		internal    ulong                   _bufferCount;
		internal    WGPUSampler*            _samplers;
		internal    ulong                   _samplerCount;
		internal    WGPUTextureView*        _textureViews;
		internal    ulong                   _textureViewCount;
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
		internal    WGPUPipelineStatisticName* _pipelineStatistics;
		internal    ulong                   _pipelineStatisticCount;
		// --- properties
		public Span<WGPUPipelineStatisticName> pipelineStatistics {
			get => ApiUtils.GetArr(_pipelineStatistics, _pipelineStatisticCount);
			set => ApiUtils.SetArr(value, out _pipelineStatistics, out _pipelineStatisticCount);
		}

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

}

