using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Text.Json;

namespace ZeroTier.Utils
{
    public class APIClient
    {
        private static readonly string BASE_URL = "https://api.zerotier.com/api/v1/";
        private string ApiToken = string.Empty;

        private HttpClient client;

        // Options JSON avec la stratégie camelCase
        private readonly JsonSerializerOptions jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,  // Applique camelCase
            WriteIndented = false  // Pour une sortie compacte
        };

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
            HandleErrors(response);
            return response;
        }

        // Méthode POST
        public async Task<HttpResponseMessage> PostAsync(string endpoint, HttpContent content)
        {
            HttpContent jsonContent = JsonContent.Create(content, null, jsonOptions);
            var response = await client.PostAsync(endpoint, jsonContent);
            // Gestion des erreurs
            HandleErrors(response);
            return response;
        }

        // Méthode DELETE
        public async Task<HttpResponseMessage> DeleteAsync(string endpoint)
        {
            var response = await client.DeleteAsync(endpoint);
            // Gestion des erreurs
            HandleErrors(response);
            return response;
        }

        // Méthode pour gérer les erreurs HTTP
        private static void HandleErrors(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                // Jeter une ApiException avec le code de statut
                throw new ApiException((int)response.StatusCode);
            }
        }
    }
}
