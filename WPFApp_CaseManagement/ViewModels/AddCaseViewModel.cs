using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using WPFApp_CaseManagement.Context;
using WPFApp_CaseManagement.Models;
using WPFApp_CaseManagement.Models.Entities;
using WPFApp_CaseManagement.Services;

namespace WPFApp_CaseManagement.ViewModels
{
    public partial class AddCaseViewModel : ObservableObject
    {
        private static DataContext _context = new DataContext();
        public ObservableCollection<CaseModel>? Cases { get; set; }

        [ObservableProperty]
        private int? id;

        [ObservableProperty]
        private int accountId;

        [ObservableProperty]
        private DateTime? caseCreated;

        [ObservableProperty]
        private int? severity;

        [ObservableProperty]
        public CaseStatus status;

        [ObservableProperty]
        private string subject = string.Empty;

        [ObservableProperty]
        private string description = string.Empty;

        [ObservableProperty]
        private string comment = string.Empty;

        [ObservableProperty]
        private DateTime? commentAdded;

        [ObservableProperty]
        private string firstName = string.Empty;

        [ObservableProperty]
        private string lastName = string.Empty;

        [ObservableProperty]
        private string email = string.Empty;

        [ObservableProperty]
        private string phoneNumber = string.Empty;

        [ObservableProperty]
        private DateTime? resolutionTime;

        [ObservableProperty]
        private DateTime? responseTime;

        [ObservableProperty]
        private int? agreementResponse;

        [ObservableProperty]
        private int? agreementResolution;

        public AddCaseViewModel()
        {

        }

        // Add a new case when clickin on Add case-button
        [RelayCommand]
        private async Task AddCase()
        {
            CaseModel _newcase = new CaseModel
            {
                AccountId = AccountId,
                Severity = (int?)Severity,
                Status = CaseStatus.Open,
                Subject = Subject,
                Description = Description,
                CaseCreated = DateTime.Now,
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                PhoneNumber = PhoneNumber,
            };

            // Call the GetSLA method to get the agreementResponse and agreementResolution
            (int agreementResponse, int agreementResolution) = GetSLA(_newcase.Severity);
            _newcase.AgreementResponse = agreementResponse;
            _newcase.AgreementResolution = agreementResolution;
            int sla = agreementResponse + agreementResolution;

            await CaseService.AddCaseAsync(_newcase);

            Severity = Severity;
            AccountId = AccountId;
            Status = CaseStatus.Open;
            Subject = string.Empty;
            Description = string.Empty;
            CaseCreated = CaseCreated;
            FirstName = string.Empty;
            LastName = string.Empty;
            Email = string.Empty;
            PhoneNumber = string.Empty;
        }

        // Adds agreementResponse and agreementResolution time
        private static (int agreementResponse, int agreementResolution) GetSLA(int? severity)
        {
            // hour values is set based on the severity of the case
            int responseTimeInMinutes = 0;
            int resolutionTimeInMinutes = 0;

            if (severity == 1)
            {
                responseTimeInMinutes = 60; // 1 hour
                resolutionTimeInMinutes = 480; // 8 hours
            }
            else if (severity == 2)
            {
                responseTimeInMinutes = 120; // 2 hours
                resolutionTimeInMinutes = 720; // 12 hours
            }
            else if (severity == 3)
            {
                responseTimeInMinutes = 240; // 4 hours
                resolutionTimeInMinutes = 1440; // 1 day
            }
            else if (severity == 4)
            {
                responseTimeInMinutes = 480; // 8 hours
                resolutionTimeInMinutes = 2880; // 2 days
            }
            else if (severity == 5)
            {
                responseTimeInMinutes = 1440; // 1 day
                resolutionTimeInMinutes = 4320; // 3 days
            }

            return (responseTimeInMinutes, resolutionTimeInMinutes);
        }
    }
}