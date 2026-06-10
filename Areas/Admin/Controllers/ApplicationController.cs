using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AdmissionWeb.Services.Interfaces;

namespace AdmissionWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,AdmissionOfficer")]
    public class ApplicationController : Controller
    {
        private readonly IApplicationService _applicationService;

        public ApplicationController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        public async Task<IActionResult> Index()
        {
            var apps = await _applicationService.GetAllApplicationsAsync();
            return View(apps);
        }

        public async Task<IActionResult> Details(int id)
        {
            var app = await _applicationService.GetApplicationByIdAsync(id);
            if (app == null) return NotFound();
            return View(app);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStatus(int id, string status, string reason)
        {
            await _applicationService.UpdateStatusAsync(id, status, reason);
            return RedirectToAction(nameof(Details), new { id });
        }
    }
}
