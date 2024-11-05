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
        private APIClient _apiClient = new();
        private NetworkViewModel? _selectedNetwork;
        private TextBlock _selectedTextBlock = new();
        public NetworkListControl networkListControl = new();
        public NetworkDetailsControl networkDetailsControl = new();
        public NetworkAdditionalDetailsControl networkAdditionalDetailsControl = new();
        public event EventHandler<NetworkViewModel> NetworkSelectedEvent = delegate { };

        public NetworkSectionControl()
        {
            InitializeComponent();
            networkListControl = (NetworkListControl)FindName("NetworkListControl"); 
            
            if (networkListControl == null)
            {
                MessageBox.Show("NetworkListControl is not found!");
            }
            else
            {
                networkListControl.NetworkSelected += OnNetworkSelected; // TODO corriger le warning null
            }
        }

        public void Initialize(APIClient apiClient)
        {
            this._apiClient = apiClient;
            networkListControl.Initialize(apiClient);
            //Ipv6AutoRangeGrid.Visibility = Visibility.Collapsed;
        }

        private void OnNetworkSelected(object sender, NetworkViewModel selectedNetwork)
        {
            _selectedNetwork = selectedNetwork;
            NetworkSelectedEvent?.Invoke(this, _selectedNetwork);
            Debug.WriteLine("OnNetworkSelected");
            InitializeSelectedIp();
        }


        private void InitializeSelectedIp()
        {
            // Récupérer l'IP stockée
            var storedIp = _selectedNetwork.Config.Routes[0].Target.Split("/")[0];
            Debug.WriteLine("InitializeSelectedIp");

            // Vérification des TextBlocks
            foreach (TextBlock textBlock in IPv4AutoAssignGrid.Children)
            {
                string regex = textBlock.Text.Replace(".", "\\.").Replace("*", "[^\\.]+");
                if (Regex.IsMatch(storedIp, regex))
                {
                    SelectTextBlock(textBlock);
                    break;
                }
            }
        }

        private void IPv4AutoAssign_Checked(object sender, RoutedEventArgs e)
        {
            // Activer la sélection uniquement si IPv4AutoAssign est coché
            IPv4AutoAssignGrid.IsEnabled = true;
        }

        private void IPv4AutoAssign_Unchecked(object sender, RoutedEventArgs e)
        {
            // Désactiver la sélection si IPv4AutoAssign est décoché
            IPv4AutoAssignGrid.IsEnabled = false;
            DeselectTextBlock();
        }

        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is TextBlock textBlock && IPv4AutoAssign.IsChecked == true)
            {
                SelectTextBlock(textBlock);
            }
        }

        private void SelectTextBlock(TextBlock textBlock)
        {
            // Désélectionner le précédent
            DeselectTextBlock();

            // Sélectionner le nouveau et changer le fond
            IPv4AutoAssign.IsChecked = true;
            _selectedTextBlock = textBlock;
            _selectedTextBlock.Tag = "Selected";
        }
        
        private void DeselectTextBlock()
        {
            // Retirer la sélection précédente si elle existe
            if (_selectedTextBlock != null)
            {
                _selectedTextBlock.ClearValue(TextBlock.TagProperty);
                _selectedTextBlock = null;
            }
        }

        private void SubmitIPv4AutoAssign_Click(object sender, RoutedEventArgs e)
        {
            // Mettre à jour Network.Config.IpAssignmentPoolViewModel[0] avec le texte du TextBlock sélectionné
            if (_selectedTextBlock != null)
            {
                _selectedNetwork.Config.Routes[0].Target = _selectedTextBlock.Text;
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner une adresse IP avant de soumettre.", "Aucune sélection", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            if (checkBox.IsChecked == true)
            {
                // Vérifier si une autre checkbox est déjà sélectionnée
                foreach (CheckBox cb in new[] { IPv6RFC4193CheckBox, IPv66PLANECheckBox, IPv6utoAssignCheckBox })
                {
                    if (cb.IsChecked == true && cb != checkBox)
                    {
                        cb.IsChecked = false;
                    }
                }
            }
        }

    }
}
