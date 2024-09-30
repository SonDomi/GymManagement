using GYM_MANAGEMENT.BAL.DTOs.MemberDtos;
using GYM_MANAGEMENT.BAL.DTOs.SubscriptionsDtos;
using GYM_MANAGEMENT.BAL.Infrastructure;
using GYM_MANAGEMENT.BAL.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GYM_MANAGEMENT.Controllers
{
    public class SubscriptionsController : Controller
    {
        private readonly ISubscriptionService subscriptionService;
        // Constructor to inject the subscription service
        public SubscriptionsController(ISubscriptionService subscriptionService)
        {
            this.subscriptionService = subscriptionService;
        }
        public async Task<IActionResult> Index()
        {
            // Retrieve the list of subscriptions for display in the grid
            var ListOfSubscriptions = await subscriptionService.GetForGrid();
            return View(ListOfSubscriptions);
        }
        public IActionResult Create()
        {
            // Return the view to create a new subscription
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Create(AddSubscriptionsDto addSubscriptionsDto)
        {
            // Create a new subscription using the provided data
            await subscriptionService.Create(addSubscriptionsDto);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Edit(int id)
        {
            // Get the subscription details to edit by id
            var getSubscriptionById = await subscriptionService.GetSubscriptionForEditById(id);
            return View(getSubscriptionById);
        }
        public async Task<IActionResult> Update(EditSubscriptionsDto editSubscriptionsDto)
        {
            // Update the subscription details
            await subscriptionService.Edit(editSubscriptionsDto);
            return RedirectToAction("Index");
        }
    }
}
