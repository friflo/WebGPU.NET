using System;
using System.Runtime.InteropServices;

namespace Evergine.Bindings.WebGPU
{
	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPUAdapterInfo
	{
		public WGPUChainedStructOut* nextInChain;
		public char* vendor;
		public char* architecture;
		public char* device;
		public char* description;
		public WGPUBackendType backendType;
		public WGPUAdapterType adapterType;
		public uint vendorID;
		public uint deviceID;
		// --- properties
		public WGPUChainedStructOut? NextInChain {
			get => ApiUtils.GetOpt(nextInChain);
			set => ApiUtils.SetOpt(out nextInChain, value);
		}
		public ReadOnlySpan<char> Vendor {
			get => ApiUtils.GetStr(vendor);
			set => ApiUtils.SetStr(value, out this.vendor);
		}
		public ReadOnlySpan<char> Architecture {
			get => ApiUtils.GetStr(architecture);
			set => ApiUtils.SetStr(value, out this.architecture);
		}
		public ReadOnlySpan<char> Device {
			get => ApiUtils.GetStr(device);
			set => ApiUtils.SetStr(value, out this.device);
		}
		public ReadOnlySpan<char> Description {
			get => ApiUtils.GetStr(description);
			set => ApiUtils.SetStr(value, out this.description);
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPUBindGroupEntry
	{
		public WGPUChainedStruct* nextInChain;
		public uint binding;
		public WGPUBuffer buffer;
		public ulong offset;
		public ulong size;
		public WGPUSampler sampler;
		public WGPUTextureView textureView;
		// --- properties
		public WGPUChainedStruct? NextInChain {
			get => ApiUtils.GetOpt(nextInChain);
			set => ApiUtils.SetOpt(out nextInChain, value);
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPUBlendComponent
	{
		public WGPUBlendOperation operation;
		public WGPUBlendFactor srcFactor;
		public WGPUBlendFactor dstFactor;
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPUBufferBindingLayout
	{
		public WGPUChainedStruct* nextInChain;
		public WGPUBufferBindingType type;
		public WGPUBool hasDynamicOffset;
		public ulong minBindingSize;
		// --- properties
		public WGPUChainedStruct? NextInChain {
			get => ApiUtils.GetOpt(nextInChain);
			set => ApiUtils.SetOpt(out nextInChain, value);
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPUBufferDescriptor
	{
		public WGPUChainedStruct* nextInChain;
		public char* label;
		public WGPUBufferUsage usage;
		public ulong size;
		public WGPUBool mappedAtCreation;
		// --- properties
		public WGPUChainedStruct? NextInChain {
			get => ApiUtils.GetOpt(nextInChain);
			set => ApiUtils.SetOpt(out nextInChain, value);
		}
		public ReadOnlySpan<char> Label {
			get => ApiUtils.GetStr(label);
			set => ApiUtils.SetStr(value, out this.label);
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPUColor
	{
		public double r;
		public double g;
		public double b;
		public double a;
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPUCommandBufferDescriptor
	{
		public WGPUChainedStruct* nextInChain;
		public char* label;
		// --- properties
		public WGPUChainedStruct? NextInChain {
			get => ApiUtils.GetOpt(nextInChain);
			set => ApiUtils.SetOpt(out nextInChain, value);
		}
		public ReadOnlySpan<char> Label {
			get => ApiUtils.GetStr(label);
			set => ApiUtils.SetStr(value, out this.label);
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPUCommandEncoderDescriptor
	{
		public WGPUChainedStruct* nextInChain;
		public char* label;
		// --- properties
		public WGPUChainedStruct? NextInChain {
			get => ApiUtils.GetOpt(nextInChain);
			set => ApiUtils.SetOpt(out nextInChain, value);
		}
		public ReadOnlySpan<char> Label {
			get => ApiUtils.GetStr(label);
			set => ApiUtils.SetStr(value, out this.label);
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPUCompilationMessage
	{
		public WGPUChainedStruct* nextInChain;
		public char* message;
		public WGPUCompilationMessageType type;
		public ulong lineNum;
		public ulong linePos;
		public ulong offset;
		public ulong length;
		public ulong utf16LinePos;
		public ulong utf16Offset;
		public ulong utf16Length;
		// --- properties
		public WGPUChainedStruct? NextInChain {
			get => ApiUtils.GetOpt(nextInChain);
			set => ApiUtils.SetOpt(out nextInChain, value);
		}
		public ReadOnlySpan<char> Message {
			get => ApiUtils.GetStr(message);
			set => ApiUtils.SetStr(value, out this.message);
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPUComputePassTimestampWrites
	{
		public WGPUQuerySet querySet;
		public uint beginningOfPassWriteIndex;
		public uint endOfPassWriteIndex;
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPUConstantEntry
	{
		public WGPUChainedStruct* nextInChain;
		public char* key;
		public double value;
		// --- properties
		public WGPUChainedStruct? NextInChain {
			get => ApiUtils.GetOpt(nextInChain);
			set => ApiUtils.SetOpt(out nextInChain, value);
		}
		public ReadOnlySpan<char> Key {
			get => ApiUtils.GetStr(key);
			set => ApiUtils.SetStr(value, out this.key);
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPUExtent3D
	{
		public uint width;
		public uint height;
		public uint depthOrArrayLayers;
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPUInstanceDescriptor
	{
		public WGPUChainedStruct* nextInChain;
		// --- properties
		public WGPUChainedStruct? NextInChain {
			get => ApiUtils.GetOpt(nextInChain);
			set => ApiUtils.SetOpt(out nextInChain, value);
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPULimits
	{
		public uint maxTextureDimension1D;
		public uint maxTextureDimension2D;
		public uint maxTextureDimension3D;
		public uint maxTextureArrayLayers;
		public uint maxBindGroups;
		public uint maxBindGroupsPlusVertexBuffers;
		public uint maxBindingsPerBindGroup;
		public uint maxDynamicUniformBuffersPerPipelineLayout;
		public uint maxDynamicStorageBuffersPerPipelineLayout;
		public uint maxSampledTexturesPerShaderStage;
		public uint maxSamplersPerShaderStage;
		public uint maxStorageBuffersPerShaderStage;
		public uint maxStorageTexturesPerShaderStage;
		public uint maxUniformBuffersPerShaderStage;
		public ulong maxUniformBufferBindingSize;
		public ulong maxStorageBufferBindingSize;
		public uint minUniformBufferOffsetAlignment;
		public uint minStorageBufferOffsetAlignment;
		public uint maxVertexBuffers;
		public ulong maxBufferSize;
		public uint maxVertexAttributes;
		public uint maxVertexBufferArrayStride;
		public uint maxInterStageShaderComponents;
		public uint maxInterStageShaderVariables;
		public uint maxColorAttachments;
		public uint maxColorAttachmentBytesPerSample;
		public uint maxComputeWorkgroupStorageSize;
		public uint maxComputeInvocationsPerWorkgroup;
		public uint maxComputeWorkgroupSizeX;
		public uint maxComputeWorkgroupSizeY;
		public uint maxComputeWorkgroupSizeZ;
		public uint maxComputeWorkgroupsPerDimension;
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPUMultisampleState
	{
		public WGPUChainedStruct* nextInChain;
		public uint count;
		public uint mask;
		public WGPUBool alphaToCoverageEnabled;
		// --- properties
		public WGPUChainedStruct? NextInChain {
			get => ApiUtils.GetOpt(nextInChain);
			set => ApiUtils.SetOpt(out nextInChain, value);
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPUOrigin3D
	{
		public uint x;
		public uint y;
		public uint z;
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPUPipelineLayoutDescriptor
	{
		public WGPUChainedStruct* nextInChain;
		public char* label;
		public ulong bindGroupLayoutCount;
		public WGPUBindGroupLayout* bindGroupLayouts;
		// --- properties
		public WGPUChainedStruct? NextInChain {
			get => ApiUtils.GetOpt(nextInChain);
			set => ApiUtils.SetOpt(out nextInChain, value);
		}
		public ReadOnlySpan<char> Label {
			get => ApiUtils.GetStr(label);
			set => ApiUtils.SetStr(value, out this.label);
		}
		public Span<WGPUBindGroupLayout> BindGroupLayouts {
			get => new (bindGroupLayouts, (int)bindGroupLayoutCount);
			set => value.SetArr(out bindGroupLayouts, out bindGroupLayoutCount);
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPUPrimitiveDepthClipControl
	{
		public WGPUChainedStruct chain;
		public WGPUBool unclippedDepth;
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPUPrimitiveState
	{
		public WGPUChainedStruct* nextInChain;
		public WGPUPrimitiveTopology topology;
		public WGPUIndexFormat stripIndexFormat;
		public WGPUFrontFace frontFace;
		public WGPUCullMode cullMode;
		// --- properties
		public WGPUChainedStruct? NextInChain {
			get => ApiUtils.GetOpt(nextInChain);
			set => ApiUtils.SetOpt(out nextInChain, value);
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPUQuerySetDescriptor
	{
		public WGPUChainedStruct* nextInChain;
		public char* label;
		public WGPUQueryType type;
		public uint count;
		// --- properties
		public WGPUChainedStruct? NextInChain {
			get => ApiUtils.GetOpt(nextInChain);
			set => ApiUtils.SetOpt(out nextInChain, value);
		}
		public ReadOnlySpan<char> Label {
			get => ApiUtils.GetStr(label);
			set => ApiUtils.SetStr(value, out this.label);
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPUQueueDescriptor
	{
		public WGPUChainedStruct* nextInChain;
		public char* label;
		// --- properties
		public WGPUChainedStruct? NextInChain {
			get => ApiUtils.GetOpt(nextInChain);
			set => ApiUtils.SetOpt(out nextInChain, value);
		}
		public ReadOnlySpan<char> Label {
			get => ApiUtils.GetStr(label);
			set => ApiUtils.SetStr(value, out this.label);
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPURenderBundleDescriptor
	{
		public WGPUChainedStruct* nextInChain;
		public char* label;
		// --- properties
		public WGPUChainedStruct? NextInChain {
			get => ApiUtils.GetOpt(nextInChain);
			set => ApiUtils.SetOpt(out nextInChain, value);
		}
		public ReadOnlySpan<char> Label {
			get => ApiUtils.GetStr(label);
			set => ApiUtils.SetStr(value, out this.label);
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPURenderBundleEncoderDescriptor
	{
		public WGPUChainedStruct* nextInChain;
		public char* label;
		public ulong colorFormatCount;
		public WGPUTextureFormat* colorFormats;
		public WGPUTextureFormat depthStencilFormat;
		public uint sampleCount;
		public WGPUBool depthReadOnly;
		public WGPUBool stencilReadOnly;
		// --- properties
		public WGPUChainedStruct? NextInChain {
			get => ApiUtils.GetOpt(nextInChain);
			set => ApiUtils.SetOpt(out nextInChain, value);
		}
		public ReadOnlySpan<char> Label {
			get => ApiUtils.GetStr(label);
			set => ApiUtils.SetStr(value, out this.label);
		}
		public Span<WGPUTextureFormat> ColorFormats {
			get => new (colorFormats, (int)colorFormatCount);
			set => value.SetArr(out colorFormats, out colorFormatCount);
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPURenderPassDepthStencilAttachment
	{
		public WGPUTextureView view;
		public WGPULoadOp depthLoadOp;
		public WGPUStoreOp depthStoreOp;
		public float depthClearValue;
		public WGPUBool depthReadOnly;
		public WGPULoadOp stencilLoadOp;
		public WGPUStoreOp stencilStoreOp;
		public uint stencilClearValue;
		public WGPUBool stencilReadOnly;
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPURenderPassDescriptorMaxDrawCount
	{
		public WGPUChainedStruct chain;
		public ulong maxDrawCount;
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPURenderPassTimestampWrites
	{
		public WGPUQuerySet querySet;
		public uint beginningOfPassWriteIndex;
		public uint endOfPassWriteIndex;
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPURequestAdapterOptions
	{
		public WGPUChainedStruct* nextInChain;
		public WGPUSurface compatibleSurface;
		public WGPUPowerPreference powerPreference;
		public WGPUBackendType backendType;
		public WGPUBool forceFallbackAdapter;
		// --- properties
		public WGPUChainedStruct? NextInChain {
			get => ApiUtils.GetOpt(nextInChain);
			set => ApiUtils.SetOpt(out nextInChain, value);
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPUSamplerBindingLayout
	{
		public WGPUChainedStruct* nextInChain;
		public WGPUSamplerBindingType type;
		// --- properties
		public WGPUChainedStruct? NextInChain {
			get => ApiUtils.GetOpt(nextInChain);
			set => ApiUtils.SetOpt(out nextInChain, value);
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPUSamplerDescriptor
	{
		public WGPUChainedStruct* nextInChain;
		public char* label;
		public WGPUAddressMode addressModeU;
		public WGPUAddressMode addressModeV;
		public WGPUAddressMode addressModeW;
		public WGPUFilterMode magFilter;
		public WGPUFilterMode minFilter;
		public WGPUMipmapFilterMode mipmapFilter;
		public float lodMinClamp;
		public float lodMaxClamp;
		public WGPUCompareFunction compare;
		public ushort maxAnisotropy;
		// --- properties
		public WGPUChainedStruct? NextInChain {
			get => ApiUtils.GetOpt(nextInChain);
			set => ApiUtils.SetOpt(out nextInChain, value);
		}
		public ReadOnlySpan<char> Label {
			get => ApiUtils.GetStr(label);
			set => ApiUtils.SetStr(value, out this.label);
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPUShaderModuleCompilationHint
	{
		public WGPUChainedStruct* nextInChain;
		public char* entryPoint;
		public WGPUPipelineLayout layout;
		// --- properties
		public WGPUChainedStruct? NextInChain {
			get => ApiUtils.GetOpt(nextInChain);
			set => ApiUtils.SetOpt(out nextInChain, value);
		}
		public ReadOnlySpan<char> EntryPoint {
			get => ApiUtils.GetStr(entryPoint);
			set => ApiUtils.SetStr(value, out this.entryPoint);
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPUShaderModuleSPIRVDescriptor
	{
		public WGPUChainedStruct chain;
		public uint codeSize;
		public uint* code;
		// --- properties
		public uint? Code {
			get => ApiUtils.GetOpt(code);
			set => ApiUtils.SetOpt(out code, value);
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPUShaderModuleWGSLDescriptor
	{
		public WGPUChainedStruct chain;
		public char* code;
		// --- properties
		public ReadOnlySpan<char> Code {
			get => ApiUtils.GetStr(code);
			set => ApiUtils.SetStr(value, out this.code);
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPUStencilFaceState
	{
		public WGPUCompareFunction compare;
		public WGPUStencilOperation failOp;
		public WGPUStencilOperation depthFailOp;
		public WGPUStencilOperation passOp;
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPUStorageTextureBindingLayout
	{
		public WGPUChainedStruct* nextInChain;
		public WGPUStorageTextureAccess access;
		public WGPUTextureFormat format;
		public WGPUTextureViewDimension viewDimension;
		// --- properties
		public WGPUChainedStruct? NextInChain {
			get => ApiUtils.GetOpt(nextInChain);
			set => ApiUtils.SetOpt(out nextInChain, value);
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPUSurfaceCapabilities
	{
		public WGPUChainedStructOut* nextInChain;
		public WGPUTextureUsage usages;
		public ulong formatCount;
		public WGPUTextureFormat* formats;
		public ulong presentModeCount;
		public WGPUPresentMode* presentModes;
		public ulong alphaModeCount;
		public WGPUCompositeAlphaMode* alphaModes;
		// --- properties
		public WGPUChainedStructOut? NextInChain {
			get => ApiUtils.GetOpt(nextInChain);
			set => ApiUtils.SetOpt(out nextInChain, value);
		}
		public Span<WGPUTextureFormat> Formats {
			get => new (formats, (int)formatCount);
			set => value.SetArr(out formats, out formatCount);
		}
		public Span<WGPUPresentMode> PresentModes {
			get => new (presentModes, (int)presentModeCount);
			set => value.SetArr(out presentModes, out presentModeCount);
		}
		public Span<WGPUCompositeAlphaMode> AlphaModes {
			get => new (alphaModes, (int)alphaModeCount);
			set => value.SetArr(out alphaModes, out alphaModeCount);
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPUSurfaceConfiguration
	{
		public WGPUChainedStruct* nextInChain;
		public WGPUDevice device;
		public WGPUTextureFormat format;
		public WGPUTextureUsage usage;
		public ulong viewFormatCount;
		public WGPUTextureFormat* viewFormats;
		public WGPUCompositeAlphaMode alphaMode;
		public uint width;
		public uint height;
		public WGPUPresentMode presentMode;
		// --- properties
		public WGPUChainedStruct? NextInChain {
			get => ApiUtils.GetOpt(nextInChain);
			set => ApiUtils.SetOpt(out nextInChain, value);
		}
		public Span<WGPUTextureFormat> ViewFormats {
			get => new (viewFormats, (int)viewFormatCount);
			set => value.SetArr(out viewFormats, out viewFormatCount);
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPUSurfaceDescriptor
	{
		public WGPUChainedStruct* nextInChain;
		public char* label;
		// --- properties
		public WGPUChainedStruct? NextInChain {
			get => ApiUtils.GetOpt(nextInChain);
			set => ApiUtils.SetOpt(out nextInChain, value);
		}
		public ReadOnlySpan<char> Label {
			get => ApiUtils.GetStr(label);
			set => ApiUtils.SetStr(value, out this.label);
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPUSurfaceDescriptorFromAndroidNativeWindow
	{
		public WGPUChainedStruct chain;
		public void* window;
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPUSurfaceDescriptorFromCanvasHTMLSelector
	{
		public WGPUChainedStruct chain;
		public char* selector;
		// --- properties
		public ReadOnlySpan<char> Selector {
			get => ApiUtils.GetStr(selector);
			set => ApiUtils.SetStr(value, out this.selector);
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPUSurfaceDescriptorFromMetalLayer
	{
		public WGPUChainedStruct chain;
		public void* layer;
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPUSurfaceDescriptorFromWaylandSurface
	{
		public WGPUChainedStruct chain;
		public void* display;
		public void* surface;
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPUSurfaceDescriptorFromWindowsHWND
	{
		public WGPUChainedStruct chain;
		public void* hinstance;
		public void* hwnd;
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPUSurfaceDescriptorFromXcbWindow
	{
		public WGPUChainedStruct chain;
		public void* connection;
		public uint window;
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPUSurfaceDescriptorFromXlibWindow
	{
		public WGPUChainedStruct chain;
		public void* display;
		public ulong window;
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPUSurfaceTexture
	{
		public WGPUTexture texture;
		public WGPUBool suboptimal;
		public WGPUSurfaceGetCurrentTextureStatus status;
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPUTextureBindingLayout
	{
		public WGPUChainedStruct* nextInChain;
		public WGPUTextureSampleType sampleType;
		public WGPUTextureViewDimension viewDimension;
		public WGPUBool multisampled;
		// --- properties
		public WGPUChainedStruct? NextInChain {
			get => ApiUtils.GetOpt(nextInChain);
			set => ApiUtils.SetOpt(out nextInChain, value);
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPUTextureDataLayout
	{
		public WGPUChainedStruct* nextInChain;
		public ulong offset;
		public uint bytesPerRow;
		public uint rowsPerImage;
		// --- properties
		public WGPUChainedStruct? NextInChain {
			get => ApiUtils.GetOpt(nextInChain);
			set => ApiUtils.SetOpt(out nextInChain, value);
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPUTextureViewDescriptor
	{
		public WGPUChainedStruct* nextInChain;
		public char* label;
		public WGPUTextureFormat format;
		public WGPUTextureViewDimension dimension;
		public uint baseMipLevel;
		public uint mipLevelCount;
		public uint baseArrayLayer;
		public uint arrayLayerCount;
		public WGPUTextureAspect aspect;
		// --- properties
		public WGPUChainedStruct? NextInChain {
			get => ApiUtils.GetOpt(nextInChain);
			set => ApiUtils.SetOpt(out nextInChain, value);
		}
		public ReadOnlySpan<char> Label {
			get => ApiUtils.GetStr(label);
			set => ApiUtils.SetStr(value, out this.label);
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPUUncapturedErrorCallbackInfo
	{
		public WGPUChainedStruct* nextInChain;
		public delegate* unmanaged<WGPUErrorType, char*, void*, void> callback;
		public void* userdata;
		// --- properties
		public WGPUChainedStruct? NextInChain {
			get => ApiUtils.GetOpt(nextInChain);
			set => ApiUtils.SetOpt(out nextInChain, value);
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPUVertexAttribute
	{
		public WGPUVertexFormat format;
		public ulong offset;
		public uint shaderLocation;
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPUBindGroupDescriptor
	{
		public WGPUChainedStruct* nextInChain;
		public char* label;
		public WGPUBindGroupLayout layout;
		public ulong entryCount;
		public WGPUBindGroupEntry* entries;
		// --- properties
		public WGPUChainedStruct? NextInChain {
			get => ApiUtils.GetOpt(nextInChain);
			set => ApiUtils.SetOpt(out nextInChain, value);
		}
		public ReadOnlySpan<char> Label {
			get => ApiUtils.GetStr(label);
			set => ApiUtils.SetStr(value, out this.label);
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPUBindGroupLayoutEntry
	{
		public WGPUChainedStruct* nextInChain;
		public uint binding;
		public WGPUShaderStage visibility;
		public WGPUBufferBindingLayout buffer;
		public WGPUSamplerBindingLayout sampler;
		public WGPUTextureBindingLayout texture;
		public WGPUStorageTextureBindingLayout storageTexture;
		// --- properties
		public WGPUChainedStruct? NextInChain {
			get => ApiUtils.GetOpt(nextInChain);
			set => ApiUtils.SetOpt(out nextInChain, value);
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPUBlendState
	{
		public WGPUBlendComponent color;
		public WGPUBlendComponent alpha;
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPUCompilationInfo
	{
		public WGPUChainedStruct* nextInChain;
		public ulong messageCount;
		public WGPUCompilationMessage* messages;
		// --- properties
		public WGPUChainedStruct? NextInChain {
			get => ApiUtils.GetOpt(nextInChain);
			set => ApiUtils.SetOpt(out nextInChain, value);
		}
		public Span<WGPUCompilationMessage> Messages {
			get => new (messages, (int)messageCount);
			set => value.SetArr(out messages, out messageCount);
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPUComputePassDescriptor
	{
		public WGPUChainedStruct* nextInChain;
		public char* label;
		public WGPUComputePassTimestampWrites* timestampWrites;
		// --- properties
		public WGPUChainedStruct? NextInChain {
			get => ApiUtils.GetOpt(nextInChain);
			set => ApiUtils.SetOpt(out nextInChain, value);
		}
		public ReadOnlySpan<char> Label {
			get => ApiUtils.GetStr(label);
			set => ApiUtils.SetStr(value, out this.label);
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPUDepthStencilState
	{
		public WGPUChainedStruct* nextInChain;
		public WGPUTextureFormat format;
		public WGPUBool depthWriteEnabled;
		public WGPUCompareFunction depthCompare;
		public WGPUStencilFaceState stencilFront;
		public WGPUStencilFaceState stencilBack;
		public uint stencilReadMask;
		public uint stencilWriteMask;
		public int depthBias;
		public float depthBiasSlopeScale;
		public float depthBiasClamp;
		// --- properties
		public WGPUChainedStruct? NextInChain {
			get => ApiUtils.GetOpt(nextInChain);
			set => ApiUtils.SetOpt(out nextInChain, value);
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPUImageCopyBuffer
	{
		public WGPUChainedStruct* nextInChain;
		public WGPUTextureDataLayout layout;
		public WGPUBuffer buffer;
		// --- properties
		public WGPUChainedStruct? NextInChain {
			get => ApiUtils.GetOpt(nextInChain);
			set => ApiUtils.SetOpt(out nextInChain, value);
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPUImageCopyTexture
	{
		public WGPUChainedStruct* nextInChain;
		public WGPUTexture texture;
		public uint mipLevel;
		public WGPUOrigin3D origin;
		public WGPUTextureAspect aspect;
		// --- properties
		public WGPUChainedStruct? NextInChain {
			get => ApiUtils.GetOpt(nextInChain);
			set => ApiUtils.SetOpt(out nextInChain, value);
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPUProgrammableStageDescriptor
	{
		public WGPUChainedStruct* nextInChain;
		public WGPUShaderModule module;
		public char* entryPoint;
		public ulong constantCount;
		public WGPUConstantEntry* constants;
		// --- properties
		public WGPUChainedStruct? NextInChain {
			get => ApiUtils.GetOpt(nextInChain);
			set => ApiUtils.SetOpt(out nextInChain, value);
		}
		public ReadOnlySpan<char> EntryPoint {
			get => ApiUtils.GetStr(entryPoint);
			set => ApiUtils.SetStr(value, out this.entryPoint);
		}
		public Span<WGPUConstantEntry> Constants {
			get => new (constants, (int)constantCount);
			set => value.SetArr(out constants, out constantCount);
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPURenderPassColorAttachment
	{
		public WGPUChainedStruct* nextInChain;
		public WGPUTextureView view;
		public uint depthSlice;
		public WGPUTextureView resolveTarget;
		public WGPULoadOp loadOp;
		public WGPUStoreOp storeOp;
		public WGPUColor clearValue;
		// --- properties
		public WGPUChainedStruct? NextInChain {
			get => ApiUtils.GetOpt(nextInChain);
			set => ApiUtils.SetOpt(out nextInChain, value);
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPURequiredLimits
	{
		public WGPUChainedStruct* nextInChain;
		public WGPULimits limits;
		// --- properties
		public WGPUChainedStruct? NextInChain {
			get => ApiUtils.GetOpt(nextInChain);
			set => ApiUtils.SetOpt(out nextInChain, value);
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPUShaderModuleDescriptor
	{
		public WGPUChainedStruct* nextInChain;
		public char* label;
		public ulong hintCount;
		public WGPUShaderModuleCompilationHint* hints;
		// --- properties
		public WGPUChainedStruct? NextInChain {
			get => ApiUtils.GetOpt(nextInChain);
			set => ApiUtils.SetOpt(out nextInChain, value);
		}
		public ReadOnlySpan<char> Label {
			get => ApiUtils.GetStr(label);
			set => ApiUtils.SetStr(value, out this.label);
		}
		public Span<WGPUShaderModuleCompilationHint> Hints {
			get => new (hints, (int)hintCount);
			set => value.SetArr(out hints, out hintCount);
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPUSupportedLimits
	{
		public WGPUChainedStructOut* nextInChain;
		public WGPULimits limits;
		// --- properties
		public WGPUChainedStructOut? NextInChain {
			get => ApiUtils.GetOpt(nextInChain);
			set => ApiUtils.SetOpt(out nextInChain, value);
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPUTextureDescriptor
	{
		public WGPUChainedStruct* nextInChain;
		public char* label;
		public WGPUTextureUsage usage;
		public WGPUTextureDimension dimension;
		public WGPUExtent3D size;
		public WGPUTextureFormat format;
		public uint mipLevelCount;
		public uint sampleCount;
		public ulong viewFormatCount;
		public WGPUTextureFormat* viewFormats;
		// --- properties
		public WGPUChainedStruct? NextInChain {
			get => ApiUtils.GetOpt(nextInChain);
			set => ApiUtils.SetOpt(out nextInChain, value);
		}
		public ReadOnlySpan<char> Label {
			get => ApiUtils.GetStr(label);
			set => ApiUtils.SetStr(value, out this.label);
		}
		public Span<WGPUTextureFormat> ViewFormats {
			get => new (viewFormats, (int)viewFormatCount);
			set => value.SetArr(out viewFormats, out viewFormatCount);
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPUVertexBufferLayout
	{
		public ulong arrayStride;
		public WGPUVertexStepMode stepMode;
		public ulong attributeCount;
		public WGPUVertexAttribute* attributes;
		// --- properties
		public Span<WGPUVertexAttribute> Attributes {
			get => new (attributes, (int)attributeCount);
			set => value.SetArr(out attributes, out attributeCount);
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPUBindGroupLayoutDescriptor
	{
		public WGPUChainedStruct* nextInChain;
		public char* label;
		public ulong entryCount;
		public WGPUBindGroupLayoutEntry* entries;
		// --- properties
		public WGPUChainedStruct? NextInChain {
			get => ApiUtils.GetOpt(nextInChain);
			set => ApiUtils.SetOpt(out nextInChain, value);
		}
		public ReadOnlySpan<char> Label {
			get => ApiUtils.GetStr(label);
			set => ApiUtils.SetStr(value, out this.label);
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPUColorTargetState
	{
		public WGPUChainedStruct* nextInChain;
		public WGPUTextureFormat format;
		public WGPUBlendState* blend;
		public WGPUColorWriteMask writeMask;
		// --- properties
		public WGPUChainedStruct? NextInChain {
			get => ApiUtils.GetOpt(nextInChain);
			set => ApiUtils.SetOpt(out nextInChain, value);
		}
		public WGPUBlendState? Blend {
			get => ApiUtils.GetOpt(blend);
			set => ApiUtils.SetOpt(out blend, value);
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPUComputePipelineDescriptor
	{
		public WGPUChainedStruct* nextInChain;
		public char* label;
		public WGPUPipelineLayout layout;
		public WGPUProgrammableStageDescriptor compute;
		// --- properties
		public WGPUChainedStruct? NextInChain {
			get => ApiUtils.GetOpt(nextInChain);
			set => ApiUtils.SetOpt(out nextInChain, value);
		}
		public ReadOnlySpan<char> Label {
			get => ApiUtils.GetStr(label);
			set => ApiUtils.SetStr(value, out this.label);
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPUDeviceDescriptor
	{
		public WGPUChainedStruct* nextInChain;
		public char* label;
		public ulong requiredFeatureCount;
		public WGPUFeatureName* requiredFeatures;
		public WGPURequiredLimits* requiredLimits;
		public WGPUQueueDescriptor defaultQueue;
		public delegate* unmanaged<WGPUDeviceLostReason, char*, void*, void> deviceLostCallback;
		public void* deviceLostUserdata;
		public WGPUUncapturedErrorCallbackInfo uncapturedErrorCallbackInfo;
		// --- properties
		public WGPUChainedStruct? NextInChain {
			get => ApiUtils.GetOpt(nextInChain);
			set => ApiUtils.SetOpt(out nextInChain, value);
		}
		public ReadOnlySpan<char> Label {
			get => ApiUtils.GetStr(label);
			set => ApiUtils.SetStr(value, out this.label);
		}
		public Span<WGPUFeatureName> RequiredFeatures {
			get => new (requiredFeatures, (int)requiredFeatureCount);
			set => value.SetArr(out requiredFeatures, out requiredFeatureCount);
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPURenderPassDescriptor
	{
		public WGPUChainedStruct* nextInChain;
		public char* label;
		public ulong colorAttachmentCount;
		public WGPURenderPassColorAttachment* colorAttachments;
		public WGPURenderPassDepthStencilAttachment* depthStencilAttachment;
		public WGPUQuerySet occlusionQuerySet;
		public WGPURenderPassTimestampWrites* timestampWrites;
		// --- properties
		public WGPUChainedStruct? NextInChain {
			get => ApiUtils.GetOpt(nextInChain);
			set => ApiUtils.SetOpt(out nextInChain, value);
		}
		public ReadOnlySpan<char> Label {
			get => ApiUtils.GetStr(label);
			set => ApiUtils.SetStr(value, out this.label);
		}
		public Span<WGPURenderPassColorAttachment> ColorAttachments {
			get => new (colorAttachments, (int)colorAttachmentCount);
			set => value.SetArr(out colorAttachments, out colorAttachmentCount);
		}
		public WGPURenderPassDepthStencilAttachment? DepthStencilAttachment {
			get => ApiUtils.GetOpt(depthStencilAttachment);
			set => ApiUtils.SetOpt(out depthStencilAttachment, value);
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPUVertexState
	{
		public WGPUChainedStruct* nextInChain;
		public WGPUShaderModule module;
		public char* entryPoint;
		public ulong constantCount;
		public WGPUConstantEntry* constants;
		public ulong bufferCount;
		public WGPUVertexBufferLayout* buffers;
		// --- properties
		public WGPUChainedStruct? NextInChain {
			get => ApiUtils.GetOpt(nextInChain);
			set => ApiUtils.SetOpt(out nextInChain, value);
		}
		public ReadOnlySpan<char> EntryPoint {
			get => ApiUtils.GetStr(entryPoint);
			set => ApiUtils.SetStr(value, out this.entryPoint);
		}
		public Span<WGPUConstantEntry> Constants {
			get => new (constants, (int)constantCount);
			set => value.SetArr(out constants, out constantCount);
		}
		public Span<WGPUVertexBufferLayout> Buffers {
			get => new (buffers, (int)bufferCount);
			set => value.SetArr(out buffers, out bufferCount);
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPUFragmentState
	{
		public WGPUChainedStruct* nextInChain;
		public WGPUShaderModule module;
		public char* entryPoint;
		public ulong constantCount;
		public WGPUConstantEntry* constants;
		public ulong targetCount;
		public WGPUColorTargetState* targets;
		// --- properties
		public WGPUChainedStruct? NextInChain {
			get => ApiUtils.GetOpt(nextInChain);
			set => ApiUtils.SetOpt(out nextInChain, value);
		}
		public ReadOnlySpan<char> EntryPoint {
			get => ApiUtils.GetStr(entryPoint);
			set => ApiUtils.SetStr(value, out this.entryPoint);
		}
		public Span<WGPUConstantEntry> Constants {
			get => new (constants, (int)constantCount);
			set => value.SetArr(out constants, out constantCount);
		}
		public Span<WGPUColorTargetState> Targets {
			get => new (targets, (int)targetCount);
			set => value.SetArr(out targets, out targetCount);
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPURenderPipelineDescriptor
	{
		public WGPUChainedStruct* nextInChain;
		public char* label;
		public WGPUPipelineLayout layout;
		public WGPUVertexState vertex;
		public WGPUPrimitiveState primitive;
		public WGPUDepthStencilState* depthStencil;
		public WGPUMultisampleState multisample;
		public WGPUFragmentState* fragment;
		// --- properties
		public WGPUChainedStruct? NextInChain {
			get => ApiUtils.GetOpt(nextInChain);
			set => ApiUtils.SetOpt(out nextInChain, value);
		}
		public ReadOnlySpan<char> Label {
			get => ApiUtils.GetStr(label);
			set => ApiUtils.SetStr(value, out this.label);
		}
		public WGPUDepthStencilState? DepthStencil {
			get => ApiUtils.GetOpt(depthStencil);
			set => ApiUtils.SetOpt(out depthStencil, value);
		}
		public WGPUFragmentState? Fragment {
			get => ApiUtils.GetOpt(fragment);
			set => ApiUtils.SetOpt(out fragment, value);
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPUChainedStruct
	{
		public WGPUChainedStruct* next;
		public WGPUSType sType;
		// --- properties
		public WGPUChainedStruct? Next {
			get => ApiUtils.GetOpt(next);
			set => ApiUtils.SetOpt(out next, value);
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPUChainedStructOut
	{
		public WGPUChainedStructOut* next;
		public WGPUSType sType;
		// --- properties
		public WGPUChainedStructOut? Next {
			get => ApiUtils.GetOpt(next);
			set => ApiUtils.SetOpt(out next, value);
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPUInstanceExtras
	{
		public WGPUChainedStruct chain;
		public WGPUInstanceBackend backends;
		public WGPUInstance flags;
		public WGPUDx12Compiler dx12ShaderCompiler;
		public WGPUGles3MinorVersion gles3MinorVersion;
		public char* dxilPath;
		public char* dxcPath;
		// --- properties
		public ReadOnlySpan<char> DxilPath {
			get => ApiUtils.GetStr(dxilPath);
			set => ApiUtils.SetStr(value, out this.dxilPath);
		}
		public ReadOnlySpan<char> DxcPath {
			get => ApiUtils.GetStr(dxcPath);
			set => ApiUtils.SetStr(value, out this.dxcPath);
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPUDeviceExtras
	{
		public WGPUChainedStruct chain;
		public char* tracePath;
		// --- properties
		public ReadOnlySpan<char> TracePath {
			get => ApiUtils.GetStr(tracePath);
			set => ApiUtils.SetStr(value, out this.tracePath);
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPUNativeLimits
	{
		public uint maxPushConstantSize;
		public uint maxNonSamplerBindings;
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPURequiredLimitsExtras
	{
		public WGPUChainedStruct chain;
		public WGPUNativeLimits limits;
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPUSupportedLimitsExtras
	{
		public WGPUChainedStructOut chain;
		public WGPUNativeLimits limits;
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPUPushConstantRange
	{
		public WGPUShaderStage stages;
		public uint start;
		public uint end;
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPUPipelineLayoutExtras
	{
		public WGPUChainedStruct chain;
		public ulong pushConstantRangeCount;
		public WGPUPushConstantRange* pushConstantRanges;
		// --- properties
		public Span<WGPUPushConstantRange> PushConstantRanges {
			get => new (pushConstantRanges, (int)pushConstantRangeCount);
			set => value.SetArr(out pushConstantRanges, out pushConstantRangeCount);
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPUWrappedSubmissionIndex
	{
		public WGPUQueue queue;
		public ulong submissionIndex;
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPUShaderDefine
	{
		public char* name;
		public char* value;
		// --- properties
		public ReadOnlySpan<char> Name {
			get => ApiUtils.GetStr(name);
			set => ApiUtils.SetStr(value, out this.name);
		}
		public ReadOnlySpan<char> Value {
			get => ApiUtils.GetStr(value);
			set => ApiUtils.SetStr(value, out this.value);
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPUShaderModuleGLSLDescriptor
	{
		public WGPUChainedStruct chain;
		public WGPUShaderStage stage;
		public char* code;
		public uint defineCount;
		public WGPUShaderDefine* defines;
		// --- properties
		public ReadOnlySpan<char> Code {
			get => ApiUtils.GetStr(code);
			set => ApiUtils.SetStr(value, out this.code);
		}
		public Span<WGPUShaderDefine> Defines {
			get => new (defines, (int)defineCount);
			set => value.SetArr(out defines, out defineCount);
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPURegistryReport
	{
		public ulong numAllocated;
		public ulong numKeptFromUser;
		public ulong numReleasedFromUser;
		public ulong numError;
		public ulong elementSize;
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPUHubReport
	{
		public WGPURegistryReport adapters;
		public WGPURegistryReport devices;
		public WGPURegistryReport queues;
		public WGPURegistryReport pipelineLayouts;
		public WGPURegistryReport shaderModules;
		public WGPURegistryReport bindGroupLayouts;
		public WGPURegistryReport bindGroups;
		public WGPURegistryReport commandBuffers;
		public WGPURegistryReport renderBundles;
		public WGPURegistryReport renderPipelines;
		public WGPURegistryReport computePipelines;
		public WGPURegistryReport querySets;
		public WGPURegistryReport buffers;
		public WGPURegistryReport textures;
		public WGPURegistryReport textureViews;
		public WGPURegistryReport samplers;
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPUGlobalReport
	{
		public WGPURegistryReport surfaces;
		public WGPUBackendType backendType;
		public WGPUHubReport vulkan;
		public WGPUHubReport metal;
		public WGPUHubReport dx12;
		public WGPUHubReport gl;
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPUInstanceEnumerateAdapterOptions
	{
		public WGPUChainedStruct* nextInChain;
		public WGPUInstanceBackend backends;
		// --- properties
		public WGPUChainedStruct? NextInChain {
			get => ApiUtils.GetOpt(nextInChain);
			set => ApiUtils.SetOpt(out nextInChain, value);
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPUBindGroupEntryExtras
	{
		public WGPUChainedStruct chain;
		public WGPUBuffer* buffers;
		public ulong bufferCount;
		public WGPUSampler* samplers;
		public ulong samplerCount;
		public WGPUTextureView* textureViews;
		public ulong textureViewCount;
		// --- properties
		public Span<WGPUBuffer> Buffers {
			get => new (buffers, (int)bufferCount);
			set => value.SetArr(out buffers, out bufferCount);
		}
		public Span<WGPUSampler> Samplers {
			get => new (samplers, (int)samplerCount);
			set => value.SetArr(out samplers, out samplerCount);
		}
		public Span<WGPUTextureView> TextureViews {
			get => new (textureViews, (int)textureViewCount);
			set => value.SetArr(out textureViews, out textureViewCount);
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPUBindGroupLayoutEntryExtras
	{
		public WGPUChainedStruct chain;
		public uint count;
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPUQuerySetDescriptorExtras
	{
		public WGPUChainedStruct chain;
		public WGPUPipelineStatisticName* pipelineStatistics;
		public ulong pipelineStatisticCount;
		// --- properties
		public Span<WGPUPipelineStatisticName> PipelineStatistics {
			get => new (pipelineStatistics, (int)pipelineStatisticCount);
			set => value.SetArr(out pipelineStatistics, out pipelineStatisticCount);
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct WGPUSurfaceConfigurationExtras
	{
		public WGPUChainedStruct chain;
		public uint desiredMaximumFrameLatency;
	}

}

