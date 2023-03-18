using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WPFApp_CaseManagement.Models.Entities
{
    public enum CaseStatus
    {
        Open,
        InProgress,
        Closed
    }
    public class CaseEntity
    {
        [Key]
        public int Id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime? CaseCreated { get; set; }
       
        public int? Severity  { get; set; }
        public CaseStatus Status { get; set; } = CaseStatus.Open;

        [Column(TypeName = "nvarchar(50)")]
        public string Subject { get; set; } = string.Empty;

        [Column(TypeName = "nvarchar(150)")]
        public string Description { get; set; } = string.Empty;

        [Required]
        public int AccountId { get; set; }
        public AccountEntity Account { get; set; } = null!;

        public int? SLAId { get; set; }
        public SLAEntity? SLA { get; set; }

        public ICollection<CaseDescriptionEntity> CaseDescriptions { get; set; } = new HashSet<CaseDescriptionEntity>();
    }
}