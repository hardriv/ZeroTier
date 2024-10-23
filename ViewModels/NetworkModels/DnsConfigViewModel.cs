namespace ZeroTier.ViewModels.NetworkModels
{
    public class DnsConfigViewModel
    {
        public string Domain { get; set; } = string.Empty;
        public List<string>? Servers { get; set; }
    }
}
