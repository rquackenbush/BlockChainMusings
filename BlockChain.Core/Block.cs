using System.Collections.Immutable;
using System.ComponentModel;

namespace BlockChain.Core;

[ImmutableObject(true)]
public class Block : BlockBase
{
    public required ImmutableArray<byte> Hash { get; init; }
}

[ImmutableObject(true)]
public class BlockBase
{
    public required Guid Id { get; init; }

    public required ImmutableArray<byte> Data { get; init; }

    public required DateTime Timestamp { get; init; }
}
