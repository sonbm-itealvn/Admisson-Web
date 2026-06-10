using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AdmissionWeb.Services.Interfaces;
using AdmissionWeb.Models.Entities;
using AdmissionWeb.Models.ViewModels;

namespace AdmissionWeb.Controllers
{
    [Authorize]
    public class ApplicationController : Controller
    {
        private readonly IApplicationService _applicationService;
        private readonly IAdmissionService _admissionService;

        public ApplicationController(IApplicationService applicationService, IAdmissionService admissionService)
        {
            _applicationService = applicationService;
            _admissionService = admissionService;
        }

        public async Task<IActionResult> Register(int periodId)
        {
            var period = await _admissionService.GetPeriodByIdAsync(periodId);
            if (period == null) return NotFound();

            var model = new ApplicationFormViewModel
            {
                AdmissionPeriodId = periodId,
                PeriodName = period.Name,
                ProgramOptions = await _admissionService.GetProgramOptionsAsync(),
                Application = new Application { AdmissionPeriodId = periodId }
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Submit(Application model)
        {
            if (ModelState.IsValid)
            {
                model.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var result = await _applicationService.SubmitApplicationAsync(model);
                return RedirectToAction("Status", new { code = result.ApplicationCode });
            }
            
            ViewBag.PeriodName = (await _admissionService.GetPeriodByIdAsync(model.AdmissionPeriodId))?.Name;
            ViewBag.ProgramOptions = await _admissionService.GetProgramOptionsAsync();
            return View("Register", new ApplicationFormViewModel { 
                AdmissionPeriodId = model.AdmissionPeriodId,
                Application = model,
                ProgramOptions = (IEnumerable<ProgramOption>)ViewBag.ProgramOptions
            });
        }

        public async Task<IActionResult> MyApplications()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var apps = await _applicationService.GetUserApplicationsAsync(userId);
            return View(apps);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Status(string code)
        {
            if (string.IsNullOrEmpty(code)) return View(new ResultSearchViewModel { IsSearched = false });
            
            var app = await _applicationService.GetApplicationByCodeAsync(code);
            var model = new ResultSearchViewModel
            {
                SearchTerm = code,
                Application = app,
                IsSearched = true
            };
            return View(model);
        }
    }
}
