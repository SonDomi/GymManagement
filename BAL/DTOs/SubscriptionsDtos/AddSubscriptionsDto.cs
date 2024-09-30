using Newtonsoft.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using static GYM_MANAGEMENT.DAL.Models.Subscriptions;

namespace GYM_MANAGEMENT.BAL.DTOs.SubscriptionsDtos
{
    public class AddSubscriptionsDto
    {
        public int Id { get; set; }
        [Required]
        public int Code { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        [Range(1, 12, ErrorMessage = "You can choose from 1-12 month for a Subscription")] // Validates that NumberOfMonths is between 1 and 12
        public int NumberOfMonths { get; set; }
        [Required]
        [Range(2, 5, ErrorMessage = " 2dd/week, 3dd/week, 4dd/week, 5dd/week")] // Validates that WeekFrequency is between 2 and 5
        public int WeekFrequency { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed")] // Validates that TotalPrice is a positive number
        public float TotalPrice { get; set; }
        public bool IsDeleted { get; set; }
        public SubscriptionTime Time { get; set; }
    }
}
