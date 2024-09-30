using GYM_MANAGEMENT.DAL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using GYM_MANAGEMENT.BAL.DTOs.MemberDtos;
using GYM_MANAGEMENT.BAL.DTOs.SubscriptionsDtos;
using GYM_MANAGEMENT.BAL.DTOs.MemberSubscriptionDtos;
using GYM_MANAGEMENT.BAL.DTOs.CheckSessionsDto;

namespace GYM_MANAGEMENT.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        public DbSet<Members> Members { get; set; }
        public DbSet<Subscriptions> Subscriptions { get; set; }
        public DbSet<MemberSubscription> MemberSubscriptions { get; set; }
    }
}
