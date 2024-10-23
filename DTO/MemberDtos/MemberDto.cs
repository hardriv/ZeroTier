using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ZeroTier.DTO.MemberDtos
{
    public class MemberDto
    {
        public required string Id { get; set; }
        public required string Type { get; set; }
        public required long Clock { get; set; }
        public required string NetworkId { get; set; }
        public required string NodeId { get; set; }
        public required string ControllerId { get; set; }
        public bool Hidden { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public required MemberConfigDto Config { get; set; }
        public long LastOnline { get; set; }
        public long LastSeen { get; set; }
        public string? PhysicalAddress { get; set; }
        public object? PhysicalLocation { get; set; }
        public string? ClientVersion { get; set; }
        public int ProtocolVersion { get; set; }
        public bool SupportsRulesEngine { get; set; }
    }
}
