using System.ComponentModel.DataAnnotations;
using static GYM_MANAGEMENT.DAL.Models.MemberSubscription;

namespace GYM_MANAGEMENT.BAL.DTOs.MemberSubscriptionDtos
{
    public class EditMemberSubscriptionDto
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public int SubscriptionId { get; set; }
        public float OriginalPrice { get; set; }
        public float DiscountValue { get; set; }
        public float PaidPrice { get; set; }
        //[DataType(DataType.Date)]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime EndDate { get; set; }
        public int RemainingSessions { get; set; }
        public bool IsDeleted { get; set; }
        public string MemberFullName { get; set; }
        public string SubscriptionDescription { get; set; }
        public TimeOfDayEnum TimeOfDAY { get; set; }
    }
}
