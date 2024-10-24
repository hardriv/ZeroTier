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
        public NetworkListControl networkListControl = new();
        public NetworkDetailsControl networkDetailsControl = new();
        public NetworkAdditionalDetailsControl networkAdditionalDetailsControl = new();
        public event EventHandler<NetworkViewModel> NetworkSelected = delegate { };

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
                networkListControl.NetworkSelected += OnNetworkSelected; // TODO corriger le warning null
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
