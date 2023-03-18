using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WPFApp_CaseManagement.Models.Entities
{
    public class AccountEntity
    {
        [Key]
        public int Id { get; set; }
        public Guid AccountNo { get; set; } = Guid.NewGuid();

        [StringLength(50)]
        public string FirstName { get; set; } = null!;

        [StringLength(50)]
        public string LastName { get; set; } = null!;

        [StringLength(100)]
        public string Email { get; set; } = null!;

        [Column(TypeName = "char(13)")]
        public string? PhoneNumber { get; set; } = null!;
        public ICollection<CaseEntity> Cases { get; set; } = new HashSet<CaseEntity>();

    }
}
