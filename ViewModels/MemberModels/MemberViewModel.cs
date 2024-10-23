using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ZeroTier.ViewModels.MemberModels
{
    public class MemberViewModel
    {
        public required string Id { get; set; }
        public required string Type { get; set; }
        public required DateTime Clock { get; set; }
        public required string NetworkId { get; set; }
        public required string NodeId { get; set; }
        public required string ControllerId { get; set; }
        public bool Hidden { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public required MemberConfigViewModel Config { get; set; }
        public DateTime LastOnline { get; set; }
        public DateTime LastSeen { get; set; }
        public string? PhysicalAddress { get; set; }
        public object? PhysicalLocation { get; set; }
        public string? ClientVersion { get; set; }
        public int ProtocolVersion { get; set; }
        public bool SupportsRulesEngine { get; set; }
        public bool IsSelected { get; set; }
    }
}
