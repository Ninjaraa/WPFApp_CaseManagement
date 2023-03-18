using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFApp_CaseManagement.Models.Entities;

namespace WPFApp_CaseManagement.Models
{
    public class CaseDescriptionModel
    {
        public int Id { get; set; }
        public string? Comment { get; set; } = string.Empty;
        public DateTime? CommentAdded { get; set; }
    }
}

