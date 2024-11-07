using System.Text.Json.Serialization;

namespace ZeroTier.DTO.NetworkDtos
{
    public class V6AssignModeDto
    {
        [JsonPropertyName("6plane")]
        public bool Sixplane { get; set; }
        public bool Rfc4193 { get; set; }
        public bool Zt { get; set; }
    }
}
