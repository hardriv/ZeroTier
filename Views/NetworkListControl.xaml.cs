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
        private readonly DataGrid _networksGrid;
        
        public event EventHandler<NetworkViewModel>? NetworkSelected = delegate { };

        public NetworkListControl()
        {
            InitializeComponent();
            _networksGrid = (DataGrid)FindName("NetworksGrid") ?? throw new NullReferenceException("NetworksGrid non trouvé");
            _networksGrid.SelectionChanged += NetworksGrid_SelectionChanged;
        }

        // Méthode pour initialiser l'API client
        public void Initialize(APIClient apiClient)
        {
            this._apiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));
        }

        private void NetworksGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_networksGrid.SelectedItem is NetworkViewModel selectedNetwork)
            {
                NetworkSelected?.Invoke(this, selectedNetwork);
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
                    bool isDeleted = await NetworkService.DeleteNetwork(_apiClient, network.Id);
                    if (isDeleted)
                    {
                        var networks = await NetworkService.GetNetworks(_apiClient);
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
