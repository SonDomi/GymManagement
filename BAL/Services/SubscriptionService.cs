using GYM_MANAGEMENT.BAL.DTOs.MemberDtos;
using GYM_MANAGEMENT.BAL.DTOs.SubscriptionsDtos;
using GYM_MANAGEMENT.BAL.Infrastructure;
using GYM_MANAGEMENT.DAL.Models;
using GYM_MANAGEMENT.Data;
using GYM_MANAGEMENT.Data.Migrations;
using Microsoft.EntityFrameworkCore;

namespace GYM_MANAGEMENT.BAL.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly ApplicationDbContext db;
        public SubscriptionService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task Create(AddSubscriptionsDto subscriptionsDto)
        {
            try
            {
                // Create a new subscription 
                var subscription = new Subscriptions
                {
                    Code = subscriptionsDto.Code,
                    Description = subscriptionsDto.Description,
                    NumberOfMonths = subscriptionsDto.NumberOfMonths,
                    WeekFrequency = subscriptionsDto.WeekFrequency,
                    TotalNumberOfSessions = subscriptionsDto.WeekFrequency * 4,
                    TotalPrice = subscriptionsDto.TotalPrice, 
                    IsDeleted = subscriptionsDto.IsDeleted,
                    Time = subscriptionsDto.Time
                };
                // Check if a subscription with the same code already exists
                var checkSubscription = db.Subscriptions.FirstOrDefault(x => x.Code == subscriptionsDto.Code);
                if (checkSubscription == null)
                {
                    db.Subscriptions.Add(subscription);
                    await db.SaveChangesAsync();
                }
                else
                {
                    throw new Exception("Subscription with this code already exists in the gym.");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<List<GridSubscriptionsDto>> GetForGrid()
        {
            try
            {
                var gridSubscription = new List<GridSubscriptionsDto>();
                // Retrieve all subscriptions from the database
                var subscriptionFromDb = await db.Subscriptions.ToListAsync();

                foreach (var subscription in subscriptionFromDb)
                {
                    var subscriptionDto = new GridSubscriptionsDto
                    {
                        Id = subscription.Id,
                        Code = subscription.Code,
                        Description = subscription.Description,
                        WeekFrequency = subscription.WeekFrequency,
                        NumberOfMonths = subscription.NumberOfMonths,
                        TotalNumberOfSessions = subscription.TotalNumberOfSessions,
                        TotalPrice = subscription.TotalPrice,
                        IsDeleted = subscription.IsDeleted,
                        Time = subscription.Time
                    };
                    gridSubscription.Add(subscriptionDto);
                }
                return gridSubscription;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<EditSubscriptionsDto> GetSubscriptionForEditById(int id)
        {
            try
            {
                var editSubscriptionsDto = new EditSubscriptionsDto();
                var subscription = await db.Subscriptions.SingleAsync(x => x.Id == id);

                editSubscriptionsDto.Id = subscription.Id;
                editSubscriptionsDto.Code = subscription.Code;
                editSubscriptionsDto.Description = subscription.Description;
                editSubscriptionsDto.NumberOfMonths = subscription.NumberOfMonths;
                editSubscriptionsDto.WeekFrequency = subscription.WeekFrequency;
                editSubscriptionsDto.TotalPrice = subscription.TotalPrice;
                editSubscriptionsDto.IsDeleted = subscription.IsDeleted;
                editSubscriptionsDto.Time = subscription.Time;

                return editSubscriptionsDto;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<EditSubscriptionsDto> Edit(EditSubscriptionsDto editsubscriptionsDto)
        {
            try
            {
                var subscription = await db.Subscriptions.SingleAsync(x => x.Id == editsubscriptionsDto.Id);
                // Update subscription properties
                subscription.Code = editsubscriptionsDto.Code;
                subscription.Description = editsubscriptionsDto.Description;
                subscription.NumberOfMonths = editsubscriptionsDto.NumberOfMonths;
                subscription.WeekFrequency = editsubscriptionsDto.WeekFrequency;
                subscription.TotalNumberOfSessions = editsubscriptionsDto.WeekFrequency * 4;
                subscription.TotalPrice = editsubscriptionsDto.TotalPrice;
                subscription.IsDeleted = editsubscriptionsDto.IsDeleted;
                subscription.Time = editsubscriptionsDto.Time;
                // Check if another subscription with the same code exists
                var check = db.Subscriptions.FirstOrDefault(x => x.Code == editsubscriptionsDto.Code && x.Id!=editsubscriptionsDto.Id);
                if (check == null)
                {
                    db.Subscriptions.Update(subscription);
                    await db.SaveChangesAsync();
                    return editsubscriptionsDto;
                }
                else
                {
                    throw new Exception("Subscription with this code already exists in the gym.");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<List<GridSubscriptionsDto>> GetActiveSubscription()
        {
            try
            {
                var gridSubscription = new List<GridSubscriptionsDto>();
                // Retrieve active subscriptions (not deleted)
                var subscriptionFromDb = await db.Subscriptions.Where(x =>x.IsDeleted == false).ToListAsync();

                foreach (var subscription in subscriptionFromDb)
                {
                    var subscriptionDto = new GridSubscriptionsDto
                    {
                        Id = subscription.Id,
                        Code = subscription.Code,
                        Description = subscription.Description,
                        WeekFrequency = subscription.WeekFrequency,
                        NumberOfMonths = subscription.NumberOfMonths,
                        TotalNumberOfSessions = subscription.TotalNumberOfSessions,
                        TotalPrice = subscription.TotalPrice,
                        IsDeleted = subscription.IsDeleted
                    };
                    gridSubscription.Add(subscriptionDto);
                }
                return gridSubscription;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
