using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using WPFApp_CaseManagement.Context;
using WPFApp_CaseManagement.Models.Entities;
using WPFApp_CaseManagement.Models;
using WPFApp_CaseManagement.Services;
using System.Collections.Generic;

namespace WPFApp_CaseManagement.ViewModels
{
    public partial class CaseViewModel : ObservableObject
    {
        private static DataContext _context = new DataContext();

        [ObservableProperty]
        public CaseModel selectedCase = null!;
        public ObservableCollection<CaseModel> Cases { get; set; } = new ObservableCollection<CaseModel>();

        // Fetch and load from db, also include CaseDescriptions if any
        public async Task LoadCases()
        {
            var cases = await CaseService.FetchCaseAsync();

            foreach (var _case in cases)
            {
                _case.CaseDescriptions = await CaseService.FetchDescriptionAsync(_case.Id);
                Cases.Add(_case);
            }
        }

        // Used to set up observablecollection and load from db
        public async Task Initialize()
        {
            Cases = new ObservableCollection<CaseModel>();
            await LoadCases();
        }

        public CaseViewModel()
        {
            Initialize().ConfigureAwait(false);
        }

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

        [ObservableProperty]
        private string commentText = string.Empty;


        // Update case comment for the selected case
        [RelayCommand]
        public async Task UpdateCaseComment()
        {
            if (SelectedCase == null)
            {
                return;
            }

            // Create new comment
            var comment = new CaseDescriptionModel()
            {
                Comment = CommentText, 
                CommentAdded = DateTime.Now
            };

            // Add new comment to db
            var result = await CaseService.AddDescriptionAsync(SelectedCase.Id, comment);

            if (result)
            {
                SelectedCase.CaseDescriptions = await CaseService.FetchDescriptionAsync(SelectedCase.Id);
                CommentText = ""; 
            }
        }


        // ** Shout out to Sara Lindström for helping me out with this one <3 **
        // Return all comments for a selected case
        [RelayCommand]
        public async Task<List<CaseDescriptionModel>> GetAllCommentsForCase(int caseId)
        {
            var comments = await CaseService.GetDescriptionsForCaseAsync(caseId);

            // To return a list of the comments
            return comments.Select(c => new CaseDescriptionModel
            {
                Comment = c.Comment,
                CommentAdded = c.CommentAdded
            }).ToList();
        }

        // Delete selected case
        [RelayCommand]
        public async Task DeleteCase()
        {
            await CaseService.DeleteSelectedCaseAsync(SelectedCase);
            Cases.Remove(SelectedCase);
        }

        // Change status to InProgress when clicking start case-button
        [RelayCommand]
        private async Task UpdateStatusStart()
        {
            await CaseService.UpdateCaseStatusAsync(SelectedCase, "InProgress");
        }

        // Change status to Closed when clicking start close case-button
        [RelayCommand]
        private async Task UpdateStatusClosed()
        {
            await CaseService.UpdateCaseStatusAsync(SelectedCase, "Closed");
        }
    }
}
