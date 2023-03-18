using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WPFApp_CaseManagement.Models.Entities
{
    public class SLAEntity
    {
        [Key]
        public int Id { get; set; }

        public DateTime? ResponseTime { get; set; }

        public DateTime? ResolutionTime { get; set; }

        public int? AgreementResponse { get; set; }

        public int? AgreementResolution { get; set; }

        public CaseEntity? Case { get; set; }

    }
}
