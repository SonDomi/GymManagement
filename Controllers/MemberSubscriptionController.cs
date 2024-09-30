using GYM_MANAGEMENT.BAL.DTOs.MemberDtos;
using GYM_MANAGEMENT.BAL.DTOs.MemberSubscriptionDtos;
using GYM_MANAGEMENT.BAL.DTOs.SubscriptionsDtos;
using GYM_MANAGEMENT.BAL.Infrastructure;
using GYM_MANAGEMENT.BAL.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GYM_MANAGEMENT.Controllers
{
    public class MemberSubscriptionController : Controller
    {
        private readonly IMemberSubscriptionService memberSubscriptionService; // Service for member subscriptions
        private readonly IMembersService memberService; // Service for managing members
        private readonly ISubscriptionService subscriptionService; // Service for managing subscriptions
        // Constructor to inject the necessary services
        public MemberSubscriptionController(IMemberSubscriptionService membersubscriptionService, IMembersService membersService, ISubscriptionService subscriptionService)
        {
            this.memberSubscriptionService = membersubscriptionService;
            this.memberService = membersService;
            this.subscriptionService = subscriptionService;
        }
        public async Task<IActionResult> Index()
        {
            // Retrieve the list of member subscriptions for display in the grid
            var listOfMemberSubcriptionForGrid = await memberSubscriptionService.GetForGrid();
            return View(listOfMemberSubcriptionForGrid);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            // Get active members and active subscriptions for dropdowns
            var members = await memberService.GetActiveMember();
            var subscriptions = await subscriptionService.GetActiveSubscription();
            // Populate ViewBag for dropdown lists
            ViewBag.Members = new SelectList(members, "Id", "Fullname");
            ViewBag.Subscriptions = new SelectList(subscriptions, "Id", "Description");

            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Create(AddMemberSubscriptionDto addMemberSubscriptionDto)
        {
            // Create a new member subscription
            await memberSubscriptionService.Create(addMemberSubscriptionDto);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            // Get the member subscription to edit by id
            var getMemberSubscriptionById = await memberSubscriptionService.GetMemberSubscriptionForEditById(id);
            // Get active members and active subscriptions for dropdowns
            var members = await memberService.GetActiveMember();
            var subscriptions = await subscriptionService.GetActiveSubscription();
            // Populate ViewBag for dropdown lists
            ViewBag.Members = new SelectList(members, "Id", "Fullname");
            ViewBag.Subscriptions = new SelectList(subscriptions, "Id", "Description");

            return View(getMemberSubscriptionById);
        }
        public async Task<IActionResult> Update(EditMemberSubscriptionDto editMemberSubscriptionDto)
        {
            // Update the member subscription details
            await memberSubscriptionService.Edit(editMemberSubscriptionDto);
            return RedirectToAction("Index");
        }
    }
}
