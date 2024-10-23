using System.Collections.Generic;

namespace ZeroTier.DTO.MemberDtos
{
    public class MemberConfigDto
    {
        public bool ActiveBridge { get; set; }
        public required string Address { get; set; }
        public bool Authorized { get; set; }
        public List<object>? Capabilities { get; set; }
        public long CreationTime { get; set; }
        public required string Id { get; set; }
        public required string Identity { get; set; }
        public required List<string> IpAssignments { get; set; }
        public long LastAuthorizedTime { get; set; }
        public long LastDeauthorizedTime { get; set; }
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
