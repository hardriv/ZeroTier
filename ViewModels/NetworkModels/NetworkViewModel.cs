using System;
using System.Collections.Generic;

namespace ZeroTier.ViewModels.NetworkModels
{
    public class NetworkViewModel
    {
        public required string Id { get; set; }
        public string? Type { get; set; }
        public required DateTime Clock { get; set; }
        public required NetworkConfigViewModel Config { get; set; }
        public string? Description { get; set; }
        public string RulesSource { get; set; } = string.Empty;
        public PermissionsViewModel? Permissions { get; set; }
        public string OwnerId { get; set; } = string.Empty;
        public required int OnlineMemberCount { get; set; }
        public int AuthorizedMemberCount { get; set; }
        public int TotalMemberCount { get; set; }
        public Dictionary<string, bool> CapabilitiesByName { get; set; } = new Dictionary<string, bool>();
        public Dictionary<string, bool> TagsByName { get; set; } = new Dictionary<string, bool>();
        public UserInterfaceSettingsViewModel? Ui { get; set; }
    }
}
