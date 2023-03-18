using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFApp_CaseManagement.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Windows.Controls;

// Each Account can have multiple Cases, each Case can only belong to one Account.
// Each Case can have multiple CaseDescriptions, each CaseDescription can only belong to one Case.
// Each Case can have one associated SLA, each SLA can only belong to one Case.

namespace WPFApp_CaseManagement.Models
{
    public class CaseModel
    {
        public int Id { get; set; }
        public DateTime? CaseCreated { get; set; }
        public int? Severity { get; set; }
        public CaseStatus Status { get; set; } = CaseStatus.Open;
        public string Subject { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int? AgreementResponse { get; set; }
        public int? AgreementResolution { get; set; }
        public DateTime? ResponseTime { get; set; }
        public DateTime? ResolutionTime { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public int? SLAId { get; set; }
        public int? AccountId { get; set; }
        public string NewComment { get; set; } = string.Empty;
        public ICollection<CaseDescriptionEntity> CaseDescriptions { get; set; } = new HashSet<CaseDescriptionEntity>();
    }
}