using System;
using System.Collections.Generic;
using ZeroTier.DTO.NetworkDtos;
using ZeroTier.ViewModels.NetworkModels;
using ZeroTier.Utils;

public static class NetworkMapper
{
    public static NetworkViewModel NetworkToViewModel(NetworkDto dto)
    {
        return new NetworkViewModel
        {
            Id = dto.Id,
            Type = dto.Type,
            Clock = DateTimeUtils.FromUnixTimeMilliseconds(dto.Clock),
            Config = NetworkConfigToViewModel(dto.Config),
            Description = dto.Description,
            RulesSource = dto.RulesSource,
            Permissions = PermissionsToViewModel(dto.Permissions),
            OwnerId = dto.OwnerId,
            OnlineMemberCount = dto.OnlineMemberCount,
            AuthorizedMemberCount = dto.AuthorizedMemberCount,
            TotalMemberCount = dto.TotalMemberCount,
            CapabilitiesByName = new Dictionary<string, bool>(dto.CapabilitiesByName),
            TagsByName = new Dictionary<string, bool>(dto.TagsByName),
            Ui = UiToViewModel(dto.Ui)
        };
    }

    public static NetworkDto NetworkToDto(NetworkViewModel viewModel)
    {
        return new NetworkDto
        {
            Id = viewModel.Id,
            Type = viewModel.Type,
            Clock = DateTimeUtils.ToUnixTimeMilliseconds(viewModel.Clock),
            Config = NetworkConfigToDto(viewModel.Config),
            Description = viewModel.Description,
            RulesSource = viewModel.RulesSource,
            Permissions = PermissionsToDto(viewModel.Permissions), // TODO corriger le warning null
            OwnerId = viewModel.OwnerId,
            OnlineMemberCount = viewModel.OnlineMemberCount,
            AuthorizedMemberCount = viewModel.AuthorizedMemberCount,
            TotalMemberCount = viewModel.TotalMemberCount,
            CapabilitiesByName = new Dictionary<string, bool>(viewModel.CapabilitiesByName),
            TagsByName = new Dictionary<string, bool>(viewModel.TagsByName),
            Ui = UiToDto(viewModel.Ui) // TODO corriger le warning null
        };
    }
    public static NetworkConfigViewModel NetworkConfigToViewModel(NetworkConfigDto dto)
    {
        return new NetworkConfigViewModel
        {
            AuthTokens = dto.AuthTokens,
            CreationTime = DateTimeUtils.FromUnixTimeMilliseconds(dto.CreationTime),
            Capabilities = dto.Capabilities,
            EnableBroadcast = dto.EnableBroadcast,
            Id = dto.Id,
            IpAssignmentPools = IpAssignmentPoolToViewModels(dto.IpAssignmentPools),
            LastModified = DateTimeUtils.FromUnixTimeMilliseconds(dto.LastModified),
            Mtu = dto.Mtu,
            MulticastLimit = dto.MulticastLimit,
            Name = dto.Name,
            Private = dto.Private,
            RemoteTraceLevel = dto.RemoteTraceLevel,
            RemoteTraceTarget = dto.RemoteTraceTarget,
            Routes = RouteToViewModels(dto.Routes),
            Rules = RuleToViewModels(dto.Rules),
            Tags = dto.Tags,
            V4AssignMode = V4AssignModeToViewModel(dto.V4AssignMode),
            V6AssignMode = V6AssignModeToViewModel(dto.V6AssignMode),
            Dns = DnsConfigToViewModel(dto.Dns),
            SsoConfig = SsoConfigToViewModel(dto.SsoConfig)
        };
    }

    public static NetworkConfigDto NetworkConfigToDto(NetworkConfigViewModel viewModel)
    {
        return new NetworkConfigDto
        {
            AuthTokens = viewModel.AuthTokens,
            CreationTime = DateTimeUtils.ToUnixTimeMilliseconds(viewModel.CreationTime),
            Capabilities = viewModel.Capabilities,
            EnableBroadcast = viewModel.EnableBroadcast,
            Id = viewModel.Id,
            IpAssignmentPools = IpAssignmentPoolToDtos(viewModel.IpAssignmentPools),
            LastModified = DateTimeUtils.ToUnixTimeMilliseconds(viewModel.LastModified),
            Mtu = viewModel.Mtu,
            MulticastLimit = viewModel.MulticastLimit,
            Name = viewModel.Name,
            Private = viewModel.Private,
            RemoteTraceLevel = viewModel.RemoteTraceLevel,
            RemoteTraceTarget = viewModel.RemoteTraceTarget,
            Routes = RouteToDtos(viewModel.Routes),
            Rules = RuleToDtos(viewModel.Rules),
            Tags = viewModel.Tags,
            V4AssignMode = V4AssignModeToDto(viewModel.V4AssignMode),
            V6AssignMode = V6AssignModeToDto(viewModel.V6AssignMode),
            Dns = DnsConfigToDto(viewModel.Dns),
            SsoConfig = SsoConfigToDto(viewModel.SsoConfig)
        };
    }

