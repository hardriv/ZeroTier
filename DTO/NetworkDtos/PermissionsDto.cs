using System.Collections.Generic;

namespace ZeroTier.DTO.NetworkDtos
{
    public class PermissionsDto
    {
        public Dictionary<string, PermissionDetailDto> PermissionDetails { get; set; } = new Dictionary<string, PermissionDetailDto>();
    }

    public class PermissionDetailDto
    {
        public bool A { get; set; }
        public bool D { get; set; }
        public bool M { get; set; }
        public bool R { get; set; }
    }
}
