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
using ZeroTier.Mappers;
using AutoMapper;

namespace ZeroTier.Services
{
    public class NetworkService(NetworkMapper mapper)
    {
        private readonly NetworkMapper _mapper = mapper;

        public async Task<ObservableCollection<NetworkViewModel>?> GetNetworks(APIClient apiClient)
        {
            HttpResponseMessage response = await apiClient.GetAsync("network");

            List<NetworkDto>? dtos = await response.Content.ReadFromJsonAsync<List<NetworkDto>>();
            if (dtos == null || dtos.Count == 0)
            {
                return [];
            }
            
            return new ObservableCollection<NetworkViewModel>(_mapper.MapToViewModels(dtos));
        }

        public async Task<NetworkViewModel> GetNetworkById(APIClient apiClient, string networkId)
        {
            HttpResponseMessage response = await apiClient.GetAsync($"network/{networkId}");

            NetworkDto dto = await response.Content.ReadFromJsonAsync<NetworkDto>();
            if (dto == null)
            {
                return null;
            }

            return _mapper.MapToViewModel(dto);
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
