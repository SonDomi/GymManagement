using GYM_MANAGEMENT.BAL.DTOs.MemberDtos;
using GYM_MANAGEMENT.BAL.DTOs.CheckSessionsDto;
using GYM_MANAGEMENT.BAL.Infrastructure;
using GYM_MANAGEMENT.Data;
using GYM_MANAGEMENT.BAL.DTOs.TimeDto;
using static GYM_MANAGEMENT.DAL.Models.MemberSubscription;

namespace GYM_MANAGEMENT.BAL.Services
{
    public class CheckinService : ICheckinService
    {
        private readonly ApplicationDbContext db;
        private readonly IClock clock;

        public CheckinService(ApplicationDbContext db, IClock clock)
        {
            this.db = db;
            this.clock = clock;
        }
        public async Task<CheckinDto> GetInformation(CheckinDto checkinDto)
        {
            try
            {
                var currentHour = clock.Now.Hour;
                TimeOfDayEnum currentTimeOfDay;
                // Determine the current time of day
                if (currentHour >= 8 && currentHour < 14)
                {
                    currentTimeOfDay = TimeOfDayEnum.Morning;
                }
                else if (currentHour >= 14 && currentHour < 8)
                {
                    currentTimeOfDay = TimeOfDayEnum.Afternoon;
                }
                else
                {
                    checkinDto.IsError = true;
                    checkinDto.Info = "CheckIn is allowed only in the morning or afternoon.";
                    return checkinDto;
                }
                // Check for active member
                var activeMember = db.Members.FirstOrDefault(x => x.RegistrationCard == checkinDto.Code && x.IsDeleted == false);

                if (activeMember == null)
                {
                    checkinDto.IsError = true;
                    checkinDto.Info = "No member with this card code";
                    return checkinDto;
                }

                // Check for active memberSubscription
                var activeMemberSubscription = db.MemberSubscriptions.FirstOrDefault(
                    x => x.MemberId == activeMember.Id &&
                    x.StartDate <= DateTime.Now && DateTime.Now <= x.EndDate &&
                    x.IsDeleted == false);

                if (activeMemberSubscription == null)
                {
                    checkinDto.IsError = true;
                    checkinDto.Info = "No active subscription";
                    return checkinDto;
                }

                // Check remaining sessions
                if (activeMemberSubscription.RemainingSessions == 0)
                {
                    checkinDto.IsError = true;
                    checkinDto.Info = "No more Remaining Sessions";
                    return checkinDto;
                }
                if (activeMemberSubscription.TimeOfDAY != currentTimeOfDay)
                {
                    checkinDto.IsError = true;
                    checkinDto.Info = $"This subscription is valid only for {activeMemberSubscription.TimeOfDAY}.";
                    return checkinDto;
                }

                // Deduct a session
                activeMemberSubscription.RemainingSessions--;
                db.Update(activeMemberSubscription);
                db.SaveChanges();

                // Populate result
                checkinDto.MemberName = activeMember.FirstName + " " + activeMember.LastName;
                checkinDto.RemainingSessions = activeMemberSubscription.RemainingSessions;

                // Set success message
                checkinDto.Info = $"Welcome to the gym, {checkinDto.MemberName}! Enjoy your workout!";

                return checkinDto;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
