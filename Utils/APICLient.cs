using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Text.Json;
using System.Windows;

namespace ZeroTier.Utils
{
    public class APIClient
    {
        private static readonly string BASE_URL = "https://api.zerotier.com/api/v1/";
        private string ApiToken = string.Empty;
        private readonly HttpClient client;
        private readonly JsonSerializerOptions jsonOptions;

        public APIClient()
        {
            client = new HttpClient { BaseAddress = new Uri(BASE_URL) };
            jsonOptions = new()
            {
                // Applique camelCase
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                // Pour une sortie compacte
                WriteIndented = false
            };
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
            HttpResponseMessage response = new();
            try
            {
                response = await client.GetAsync(endpoint);
            }
            catch (HttpRequestException e)
            {
                HandleExceptions(null, e);
            }
            catch (Exception e)
            {
                HandleExceptions(e, null);
            }
            
            return response;
        }

        // Méthode POST
        public async Task<HttpResponseMessage> PostAsync(string endpoint, HttpContent content)
        {
            HttpResponseMessage response = new();
            try
            {
                HttpContent jsonContent = JsonContent.Create(content, null, jsonOptions);
                response = await client.PostAsync(endpoint, jsonContent);
            }
            catch (HttpRequestException e)
            {
                HandleExceptions(null, e);
            }
            catch (Exception e)
            {
                HandleExceptions(e, null);
            }
            
            return response;
        }

        // Méthode DELETE
        public async Task<HttpResponseMessage> DeleteAsync(string endpoint)
        {
            HttpResponseMessage response = new(System.Net.HttpStatusCode.NotAcceptable);
            try
            {
                response = await client.DeleteAsync(endpoint);
            }
            catch (HttpRequestException e)
            {
                HandleExceptions(null, e);
            }
            catch (Exception e)
            {
                HandleExceptions(e, null);
            }
            
            return response;
        }

        private static void HandleExceptions(Exception? e, HttpRequestException? httpRequestException)  
        {
            if (httpRequestException != null)
            {
                int statusCode = httpRequestException.StatusCode.HasValue ? (int)httpRequestException.StatusCode : 0;
                ApiException apiException = new(statusCode, httpRequestException.Message);
                MessageBox.Show($"Erreur : {apiException.StatusCode} - {apiException.Message}");
            }
            else if (e != null)
            {
                MessageBox.Show($"Erreur inattendue : {e.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
