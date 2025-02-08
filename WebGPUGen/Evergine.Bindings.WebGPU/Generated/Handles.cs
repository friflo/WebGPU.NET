#pragma warning disable CS0649 // Field is never assigned to, and will always have its default value
// ReSharper disable InconsistentNaming
// ReSharper disable StructCanBeMadeReadOnly
// ReSharper disable ConvertToPrimaryConstructor
// ReSharper disable MemberCanBePrivate.Global
namespace Evergine.Bindings.WebGPU
{
	public readonly partial struct WGPUAdapter : IEquatable<WGPUAdapter>, IHandle
	{
		internal readonly   IntPtr  Handle;
		public              IntPtr  GetHandle()           => Handle;
		public              bool    Equals(WGPUAdapter h) => Handle == h.Handle;
		public   override   bool    Equals(object? o)     => o is WGPUAdapter h && Equals(h);
		public   override   int     GetHashCode()         => Handle.GetHashCode();
	}

	public readonly partial struct WGPUBindGroup : IEquatable<WGPUBindGroup>, IHandle
	{
		internal readonly   IntPtr  Handle;
		public              IntPtr  GetHandle()           => Handle;
		public              bool    Equals(WGPUBindGroup h) => Handle == h.Handle;
		public   override   bool    Equals(object? o)     => o is WGPUBindGroup h && Equals(h);
		public   override   int     GetHashCode()         => Handle.GetHashCode();
	}

	public readonly partial struct WGPUBindGroupLayout : IEquatable<WGPUBindGroupLayout>, IHandle
	{
		internal readonly   IntPtr  Handle;
		public              IntPtr  GetHandle()           => Handle;
		public              bool    Equals(WGPUBindGroupLayout h) => Handle == h.Handle;
		public   override   bool    Equals(object? o)     => o is WGPUBindGroupLayout h && Equals(h);
		public   override   int     GetHashCode()         => Handle.GetHashCode();
	}

	public readonly partial struct WGPUBuffer : IEquatable<WGPUBuffer>, IHandle
	{
		internal readonly   IntPtr  Handle;
		public              IntPtr  GetHandle()           => Handle;
		public              bool    Equals(WGPUBuffer h) => Handle == h.Handle;
		public   override   bool    Equals(object? o)     => o is WGPUBuffer h && Equals(h);
		public   override   int     GetHashCode()         => Handle.GetHashCode();
	}

	public readonly partial struct WGPUCommandBuffer : IEquatable<WGPUCommandBuffer>, IHandle
	{
		internal readonly   IntPtr  Handle;
		public              IntPtr  GetHandle()           => Handle;
		public              bool    Equals(WGPUCommandBuffer h) => Handle == h.Handle;
		public   override   bool    Equals(object? o)     => o is WGPUCommandBuffer h && Equals(h);
		public   override   int     GetHashCode()         => Handle.GetHashCode();
	}

	public readonly partial struct WGPUCommandEncoder : IEquatable<WGPUCommandEncoder>, IHandle
	{
		internal readonly   IntPtr  Handle;
		public              IntPtr  GetHandle()           => Handle;
		public              bool    Equals(WGPUCommandEncoder h) => Handle == h.Handle;
		public   override   bool    Equals(object? o)     => o is WGPUCommandEncoder h && Equals(h);
		public   override   int     GetHashCode()         => Handle.GetHashCode();
	}

	public readonly partial struct WGPUComputePassEncoder : IEquatable<WGPUComputePassEncoder>, IHandle
	{
		internal readonly   IntPtr  Handle;
		public              IntPtr  GetHandle()           => Handle;
		public              bool    Equals(WGPUComputePassEncoder h) => Handle == h.Handle;
		public   override   bool    Equals(object? o)     => o is WGPUComputePassEncoder h && Equals(h);
		public   override   int     GetHashCode()         => Handle.GetHashCode();
	}

	public readonly partial struct WGPUComputePipeline : IEquatable<WGPUComputePipeline>, IHandle
	{
		internal readonly   IntPtr  Handle;
		public              IntPtr  GetHandle()           => Handle;
		public              bool    Equals(WGPUComputePipeline h) => Handle == h.Handle;
		public   override   bool    Equals(object? o)     => o is WGPUComputePipeline h && Equals(h);
		public   override   int     GetHashCode()         => Handle.GetHashCode();
	}

	public readonly partial struct WGPUDevice : IEquatable<WGPUDevice>, IHandle
	{
		internal readonly   IntPtr  Handle;
		public              IntPtr  GetHandle()           => Handle;
		public              bool    Equals(WGPUDevice h) => Handle == h.Handle;
		public   override   bool    Equals(object? o)     => o is WGPUDevice h && Equals(h);
		public   override   int     GetHashCode()         => Handle.GetHashCode();
	}

	public readonly partial struct WGPUInstance : IEquatable<WGPUInstance>, IHandle
	{
		internal readonly   IntPtr  Handle;
		public              IntPtr  GetHandle()           => Handle;
		public              bool    Equals(WGPUInstance h) => Handle == h.Handle;
		public   override   bool    Equals(object? o)     => o is WGPUInstance h && Equals(h);
		public   override   int     GetHashCode()         => Handle.GetHashCode();
	}

