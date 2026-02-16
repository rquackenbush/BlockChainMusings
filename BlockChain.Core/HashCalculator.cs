using System.Collections.Immutable;
using System.Security.Cryptography;

namespace BlockChain.Core;

public class HashCalculator(HashAlgorithm hashAlgorithm)
{
    public byte[] CalculateHash(BlockBase request, ImmutableArray<byte> previousHash)
    {
        using var stream = new MemoryStream();

        stream.Write(request.Id.ToByteArray());
        stream.Write(request.Data.ToArray());
        stream.Write(previousHash.ToArray());
        stream.Write(BitConverter.GetBytes(request.Timestamp.Ticks));

        var bytes = stream.ToArray();

        return hashAlgorithm.ComputeHash(bytes);
    }

    public HashAlgorithm HashAlgorithm => hashAlgorithm;
}
