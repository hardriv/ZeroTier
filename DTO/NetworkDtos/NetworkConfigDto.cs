using System.Collections.Generic;

namespace ZeroTier.DTO.NetworkDtos
{
    public class NetworkConfigDto
    {
        public List<string>? AuthTokens { get; set; }
        public long CreationTime { get; set; }
        public List<object>? Capabilities { get; set; }
        public bool EnableBroadcast { get; set; }
        public required string Id { get; set; }
        public List<IpAssignmentPoolDto> IpAssignmentPools { get; set; } = new List<IpAssignmentPoolDto>();
        public long LastModified { get; set; }
        public int Mtu { get; set; }
        public int MulticastLimit { get; set; }
        public required string Name { get; set; }
        public bool Private { get; set; }
        public int RemoteTraceLevel { get; set; }
        public string? RemoteTraceTarget { get; set; }
        public List<RouteDto> Routes { get; set; } = new List<RouteDto>();
        public List<RuleDto> Rules { get; set; } = new List<RuleDto>();
        public List<object>? Tags { get; set; } = new List<object>();
        public V4AssignModeDto V4AssignMode { get; set; } = new V4AssignModeDto();
        public V6AssignModeDto V6AssignMode { get; set; } = new V6AssignModeDto();
        public DnsConfigDto Dns { get; set; } = new DnsConfigDto();
        public SsoConfigDto SsoConfig { get; set; } = new SsoConfigDto();
    }
}
