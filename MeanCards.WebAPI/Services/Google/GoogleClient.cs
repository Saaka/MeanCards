using System.Net.Http;
using System.Threading.Tasks;

namespace MeanCards.WebAPI.Services.Google
{
    public class GoogleClient
    {
        private readonly HttpClient httpClient;
        private const string TokenInfoAddress = "/oauth2/v3/tokeninfo?id_token=";

        public GoogleClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<GoogleTokenInfo> GetTokenInfo(string token)
        {
            try
            {
                var response = await httpClient.GetAsync($"{TokenInfoAddress}{token}");
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsAsync<GoogleTokenInfo>();
            }
            catch(HttpRequestException exception)
            {
                return null;
            }
        }
    }
}
