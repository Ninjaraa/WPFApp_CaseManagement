using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using WPFApp_CaseManagement.Context;
using WPFApp_CaseManagement.Models;
using WPFApp_CaseManagement.Services;

namespace WPFApp_CaseManagement.ViewModels
{
    public partial class AccountViewModel : ObservableObject
    {
        private static DataContext _context = new DataContext();

        [ObservableProperty]
        public AccountModel selectedAccount = null!;
        public ObservableCollection<AccountModel> Accounts { get; set; } = new ObservableCollection<AccountModel>();

        // Fetch and load from db
        public async Task LoadAccounts()
        {
            var accounts = await AccountService.FetchAccountAsync();
            foreach (var _account in accounts)
            {
                Accounts.Add(_account);
            }
        }

        // Used to set up observablecollection and load from db
        public async Task Initialize()
        {
            Accounts = new ObservableCollection<AccountModel>();
            await LoadAccounts();
        }

        public AccountViewModel()
        {
            Initialize().ConfigureAwait(false);
        }

        [ObservableProperty]
        private int id;

        [ObservableProperty]
        private string firstName = string.Empty;

        [ObservableProperty]
        private string lastName = string.Empty;

        [ObservableProperty]
        private string email = string.Empty;

        [ObservableProperty]
        private string phoneNumber = string.Empty;

        [ObservableProperty]
        private Guid accountNo;

        // Edit account when clicking on edit-button
        [RelayCommand]
        private async Task EditAccount()
        {
            await AccountService.EditSelectedAccountAsync(SelectedAccount);
        }

        // Delete account when clicking on delete-button
        [RelayCommand]
        private async Task DeleteAccount()
        {
            await AccountService.DeleteSelectedAccountAsync(SelectedAccount);
        }
    }
}