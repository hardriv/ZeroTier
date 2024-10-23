using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using ZeroTier.ViewModels.NetworkModels;
using ZeroTier.Utils;

namespace ZeroTier.Views
{
    public partial class NetworkAdditionalDetailsControl : UserControl
    {
        private readonly TextBlock networkType;
        private readonly TextBlock networkRoute;
        private readonly TextBlock networkIpStart;
        private readonly TextBlock networkIpEnd;
        private readonly TextBlock networkIpV4Enabled;
        private readonly TextBlock networkIpV6Enabled;
        private readonly TextBlock networkDns;
        private readonly TextBlock networkSsoEnabled;

        public NetworkAdditionalDetailsControl()
        {
            InitializeComponent();

            networkType = (TextBlock)FindName("NetworkType");
            networkRoute = (TextBlock)FindName("NetworkRoute");
            networkIpStart = (TextBlock)FindName("NetworkIpStart");
            networkIpEnd = (TextBlock)FindName("NetworkIpEnd");
            networkIpV4Enabled = (TextBlock)FindName("NetworkIpV4Enabled");
            networkIpV6Enabled = (TextBlock)FindName("NetworkIpV6Enabled");
            networkDns = (TextBlock)FindName("NetworkDns");
            networkSsoEnabled = (TextBlock)FindName("NetworkSsoEnabled");
        }

        public void DisplayNetworkAdditionalDetails(NetworkViewModel network)
        {
            if (network == null || network.Config == null)
            {
                return;
            }

            networkType.Text = $"{network.Type}";
            
            if (NullController.IsCollectionValid(network.Config.Routes))
            {
                networkRoute.Text = $"{NullController.GetSafeValue(network.Config.Routes[0].Target)}";
            }


            if (NullController.IsCollectionValid(network.Config.IpAssignmentPools))
            {
                networkIpStart.Text = $"{NullController.GetSafeValue(network.Config.IpAssignmentPools[0].IpRangeStart)}";
                networkIpEnd.Text = $"{NullController.GetSafeValue(network.Config.IpAssignmentPools[0].IpRangeEnd)}";
            }

            networkIpV4Enabled.Text = $"{network.Config.V4AssignMode.Zt}";
            networkIpV6Enabled.Text = $"{network.Config.V6AssignMode.Zt}";
            networkDns.Text = $"{network.Config.Dns.Domain}";
            networkSsoEnabled.Text = $"{network.Config.SsoConfig.Enabled}";
        }
    }
}
