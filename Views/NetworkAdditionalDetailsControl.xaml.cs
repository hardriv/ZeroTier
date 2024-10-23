using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using ZeroTier.ViewModels.NetworkModels;
using ZeroTier.Utils;

namespace ZeroTier.Views
{
    public partial class NetworkAdditionalDetailsControl : UserControl
    {
        public NetworkAdditionalDetailsControl()
        {
            InitializeComponent();
        }

        public void DisplayNetworkAdditionalDetails(NetworkViewModel network)
        {
            if (network == null) return;

            NetworkType.Text = $"{network.Type}";
            
            if (NullController.IsCollectionValid(network.Config?.Routes))
            {
                NetworkRoute.Text = $"{NullController.GetSafeValue(network.Config.Routes[0].Target)}";
            }


            if (NullController.IsCollectionValid(network.Config?.IpAssignmentPools))
            {
                NetworkIpStart.Text = $"{NullController.GetSafeValue(network.Config.IpAssignmentPools[0].IpRangeStart)}";
                NetworkIpEnd.Text = $"{NullController.GetSafeValue(network.Config.IpAssignmentPools[0].IpRangeEnd)}";
            }

            NetworkIpV4Enabled.Text = $"{network.Config.V4AssignMode.Zt}";
            NetworkIpV6Enabled.Text = $"{network.Config.V6AssignMode.Zt}";
            NetworkDns.Text = $"{network.Config.Dns.Domain}";
            NetworkSsoEnabled.Text = $"{network.Config.SsoConfig.Enabled}";
        }
    }
}