    public static DnsConfigViewModel DnsConfigToViewModel(DnsConfigDto dto)
    {
        return new DnsConfigViewModel
        {
            Domain = dto.Domain,
            Servers = dto.Servers
        };
    }

    public static DnsConfigDto DnsConfigToDto(DnsConfigViewModel viewModel)
    {
        return new DnsConfigDto
        {
            Domain = viewModel.Domain,
            Servers = viewModel.Servers
        };
    }
    
    public static IpAssignmentPoolViewModel IpAssignmentPoolToViewModel(IpAssignmentPoolDto dto)
    {
        return new IpAssignmentPoolViewModel
        {
            IpRangeStart = dto.IpRangeStart,
            IpRangeEnd = dto.IpRangeEnd
        };
    }

    public static IpAssignmentPoolDto IpAssignmentPoolToDto(IpAssignmentPoolViewModel viewModel)
    {
        return new IpAssignmentPoolDto
        {
            IpRangeStart = viewModel.IpRangeStart,
            IpRangeEnd = viewModel.IpRangeEnd
        };
    }
    
    public static List<IpAssignmentPoolViewModel> IpAssignmentPoolToViewModels(List<IpAssignmentPoolDto> dtos)
    {
        List<IpAssignmentPoolViewModel> viewModels = new List<IpAssignmentPoolViewModel>();
        foreach (var dto in dtos)
        {
            IpAssignmentPoolViewModel viewModel = IpAssignmentPoolToViewModel(dto);
            viewModels.Add(viewModel);
        }

        return viewModels;
    }

    public static List<IpAssignmentPoolDto> IpAssignmentPoolToDtos(List<IpAssignmentPoolViewModel> viewModels)
    {
        List<IpAssignmentPoolDto> dtos = new List<IpAssignmentPoolDto>();
        foreach (var viewModel in viewModels)
        {
            IpAssignmentPoolDto dto = IpAssignmentPoolToDto(viewModel);
            dtos.Add(dto);
        }

        return dtos;
    }

    public static PermissionsViewModel? PermissionsToViewModel(PermissionsDto dto)
    {
        if (dto == null)
        {
            return null;
        }

        var viewModel = new PermissionsViewModel
        {
            PermissionDetails = new Dictionary<string, PermissionDetailViewModel>()
        };

        foreach (var kvp in dto.PermissionDetails)
        {
            viewModel.PermissionDetails[kvp.Key] = PermissionDetailToViewModel(kvp.Value);
        }

        return viewModel;
    }

    public static PermissionsDto? PermissionsToDto(PermissionsViewModel viewModel)
    {
        if (viewModel == null)
        {
            return null;
        }

        var dto = new PermissionsDto
        {
            PermissionDetails = new Dictionary<string, PermissionDetailDto>()
        };

        foreach (var kvp in viewModel.PermissionDetails)
        {
            dto.PermissionDetails[kvp.Key] = PermissionDetailToDto(kvp.Value);
        }

        return dto;
    }

    public static PermissionDetailViewModel PermissionDetailToViewModel(PermissionDetailDto dto)
    {
        return new PermissionDetailViewModel
        {
            A = dto.A,
            D = dto.D,
            M = dto.M,
            R = dto.R
        };
    }

    public static PermissionDetailDto PermissionDetailToDto(PermissionDetailViewModel viewModel)
    {
        return new PermissionDetailDto
        {
            A = viewModel.A,
            D = viewModel.D,
            M = viewModel.M,
            R = viewModel.R
        };
    }
    
    public static RouteViewModel RouteToViewModel(RouteDto dto)
    {
        return new RouteViewModel
        {
            Target = dto.Target
        };
    }

    public static RouteDto RouteToDto(RouteViewModel viewModel)
    {
        return new RouteDto
        {
            Target = viewModel.Target
        };
    }
    
    public static List<RouteViewModel> RouteToViewModels(List<RouteDto> dtos)
    {
        List<RouteViewModel> viewModels = new List<RouteViewModel>();
        foreach (var dto in dtos)
        {
            RouteViewModel viewModel = RouteToViewModel(dto);
            viewModels.Add(viewModel);
        }

        return viewModels;
    }

