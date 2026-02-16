using System.Security.Cryptography;

namespace BlockChain.Core;

public class HashCalculator(HashAlgorithm hashAlgorithm)
{
    public byte[] CalculateHash(CalculateHashRequest request)
    {
        using var stream = new MemoryStream();

        stream.Write(request.Id.ToByteArray());
        stream.Write(request.Data);
        stream.Write(request.PreviousHash);
        stream.Write(BitConverter.GetBytes(request.Timestamp.Ticks));

        var bytes = stream.ToArray();

        return hashAlgorithm.ComputeHash(bytes);
    }
}
