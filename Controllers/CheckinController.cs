using GYM_MANAGEMENT.BAL.DTOs.CheckSessionsDto;
using GYM_MANAGEMENT.BAL.Infrastructure;
using GYM_MANAGEMENT.BAL.Services;
using Microsoft.AspNetCore.Mvc;

namespace GYM_MANAGEMENT.Controllers
{
    public class CheckinController : Controller
    {
        // Constructor to inject the check-in service
        private readonly ICheckinService checkinService;
        public CheckinController(ICheckinService checkinService)
        {
            this.checkinService = checkinService;
        }
        // GET: Display the initial check-in view with an empty CheckinDto
        public async Task<IActionResult> Index()
        {
            var checkin = new CheckinDto();
            return View(checkin);
        }
        [HttpPost]
        public async Task<IActionResult> Index(CheckinDto checkin)
        {
            // Validate the model state; if invalid, return the same view with the current model
            if (!ModelState.IsValid)
            {
                return View(checkin);
            }
            // Call the service to get information based on the provided CheckinDto
            var resultForCheckin = await checkinService.GetInformation(checkin);
            return View(resultForCheckin); 
        }
    }
}
