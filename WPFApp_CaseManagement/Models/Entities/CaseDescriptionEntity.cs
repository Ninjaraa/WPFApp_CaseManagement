using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WPFApp_CaseManagement.Models.Entities
{
    public class CaseDescriptionEntity
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string? Comment { get; set; } = string.Empty;

        public DateTime? CommentAdded { get; set; }

        [Required]
        public int CaseId { get; set; }
        public CaseEntity Case { get; set; } = null!;

    }
}