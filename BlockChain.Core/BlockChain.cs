using System.Security.Cryptography;
using System.Text;

namespace BlockChain.Core;

public class BlockChain
{
    private readonly List<Block> _blocks = [];

    private static readonly SHA256 _sha = SHA256.Create();

    private static readonly HashCalculator _hashCalculator = new HashCalculator(_sha);

    public BlockChain()
    {
        var data = Encoding.UTF8.GetBytes("Gensis Block");

        //Add genesis block
        _blocks.Add(new Block 
        { 
            Id = Guid.Empty,
            Data = data,
            Hash = _sha.ComputeHash(data.ToArray()),
            Timestamp = DateTime.UtcNow
        });
    }

    public Block Add(byte[] data)
    {
        var previousBlock = _blocks.Last();

        var timestamp = DateTime.UtcNow;
        var id = Guid.NewGuid();

        var calcRequest = new CalculateHashRequest
        {
            Id = id,
            Data = data,
            PreviousHash = previousBlock.Hash,
            Timestamp = timestamp
        };

        var hash = _hashCalculator.CalculateHash(calcRequest);

        var block = new Block
        {
            Id = id,
            Data = data,
            Hash = hash,
            Timestamp = timestamp
        };

        _blocks.Add(block);

        return block;

    }

    public byte[] CalculateHash(Guid id, byte[] data, DateTime timetamp, byte[] previousHash)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Block> Blocks => _blocks;
}
