using System.Collections.Generic;
using System.Net.Http.Headers;

namespace ZeroTier.ViewModels.NetworkModels
{
    public class NetworkConfigViewModel
    {
        public List<string>? AuthTokens { get; set; }
        public DateTime CreationTime { get; set; }
        public List<object>? Capabilities { get; set; }
        public bool EnableBroadcast { get; set; }
        public required string Id { get; set; }
        public IpAssignmentPoolViewModel IpAssignmentPool { get; set; } = new();
        public DateTime LastModified { get; set; }
        public int Mtu { get; set; }
        public int MulticastLimit { get; set; }
        public required string Name { get; set; }
        public bool Private { get; set; }
        public int RemoteTraceLevel { get; set; }
        public string? RemoteTraceTarget { get; set; }
        public List<RouteViewModel> Routes { get; set; } = [];
        public List<RuleViewModel> Rules { get; set; } = [];
        public List<object>? Tags { get; set; } = [];
        public V4AssignModeViewModel V4AssignMode { get; set; } = new();
        public V6AssignModeViewModel V6AssignMode { get; set; } = new();
        public DnsConfigViewModel Dns { get; set; } = new();
        public SsoConfigViewModel SsoConfig { get; set; } = new();
    }
}
