using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ZeroTier.ViewModels.MemberModels;
using ZeroTier.Services;
using ZeroTier.Utils;

namespace ZeroTier.Views
{
    public partial class MembersListControl : UserControl
    {
        private APIClient apiClient = new();

        public MembersListControl()
        {
            InitializeComponent();
            MembersDataGrid.SelectionChanged += MembersDataGrid_SelectionChanged;
        }
        
        // Si tu as besoin de passer l'APIClient, fais-le par une méthode ou propriété
        public void Initialize(APIClient apiClient)
        {
            this.apiClient = apiClient;
        }

        public async Task LoadMembers(string networkId)
        {
            var members = await MemberService.GetMembers(apiClient, networkId);

            if (members != null)
            {
                MembersDataGrid.ItemsSource = members;
            }
            else
            {
                MessageBox.Show("Erreur lors du chargement des membres");
            }
        }

        // Sélectionner/Désélectionner tous les membres
        private void SelectAllCheckBox_Click(object sender, RoutedEventArgs e)
        {
            bool isChecked = (sender as CheckBox)?.IsChecked ?? false;

            if (MembersDataGrid.ItemsSource is List<MemberViewModel> members)
            {
                foreach (var member in members)
                {
                    member.IsSelected = isChecked;
                }
                // Met à jour l'affichage pour refléter les changements
                MembersDataGrid.Items.Refresh();
            }
        }

        private void MembersDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var hasSelection = MembersDataGrid.SelectedItems.Count > 0;
            AuthorizeSelection.IsEnabled = hasSelection;
            DenySelection.IsEnabled = hasSelection;
            DeleteSelection.IsEnabled = hasSelection;
        }

        private async void AuthorizeMembers_Click(object sender, RoutedEventArgs e)
        {
            var selectedMembers = MembersDataGrid.SelectedItems.Cast<MemberViewModel>().ToList();
            if (MessageBox.Show("Êtes-vous sûr de vouloir autoriser les membres sélectionnés ?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                foreach (var member in selectedMembers)
                {
                    var success = await MemberService.AuthorizeMember(apiClient, member.NetworkId, member.Id);
                }
            }
        }

        private async void DenyMembers_Click(object sender, RoutedEventArgs e)
        {
            var selectedMembers = MembersDataGrid.SelectedItems.Cast<MemberViewModel>().ToList();
            if (MessageBox.Show("Êtes-vous sûr de vouloir refuser les membres sélectionnés ?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                foreach (var member in selectedMembers)
                {
                    var success = await MemberService.DenyMember(apiClient, member.NetworkId, member.Id);
                    // Afficher un message de succès ou d'erreur selon le résultat
                }
            }
        }

        private async void DeleteMembers_Click(object sender, RoutedEventArgs e)
        {
            var selectedMembers = MembersDataGrid.SelectedItems.Cast<MemberViewModel>().ToList();
            if (MessageBox.Show("Êtes-vous sûr de vouloir supprimer les membres sélectionnés ?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                foreach (var member in selectedMembers)
                {
                    var success = await MemberService.DeleteMember(apiClient, member.NetworkId, member.Id);
                    // Afficher un message de succès ou d'erreur selon le résultat
                }
            }
        }

        private void EditMember_Click(object sender, RoutedEventArgs e)
        {
            if (((Button)sender).DataContext is MemberViewModel member)
            {
                // Logique pour éditer le membre
            }
        }

        private async void AuthorizeMember_Click(object sender, RoutedEventArgs e)
        {
            if (((Button)sender).DataContext is MemberViewModel member)
            {
                if (MessageBox.Show("Êtes-vous sûr de vouloir autoriser ce membre ?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    var success = await MemberService.AuthorizeMember(apiClient, member.NetworkId, member.Id);
                    // Afficher un message de succès ou d'erreur selon le résultat
                }
            }
        }

        private async void DenyMember_Click(object sender, RoutedEventArgs e)
        {
            if (((Button)sender).DataContext is MemberViewModel member)
            {
                if (MessageBox.Show("Êtes-vous sûr de vouloir refuser ce membre ?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    var success = await MemberService.DenyMember(apiClient, member.NetworkId, member.Id);
                    // Afficher un message de succès ou d'erreur selon le résultat
                }
            }
        }

        private async void DeleteMember_Click(object sender, RoutedEventArgs e)
        {
            if (((Button)sender).DataContext is MemberViewModel member)
            {
                if (MessageBox.Show("Êtes-vous sûr de vouloir supprimer ce membre ?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    var success = await MemberService.DeleteMember(apiClient, member.NetworkId, member.Id);
                    // Afficher un message de succès ou d'erreur selon le résultat
                }
            }
        }
    }
}
