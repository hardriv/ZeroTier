using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using ZeroTier.ViewModels.NetworkModels;
using ZeroTier.Services;
using ZeroTier.Utils;
using System.Diagnostics;

namespace ZeroTier.Views
{
    public partial class NetworkListControl : UserControl
    {
        private APIClient? _apiClient;
        private NetworkService? _networkService;

        private readonly DataGrid _networksGrid;
        
        public event EventHandler<NetworkViewModel>? NetworkSelectedEvent = delegate { };

        public NetworkListControl()
        {
            InitializeComponent();
            _networksGrid = (DataGrid)FindName("NetworksGrid") ?? throw new NullReferenceException("NetworksGrid non trouvé");
            _networksGrid.SelectionChanged += NetworksGrid_SelectionChanged;
        }

        public void Initialize(APIClient apiClient, NetworkService networkService)
        {
            this._apiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));
            this._networkService = networkService;
        }

        private void NetworksGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_networksGrid.SelectedItem is NetworkViewModel selectedNetwork)
            {
                NetworkSelectedEvent?.Invoke(this, selectedNetwork);
            }
        }

        private async void DeleteNetwork_Click(object sender, RoutedEventArgs e)
        {
            if (((Button)sender).DataContext is NetworkViewModel network)
            {
                var result = MessageBox.Show($"Êtes-vous sûr de vouloir supprimer le réseau {network.Config.Name} ?",
                                              "Confirmation de suppression", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    bool isDeleted = await _networkService.DeleteNetwork(_apiClient, network.Id);
                    if (isDeleted)
                    {
                        var networks = await _networkService.GetNetworks(_apiClient);
                        _networksGrid.ItemsSource = networks;
                    }
                    else
                    {
                        MessageBox.Show("Erreur lors de la suppression du réseau");
                    }
                }
            }
        }
    }
}
