using GYM_MANAGEMENT.BAL.DTOs.MemberDtos;
using GYM_MANAGEMENT.BAL.DTOs.MemberSubscriptionDtos;

namespace GYM_MANAGEMENT.BAL.Infrastructure
{
    public interface IMemberSubscriptionService
    {
        // Task to create a new member subscription
        Task Create(AddMemberSubscriptionDto addMemberSubscriptionDto);
        // Task to retrieve a list of member subscriptions for grid display
        Task<List<GridMemberSubscriptionDto>> GetForGrid();
        // Task to retrieve a member subscription's details for editing by its ID
        Task<EditMemberSubscriptionDto> GetMemberSubscriptionForEditById(int id);
        // Task to update an existing member subscription
        Task<EditMemberSubscriptionDto> Edit(EditMemberSubscriptionDto editMemberSubscriptionDto);
    }
}
