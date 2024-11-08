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
using System.Collections.ObjectModel;

namespace ZeroTier
{
    public partial class MainWindow : Window
    {
        private readonly APIClient apiClient = new();
        private NetworkSectionControl _networkSectionControl = new();
        private MembersListControl _membersListControl = new();
        private readonly NetworkService _networkService = new(new());
        //private MemberService memberService;

        private TextBlock _errorText = new();
        private TextBox ApiTokenTextBox = new();
        private Button connectButton = new();

        public MainWindow()
        {
            InitializeComponent();
            InitializeControls();
            InitializeEventHandlers();
        }

        private void InitializeControls()
        {
            // Utiliser FindName avec vérification de null
            _networkSectionControl = (NetworkSectionControl)FindName("NetworkSectionControl")
                                    ?? throw new NullReferenceException("NetworkSectionControl non trouvé");

            _membersListControl = (MembersListControl)FindName("MembersListControl")
                                 ?? throw new NullReferenceException("MembersListControl non trouvé");

            _errorText = (TextBlock)FindName("ErrorText") ?? new TextBlock();

            ApiTokenTextBox = (TextBox)FindName("ApiToken")
                              ?? throw new NullReferenceException("ApiToken non trouvé");

            connectButton = (Button)FindName("ConnectButton")
                            ?? throw new NullReferenceException("ConnectButton non trouvé");

            // Passer l'APIClient à chaque contrôle
            _networkSectionControl.Initialize(apiClient, _networkService);
            _membersListControl.Initialize(apiClient);
        }

        private void InitializeEventHandlers()
        {
            // Abonnement à l'événement NetworkSelectedEvent avec vérification de nullabilité
            _networkSectionControl.NetworkSelectedEvent += OnNetworkSelected;
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
                _errorText.Text = "Le Token doit contenir exactement 32 caractères alphanumériques.";
                _errorText.Visibility = Visibility.Visible;
            }
            else
            {
                connectButton.IsEnabled = true;
                _errorText.Visibility = Visibility.Collapsed;
            }
        }

        private async void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            string apiToken = ApiTokenTextBox.Text;

            ApiToken_Validation(apiToken, _errorText);
            
            // Si le message d'erreur est visible, on annule la connexion
            if (_errorText.Visibility == Visibility.Visible)
            {
                return;
            }

            apiClient.SetApiToken(apiToken);
            
            ObservableCollection<NetworkViewModel> networks = new(await _networkService.GetNetworks(apiClient) ?? []);
            if (networks == null || networks.Count == 0)
            {
                MessageBox.Show("No networks found or networks list is null.");
            }
            else
            {
                //_networkSectionControl = (NetworkSectionControl)FindName("NetworkSectionControl");   
                //_networkSectionControl.Initialize(apiClient, _networkService);
                _networkSectionControl.NetworkListControl.NetworksGrid.ItemsSource = networks;
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
                NetworkViewModel network = await _networkService.GetNetworkById(apiClient, selectedNetwork.Id);
                _networkSectionControl.NetworkEditionControl.SelectedNetwork = network;

                try
                {
                    await _membersListControl.LoadMembers(selectedNetwork.Id);
                }
                catch (Exception ex)
                {
                    _errorText.Text = $"Error loading members : {ex.Message}";
                }
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
