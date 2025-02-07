using System.Runtime.InteropServices;

namespace Evergine.Bindings.WebGPU;

[StructLayout(LayoutKind.Explicit, Size = 8)]
internal struct AllocHeader
{
    [FieldOffset (0)] internal ushort       allocatorIndex;     //  2
    [FieldOffset (2)] internal ushort       size;               //  2
    [FieldOffset (4)] internal ArenaVersion version;            //  4
    
    public override string ToString() {
        
        return $"allocator: {version.allocator} reset: {version.reset}";
    }
}

[StructLayout(LayoutKind.Explicit, Size = 4)]
internal struct ArenaVersion
{
    [FieldOffset (0)] internal uint   all;          //  4
    [FieldOffset (0)] internal ushort allocator;    //  2
    [FieldOffset (2)] internal ushort reset;        //  2

    public override string ToString() => $"allocator: {allocator} reset: {reset}";
}

public static class AllocValidator
{
    private static ArenaVersion[] _arenaVersions = [];
    private static ArenaVersion _nextVersion;
    
    internal static AllocHeader GetArenaHeader() {
        _nextVersion.allocator++;
        _nextVersion.reset = 0;
        var len = _arenaVersions.Length;
        var versions = new ArenaVersion[len + 1];
        Array.Copy(_arenaVersions, 0, versions, 0, len);
        versions[len] = _nextVersion;
        _arenaVersions = versions;
        var header = new AllocHeader {
            version         = _nextVersion,
            allocatorIndex  = (ushort)len
        };
        return header;
    }
    
    internal static void UpdateResetVersion(AllocHeader header) {
        _arenaVersions[header.allocatorIndex] = header.version;
    }

    public static unsafe void ValidatePtr(void* ptr)
    {
        if (ptr == null) {
            return;
        }
        AllocHeader header  = *(((AllocHeader*)ptr) - 1);
        var index           = header.allocatorIndex;
        var versions        = _arenaVersions;
        if (index >= versions.Length) {
            throw new InvalidOperationException();
        }
        var version = versions[index];
        if(version.all == header.version.all) {
            return;
        }
        if (version.allocator != header.version.allocator) {
            throw new InvalidAllocation("pointer expired. Arena already disposed");
        }
        throw new InvalidAllocation("pointer expired by Arena.Reset()");
    }
}

public class InvalidAllocation : InvalidOperationException {
    internal InvalidAllocation(string message) : base(message) { }
}