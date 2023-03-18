using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using WPFApp_CaseManagement.Context;
using WPFApp_CaseManagement.Models.Entities;
using WPFApp_CaseManagement.Models;

namespace WPFApp_CaseManagement.Services
{
    internal class CaseService
    {
        private static DataContext _context = new DataContext();

        // Adds a new case and save it to the db. 
        public static async Task AddCaseAsync(CaseModel _case, IEnumerable<(string Comment, DateTime CommentAdded)>? comments = null)
        {
            try
            {
                var _caseEntity = new CaseEntity
                {
                    Severity = (int?)_case.Severity,
                    Status = _case.Status,
                    Subject = _case.Subject,
                    Description = _case.Description,
                    CaseCreated = (DateTime?)_case.CaseCreated,
                };

                var _accountEntity = await _context.Accounts.FirstOrDefaultAsync(x => x.FirstName == _case.FirstName && x.LastName == _case.LastName && x.Email == _case.Email && x.PhoneNumber == _case.PhoneNumber);
                var _slaEntity = await _context.SLAs.FirstOrDefaultAsync(x => x.AgreementResponse == _case.AgreementResponse && x.AgreementResolution == _case.AgreementResolution && x.ResponseTime == _case.ResponseTime && x.ResolutionTime == _case.ResolutionTime);

                if (_slaEntity != null)
                {
                    _caseEntity.SLAId = _slaEntity.Id;
                }
                else
                {
                    var newSlaEntity = new SLAEntity
                    {
                        AgreementResponse = (int?)_case.AgreementResponse,
                        AgreementResolution = (int?)_case.AgreementResolution,
                        ResponseTime = (DateTime?)_case.ResponseTime,
                        ResolutionTime = (DateTime?)_case.ResolutionTime,
                    };
                    _caseEntity.SLA = newSlaEntity;
                    _context.SLAs.Add(newSlaEntity);
                }

                if (_accountEntity != null)
                {
                    _caseEntity.AccountId = _accountEntity.Id;
                }
                else
                {
                    var newAccountEntity = new AccountEntity
                    {
                        FirstName = _case.FirstName,
                        LastName = _case.LastName,
                        Email = _case.Email,
                        PhoneNumber = _case.PhoneNumber,
                    };
                    _caseEntity.Account = newAccountEntity;
                    _context.Accounts.Add(newAccountEntity);
                }

                if (comments != null)
                {
                    foreach (var (comment, commentAdded) in comments)
                    {
                        var newCaseDescriptionEntity = new CaseDescriptionEntity
                        {
                            Comment = comment,
                            CommentAdded = commentAdded,
                        };
                        _caseEntity.CaseDescriptions.Add(newCaseDescriptionEntity);
                        _context.CaseDescriptions.Add(newCaseDescriptionEntity);
                    }
                }

                _context.Add(_caseEntity);
                await _context.SaveChangesAsync();

            }
            catch (DbUpdateException ex)
            {
                var message = ex.InnerException?.Message ?? ex.Message;
                throw new Exception("Error: " + message, ex);
            }
        }


        // Fetch all cases and display them
        public static async Task<IEnumerable<CaseModel>> FetchCaseAsync()
        {
            var _cases = new List<CaseModel>();


            foreach (var _case in await _context.Cases
                .Include(x => x.SLA)
                .ToListAsync())

                _cases.Add(new CaseModel
                {
                    Id = _case.Id,
                    CaseCreated = (DateTime?)_case.CaseCreated,
                    Severity = (int?)_case.Severity,
                    Status = _case.Status,
                    Subject = _case.Subject,
                    Description = _case.Description,

                });

            return _cases;
        }

        // Update the case with a new comment and timestamp when the comment was created and save to the db
        public static async Task<bool> AddDescriptionAsync(int caseId, CaseDescriptionModel comment)
        {
            var _case = await _context.Cases.FindAsync(caseId);
            if (_case == null)
            {
                throw new ArgumentException("Case not found", nameof(caseId));
            }

            var caseDescription = new CaseDescriptionEntity
            {
                Comment = comment.Comment,
                CommentAdded = comment.CommentAdded,
                CaseId = caseId
            };

            await _context.CaseDescriptions.AddAsync(caseDescription);
            await _context.SaveChangesAsync();

            return true;
        }

        // Fetch case comments from CaseDescriptions and return in the list
        public static async Task<List<CaseDescriptionEntity>> FetchDescriptionAsync(int _id)
        {
            var _comment = await _context.CaseDescriptions.Where(x => x.CaseId == _id).ToListAsync();
            return _comment;
        }

        // ** Shout out to Sara Lindström for helping me out with this one <3 **
        // Fetch and return all comments on a selected case
        public static async Task<List<CaseDescriptionModel>> GetDescriptionsForCaseAsync(int caseId)
        {
            var caseEntity = await _context.Cases
                .Include(c => c.CaseDescriptions)
                .FirstOrDefaultAsync(c => c.Id == caseId);

            if (caseEntity != null && caseEntity.CaseDescriptions != null)
            {
                return caseEntity.CaseDescriptions.Select(cd => new CaseDescriptionModel
                {
                    Id = cd.Id,
                    Comment = cd.Comment,
                    CommentAdded = cd.CommentAdded
                }).ToList();
            }

            return new List<CaseDescriptionModel>();
        }

        // Set DateTime.Now in the SLA table on button click
        // Based on if the employeer starting (responseTime) or closing (resolutionTime) the case
        // If I had more time: Should also be set if the employee adds a comment to start the case. A calculation should be added to calculate if the case was responded and resolved in time.
        public static async Task<bool> UpdateCaseStatusAsync(CaseModel selectedCase, string newStatus)
        {
            var caseEntity = await _context.Cases.FindAsync(selectedCase.Id);
            if (caseEntity != null)
            {
                if (Enum.TryParse<CaseStatus>(newStatus, out CaseStatus status))
                {
                    caseEntity.Status = status;
                    var slaEntity = await _context.SLAs.FirstOrDefaultAsync(sla => sla.Id == selectedCase.Id);
                    if (slaEntity != null)
                    {
                        if (status == CaseStatus.InProgress)
                        {
                            slaEntity.ResponseTime = DateTime.Now;
                        }
                        else if (status == CaseStatus.Closed)
                        {
                            slaEntity.ResolutionTime = DateTime.Now;
                        }
                    }
                    await _context.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        // Fetch cases from the db and add to observablecollection
        public static ObservableCollection<CaseModel> Cases()
        {
            var cases = new ObservableCollection<CaseModel>();
            foreach (var _case in _context.Cases)
            {
                cases.Add(new CaseModel
                {
                    Status = _case.Status,
                    Severity = (int?)_case.Severity,
                    Subject = _case.Subject,

                });
            }
            return cases;
        }

        // Delete selected case and save to db
        public static async Task DeleteSelectedCaseAsync(CaseModel model)
        {
            if (model != null)
            {
                var selectedCase = _context.Cases.FirstOrDefault(c => c.Id == model.Id);
                if (selectedCase != null)
                {
                    selectedCase.CaseCreated = model.CaseCreated;
                    selectedCase.Severity = model.Severity;
                    selectedCase.Subject = model.Subject;
                    selectedCase.Description = model.Description;

                    _context.Remove(selectedCase);
                    await _context.SaveChangesAsync();
                }
            }
        }
    }
}