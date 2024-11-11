using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows;
using ZeroTier.DTO.MemberDtos;
using ZeroTier.Mappers;
using ZeroTier.Config;
using ZeroTier.ViewModels.MemberModels;

namespace ZeroTier.Services
{
    public static class MemberService
    {
        public static async Task<ObservableCollection<MemberViewModel>?> GetMembers(APIClient apiClient, string networkId)
        {
            HttpResponseMessage response = await apiClient.GetAsync($"network/{networkId}/member");

            if (response.IsSuccessStatusCode)
            {
                List<MemberDto>? dtos = await response.Content.ReadFromJsonAsync<List<MemberDto>>();
                if (dtos == null || dtos.Count == 0)
                {
                    return null;
                }

                // Trier pour avoir les membres autorisés en premier
                var sortedViewModel = MemberMapper.MembersToViewModels(dtos)
                    .OrderByDescending(member => member.Config.Authorized)
                    .ToList();

                return new ObservableCollection<MemberViewModel>(sortedViewModel);
            }
            
            return null;
        }

        public static async Task<MemberViewModel?> GetMemberById(APIClient apiClient, string networkId, string memberId)
        {
            HttpResponseMessage response = await apiClient.GetAsync($"network/{networkId}/member/{memberId}");

            var dto = await response.Content.ReadFromJsonAsync<MemberDto>();
            if (dto == null)
            {
                return null;
            }

            return MemberMapper.MemberToViewModel(dto);
        }

        public static async Task<MemberViewModel?> UpdateMember(APIClient apiClient, MemberViewModel memberViewModel, bool authorized)
        {
            memberViewModel.Config.Authorized = authorized;

            MemberUpdateDto memberDto = MemberMapper.MemberUpdateToDto(memberViewModel);
            HttpResponseMessage response = await apiClient.PostAsync($"network/{memberViewModel.NetworkId}/member/{memberViewModel.NodeId}", memberDto);

            if (response.IsSuccessStatusCode)
            {
                return await GetMemberById(apiClient, memberViewModel.NetworkId, memberViewModel.NodeId);
            }
            else
            {
                MessageBox.Show($"Erreur : {(int)response.StatusCode} - {response.ReasonPhrase}");
                return null;
            }
        }

        public static async Task<bool> DeleteMember(APIClient apiClient, string networkId, string memberId)
        {
            HttpResponseMessage response = await apiClient.DeleteAsync($"network/{networkId}/member/{memberId}");

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
