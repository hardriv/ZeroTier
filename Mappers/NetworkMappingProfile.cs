using AutoMapper;
using static System.Runtime.InteropServices.JavaScript.JSType;
using ZeroTier.DTO.NetworkDtos;
using ZeroTier.ViewModels.NetworkModels;
using ZeroTier.Utils;

namespace ZeroTier.Mappers
{
    public class NetworkMappingProfile : Profile
    {
        public NetworkMappingProfile()
        {
            // Mapping simple bidirectionnel
            CreateMap<NetworkDto, NetworkViewModel>().ReverseMap();
            CreateMap<NetworkConfigDto, NetworkConfigViewModel>().ReverseMap();
            CreateMap<DnsConfigDto, DnsConfigViewModel>().ReverseMap();
            CreateMap<PermissionsDto, PermissionsViewModel>().ReverseMap();
            CreateMap<PermissionDetailDto, PermissionDetailViewModel>().ReverseMap();
            CreateMap<RouteDto, RouteViewModel>().ReverseMap();
            CreateMap<RuleDto, RuleViewModel>().ReverseMap();
            CreateMap<SsoConfigDto, SsoConfigViewModel>().ReverseMap();
            CreateMap<UserInterfaceSettingsDto, UserInterfaceSettingsViewModel>().ReverseMap();
            CreateMap<V4AssignModeDto, V4AssignModeViewModel>().ReverseMap();
            CreateMap<V6AssignModeDto, V6AssignModeViewModel>().ReverseMap();

            // Mapping avec conversion personnalisé pour IpAssignmentPool
            CreateMap<List<IpAssignmentPoolDto>, IpAssignmentPoolViewModel>()
                .ConvertUsing<IpAssignmentConverter>();

            CreateMap<IpAssignmentPoolViewModel, List<IpAssignmentPoolDto>>()
                .ConvertUsing<IpAssignmentConverter>();
        }
    }
}
