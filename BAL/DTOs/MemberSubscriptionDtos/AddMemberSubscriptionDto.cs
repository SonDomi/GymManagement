using GYM_MANAGEMENT.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static GYM_MANAGEMENT.DAL.Models.MemberSubscription;
using static GYM_MANAGEMENT.DAL.Models.Subscriptions;

namespace GYM_MANAGEMENT.BAL.DTOs.MemberSubscriptionDtos
{
    public class AddMemberSubscriptionDto
    {
        public int Id { get; set; }
        [Required]
        public int MemberId { get; set; }
        [Required]
        public int SubscriptionId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed")] // Validates that OriginalPrice is a positive number
        public float OriginalPrice { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Only positive number allowed")] // Validates that DiscountValue is a positive number
        public float DiscountValue { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Only positive number allowed")] // Validates that PaidPrice is a positive number
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
        public bool IsDeleted { get; set; }
        public TimeOfDayEnum TimeOfDAY { get; set; }
    }
}
