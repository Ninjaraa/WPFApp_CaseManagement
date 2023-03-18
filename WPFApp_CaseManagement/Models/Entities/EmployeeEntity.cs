using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WPFApp_CaseManagement.Models.Entities
{
    public class EmployeeEntity
    {
        [Key]
        public int Id { get; set; }
        public Guid EmployeeNo { get; set; } = Guid.NewGuid();

        [Column(TypeName = "nvarchar(50)")]
        public string FirstName { get; set; } = null!;

        [Column(TypeName = "nvarchar(50)")]
        public string LastName { get; set; } = null!;

        [Column(TypeName = "nvarchar(100)")]
        public string Email { get; set; } = null!;

        [Column(TypeName = "nvarchar(100)")]
        public string Department { get; set; } = null!;
    
    }
}