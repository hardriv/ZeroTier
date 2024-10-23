using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ZeroTier.ViewModels.NetworkModels;
using ZeroTier.Utils;

namespace ZeroTier.Views
{
    public partial class NetworkSectionControl : UserControl
    {
        private APIClient apiClient = new();
        public NetworkListControl networkListControl;
        public NetworkDetailsControl networkDetailsControl;
        public NetworkAdditionalDetailsControl networkAdditionalDetailsControl;
        public event EventHandler<NetworkViewModel> NetworkSelected;

        public NetworkSectionControl()
        {
            InitializeComponent();
            networkListControl = (NetworkListControl)FindName("NetworkListControl");
            networkDetailsControl = (NetworkDetailsControl)FindName("NetworkDetailsControl");
            networkAdditionalDetailsControl = (NetworkAdditionalDetailsControl)FindName("NetworkAdditionalDetailsControl");
            
            if (networkListControl == null)
            {
                MessageBox.Show("NetworkListControl is not found!");
            }
            else
            {
                networkListControl.NetworkSelected += OnNetworkSelected;
            }
        }

        // Si tu as besoin de passer l'APIClient, fais-le par une méthode ou propriété
        public void Initialize(APIClient apiClient)
        {
            this.apiClient = apiClient;
            networkListControl.Initialize(apiClient);
        }

        private void OnNetworkSelected(object sender, NetworkViewModel selectedNetwork)
        {
            NetworkSelected?.Invoke(this, selectedNetwork);
            networkDetailsControl.DisplayNetworkDetails(selectedNetwork);
            networkAdditionalDetailsControl.DisplayNetworkAdditionalDetails(selectedNetwork);
        }
    }
}
