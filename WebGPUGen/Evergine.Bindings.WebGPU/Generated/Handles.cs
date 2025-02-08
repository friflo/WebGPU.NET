// ReSharper disable InconsistentNaming
// ReSharper disable StructCanBeMadeReadOnly
// ReSharper disable ConvertToPrimaryConstructor
// ReSharper disable MemberCanBePrivate.Global
namespace Evergine.Bindings.WebGPU
{
	public partial struct WGPUAdapter : IEquatable<WGPUAdapter>
	{
	public readonly IntPtr Handle;
	public bool Equals(WGPUAdapter h) => Handle == h.Handle;
	public override bool Equals(object? o) => o is WGPUAdapter h && Equals(h);
	public override int GetHashCode() => Handle.GetHashCode();
	}

	public partial struct WGPUBindGroup : IEquatable<WGPUBindGroup>
	{
	public readonly IntPtr Handle;
	public bool Equals(WGPUBindGroup h) => Handle == h.Handle;
	public override bool Equals(object? o) => o is WGPUBindGroup h && Equals(h);
	public override int GetHashCode() => Handle.GetHashCode();
	}

	public partial struct WGPUBindGroupLayout : IEquatable<WGPUBindGroupLayout>
	{
	public readonly IntPtr Handle;
	public bool Equals(WGPUBindGroupLayout h) => Handle == h.Handle;
	public override bool Equals(object? o) => o is WGPUBindGroupLayout h && Equals(h);
	public override int GetHashCode() => Handle.GetHashCode();
	}

	public partial struct WGPUBuffer : IEquatable<WGPUBuffer>
	{
	public readonly IntPtr Handle;
	public bool Equals(WGPUBuffer h) => Handle == h.Handle;
	public override bool Equals(object? o) => o is WGPUBuffer h && Equals(h);
	public override int GetHashCode() => Handle.GetHashCode();
	}

	public partial struct WGPUCommandBuffer : IEquatable<WGPUCommandBuffer>
	{
	public readonly IntPtr Handle;
	public bool Equals(WGPUCommandBuffer h) => Handle == h.Handle;
	public override bool Equals(object? o) => o is WGPUCommandBuffer h && Equals(h);
	public override int GetHashCode() => Handle.GetHashCode();
	}

	public partial struct WGPUCommandEncoder : IEquatable<WGPUCommandEncoder>
	{
	public readonly IntPtr Handle;
	public bool Equals(WGPUCommandEncoder h) => Handle == h.Handle;
	public override bool Equals(object? o) => o is WGPUCommandEncoder h && Equals(h);
	public override int GetHashCode() => Handle.GetHashCode();
	}

	public partial struct WGPUComputePassEncoder : IEquatable<WGPUComputePassEncoder>
	{
	public readonly IntPtr Handle;
	public bool Equals(WGPUComputePassEncoder h) => Handle == h.Handle;
	public override bool Equals(object? o) => o is WGPUComputePassEncoder h && Equals(h);
	public override int GetHashCode() => Handle.GetHashCode();
	}

	public partial struct WGPUComputePipeline : IEquatable<WGPUComputePipeline>
	{
	public readonly IntPtr Handle;
	public bool Equals(WGPUComputePipeline h) => Handle == h.Handle;
	public override bool Equals(object? o) => o is WGPUComputePipeline h && Equals(h);
	public override int GetHashCode() => Handle.GetHashCode();
	}

	public partial struct WGPUDevice : IEquatable<WGPUDevice>
	{
	public readonly IntPtr Handle;
	public bool Equals(WGPUDevice h) => Handle == h.Handle;
	public override bool Equals(object? o) => o is WGPUDevice h && Equals(h);
	public override int GetHashCode() => Handle.GetHashCode();
	}

	public partial struct WGPUInstance : IEquatable<WGPUInstance>
	{
	public readonly IntPtr Handle;
	public bool Equals(WGPUInstance h) => Handle == h.Handle;
	public override bool Equals(object? o) => o is WGPUInstance h && Equals(h);
	public override int GetHashCode() => Handle.GetHashCode();
	}

