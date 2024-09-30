using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;

namespace GYM_MANAGEMENT.BAL.DTOs.MemberDtos
{
    public class AddMemberDto
    {
        [Required]
        public string? FirstName { get; set; }
        [Required]
        [MaxLength(100)]
        public string? LastName { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Birthday { get; set; }
        [Required]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Id Card Number must be 10 characters long.")]
        public string? IdCardNumber { get; set; }
        [Required]
        [MaxLength(200)]
        public string? Email { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime RegistrationDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
