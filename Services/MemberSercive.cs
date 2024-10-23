using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows;
using ZeroTier.DTO.MemberDtos;
using ZeroTier.Mappers;
using ZeroTier.Utils;
using ZeroTier.ViewModels.MemberModels;

namespace ZeroTier.Services
{
    public static class MemberService
    {
        public static async Task<List<MemberViewModel>?> GetMembers(APIClient apiClient, string networkId)
        {
            HttpResponseMessage response = await apiClient.GetAsync($"network/{networkId}/member");

            if (response.IsSuccessStatusCode)
            {
                List<MemberDto>? dtos = await response.Content.ReadFromJsonAsync<List<MemberDto>>();
                if (dtos == null || dtos.Count == 0)
                {
                    return null;
                }
                List<MemberViewModel> viewModel = MemberMapper.MembersToModels(dtos);
                return viewModel;
            }
            else if ((int)response.StatusCode >= 400 && (int)response.StatusCode < 500)
            {
                MessageBox.Show($"Erreur client : {(int)response.StatusCode} - {response.ReasonPhrase}");
            }
            else if ((int)response.StatusCode >= 500)
            {
                MessageBox.Show($"Erreur serveur : {(int)response.StatusCode} - {response.ReasonPhrase}");
            }
            return null;
        }

        public static async Task<MemberViewModel?> GetMemberById(APIClient apiClient, string networkId, string memberId)
        {
            HttpResponseMessage response = await apiClient.GetAsync($"network/{networkId}/member/{memberId}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<MemberViewModel>();
            }
            else if ((int)response.StatusCode >= 400 && (int)response.StatusCode < 500)
            {
                MessageBox.Show($"Erreur client : {(int)response.StatusCode} - {response.ReasonPhrase}");
            }
            else if ((int)response.StatusCode >= 500)
            {
                MessageBox.Show($"Erreur serveur : {(int)response.StatusCode} - {response.ReasonPhrase}");
            }
            return null;
        }

        public static async Task<bool> AuthorizeMember(APIClient apiClient, MemberViewModel memberViewModel)
        {
            MemberDto memberDto = MemberMapper.MemberToDto(memberViewModel);
            HttpContent memberJson = JsonContent.Create(memberDto);
            HttpResponseMessage response = await apiClient.PostAsync($"network/{memberDto.NetworkId}/member/{memberDto.NodeId}", memberJson);

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

        public static async Task<bool> DenyMember(APIClient apiClient, MemberViewModel memberViewModel)
        {
            MemberDto memberDto = MemberMapper.MemberToDto(memberViewModel);
            HttpContent memberJson = JsonContent.Create(memberDto);
            HttpResponseMessage response = await apiClient.PostAsync($"network/{memberDto.NetworkId}/member/{memberDto.NodeId}", memberJson);

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

        public static async Task<bool> UpdateMember(APIClient apiClient, MemberViewModel memberViewModel)
        {
            MemberDto memberDto = MemberMapper.MemberToDto(memberViewModel);
            HttpContent memberJson = JsonContent.Create(memberDto);
            HttpResponseMessage response = await apiClient.PostAsync($"network/{memberViewModel.NetworkId}/member/{memberViewModel.NodeId}", memberJson);

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
