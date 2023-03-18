using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;
using WPFApp_CaseManagement.Context;
using WPFApp_CaseManagement.Models;
using WPFApp_CaseManagement.Models.Entities;
using WPFApp_CaseManagement.Services;

namespace WPFApp_CaseManagement.ViewModels
{
    public partial class AddEmployeeViewModel: ObservableObject
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
        private string department = string.Empty;

        [ObservableProperty]
        private int employeeNo;

        public AddEmployeeViewModel()
        {

        }

        // Add new employee when clicking on Add employee-button
        [RelayCommand]
        private async Task AddEmployee()
        {
            EmployeeModel _newemployee = new EmployeeModel
            {
                Id = Id,
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                Department = Department,
            };

            await EmployeeService.Add(_newemployee);
            
            Id = Id;
            FirstName = string.Empty;
            LastName = string.Empty;
            Email = string.Empty;
            Department = string.Empty;

        }
    }
}
