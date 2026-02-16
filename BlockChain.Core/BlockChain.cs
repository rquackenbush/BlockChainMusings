using System.Collections.Immutable;

namespace BlockChain.Core;

/// <summary>
/// Trivial implementation of a block chain.
/// </summary>
public class BlockChain
{
    private readonly List<Block> _blocks = [];

    private readonly HashCalculator _hashCalculator;

    public BlockChain(HashCalculator hashCalculator)
    {
        _hashCalculator = hashCalculator;

        //Add genesis block
        _blocks.Add(new Block
        {
            Id = Guid.Empty,
            Data = Array.Empty<byte>().ToImmutableArray(),
            Hash = new byte[_hashCalculator.HashAlgorithm.HashSize].ToImmutableArray(),
            Timestamp = DateTime.UtcNow
        });
    }

    public Block Add(byte[] data)
    {
        var previousBlock = _blocks.Last();

        var timestamp = DateTime.UtcNow;
        var id = Guid.NewGuid();

        var calcRequest = new BlockBase
        {
            Id = id,
            Data = data.ToImmutableArray(),
            Timestamp = timestamp
        };

        var hash = _hashCalculator.CalculateHash(calcRequest, previousBlock.Hash);

        var block = new Block
        {
            Id = id,
            Data = data.ToImmutableArray(),
            Hash = hash.ToImmutableArray(),
            Timestamp = timestamp
        };

        _blocks.Add(block);

        return block;

    }

    public IEnumerable<Block> Blocks => _blocks;
}
