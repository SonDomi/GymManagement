using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GYM_MANAGEMENT.DAL.Models
{
    public class MemberSubscription
    {
        public enum TimeOfDayEnum 
        {
            Morning,
            Afternoon,
            AllDay
        }
        [Key]
        public int Id { get; set; }
        [ForeignKey("Members")]
        public int MemberId { get; set; }
        public Members? Member { get; set; }
        [ForeignKey("Subscriptions")]
        public int SubscriptionId { get; set; }
        public Subscriptions? Subscription { get; set; }
        public float OriginalPrice { get; set; }
        public float DiscountValue { get; set; }
        public float PaidPrice { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int RemainingSessions { get; set; }
        public bool IsDeleted { get; set; }
        public TimeOfDayEnum TimeOfDAY { get; set; }
    }
}
