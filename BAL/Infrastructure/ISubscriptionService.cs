using GYM_MANAGEMENT.BAL.DTOs.SubscriptionsDtos;

namespace GYM_MANAGEMENT.BAL.Infrastructure
{
    public interface ISubscriptionService
    {
        // Task to create a new subscription
        Task Create(AddSubscriptionsDto addSubscriptionsDto);
        // Task to retrieve a list of subscriptions for grid display
        Task<List<GridSubscriptionsDto>> GetForGrid();
        // Task to retrieve a subscription's details for editing by its ID
        Task<EditSubscriptionsDto> GetSubscriptionForEditById(int id);
        // Task to update an existing subscription
        Task<EditSubscriptionsDto> Edit(EditSubscriptionsDto editSubscriptionsDto);
        // Task to retrieve a list of active subscriptions
        Task<List<GridSubscriptionsDto>> GetActiveSubscription();
    }
}
