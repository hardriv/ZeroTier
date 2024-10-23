using System.Collections.Generic;

namespace ZeroTier.ViewModels.MemberModels
{
    public class MemberConfigViewModel
    {
        public bool ActiveBridge { get; set; }
        public required string Address { get; set; }
        public bool Authorized { get; set; }
        public List<object>? Capabilities { get; set; }
        public DateTime CreationTime { get; set; }
        public required string Id { get; set; }
        public required string Identity { get; set; }
        public required string IpAssignment { get; set; }
        public DateTime LastAuthorizedTime { get; set; }
        public DateTime LastDeauthorizedTime { get; set; }
        public bool NoAutoAssignIps { get; set; }
        public required string Nwid { get; set; }
        public required string Objtype { get; set; }
        public int RemoteTraceLevel { get; set; }
        public string? RemoteTraceTarget { get; set; }
        public int Revision { get; set; }
        public List<object>? Tags { get; set; }
        public int VMajor { get; set; }
        public int VMinor { get; set; }
        public int VRev { get; set; }
        public int VProto { get; set; }
        public bool SsoExempt { get; set; }
    }
}
