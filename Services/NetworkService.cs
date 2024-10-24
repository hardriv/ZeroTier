using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ZeroTier.ViewModels.NetworkModels;
using ZeroTier.ViewModels.MemberModels;
using System.Windows;
using ZeroTier.Utils;
using ZeroTier.DTO.NetworkDtos;
using System.Collections.ObjectModel;

namespace ZeroTier.Services
{
    public static class NetworkService
    {
        public static async Task<ObservableCollection<NetworkViewModel>?> GetNetworks(APIClient apiClient)
        {
            HttpResponseMessage response = await apiClient.GetAsync("network");

            List<NetworkDto>? dtos = await response.Content.ReadFromJsonAsync<List<NetworkDto>>();
            if (dtos == null || dtos.Count == 0)
            {
                return null;
            }
            
            return new ObservableCollection<NetworkViewModel>(NetworkMapper.NetworksToViewModels(dtos));
        }

        public static async Task<bool> DeleteNetwork(APIClient apiClient, string networkId)
        {
            HttpResponseMessage response = await apiClient.DeleteAsync($"network/{networkId}");

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                MessageBox.Show($"Erreur : {(int)response.StatusCode} - {response.ReasonPhrase}");
                return false;
            }
        }
    }
}
