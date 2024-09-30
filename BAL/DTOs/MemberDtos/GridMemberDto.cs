using System.ComponentModel.DataAnnotations;

namespace GYM_MANAGEMENT.BAL.DTOs.MemberDtos
{
    public class GridMemberDto
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Birthday { get; set; }
        public string? IdCardNumber { get; set; }
        public string? Email { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime RegistrationDate { get; set; }
        public bool IsDeleted { get; set; }
        public string Fullname => $"{FirstName} {LastName}";
        public string RegistrationCard { get; set; }
    }
}
