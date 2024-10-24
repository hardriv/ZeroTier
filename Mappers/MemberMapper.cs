using ZeroTier.DTO.MemberDtos;
using ZeroTier.Utils;
using ZeroTier.ViewModels.MemberModels;

namespace ZeroTier.Mappers
{
    public static class MemberMapper
    {
        public static MemberViewModel MemberToViewModel(MemberDto dto)
        {
            return new MemberViewModel
            {
                Id = dto.Id,
                Type = dto.Type,
                Clock = DateTimeUtils.FromUnixTimeMilliseconds(dto.Clock),
                NetworkId = dto.NetworkId,
                NodeId = dto.NodeId,
                ControllerId = dto.ControllerId,
                Hidden = dto.Hidden,
                Name = dto.Name,
                Description = dto.Description,
                Config = MemberConfigToViewModel(dto.Config),
                LastOnline = DateTimeUtils.FromUnixTimeMilliseconds(dto.LastOnline),
                LastSeen = DateTimeUtils.FromUnixTimeMilliseconds(dto.LastSeen),
                PhysicalAddress = dto.PhysicalAddress,
                ClientVersion = dto.ClientVersion,
                ProtocolVersion = dto.ProtocolVersion,
                SupportsRulesEngine = dto.SupportsRulesEngine,
                IsSelected = false // Par défaut, non sélectionné
            };
        }

        public static MemberDto MemberToDto(MemberViewModel viewModel)
        {
            return new MemberDto
            {
                Id = viewModel.Id,
                Type = viewModel.Type,
                Clock = DateTimeUtils.ToUnixTimeMilliseconds(viewModel.Clock),
                NetworkId = viewModel.NetworkId,
                NodeId = viewModel.NodeId,
                ControllerId = viewModel.ControllerId,
                Hidden = viewModel.Hidden,
                Name = viewModel.Name,
                Description = viewModel.Description,
                Config = MemberConfigToDto(viewModel.Config),
                LastOnline = DateTimeUtils.ToUnixTimeMilliseconds(viewModel.LastOnline),
                LastSeen = DateTimeUtils.ToUnixTimeMilliseconds(viewModel.LastSeen),
                PhysicalAddress = viewModel.PhysicalAddress,
                ClientVersion = viewModel.ClientVersion,
                ProtocolVersion = viewModel.ProtocolVersion,
                SupportsRulesEngine = viewModel.SupportsRulesEngine
            };
        }

        public static MemberConfigViewModel MemberConfigToViewModel(MemberConfigDto dto)
        {
            return new MemberConfigViewModel
            {
                ActiveBridge = dto.ActiveBridge,
                Address = dto.Address,
                Authorized = dto.Authorized,
                Capabilities = dto.Capabilities,
                CreationTime = DateTimeUtils.FromUnixTimeMilliseconds(dto.CreationTime),
                Id = dto.Id,
                Identity = dto.Identity,
                IpAssignment = dto.IpAssignments != null && dto.IpAssignments.Count > 0 ? dto.IpAssignments[0] : string.Empty,
                LastAuthorizedTime = DateTimeUtils.FromUnixTimeMilliseconds(dto.LastAuthorizedTime),
                LastDeauthorizedTime = DateTimeUtils.FromUnixTimeMilliseconds(dto.LastDeauthorizedTime),
                NoAutoAssignIps = dto.NoAutoAssignIps,
                Nwid = dto.Nwid,
                Objtype = dto.Objtype,
                RemoteTraceLevel = dto.RemoteTraceLevel,
                RemoteTraceTarget = dto.RemoteTraceTarget,
                Revision = dto.Revision,
                Tags = dto.Tags,
                VMajor = dto.VMajor,
                VMinor = dto.VMinor,
                VRev = dto.VRev,
                VProto = dto.VProto,
                SsoExempt = dto.SsoExempt
            };
        }

        public static MemberConfigDto MemberConfigToDto(MemberConfigViewModel viewModel)
        {
            return new MemberConfigDto
            {
                ActiveBridge = viewModel.ActiveBridge,
                Address = viewModel.Address,
                Authorized = viewModel.Authorized,
                Capabilities = viewModel.Capabilities,
                CreationTime = DateTimeUtils.ToUnixTimeMilliseconds(viewModel.CreationTime),
                Id = viewModel.Id,
                Identity = viewModel.Identity,
                IpAssignments = [viewModel.IpAssignment],
                LastAuthorizedTime = DateTimeUtils.ToUnixTimeMilliseconds(viewModel.LastAuthorizedTime),
                LastDeauthorizedTime = DateTimeUtils.ToUnixTimeMilliseconds(viewModel.LastDeauthorizedTime),
                NoAutoAssignIps = viewModel.NoAutoAssignIps,
                Nwid = viewModel.Nwid,
                Objtype = viewModel.Objtype,
                RemoteTraceLevel = viewModel.RemoteTraceLevel,
                RemoteTraceTarget = viewModel.RemoteTraceTarget,
                Revision = viewModel.Revision,
                Tags = viewModel.Tags,
                VMajor = viewModel.VMajor,
                VMinor = viewModel.VMinor,
                VRev = viewModel.VRev,
                VProto = viewModel.VProto,
                SsoExempt = viewModel.SsoExempt
            };
        }

        public static List<MemberViewModel> MembersToModels(List<MemberDto> dtos)
        {
            List<MemberViewModel> viewModels = [];
            foreach (MemberDto dto in dtos)
            {
                MemberViewModel viewModel = MemberToViewModel(dto);
                viewModels.Add(viewModel);
            }
            return viewModels;
        }

        public static List<MemberConfigViewModel> MemberConfigsToModels(List<MemberConfigDto> dtos)
        {
            List<MemberConfigViewModel> viewModels = [];
            foreach (MemberConfigDto dto in dtos)
            {
                MemberConfigViewModel viewModel = MemberConfigToViewModel(dto);
                viewModels.Add(viewModel);
            }
            return viewModels;
        }

        public static List<MemberDto> MembersToDtos(List<MemberViewModel> viewModels)
        {
            List<MemberDto> dtos = [];
            foreach (MemberViewModel viewModel in viewModels)
            {
                MemberDto dto = MemberToDto(viewModel);
                dtos.Add(dto);
            }
            return dtos;
        }

        public static List<MemberConfigDto> MemberConfigsToDtos(List<MemberConfigViewModel> viewModels)
        {
            List<MemberConfigDto> dtos = [];
            foreach (MemberConfigViewModel viewModel in viewModels)
            {
                MemberConfigDto dto = MemberConfigToDto(viewModel);
                dtos.Add(dto);
            }
            return dtos;
        }
    }
}