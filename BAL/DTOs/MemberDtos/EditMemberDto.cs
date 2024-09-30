using System.ComponentModel.DataAnnotations;

namespace GYM_MANAGEMENT.BAL.DTOs.MemberDtos
{
    public class EditMemberDto
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string? FirstName { get; set; }
        [MaxLength(100)]
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public bool IsDeleted { get; set; }
    }
}
