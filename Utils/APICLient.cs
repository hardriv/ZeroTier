using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ZeroTier.Utils
{
    public class APIClient
    {
        private static readonly string BASE_URL = "https://api.zerotier.com/api/v1/";
        private string ApiToken = string.Empty;

        private HttpClient client;

        public APIClient()
        {
            client = new HttpClient { BaseAddress = new Uri(BASE_URL) };
        }

        // Méthode pour setter l'API token
        public void SetApiToken(string token)
        {
            ApiToken = token;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ApiToken);
        }

        // Méthode GET
        public async Task<HttpResponseMessage> GetAsync(string endpoint)
        {
            var response = await client.GetAsync(endpoint);
            HandleErrors(response); // Gestion des erreurs
            return response;
        }

        // Méthode POST
        public async Task<HttpResponseMessage> PostAsync(string endpoint, HttpContent content)
        {
            var response = await client.PostAsync(endpoint, content);
            HandleErrors(response); // Gestion des erreurs
            return response;
        }

        // Méthode DELETE
        public async Task<HttpResponseMessage> DeleteAsync(string endpoint)
        {
            var response = await client.DeleteAsync(endpoint);
            HandleErrors(response); // Gestion des erreurs
            return response;
        }

        // Méthode pour gérer les erreurs HTTP
        private void HandleErrors(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                // Jeter une ApiException avec le code de statut
                throw new ApiException((int)response.StatusCode);
            }
        }
    }
}
