using System.Collections.Generic;

namespace ZeroTier.DTO.MemberDtos
{
    public class MemberConfigUpdateDto
    {
        public bool ActiveBridge { get; set; }
        public bool Authorized { get; set; }
        public List<object>? Capabilities { get; set; }
        public required List<string> IpAssignments { get; set; }
        public bool NoAutoAssignIps { get; set; }
        public List<object>? Tags { get; set; }
        public bool SsoExempt { get; set; }
    }
}
