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
        public DataGrid membersDataGrid;
        private readonly Button authorizeSelection;
        private readonly Button denySelection;
        private readonly Button deleteSelection;
        private readonly TextBlock pageInfoTextBlock;
        private readonly Button previousPageButton;
        private readonly Button nextPageButton;
        private List<MemberViewModel> allMembers = new(); // Liste complète des membres
        private int currentPage = 1;
        private readonly int pageSize = 500;

        public MembersListControl()
        {
            InitializeComponent();

            membersDataGrid = (DataGrid)FindName("MembersDataGrid");
            membersDataGrid.SelectionChanged += MembersDataGrid_SelectionChanged;
            authorizeSelection = (Button)FindName("AuthorizeSelection");
            denySelection = (Button)FindName("DenySelection");
            deleteSelection = (Button)FindName("DeleteSelection");
            pageInfoTextBlock = (TextBlock)FindName("PageInfo");
            previousPageButton = (Button)FindName("PreviousPage");
            nextPageButton = (Button)FindName("NextPage");
        }
        
        // Si tu as besoin de passer l'APIClient, fais-le par une méthode ou propriété
        public void Initialize(APIClient apiClient)
        {
            this.apiClient = apiClient;
        }

        public async Task LoadMembers(string networkId)
        {
            // var members = await MemberService.GetMembers(apiClient, networkId);
            allMembers = await MemberService.GetMembers(apiClient, networkId);

            if (allMembers != null)
            {
                UpdatePage(1); // Afficher la première page
                UpdatePaginationControls();
                // membersDataGrid.ItemsSource = members;
            }
            else
            {
                MessageBox.Show("Erreur lors du chargement des membres");
            }
        }

        private void UpdatePage(int pageNumber)
        {
            // Pagination : Obtenir les membres de la page actuelle
            currentPage = pageNumber;
            var membersToDisplay = allMembers.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
            membersDataGrid.ItemsSource = membersToDisplay;

            // Mettre à jour le texte de la page
            pageInfoTextBlock.Text = $"Page {currentPage}";

            // Rafraîchir le DataGrid
            membersDataGrid.Items.Refresh();
        }

        private void UpdatePaginationControls()
        {
            // Activer/désactiver les boutons de pagination
            previousPageButton.IsEnabled = currentPage > 1;
            nextPageButton.IsEnabled = currentPage < Math.Ceiling((double)allMembers.Count / pageSize);
        }

        private void PreviousPage_Click(object sender, RoutedEventArgs e)
        {
            if (currentPage > 1)
            {
                UpdatePage(currentPage - 1);
                UpdatePaginationControls();
            }
        }

        private void NextPage_Click(object sender, RoutedEventArgs e)
        {
            if (currentPage < Math.Ceiling((double)allMembers.Count / pageSize))
            {
                UpdatePage(currentPage + 1);
                UpdatePaginationControls();
            }
        }

        // Sélectionner/Désélectionner tous les membres
        private void SelectAllCheckBox_Click(object sender, RoutedEventArgs e)
        {
            bool isChecked = (sender as CheckBox)?.IsChecked ?? false;

            if (membersDataGrid.ItemsSource is List<MemberViewModel> members)
            {
                foreach (var member in members)
                {
                    member.IsSelected = isChecked;
                }
                // Met à jour l'affichage pour refléter les changements
                membersDataGrid.Items.Refresh();
            }
        }

        private void MembersDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var hasSelection = membersDataGrid.SelectedItems.Count > 0;
            authorizeSelection.IsEnabled = hasSelection;
            denySelection.IsEnabled = hasSelection;
            deleteSelection.IsEnabled = hasSelection;
        }

        private async void AuthorizeMembers_Click(object sender, RoutedEventArgs e)
        {
            var selectedMembers = membersDataGrid.SelectedItems.Cast<MemberViewModel>().ToList();
            if (MessageBox.Show("Êtes-vous sûr de vouloir autoriser les membres sélectionnés ?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                foreach (var member in selectedMembers)
                {
                    var success = await MemberService.AuthorizeMember(apiClient, member);
                }
            }
        }

        private async void DenyMembers_Click(object sender, RoutedEventArgs e)
        {
            var selectedMembers = membersDataGrid.SelectedItems.Cast<MemberViewModel>().ToList();
            if (MessageBox.Show("Êtes-vous sûr de vouloir refuser les membres sélectionnés ?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                foreach (var member in selectedMembers)
                {
                    var success = await MemberService.DenyMember(apiClient, member);
                    // Afficher un message de succès ou d'erreur selon le résultat
                }
            }
        }

        private async void DeleteMembers_Click(object sender, RoutedEventArgs e)
        {
            var success = false;
            var selectedMembers = membersDataGrid.SelectedItems.Cast<MemberViewModel>().ToList();
            if (MessageBox.Show("Êtes-vous sûr de vouloir supprimer les membres sélectionnés ?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                foreach (var member in selectedMembers)
                {
                    success = await MemberService.DeleteMember(apiClient, member.NetworkId, member.NodeId);
                }
            }
            
            // Afficher un message de succès ou d'erreur selon le résultat
            if (success)
            {
                MessageBox.Show("Membres supprimés.");
            }
            else
            {
                MessageBox.Show("Erreur lors de la suppression des membres");
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
                    member.Config.Authorized = true;
                    var success = await MemberService.AuthorizeMember(apiClient, member);
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
                    member.Config.Authorized = false;
                    var success = await MemberService.DenyMember(apiClient, member);
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
                    var success = await MemberService.DeleteMember(apiClient, member.NetworkId, member.NodeId);
                    // Afficher un message de succès ou d'erreur selon le résultat
                }
            }
        }
    }
}