    public static List<RouteDto> RouteToDtos(List<RouteViewModel> viewModels)
    {
        List<RouteDto> dtos = new List<RouteDto>();
        foreach (var viewModel in viewModels)
        {
            RouteDto dto = RouteToDto(viewModel);
            dtos.Add(dto);
        }

        return dtos;
    }
    
    public static RuleViewModel RuleToViewModel(RuleDto dto)
    {
        return new RuleViewModel
        {
            EtherType = dto.EtherType,
            Not = dto.Not,
            Or = dto.Or,
            Type = dto.Type
        };
    }

    public static RuleDto RuleToDto(RuleViewModel viewModel)
    {
        return new RuleDto
        {
            EtherType = viewModel.EtherType,
            Not = viewModel.Not,
            Or = viewModel.Or,
            Type = viewModel.Type
        };
    }
    
    public static List<RuleViewModel> RuleToViewModels(List<RuleDto> dtos)
    {
        List<RuleViewModel> viewModels = new List<RuleViewModel>();
        foreach (var dto in dtos)
        {
            RuleViewModel viewModel = RuleToViewModel(dto);
            viewModels.Add(viewModel);
        }

        return viewModels;
    }

    public static List<RuleDto> RuleToDtos(List<RuleViewModel> viewModels)
    {
        List<RuleDto> dtos = new List<RuleDto>();
        foreach (var viewModel in viewModels)
        {
            RuleDto dto = RuleToDto(viewModel);
            dtos.Add(dto);
        }

        return dtos;
    }
    
    public static SsoConfigViewModel SsoConfigToViewModel(SsoConfigDto dto)
    {
        return new SsoConfigViewModel
        {
            Enabled = dto.Enabled,
            Mode = dto.Mode
        };
    }

    public static SsoConfigDto SsoConfigToDto(SsoConfigViewModel viewModel)
    {
        return new SsoConfigDto
        {
            Enabled = viewModel.Enabled,
            Mode = viewModel.Mode
        };
    }
    
    public static UserInterfaceSettingsViewModel? UiToViewModel(UserInterfaceSettingsDto dto)
    {
        if (dto == null)
        {
            return null;
        }

        return new UserInterfaceSettingsViewModel
        {
            MembersHelpCollapsed = dto.MembersHelpCollapsed,
            RulesHelpCollapsed = dto.RulesHelpCollapsed,
            SettingsHelpCollapsed = dto.SettingsHelpCollapsed,
            V4EasyMode = dto.V4EasyMode
        };
    }

    public static UserInterfaceSettingsDto? UiToDto(UserInterfaceSettingsViewModel viewModel)
    {
        if (viewModel == null)
        {
            return null;
        }
        
        return new UserInterfaceSettingsDto
        {
            MembersHelpCollapsed = viewModel.MembersHelpCollapsed,
            RulesHelpCollapsed = viewModel.RulesHelpCollapsed,
            SettingsHelpCollapsed = viewModel.SettingsHelpCollapsed,
            V4EasyMode = viewModel.V4EasyMode
        };
    }
    
    public static V4AssignModeViewModel V4AssignModeToViewModel(V4AssignModeDto dto)
    {
        return new V4AssignModeViewModel
        {
            Zt = dto.Zt
        };
    }

    public static V4AssignModeDto V4AssignModeToDto(V4AssignModeViewModel viewModel)
    {
        return new V4AssignModeDto
        {
            Zt = viewModel.Zt
        };
    }
    
    public static V6AssignModeViewModel V6AssignModeToViewModel(V6AssignModeDto dto)
    {
        return new V6AssignModeViewModel
        {
            Sixplane = dto.Sixplane,
            Rfc4193 = dto.Rfc4193,
            Zt = dto.Zt
        };
    }

    public static V6AssignModeDto V6AssignModeToDto(V6AssignModeViewModel viewModel)
    {
        return new V6AssignModeDto
        {
            Sixplane = viewModel.Sixplane,
            Rfc4193 = viewModel.Rfc4193,
            Zt = viewModel.Zt
        };
    }

    // Méthode pour convertir une liste de DTOs en une liste de ViewModels
    public static List<NetworkViewModel> NetworksToViewModels(List<NetworkDto> dtoList)
    {
        var viewModelList = new List<NetworkViewModel>();
        foreach (var dto in dtoList)
        {
            viewModelList.Add(NetworkToViewModel(dto));
        }
        return viewModelList;
    }

    // Méthode pour convertir une liste de ViewModels en une liste de DTOs
    public static List<NetworkDto> NetworksToDtos(List<NetworkViewModel> viewModelList)
    {
        var dtoList = new List<NetworkDto>();
        foreach (var viewModel in viewModelList)
        {
            dtoList.Add(NetworkToDto(viewModel));
        }
        return dtoList;
    }
}
