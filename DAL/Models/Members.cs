using Microsoft.CodeAnalysis.Operations;
using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;

namespace GYM_MANAGEMENT.DAL.Models
{
    public class Members
    {
        [Key]
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime Birthday { get; set; }
        public string? IdCardNumber { get; set; }
        public string? Email { get; set; }
        public DateTime RegistrationDate { get; set; }
        public bool IsDeleted { get; set; }
        public string RegistrationCard { get; set; }
    }
}
