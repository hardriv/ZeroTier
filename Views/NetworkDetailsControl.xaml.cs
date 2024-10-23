using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using ZeroTier.ViewModels.NetworkModels;
using ZeroTier.Utils;

namespace ZeroTier.Views
{
    public partial class NetworkDetailsControl : UserControl
    {
        public NetworkDetailsControl()
        {
            InitializeComponent();
        }

        public void DisplayNetworkDetails(NetworkViewModel network)
        {
            if (network == null) return;

            NetworkId.Text = $"{network.Id}";
            NetworkName.Text = $"{network.Config.Name}";
            NetworkPrivate.Text = $"{network.Config.Private}";
            NetworkCreationTime.Text = $"{network.Config.CreationTime}";
            NetworkModifiedTime.Text = $"{network.Config.LastModified}";
            NetworkOnline.Text = $"{network.OnlineMemberCount}";
            NetworkAuthorized.Text = $"{network.AuthorizedMemberCount}";
            NetworkTotal.Text = $"{network.TotalMemberCount}";
        }
    }
}
