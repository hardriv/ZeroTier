using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ZeroTier.ViewModels.MemberModels;
using ZeroTier.Services;
using ZeroTier.Utils;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ZeroTier.Views
{
    public partial class MembersListControl : UserControl
    {
        private APIClient apiClient = new();
        public DataGrid membersDataGrid = new();
        private readonly Button authorizeSelection;
        private readonly Button denySelection;
        private readonly Button deleteSelection;
        private readonly TextBlock pageInfoTextBlock;
        private readonly Button previousPageButton;
        private readonly Button nextPageButton;
        private readonly CheckBox selectAllCheckBox;
        private ObservableCollection<MemberViewModel> allMembers = [];
        private ObservableCollection<MemberViewModel> selectedMembers = [];
        private int currentPage = 1;
        private readonly int pageSize = 21;

        public MembersListControl()
        {
            InitializeComponent();

            membersDataGrid = (DataGrid)FindName("MembersDataGrid");
            authorizeSelection = (Button)FindName("AuthorizeSelection");
            denySelection = (Button)FindName("DenySelection");
            deleteSelection = (Button)FindName("DeleteSelection");
            pageInfoTextBlock = (TextBlock)FindName("PageInfo");
            previousPageButton = (Button)FindName("PreviousPage");
            nextPageButton = (Button)FindName("NextPage");
            selectAllCheckBox = (CheckBox)FindName("SelectAllCheckBox");
        }
        
        public void Initialize(APIClient apiClient)
        {
            this.apiClient = apiClient;
        }

        public async Task LoadMembers(string networkId)
        {
            allMembers = await MemberService.GetMembers(apiClient, networkId) ?? [];

            if (allMembers.Count > 0)
            {
                UpdatePage(1);
                UpdatePaginationControls();
                // On réinitialise la liste des membres sélectionnés
                selectedMembers.Clear();

                foreach (var member in allMembers)
                {
                    // Abonnement à l'événement PropertyChanged
                    member.PropertyChanged += MemberSelectionChanged; //TODO corriger le warning null
                }
            
            }
            else
            {
                MessageBox.Show("Aucun membre trouvé ou erreur lors du chargement des membres");
            }
        }

        private void UpdatePage(int pageNumber)
        {
            // Pagination : Obtenir les membres de la page actuelle
            currentPage = pageNumber;
            ObservableCollection<MemberViewModel> membersToDisplay = new(allMembers
                .Where(member => member != null) // Filtrer les membres non nuls
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize)
                .ToList());

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

            // Cast ItemsSource to ObservableCollection
            if (membersDataGrid.ItemsSource is ObservableCollection<MemberViewModel> members)
            {
                foreach (var member in members)
                {
                    // Mettre à jour la propriété IsSelected de chaque membre
                    member.IsSelected = isChecked;
                }
            }

            // Mettre à jour les boutons en fonction de la sélection
            UpdateActionButtonsState();
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox checkBox && checkBox.DataContext is MemberViewModel member)
            {
                // Met à jour la sélection
                member.IsSelected = checkBox.IsChecked ?? false;

                // Mise à jour des boutons d'action et de la case SelectAllCheckBox
                UpdateActionButtonsState();
                UpdateSelectAllCheckBoxState();
            }
        }

        private void MemberSelectionChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(MemberViewModel.IsSelected))
            {
                if (sender is MemberViewModel member)
                {
                    if (member.IsSelected && !selectedMembers.Contains(member))
                    {
                        selectedMembers.Add(member);
                    }
                    else if (!member.IsSelected && selectedMembers.Contains(member))
                    {
                        selectedMembers.Remove(member);
                    }

                    // Mise à jour des boutons d'action en fonction de la sélection
                    UpdateActionButtonsState();

                    // Mettre à jour l'état de la case SelectAllCheckBox
                    UpdateSelectAllCheckBoxState();
                }
            }
        }
        
        private void UpdateActionButtonsState()
        {
            // Activer/désactiver les boutons en fonction du nombre de membres sélectionnés
            bool hasSelection = selectedMembers.Count > 0;
            authorizeSelection.IsEnabled = hasSelection;
            denySelection.IsEnabled = hasSelection;
            deleteSelection.IsEnabled = hasSelection;
        }

        private void UpdateSelectAllCheckBoxState()
        {
            if (membersDataGrid.ItemsSource is IEnumerable<MemberViewModel> members)
            {
                // Si tous les membres sont sélectionnés
                selectAllCheckBox.IsChecked = members.All(m => m.IsSelected);
                // Désactiver la case si aucun membre n'est présent
                selectAllCheckBox.IsEnabled = members.Any();
            }
        }

        private void AuthorizeMembers_Click(object sender, RoutedEventArgs e)
        {
            if (selectedMembers.Count > 0 && MessageBox.Show("Êtes-vous sûr de vouloir autoriser les membres sélectionnés ?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                foreach (var memberToUpdate in selectedMembers)
                {
                    UpdateAuthorizedOrDeniedMember(true, memberToUpdate);
                }

                // Rafraîchir l'affichage
                membersDataGrid.Items.Refresh();
            }
        }

        private void AuthorizeMember_Click(object sender, RoutedEventArgs e)
        {
            if (((Button)sender).DataContext is MemberViewModel memberToUpdate)
            {
                if (MessageBox.Show("Êtes-vous sûr de vouloir autoriser ce membre ?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    UpdateAuthorizedOrDeniedMember(true, memberToUpdate);
                }
            }
        }

        private void DenyMembers_Click(object sender, RoutedEventArgs e)
        {
            if (selectedMembers.Count > 0 && MessageBox.Show("Êtes-vous sûr de vouloir refuser les membres sélectionnés ?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                foreach (var memberToUpdate in selectedMembers)
                {
                    UpdateAuthorizedOrDeniedMember(false, memberToUpdate);
                }

                // Rafraîchir l'affichage
                membersDataGrid.Items.Refresh();
            }
        }

        private void DenyMember_Click(object sender, RoutedEventArgs e)
        {
            if (((Button)sender).DataContext is MemberViewModel memberToUpdate)
            {
                if (MessageBox.Show("Êtes-vous sûr de vouloir refuser ce membre ?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    UpdateAuthorizedOrDeniedMember(false, memberToUpdate);
                }
            }
        }

        private async void UpdateAuthorizedOrDeniedMember(bool Authorized, MemberViewModel memberToUpdate)
        {
            MemberViewModel updatedMember;
            if (Authorized)
            {
                updatedMember = await MemberService.AuthorizeMember(apiClient, memberToUpdate) ?? memberToUpdate;
            }
            else
            {
                updatedMember = await MemberService.DenyMember(apiClient, memberToUpdate) ?? memberToUpdate;
            }

            if (updatedMember != null)
            {
                memberToUpdate.Config.Authorized = updatedMember.Config.Authorized;
            }
            else
            {
                MessageBox.Show("Erreur lors du chargement du membre");
            }
        }

        private async void DeleteMembers_Click(object sender, RoutedEventArgs e)
        {
            var success = false;
            if (selectedMembers.Count > 0 && MessageBox.Show("Êtes-vous sûr de vouloir supprimer les membres sélectionnés ?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                foreach (var memberToDelete in selectedMembers)
                {
                    success = await MemberService.DeleteMember(apiClient, memberToDelete.NetworkId, memberToDelete.NodeId);
                    if (success)
                    {
                        // Retirer le membre de la liste locale
                        allMembers.Remove(memberToDelete);
                    }
                }
            
                RefreshMembersAfterDelete(success);
            }
        }

        private async void DeleteMember_Click(object sender, RoutedEventArgs e)
        {
            var success = false; 
            if (((Button)sender).DataContext is MemberViewModel memberToDelete)
            {
                if (MessageBox.Show("Êtes-vous sûr de vouloir supprimer ce membre ?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    success = await MemberService.DeleteMember(apiClient, memberToDelete.NetworkId, memberToDelete.NodeId);
                    if (success)
                    {
                        // Retirer le membre de la liste locale
                        allMembers.Remove(memberToDelete);
                    }          
            
                    RefreshMembersAfterDelete(success);
                }
            }
        }

        private void RefreshMembersAfterDelete(bool success)
        {
            if (success)
            {
                // Mettre à jour la pagination si le nombre de membres a changé
                if (currentPage > Math.Ceiling((double)allMembers.Count / pageSize))
                {
                    // Revenir à la page précédente si la dernière est vide
                    currentPage = Math.Max(1, currentPage - 1);
                }

                // Mettre à jour la page pour refléter les changements
                UpdatePage(currentPage);
                UpdatePaginationControls();
                MessageBox.Show("Membres supprimés.");
            }
            else
            {
                MessageBox.Show("Erreur lors de la suppression des membres");
            }
        }

        private void EditMember_Click(object sender, RoutedEventArgs e)
        {
            if (((Button)sender).DataContext is MemberViewModel memberToDelete)
            {
                // Logique pour éditer le membre
            }
        }
    }
}
