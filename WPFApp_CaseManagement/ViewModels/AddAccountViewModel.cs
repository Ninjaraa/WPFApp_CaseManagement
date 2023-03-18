using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Threading.Tasks;
using WPFApp_CaseManagement.Context;
using WPFApp_CaseManagement.Models;
using WPFApp_CaseManagement.Models.Entities;
using WPFApp_CaseManagement.Services;

namespace WPFApp_CaseManagement.ViewModels
{
    public partial class AddAccountViewModel : ObservableObject
    {
        private DataContext _context = new DataContext();

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

        public AddAccountViewModel()
        {

        }

        // Add a new account when clickin on Add account-button
        [RelayCommand]
        private async Task AddAccount()
        {
            AccountModel _newaccount = new AccountModel
            {
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                PhoneNumber = PhoneNumber,
            };

            await AccountService.AddAccountAsync(_newaccount);

            FirstName = string.Empty;
            LastName = string.Empty;
            Email = string.Empty;
            PhoneNumber = string.Empty;
        }
    }
}