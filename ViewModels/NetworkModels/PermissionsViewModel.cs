using System.Collections.Generic;

namespace ZeroTier.ViewModels.NetworkModels
{
    public class PermissionsViewModel
    {
        public Dictionary<string, PermissionDetailViewModel> PermissionDetails { get; set; } = new Dictionary<string, PermissionDetailViewModel>();
    }

    public class PermissionDetailViewModel
    {
        public bool A { get; set; }
        public bool D { get; set; }
        public bool M { get; set; }
        public bool R { get; set; }
    }
}
