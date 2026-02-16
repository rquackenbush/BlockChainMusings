using Shouldly;
using System.Security.Cryptography;
using System.Text;

namespace BlockChain.Core.Tests;

public class Tests(ITestOutputHelper output)
{
    [Theory]
    [InlineData("foo")]
    [InlineData("asdfasfasfsdf")]
    public void HappyPathTest(string source)
    {
        var blockChain = new BlockChain();

        var data = Encoding.UTF8.GetBytes(source);

        var block= blockChain.Add(data);

        block.Id.ShouldNotBe(Guid.Empty);

        block.Data.ShouldBe(data);
    }

    [Fact]
    public void Validate()
    {
        var blockChain = new BlockChain();

        var sourceData = new string[]
        {
            "one",
            "two",
            "three"
        };

        foreach(var data in sourceData)
        {
            blockChain.Add(Encoding.UTF8.GetBytes(data));
        }

        blockChain.Blocks.Verify(new HashCalculator(SHA256.Create()));
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    public void VerifyInvalidChain(int index)
    {
        var blockChain = new BlockChain();

        var sourceData = new string[]
        {
            "one",
            "two",
            "three"
        };

        foreach (var data in sourceData)
        {
            blockChain.Add(Encoding.UTF8.GetBytes(data));
        }

        var blocks = blockChain.Blocks.ToArray();

        blocks[index] = MutateBlock(blocks[index]);

        var exception = Should.Throw<InvalidBlockException>(() => blocks.Verify(new HashCalculator(SHA256.Create())));

        exception.BlockIndex.ShouldBe(index);

        output.WriteLine(exception.Message);
    }

    private static Block MutateBlock(Block source)
    {
        var dataCopy = source.Data.ToArray();

        dataCopy[0]++;

        return new Block
        {
            Id = source.Id,
            Data = dataCopy,
            Hash = source.Hash,
            Timestamp = source.Timestamp,
        };
    }
}
