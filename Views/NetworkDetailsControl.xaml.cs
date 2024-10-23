using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using ZeroTier.ViewModels.NetworkModels;
using ZeroTier.Utils;

namespace ZeroTier.Views
{
    public partial class NetworkDetailsControl : UserControl
    {
        private readonly TextBlock networkId;
        private readonly TextBlock networkName;
        private readonly TextBlock networkPrivate;
        private readonly TextBlock networkCreationTime;
        private readonly TextBlock networkModifiedTime;
        private readonly TextBlock networkOnline;
        private readonly TextBlock networkAuthorized;
        private readonly TextBlock networkTotal;

        public NetworkDetailsControl()
        {
            InitializeComponent();
            
            networkId = (TextBlock)FindName("NetworkId");
            networkName = (TextBlock)FindName("NetworkName");
            networkPrivate = (TextBlock)FindName("NetworkPrivate");
            networkCreationTime = (TextBlock)FindName("NetworkCreationTime");
            networkModifiedTime = (TextBlock)FindName("NetworkModifiedTime");
            networkOnline = (TextBlock)FindName("NetworkOnline");
            networkAuthorized = (TextBlock)FindName("NetworkAuthorized");
            networkTotal = (TextBlock)FindName("NetworkTotal");
        }

        public void DisplayNetworkDetails(NetworkViewModel network)
        {
            if (network == null) return;

            networkId.Text = $"{network.Id}";
            networkName.Text = $"{network.Config.Name}";
            networkPrivate.Text = $"{network.Config.Private}";
            networkCreationTime.Text = $"{network.Config.CreationTime}";
            networkModifiedTime.Text = $"{network.Config.LastModified}";
            networkOnline.Text = $"{network.OnlineMemberCount}";
            networkAuthorized.Text = $"{network.AuthorizedMemberCount}";
            networkTotal.Text = $"{network.TotalMemberCount}";
        }
    }
}
