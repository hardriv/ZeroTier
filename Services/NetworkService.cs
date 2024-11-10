using System.Net.Http;
using System.Net.Http.Json;
using ZeroTier.ViewModels.NetworkModels;
using System.Windows;
using ZeroTier.Utils;
using ZeroTier.DTO.NetworkDtos;
using System.Collections.ObjectModel;
using ZeroTier.Mappers;

namespace ZeroTier.Services
{
    public class NetworkService()
    {
        public async Task<ObservableCollection<NetworkViewModel>?> GetNetworks(APIClient apiClient)
        {
            HttpResponseMessage response = await apiClient.GetAsync("network");

            List<NetworkDto>? dtos = await response.Content.ReadFromJsonAsync<List<NetworkDto>>();
            if (dtos == null || dtos.Count == 0)
            {
                return [];
            }
            
            return new ObservableCollection<NetworkViewModel>(NetworkMapper.NetworksToViewModels(dtos));
        }

        public async Task<NetworkViewModel> GetNetworkById(APIClient apiClient, string networkId)
        {
            HttpResponseMessage response = await apiClient.GetAsync($"network/{networkId}");

            NetworkDto dto = await response.Content.ReadFromJsonAsync<NetworkDto>();
            if (dto == null)
            {
                return null;
            }

            return NetworkMapper.NetworkToViewModel(dto);
        }

        public async Task<bool> DeleteNetwork(APIClient apiClient, string networkId)
        {
            HttpResponseMessage response = await apiClient.DeleteAsync($"network/{networkId}");

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                MessageBox.Show($"Error : {(int)response.StatusCode} - {response.ReasonPhrase}");
                return false;
            }
        }
    }
}
