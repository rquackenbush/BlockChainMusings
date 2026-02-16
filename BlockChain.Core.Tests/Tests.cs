using Shouldly;
using System.Security.Cryptography;
using System.Text;

namespace BlockChain.Core.Tests;

public class Tests(ITestOutputHelper output)
{
    private readonly HashCalculator _hashCalculator = new HashCalculator(SHA256.Create());

    [Fact]
    public void GenisisBlockShouldHaveEmptyHash()
    {
        var blockChain = new BlockChain(_hashCalculator);

        var blocks = blockChain.Blocks
            .ToArray();

        blocks.Length.ShouldBe(1);

        blocks[0].Hash.ShouldBe(new byte[_hashCalculator.HashAlgorithm.HashSize]);
    }

    [Theory]
    [InlineData("foo")]
    [InlineData("asdfasfasfsdf")]
    public void AddBBlockTest(string source)
    {
        var blockChain = new BlockChain(_hashCalculator);

        var data = Encoding.UTF8.GetBytes(source);

        var block= blockChain.Add(data);

        block.Id.ShouldNotBe(Guid.Empty);

        block.Data.ShouldBe(data);
    }

    [Fact]
    public void VerifyValidChain()
    {
        var blockChain = new BlockChain(_hashCalculator);

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
        var blockChain = new BlockChain(_hashCalculator);

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

        blocks[index] = blocks[index].MutateData();

        var exception = Should.Throw<InvalidBlockException>(() => blocks.Verify(new HashCalculator(SHA256.Create())));

        exception.BlockIndex.ShouldBe(index);

        output.WriteLine(exception.Message);
    }
}
