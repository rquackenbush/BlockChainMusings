using System.Collections.Immutable;

namespace BlockChain.Core.Tests;

public static class Extensions
{
    /// <summary>
    /// Subtley alter the data for a block.
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static Block MutateData(this Block source)
    {
        var dataCopy = source.Data.ToArray();

        if (dataCopy.Length == 0)
        {
            dataCopy = new byte[] { 0 };
        }

        dataCopy[0]++;

        return new Block
        {
            Id = source.Id,
            Data = dataCopy.ToImmutableArray(),
            Hash = source.Hash,
            Timestamp = source.Timestamp,
        };
    }
}
