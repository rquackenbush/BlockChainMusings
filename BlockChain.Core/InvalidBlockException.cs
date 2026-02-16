namespace BlockChain.Core;

public class InvalidBlockException : Exception
{
    public InvalidBlockException(int blockIndex, Guid blockId)
    {
        BlockIndex = blockIndex;
        BlockId = blockId;
    }

    public override string Message => $"Invalid block {BlockId} at index [{BlockIndex}]";

    public int BlockIndex { get; }

    public Guid BlockId { get; }
}
