using System.Collections.Generic;

namespace ZeroTier.ViewModels.NetworkModels
{
    public class NetworkConfigViewModel
    {
        public List<string>? AuthTokens { get; set; }
        public DateTime CreationTime { get; set; }
        public List<object>? Capabilities { get; set; }
        public bool EnableBroadcast { get; set; }
        public required string Id { get; set; }
        public List<IpAssignmentPoolViewModel> IpAssignmentPools { get; set; } = new List<IpAssignmentPoolViewModel>();
        public DateTime LastModified { get; set; }
        public int Mtu { get; set; }
        public int MulticastLimit { get; set; }
        public required string Name { get; set; }
        public bool Private { get; set; }
        public int RemoteTraceLevel { get; set; }
        public string? RemoteTraceTarget { get; set; }
        public List<RouteViewModel> Routes { get; set; } = new List<RouteViewModel>();
        public List<RuleViewModel> Rules { get; set; } = new List<RuleViewModel>();
        public List<object>? Tags { get; set; } = new List<object>();
        public V4AssignModeViewModel V4AssignMode { get; set; } = new V4AssignModeViewModel();
        public V6AssignModeViewModel V6AssignMode { get; set; } = new V6AssignModeViewModel();
        public DnsConfigViewModel Dns { get; set; } = new DnsConfigViewModel();
        public SsoConfigViewModel SsoConfig { get; set; } = new SsoConfigViewModel();
    }
}
