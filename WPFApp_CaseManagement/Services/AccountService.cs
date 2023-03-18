using System;
using WPFApp_CaseManagement.Models.Entities;
using WPFApp_CaseManagement.Models;
using WPFApp_CaseManagement.Context;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;

namespace WPFApp_CaseManagement.Services
{
    internal class AccountService
    {
        private static DataContext _context = new DataContext();

        // Add new account and save it to the db
        public static async Task AddAccountAsync(AccountModel _account)
        {
            if (_account == null)
            {
                throw new ArgumentNullException(nameof(_account));
            }
            try
            {
                // Check if the email address is already in the db, if so - return a prompt
                var existingCustomer = await _context.Accounts.FirstOrDefaultAsync(a => a.Email == _account.Email);
                if (existingCustomer != null)
                {
                    var result = MessageBox.Show("An account with the same email address already exists. Please change the email address to create a new account", "Duplicate Email Address", MessageBoxButton.OK);
                    if (result == MessageBoxResult.OK)
                    {
                        // Cancel, do not save the result
                        return;
                    }
                }

                // If no duplicated email address, save the new record to the db
                var _accountEntity = new AccountEntity
                {
                    FirstName = _account.FirstName,
                    LastName = _account.LastName,
                    Email = _account.Email,
                    PhoneNumber = _account.PhoneNumber,
                    AccountNo = Guid.NewGuid()
                };

                _context.Add(_accountEntity);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                var message = ex.InnerException?.Message ?? ex.Message;
                MessageBox.Show("Error: " + message, "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Fetch all accounts in the db and display them
        public static async Task<IEnumerable<AccountModel>> FetchAccountAsync()
        {
            var _accounts = new List<AccountModel>();

            foreach (var _account in await _context.Accounts.ToListAsync())
                _accounts.Add(new AccountModel
                {
                    Id = _account.Id,
                    FirstName = _account.FirstName,
                    LastName = _account.LastName,
                    Email = _account.Email,
                    PhoneNumber = _account.PhoneNumber,
                });

            return _accounts;
        }

        // Fetch accounts from the db and add to observablecollection
        public static ObservableCollection<AccountModel> Accounts()
        {
            var accounts = new ObservableCollection<AccountModel>();
            foreach (var _account in _context.Accounts)
            {
                accounts.Add(new AccountModel
                {
                    FirstName = _account.FirstName,
                    LastName = _account.LastName,
                    Email = _account.Email,
                    PhoneNumber = _account.PhoneNumber,
                });
            }
            return accounts;
        }

        // Delete selected account based on the ID as in the accountmodel and save to db
        // If I had more time: Should also added a prompt "Are you sure you want to delete X?", and update the view without having to change view and then go back to make it easier for the user to see.
        public static async Task DeleteSelectedAccountAsync(AccountModel model)
        {
            if (model != null)
            {
                var selectedAccount = _context.Accounts.FirstOrDefault(c => c.Id == model.Id);
                if (selectedAccount != null)
                {
                    selectedAccount.FirstName = model.FirstName;
                    selectedAccount.LastName = model.LastName;
                    selectedAccount.Email = model.Email;
                    selectedAccount.PhoneNumber = model.PhoneNumber;

                    _context.Remove(selectedAccount);
                    await _context.SaveChangesAsync();
                }
            }
        }

        // Find account with the same email address as in accountmodel and edit the selected account
        public static async Task EditSelectedAccountAsync(AccountModel model)
        {
            if (model != null)
            {
                var selectedAccount = _context.Accounts.FirstOrDefault(c => c.Email == model.Email);
                if (selectedAccount != null)
                {
                    selectedAccount.FirstName = model.FirstName;
                    selectedAccount.LastName = model.LastName;
                    selectedAccount.Email = model.Email;
                    selectedAccount.PhoneNumber = model.PhoneNumber;

                    await _context.SaveChangesAsync();
                }
            }
        }
    }
}