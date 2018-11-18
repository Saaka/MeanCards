using MeanCards.Common.Helpers;
using MeanCards.Common.ProfileImageUrlProvider;
using Xunit;

namespace MeanCards.Tests.Unit.ProfileImageUrlProviderTests
{
    public class GravatarImageUrlProviderShould
    {
        [Fact]
        public void GenerateUrlForEmail()
        {
            var email = "saka1988@gmail.com";
            var imageProvider = new GravatarImageUrlProvider(new HashGenerator());

            var imageUrl = imageProvider.GetImageUrl(email);

            Assert.NotNull(imageUrl);
            Assert.NotEmpty(imageUrl);
            Assert.Equal("https://www.gravatar.com/avatar/7bd021685b66a1edc08a268bafd22bb8", imageUrl);
        }
        
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData(" ")]
        public void GenerateNullForEmptyEmail(string email)
        {
            var imageProvider = new GravatarImageUrlProvider(new HashGenerator());

            var imageUrl = imageProvider.GetImageUrl(email);

            Assert.Null(imageUrl);
        }
    }
}