	public readonly partial struct WGPUPipelineLayout : IEquatable<WGPUPipelineLayout>, IHandle
	{
		internal readonly   IntPtr  Handle;
		public              IntPtr  GetHandle()           => Handle;
		public              bool    Equals(WGPUPipelineLayout h) => Handle == h.Handle;
		public   override   bool    Equals(object? o)     => o is WGPUPipelineLayout h && Equals(h);
		public   override   int     GetHashCode()         => Handle.GetHashCode();
	}

	public readonly partial struct WGPUQuerySet : IEquatable<WGPUQuerySet>, IHandle
	{
		internal readonly   IntPtr  Handle;
		public              IntPtr  GetHandle()           => Handle;
		public              bool    Equals(WGPUQuerySet h) => Handle == h.Handle;
		public   override   bool    Equals(object? o)     => o is WGPUQuerySet h && Equals(h);
		public   override   int     GetHashCode()         => Handle.GetHashCode();
	}

	public readonly partial struct WGPUQueue : IEquatable<WGPUQueue>, IHandle
	{
		internal readonly   IntPtr  Handle;
		public              IntPtr  GetHandle()           => Handle;
		public              bool    Equals(WGPUQueue h) => Handle == h.Handle;
		public   override   bool    Equals(object? o)     => o is WGPUQueue h && Equals(h);
		public   override   int     GetHashCode()         => Handle.GetHashCode();
	}

	public readonly partial struct WGPURenderBundle : IEquatable<WGPURenderBundle>, IHandle
	{
		internal readonly   IntPtr  Handle;
		public              IntPtr  GetHandle()           => Handle;
		public              bool    Equals(WGPURenderBundle h) => Handle == h.Handle;
		public   override   bool    Equals(object? o)     => o is WGPURenderBundle h && Equals(h);
		public   override   int     GetHashCode()         => Handle.GetHashCode();
	}

	public readonly partial struct WGPURenderBundleEncoder : IEquatable<WGPURenderBundleEncoder>, IHandle
	{
		internal readonly   IntPtr  Handle;
		public              IntPtr  GetHandle()           => Handle;
		public              bool    Equals(WGPURenderBundleEncoder h) => Handle == h.Handle;
		public   override   bool    Equals(object? o)     => o is WGPURenderBundleEncoder h && Equals(h);
		public   override   int     GetHashCode()         => Handle.GetHashCode();
	}

	public readonly partial struct WGPURenderPassEncoder : IEquatable<WGPURenderPassEncoder>, IHandle
	{
		internal readonly   IntPtr  Handle;
		public              IntPtr  GetHandle()           => Handle;
		public              bool    Equals(WGPURenderPassEncoder h) => Handle == h.Handle;
		public   override   bool    Equals(object? o)     => o is WGPURenderPassEncoder h && Equals(h);
		public   override   int     GetHashCode()         => Handle.GetHashCode();
	}

	public readonly partial struct WGPURenderPipeline : IEquatable<WGPURenderPipeline>, IHandle
	{
		internal readonly   IntPtr  Handle;
		public              IntPtr  GetHandle()           => Handle;
		public              bool    Equals(WGPURenderPipeline h) => Handle == h.Handle;
		public   override   bool    Equals(object? o)     => o is WGPURenderPipeline h && Equals(h);
		public   override   int     GetHashCode()         => Handle.GetHashCode();
	}

	public readonly partial struct WGPUSampler : IEquatable<WGPUSampler>, IHandle
	{
		internal readonly   IntPtr  Handle;
		public              IntPtr  GetHandle()           => Handle;
		public              bool    Equals(WGPUSampler h) => Handle == h.Handle;
		public   override   bool    Equals(object? o)     => o is WGPUSampler h && Equals(h);
		public   override   int     GetHashCode()         => Handle.GetHashCode();
	}

	public readonly partial struct WGPUShaderModule : IEquatable<WGPUShaderModule>, IHandle
	{
		internal readonly   IntPtr  Handle;
		public              IntPtr  GetHandle()           => Handle;
		public              bool    Equals(WGPUShaderModule h) => Handle == h.Handle;
		public   override   bool    Equals(object? o)     => o is WGPUShaderModule h && Equals(h);
		public   override   int     GetHashCode()         => Handle.GetHashCode();
	}

	public readonly partial struct WGPUSurface : IEquatable<WGPUSurface>, IHandle
	{
		internal readonly   IntPtr  Handle;
		public              IntPtr  GetHandle()           => Handle;
		public              bool    Equals(WGPUSurface h) => Handle == h.Handle;
		public   override   bool    Equals(object? o)     => o is WGPUSurface h && Equals(h);
		public   override   int     GetHashCode()         => Handle.GetHashCode();
	}

	public readonly partial struct WGPUTexture : IEquatable<WGPUTexture>, IHandle
	{
		internal readonly   IntPtr  Handle;
		public              IntPtr  GetHandle()           => Handle;
		public              bool    Equals(WGPUTexture h) => Handle == h.Handle;
		public   override   bool    Equals(object? o)     => o is WGPUTexture h && Equals(h);
		public   override   int     GetHashCode()         => Handle.GetHashCode();
	}

	public readonly partial struct WGPUTextureView : IEquatable<WGPUTextureView>, IHandle
	{
		internal readonly   IntPtr  Handle;
		public              IntPtr  GetHandle()           => Handle;
		public              bool    Equals(WGPUTextureView h) => Handle == h.Handle;
		public   override   bool    Equals(object? o)     => o is WGPUTextureView h && Equals(h);
		public   override   int     GetHashCode()         => Handle.GetHashCode();
	}

}
