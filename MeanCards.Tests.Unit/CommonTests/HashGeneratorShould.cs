using MeanCards.Common.Helpers;
using Xunit;

namespace MeanCards.Tests.Unit.CommonTests
{
    public class HashGeneratorShould
    {
        [Fact]
        public void GenerateHashForEmail()
        {
            var email = "saka1988@gmail.com";

            var hashGenerator = new HashGenerator();

            var hash = hashGenerator.Generate(email);

            Assert.NotNull(hash);
            Assert.NotEmpty(hash);
            Assert.Equal("7bd021685b66a1edc08a268bafd22bb8", hash);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData(" ")]
        public void ReturnEmptyHashForEmptyInput(string input)
        {
            var hashGenerator = new HashGenerator();

            var hash = hashGenerator.Generate(input);

            Assert.Equal(string.Empty, hash);
        }
    }
}
