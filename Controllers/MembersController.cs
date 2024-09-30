using GYM_MANAGEMENT.BAL.DTOs.MemberDtos;
using GYM_MANAGEMENT.BAL.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace GYM_MANAGEMENT.Controllers
{
    public class MembersController : Controller
    {
        // Constructor to inject the members service
        private readonly IMembersService memberService;
        public MembersController(IMembersService memberService)
        {
            this.memberService = memberService;
        }
        // GET: Member
        public async Task<IActionResult> Index()
        {
            // Retrieve the list of members to display in the view
            var listOfMembersForGrid = await memberService.GetForGrid();
            return View(listOfMembersForGrid);
        }
        public IActionResult Create()
        {
            // Return the view for creating a new member
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Create(AddMemberDto addMemberDto)
        {
            // Attempt to create a new member and redirect on success
            await memberService.Create(addMemberDto);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            // Retrieve the member details for editing based on the provided ID
            var getMemberById = await memberService.GetMemberForEditById(id);
            return View(getMemberById);
        }
        public async Task<IActionResult> Update(EditMemberDto editMemberDto)
        {
            // Update the member details and redirect on success
            await memberService.Edit(editMemberDto);
            return RedirectToAction("Index");
        }
    }
}
