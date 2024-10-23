namespace ZeroTier.DTO.NetworkDtos
{
    public class DnsConfigDto
    {
        public string Domain { get; set; } = string.Empty;
        public List<string>? Servers { get; set; }
    }
}
