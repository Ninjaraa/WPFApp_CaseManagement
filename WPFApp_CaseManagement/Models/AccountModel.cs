using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFApp_CaseManagement.Models.Entities;

namespace WPFApp_CaseManagement.Models
{
    public class AccountModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public Guid AccountNo { get; set; }
        public string? PhoneNumber { get; set; }
        public string StreetName { get; set; } = string.Empty;
        public string Zip { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
    }
}