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
    public partial class EmployeeViewModel : ObservableObject
    {
        private static DataContext _context = new DataContext();

        [ObservableProperty]
        public EmployeeModel selectedEmployee = null!;
        public ObservableCollection<EmployeeModel> Employees { get; set; } = new ObservableCollection<EmployeeModel>();

        // Fetch and load from db
        public async Task LoadEmployees()
        {
            var employees = await EmployeeService.FetchEmployeeAsync();
            foreach (var _employee in employees)
            {
                Employees.Add(_employee);
            }
        }

        // Used to set up observablecollection and load from db
        public async Task Initialize()
        {
            Employees = new ObservableCollection<EmployeeModel>();
            await LoadEmployees();
        }

        public EmployeeViewModel()
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
        private string department = string.Empty;

        [ObservableProperty]
        private Guid employeeNo;

        // Edit employee when clicking on edit-button
        [RelayCommand]
        private async Task EditEmployee()
        {
            await EmployeeService.EditSelectedEmployeeAsync(SelectedEmployee);
        }

        // Delete employee when clicking on delete-button
        [RelayCommand]
        private async Task DeleteEmployee()
        {
            await EmployeeService.DeleteSelectedEmployeeAsync(SelectedEmployee);
        }
    }
}