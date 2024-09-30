using static GYM_MANAGEMENT.DAL.Models.Subscriptions;

namespace GYM_MANAGEMENT.BAL.DTOs.SubscriptionsDtos
{
    public class GridSubscriptionsDto
    {
        public int Id { get; set; }
        public int Code { get; set; }
        public string? Description { get; set; }
        public int NumberOfMonths { get; set; }
        public int WeekFrequency { get; set; }
        public int TotalNumberOfSessions { get; set; }
        public float TotalPrice { get; set; }
        public bool IsDeleted { get; set; }
        public SubscriptionTime Time { get; set; }
    }
}
