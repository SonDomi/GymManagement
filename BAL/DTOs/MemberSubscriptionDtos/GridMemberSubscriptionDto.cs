using System.ComponentModel.DataAnnotations;
using static GYM_MANAGEMENT.DAL.Models.MemberSubscription;

namespace GYM_MANAGEMENT.BAL.DTOs.MemberSubscriptionDtos
{
    public class GridMemberSubscriptionDto
    {
        public int Id { get; set; }
        [Required]
        public int MemberId { get; set; }
        [Required]
        public int SubscriptionId { get; set; }
        [Required]
        public float OriginalPrice { get; set; }
        [Required]
        public float DiscountValue { get; set; }
        [Required]
        public float PaidPrice { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime StartDate { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime EndDate { get; set; }
        [Required]
        public int RemainingSessions { get; set; }
        [Required]
        public bool IsDeleted { get; set; }
        public string FullMemberName { get; set; }
        public string SubscriptionCode { get; set; }
        public TimeOfDayEnum TimeOfDAY { get; set; }

    }
}
