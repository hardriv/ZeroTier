using System;
using System.Collections.Generic;

namespace ZeroTier.DTO.NetworkDtos
{
    public class NetworkDto
    {
        public required string Id { get; set; }
        public string? Type { get; set; }
        public required long Clock { get; set; }
        public required NetworkConfigDto Config { get; set; }
        public string? Description { get; set; }
        public string RulesSource { get; set; } = string.Empty;
        public PermissionsDto Permissions { get; set; } = new PermissionsDto();
        public string OwnerId { get; set; } = string.Empty;
        public required int OnlineMemberCount { get; set; }
        public int AuthorizedMemberCount { get; set; }
        public int TotalMemberCount { get; set; }
        public Dictionary<string, bool> CapabilitiesByName { get; set; } = new Dictionary<string, bool>();
        public Dictionary<string, bool> TagsByName { get; set; } = new Dictionary<string, bool>();
        public UserInterfaceSettingsDto Ui { get; set; } = new UserInterfaceSettingsDto();
    }
}
