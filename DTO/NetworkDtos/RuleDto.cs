namespace ZeroTier.DTO.NetworkDtos
{
    public class RuleDto
    {
        public int? EtherType { get; set; }
        public bool? Not { get; set; }
        public bool? Or { get; set; }
        public string? Type { get; set; }
    }
}
