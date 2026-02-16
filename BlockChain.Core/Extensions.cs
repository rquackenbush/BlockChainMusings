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
                var request = new CalculateHashRequest
                {
                    Id = block.Id,
                    Data = block.Data,
                    PreviousHash = previousBlock.Hash,
                    Timestamp = block.Timestamp
                };

                var calculatedHash = hashCalculator.CalculateHash(request);

                if (!block.Hash.SequenceEqual(calculatedHash)) 
                {
                    throw new InvalidBlockException(index, block.Id);
                }
            }

            previousBlock = block;
            index++;
        }
    }
}
