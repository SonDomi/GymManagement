using GYM_MANAGEMENT.BAL.DTOs.MemberDtos;
using GYM_MANAGEMENT.BAL.DTOs.MemberSubscriptionDtos;
using GYM_MANAGEMENT.BAL.Infrastructure;
using GYM_MANAGEMENT.DAL.Models;
using GYM_MANAGEMENT.Data;
using GYM_MANAGEMENT.Data.Migrations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GYM_MANAGEMENT.BAL.Services
{
    public class MemberSubscriptionService : IMemberSubscriptionService
    {
        private readonly ApplicationDbContext db;
        public MemberSubscriptionService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task Create(AddMemberSubscriptionDto addMemberSubscriptionDto)
        {
            try
            {
                // Retrieve the subscription details from the database
                var subscriptionFromDb = db.Subscriptions.FirstOrDefault(x => x.Id == addMemberSubscriptionDto.SubscriptionId);

                // Create a new member subscription instance
                var memberSubscription = new MemberSubscription
                {
                  MemberId = addMemberSubscriptionDto.MemberId,
                  SubscriptionId = addMemberSubscriptionDto.SubscriptionId,
                  OriginalPrice = subscriptionFromDb.TotalPrice,
                  DiscountValue = addMemberSubscriptionDto.DiscountValue,
                  PaidPrice = subscriptionFromDb.TotalPrice - addMemberSubscriptionDto.DiscountValue,
                  StartDate = addMemberSubscriptionDto.StartDate,
                  EndDate = addMemberSubscriptionDto.StartDate.AddMonths(subscriptionFromDb.NumberOfMonths),
                  RemainingSessions = subscriptionFromDb.TotalNumberOfSessions * subscriptionFromDb.NumberOfMonths,
                  IsDeleted = addMemberSubscriptionDto.IsDeleted,
                  TimeOfDAY = addMemberSubscriptionDto.TimeOfDAY,
                };
                // Check if the member exists in the database
                var checkMemberId = db.Members.FirstOrDefault(x => x.Id == addMemberSubscriptionDto.MemberId);
                var existingMemberSubscriptions = await db.MemberSubscriptions.Where(x => x.MemberId == addMemberSubscriptionDto.MemberId).ToListAsync();
                var memberSubscriptions = await db.MemberSubscriptions.FirstOrDefaultAsync(x => x.MemberId == addMemberSubscriptionDto.MemberId);


                // Check for valid member and subscription
                if (!existingMemberSubscriptions.Any() && checkMemberId != null && subscriptionFromDb != null)
                {
                        db.MemberSubscriptions.Add(memberSubscription);
                        await db.SaveChangesAsync();
                }
                else
                {
                    if (checkMemberId == null) 
                    {
                        throw new Exception("User with this ID does not exist in the gym.");
                    }
                    if (subscriptionFromDb == null) 
                    {
                        throw new Exception("Subscription with this ID does not exist in the gym.");
                    }
                    if (existingMemberSubscriptions.Any()
                        && checkMemberId.IsDeleted == false
                        && memberSubscriptions.EndDate < DateTime.Now)
                    {
                        db.MemberSubscriptions.Add(memberSubscription);
                        await db.SaveChangesAsync();
                    }
                    else
                    {
                        throw new InvalidOperationException("Member already has an active subscription.");
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<List<GridMemberSubscriptionDto>> GetForGrid()
        {
            try
            {
                var gridMemberSubscription = new List<GridMemberSubscriptionDto>();

                // Retrieve all member subscriptions with related member and subscription details
                var membersSubscriptionFromDb = await db.MemberSubscriptions
                    .Include(x => x.Member)
                    .Include(x => x.Subscription)
                    .ToListAsync();

                foreach (var memberSubscriptionDb in membersSubscriptionFromDb)
                {
                    var memberSubscriptionDto = new GridMemberSubscriptionDto()
                    {
                        Id = memberSubscriptionDb.Id,
                        MemberId = memberSubscriptionDb.MemberId,
                        SubscriptionId = memberSubscriptionDb.SubscriptionId,
                        OriginalPrice = memberSubscriptionDb.OriginalPrice,
                        DiscountValue = memberSubscriptionDb.DiscountValue,
                        PaidPrice = memberSubscriptionDb.PaidPrice,
                        StartDate = memberSubscriptionDb.StartDate,
                        EndDate = memberSubscriptionDb.EndDate,
                        RemainingSessions = memberSubscriptionDb.RemainingSessions,
                        IsDeleted = memberSubscriptionDb.IsDeleted,
                        FullMemberName = memberSubscriptionDb.Member.FirstName + " " + memberSubscriptionDb.Member.LastName,
                        SubscriptionCode = memberSubscriptionDb.Subscription.Description + " " + memberSubscriptionDb.Subscription.Code,
                        TimeOfDAY = memberSubscriptionDb.TimeOfDAY
                    };
                    gridMemberSubscription.Add(memberSubscriptionDto);
                }
                return gridMemberSubscription;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<EditMemberSubscriptionDto> GetMemberSubscriptionForEditById(int id)
        {
            try
            {
                // Retrieve the member subscription for editing
                var memberSubscriptions = await db.MemberSubscriptions
                .Include(x => x.Member)
                .Include(x => x.Subscription)
                .FirstOrDefaultAsync(x => x.Id == id);

                var editMemberrSubscriptionDto = new EditMemberSubscriptionDto();

                editMemberrSubscriptionDto.Id = memberSubscriptions.Id;
                editMemberrSubscriptionDto.MemberId = memberSubscriptions.MemberId;
                editMemberrSubscriptionDto.SubscriptionId = memberSubscriptions.SubscriptionId;
                editMemberrSubscriptionDto.OriginalPrice = memberSubscriptions.OriginalPrice;
                editMemberrSubscriptionDto.DiscountValue = memberSubscriptions.DiscountValue;
                editMemberrSubscriptionDto.PaidPrice = memberSubscriptions.PaidPrice;
                editMemberrSubscriptionDto.StartDate = memberSubscriptions.StartDate;
                editMemberrSubscriptionDto.EndDate = memberSubscriptions.EndDate;
                editMemberrSubscriptionDto.RemainingSessions = memberSubscriptions.RemainingSessions;
                editMemberrSubscriptionDto.IsDeleted = memberSubscriptions.IsDeleted;
                editMemberrSubscriptionDto.MemberFullName = $"{memberSubscriptions.Member.FirstName} {memberSubscriptions.Member.LastName}";
                editMemberrSubscriptionDto.SubscriptionDescription = memberSubscriptions.Subscription.Description;
                editMemberrSubscriptionDto.TimeOfDAY = memberSubscriptions.TimeOfDAY;

                return editMemberrSubscriptionDto;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<EditMemberSubscriptionDto> Edit(EditMemberSubscriptionDto editMemberSubscriptionDto)
        {
            try
            {
                // Retrieve the subscription details from the database
                var subscriptionFromDb = db.Subscriptions.FirstOrDefault(x => x.Id == editMemberSubscriptionDto.SubscriptionId);
                // Retrieve the memberSubscriptions details from the database
                var memberSubscriptions = await db.MemberSubscriptions.SingleAsync(x => x.Id == editMemberSubscriptionDto.Id);
                // Update memberSubscriptions subscription properties
                    memberSubscriptions.MemberId = editMemberSubscriptionDto.MemberId;
                    memberSubscriptions.SubscriptionId = editMemberSubscriptionDto.SubscriptionId;
                    memberSubscriptions.OriginalPrice = subscriptionFromDb.TotalPrice;
                    memberSubscriptions.DiscountValue = editMemberSubscriptionDto.DiscountValue;
                    memberSubscriptions.PaidPrice = subscriptionFromDb.TotalPrice - editMemberSubscriptionDto.DiscountValue;
                    memberSubscriptions.StartDate = memberSubscriptions.StartDate;
                    memberSubscriptions.EndDate = editMemberSubscriptionDto.StartDate.AddMonths(subscriptionFromDb.NumberOfMonths);
                    memberSubscriptions.RemainingSessions = subscriptionFromDb.TotalNumberOfSessions * subscriptionFromDb.NumberOfMonths;
                    memberSubscriptions.IsDeleted = editMemberSubscriptionDto.IsDeleted;
                    memberSubscriptions.TimeOfDAY = editMemberSubscriptionDto.TimeOfDAY;

                    db.MemberSubscriptions.Update(memberSubscriptions);
                    await db.SaveChangesAsync();
                    return editMemberSubscriptionDto;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
