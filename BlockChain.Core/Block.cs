namespace BlockChain.Core;


public class Block
{
    public required Guid Id { get; init; }

    public required byte[] Data { get; init; }

    public required DateTime Timestamp { get; init; }

    public required byte[] Hash { get; init; }
}
