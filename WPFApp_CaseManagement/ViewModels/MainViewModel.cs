using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace WPFApp_CaseManagement.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableObject currentViewModel = new CaseViewModel();

        [RelayCommand]
        private void GoToAddAccount() => CurrentViewModel = new AddAccountViewModel();

        [RelayCommand]
        private void GoToAccount() => CurrentViewModel = new AccountViewModel();

        [RelayCommand]
        private void GoToEmployee() => CurrentViewModel = new EmployeeViewModel();

        [RelayCommand]
        private void GoToAddEmployee() => CurrentViewModel = new AddEmployeeViewModel();

        [RelayCommand]
        private void GoToAddCase() => CurrentViewModel = new AddCaseViewModel();

        [RelayCommand]
        private void GoToCase() => CurrentViewModel = new CaseViewModel();

        public MainViewModel()
        {
            CurrentViewModel = new CaseViewModel();
        }
    }
}