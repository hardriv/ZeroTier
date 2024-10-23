using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using ZeroTier.ViewModels.NetworkModels;
using ZeroTier.Services;
using ZeroTier.Utils;
using ZeroTier.Views;

namespace ZeroTier
{
    public partial class MainWindow : Window
    {
        private readonly APIClient apiClient = new APIClient();
        public NetworkSectionControl networkSectionControl;
        private readonly MembersListControl membersListControl;
        private readonly TextBlock errorText;
        public TextBox ApiTokenTextBox;
        public Button connectButton;

        public MainWindow()
        {
            InitializeComponent();
            networkSectionControl = (NetworkSectionControl)FindName("NetworkSectionControl");
            membersListControl = (MembersListControl)FindName("MembersListControl");
            ApiTokenTextBox = (TextBox)FindName("ApiToken");
            connectButton = (Button)FindName("ConnectButton");
        
            // Passer l'APIClient à chaque contrôle
            networkSectionControl.Initialize(apiClient);
            membersListControl.Initialize(apiClient);

            // Abonnement à l'événement
            networkSectionControl.NetworkSelected += OnNetworkSelected;

            errorText = (TextBlock)FindName("ErrorText");
        }

        private void ApiToken_TextChanged(object sender, TextChangedEventArgs e)
        {
            string apiToken = ApiTokenTextBox.Text;

            // Alphanumérique, longueur de 32 caractères
            var tokenPattern = @"^[a-zA-Z0-9]{32}$";

            // Désactiver le bouton si l'une des conditions suivantes est vraie :
            // - Champ vide
            // - Longueur différente de 32 caractères
            // - Token ne correspond pas au pattern
            if (string.IsNullOrWhiteSpace(apiToken) || apiToken.Length != 32 || !Regex.IsMatch(apiToken, tokenPattern))
            {
                connectButton.IsEnabled = false;
                errorText.Text = "Le Token doit contenir exactement 32 caractères alphanumériques.";
                errorText.Visibility = Visibility.Visible;
            }
            else
            {
                connectButton.IsEnabled = true;
                errorText.Visibility = Visibility.Collapsed;
            }
        }

        private async void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            string apiToken = ApiTokenTextBox.Text;

            ApiToken_Validation(apiToken, errorText);
            
            // Si le message d'erreur est visible, on annule la connexion
            if (errorText.Visibility == Visibility.Visible)
            {
                return;
            }

            apiClient.SetApiToken(apiToken);
            
            try
            {
                var networks = await NetworkService.GetNetworks(apiClient);
                if (networks == null || networks.Count == 0)
                {
                    MessageBox.Show("No networks found or networks list is null.");
                }

                if (networks != null)
                {
                    networkSectionControl = (NetworkSectionControl)FindName("NetworkSectionControl");                    
                    networkSectionControl.networkListControl.NetworksGrid.ItemsSource = networks;
                }
                else
                {
                    MessageBox.Show("Erreur de connexion à l'API");
                }
            }
            catch (ApiException apiEx)
            {
                MessageBox.Show(apiEx.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur: {ex.Message}");
            }
        }

        private static void ApiToken_Validation(string apiToken, TextBlock errorText)
        {
            if (string.IsNullOrWhiteSpace(apiToken))
            {
                errorText.Text = "Veuillez entrer un API Token valide.";
                errorText.Visibility = Visibility.Visible;
                return;
            }

            var tokenPattern = @"^[a-zA-Z0-9]{32}$";
            if (!Regex.IsMatch(apiToken, tokenPattern))
            {
                errorText.Text = "Le Token API doit contenir exactement 32 caractères alphanumériques.";
                errorText.Visibility = Visibility.Visible;
                return;
            }

            errorText.Visibility = Visibility.Collapsed;
        }

        private async void OnNetworkSelected(object sender, NetworkViewModel selectedNetwork)
        {
            if (selectedNetwork != null)
            {
                var networkSectionControl = (NetworkSectionControl)FindName("NetworkSectionControl");
                networkSectionControl.networkDetailsControl.DisplayNetworkDetails(selectedNetwork);
                networkSectionControl.networkAdditionalDetailsControl.DisplayNetworkAdditionalDetails(selectedNetwork);
                await membersListControl.LoadMembers(selectedNetwork.Id);
            }
        }
        
        // Gestion du clic sur le lien Hyperlink
        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri) { UseShellExecute = true });
            e.Handled = true;
        }
    }
}
