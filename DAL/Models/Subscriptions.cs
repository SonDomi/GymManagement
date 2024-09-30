using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GYM_MANAGEMENT.DAL.Models
{
    public class Subscriptions
    {
        public enum SubscriptionTime 
        {
            Morning,
            Afternoon,
            AllDay
        }
       
            [Key]
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
