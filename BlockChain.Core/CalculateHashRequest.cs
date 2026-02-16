namespace BlockChain.Core;

public record CalculateHashRequest
{
    public required Guid Id { get; init; }

    public required byte[] Data { get; init; }

    public required byte[] PreviousHash { get; init; }

    public required DateTime Timestamp { get; init; }
}
