using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ZeroTier.Utils;
using ZeroTier.ViewModels.NetworkModels;

namespace ZeroTier.Views
{
    /// <summary>
    /// Logique d'interaction pour NetworkEditionControl.xaml
    /// </summary>
    public partial class NetworkEditionControl : UserControl
    {
        private APIClient? _apiClient;
        private NetworkViewModel? _selectedNetwork;
        private TextBlock? _selectedTextBlock;

        public NetworkViewModel? SelectedNetwork
        {
            get => _selectedNetwork;
            set
            {
                if (_selectedNetwork != value)
                {
                    _selectedNetwork = value;
                    OnSelectedNetworkChanged();
                }
            }
        }

        public event EventHandler<NetworkViewModel>? NetworkSelectedEvent;

        public NetworkEditionControl()
        {
            InitializeComponent();
        }

        public void Initialize(APIClient apiClient)
        {
            _apiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));
        }

        private void OnSelectedNetworkChanged()
        {
            if (SelectedNetwork != null)
            {
                NetworkId.Text = $"{SelectedNetwork.Id}";
                InitializeSelectedIp();
            }
            else
            {
                NetworkId.Text = "Aucun réseau sélectionné";
            }
        }

        private void InitializeSelectedIp()
        {
            // Récupérer l'IP stockée
            var storedIp = SelectedNetwork.Config.Routes[0].Target.Split("/")[0];

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
                SelectedNetwork.Config.Routes[0].Target = _selectedTextBlock.Text;
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
