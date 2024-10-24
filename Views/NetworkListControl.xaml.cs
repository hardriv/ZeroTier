using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using ZeroTier.ViewModels.NetworkModels;
using ZeroTier.Services;
using ZeroTier.Utils;

namespace ZeroTier.Views
{
    public partial class NetworkListControl : UserControl
    {
        private APIClient apiClient = new();
        public DataGrid networksGrid = new();
        public DataGrid MembersGrid = new();
        public event EventHandler<NetworkViewModel> NetworkSelected = delegate { };

        public NetworkListControl()
        {
            InitializeComponent();

            networksGrid = (DataGrid)FindName("NetworksGrid");
            networksGrid.SelectionChanged += NetworksGrid_SelectionChanged;
        }
        
        // Si tu as besoin de passer l'APIClient, fais-le par une méthode ou propriété
        public void Initialize(APIClient apiClient)
        {
            this.apiClient = apiClient;
        }

        private void NetworksGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (networksGrid.SelectedItem is NetworkViewModel selectedNetwork)
            {
                NetworkSelected?.Invoke(this, selectedNetwork);
            }
        }

        private async void DeleteNetwork_Click(object sender, RoutedEventArgs e)
        {
            var network = ((Button)sender).DataContext as NetworkViewModel;
            if (network != null)
            {
                var result = MessageBox.Show($"Êtes-vous sûr de vouloir supprimer le réseau {network.Config.Name} ?", 
                                              "Confirmation de suppression", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    bool isDeleted = await NetworkService.DeleteNetwork(apiClient, network.Id);
                    if (isDeleted)
                    {
                        var networks = await NetworkService.GetNetworks(apiClient);
                        networksGrid.ItemsSource = networks;
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
