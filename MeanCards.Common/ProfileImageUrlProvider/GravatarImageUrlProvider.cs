using MeanCards.Common.Helpers;

namespace MeanCards.Common.ProfileImageUrlProvider
{
    public class GravatarImageUrlProvider : IProfileImageUrlProvider
    {
        private const string ServiceAddress = "https://www.gravatar.com/avatar/";
        private const string SizeParam = "?s=96";

        private readonly HashGenerator hashGenerator;

        public GravatarImageUrlProvider(HashGenerator hashGenerator)
        {
            this.hashGenerator = hashGenerator;
        }

        public string GetImageUrl(string email)
        {
            var emailHash = hashGenerator.Generate(email);
            if (string.IsNullOrWhiteSpace(emailHash))
                return null;

            return $"{ServiceAddress}{emailHash}{SizeParam}";
        }
    }
}
