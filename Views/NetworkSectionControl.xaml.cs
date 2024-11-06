using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ZeroTier.ViewModels.NetworkModels;
using ZeroTier.Utils;
using System.Windows.Media.Animation;
using System.Net;
using System.Windows.Input;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace ZeroTier.Views
{
    public partial class NetworkSectionControl : UserControl
    {
        private NetworkListControl _networkListControl = new();
        private NetworkEditionControl _networkEditionControl = new();

        public event EventHandler<NetworkViewModel> NetworkSelectedEvent = delegate { };

        public NetworkSectionControl()
        {
            InitializeComponent();
            InitializeControls();
            InitializeEvents();
        }

        private void InitializeControls()
        {
            _networkListControl = (NetworkListControl)FindName("NetworkListControl")
                ?? throw new NullReferenceException("NetworkListControl non trouvé");

            _networkEditionControl = (NetworkEditionControl)FindName("NetworkEditionControl")
                ?? throw new NullReferenceException("NetworkEditionControl non trouvé");
        }

        private void InitializeEvents()
        {
            if (_networkListControl != null)
            {
                _networkListControl.NetworkSelected += OnNetworkSelected;
            }

            if (_networkEditionControl != null)
            {
                _networkEditionControl.NetworkSelectedEvent += OnNetworkSelected;
            }
        }

        public void Initialize(APIClient apiClient)
        {
            _networkListControl?.Initialize(apiClient);
            _networkEditionControl?.Initialize(apiClient);
        }

        public void OnNetworkSelected(object sender, NetworkViewModel selectedNetwork)
        {
            NetworkSelectedEvent?.Invoke(this, selectedNetwork);
        }

    }
}