	public partial struct WGPUPipelineLayout : IEquatable<WGPUPipelineLayout>
	{
	public readonly IntPtr Handle;
	public bool Equals(WGPUPipelineLayout h) => Handle == h.Handle;
	public override bool Equals(object? o) => o is WGPUPipelineLayout h && Equals(h);
	public override int GetHashCode() => Handle.GetHashCode();
	}

	public partial struct WGPUQuerySet : IEquatable<WGPUQuerySet>
	{
	public readonly IntPtr Handle;
	public bool Equals(WGPUQuerySet h) => Handle == h.Handle;
	public override bool Equals(object? o) => o is WGPUQuerySet h && Equals(h);
	public override int GetHashCode() => Handle.GetHashCode();
	}

	public partial struct WGPUQueue : IEquatable<WGPUQueue>
	{
	public readonly IntPtr Handle;
	public bool Equals(WGPUQueue h) => Handle == h.Handle;
	public override bool Equals(object? o) => o is WGPUQueue h && Equals(h);
	public override int GetHashCode() => Handle.GetHashCode();
	}

	public partial struct WGPURenderBundle : IEquatable<WGPURenderBundle>
	{
	public readonly IntPtr Handle;
	public bool Equals(WGPURenderBundle h) => Handle == h.Handle;
	public override bool Equals(object? o) => o is WGPURenderBundle h && Equals(h);
	public override int GetHashCode() => Handle.GetHashCode();
	}

	public partial struct WGPURenderBundleEncoder : IEquatable<WGPURenderBundleEncoder>
	{
	public readonly IntPtr Handle;
	public bool Equals(WGPURenderBundleEncoder h) => Handle == h.Handle;
	public override bool Equals(object? o) => o is WGPURenderBundleEncoder h && Equals(h);
	public override int GetHashCode() => Handle.GetHashCode();
	}

	public partial struct WGPURenderPassEncoder : IEquatable<WGPURenderPassEncoder>
	{
	public readonly IntPtr Handle;
	public bool Equals(WGPURenderPassEncoder h) => Handle == h.Handle;
	public override bool Equals(object? o) => o is WGPURenderPassEncoder h && Equals(h);
	public override int GetHashCode() => Handle.GetHashCode();
	}

	public partial struct WGPURenderPipeline : IEquatable<WGPURenderPipeline>
	{
	public readonly IntPtr Handle;
	public bool Equals(WGPURenderPipeline h) => Handle == h.Handle;
	public override bool Equals(object? o) => o is WGPURenderPipeline h && Equals(h);
	public override int GetHashCode() => Handle.GetHashCode();
	}

	public partial struct WGPUSampler : IEquatable<WGPUSampler>
	{
	public readonly IntPtr Handle;
	public bool Equals(WGPUSampler h) => Handle == h.Handle;
	public override bool Equals(object? o) => o is WGPUSampler h && Equals(h);
	public override int GetHashCode() => Handle.GetHashCode();
	}

	public partial struct WGPUShaderModule : IEquatable<WGPUShaderModule>
	{
	public readonly IntPtr Handle;
	public bool Equals(WGPUShaderModule h) => Handle == h.Handle;
	public override bool Equals(object? o) => o is WGPUShaderModule h && Equals(h);
	public override int GetHashCode() => Handle.GetHashCode();
	}

	public partial struct WGPUSurface : IEquatable<WGPUSurface>
	{
	public readonly IntPtr Handle;
	public bool Equals(WGPUSurface h) => Handle == h.Handle;
	public override bool Equals(object? o) => o is WGPUSurface h && Equals(h);
	public override int GetHashCode() => Handle.GetHashCode();
	}

	public partial struct WGPUTexture : IEquatable<WGPUTexture>
	{
	public readonly IntPtr Handle;
	public bool Equals(WGPUTexture h) => Handle == h.Handle;
	public override bool Equals(object? o) => o is WGPUTexture h && Equals(h);
	public override int GetHashCode() => Handle.GetHashCode();
	}

	public partial struct WGPUTextureView : IEquatable<WGPUTextureView>
	{
	public readonly IntPtr Handle;
	public bool Equals(WGPUTextureView h) => Handle == h.Handle;
	public override bool Equals(object? o) => o is WGPUTextureView h && Equals(h);
	public override int GetHashCode() => Handle.GetHashCode();
	}

}
