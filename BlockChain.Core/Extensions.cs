using System.Collections.Immutable;

namespace BlockChain.Core;

public static class Extensions
{
    public static void Verify(this IEnumerable<Block> blocks, HashCalculator hashCalculator)
    {
        Block? previousBlock = null;

        var index = 0;

        foreach (var block in blocks)
        {
            if (previousBlock != null)
            {
                var calculatedHash = block.CalculateHash(previousBlock.Hash, hashCalculator);

                if (!block.Hash.SequenceEqual(calculatedHash)) 
                {
                    throw new InvalidBlockException(index, block.Id);
                }
            }

            previousBlock = block;
            index++;
        }
    }

    public static byte[] CalculateHash(this BlockBase block, ImmutableArray<byte> previousHash, HashCalculator hashCalculator)
    {
        return hashCalculator.CalculateHash(block, previousHash);
    }
}
