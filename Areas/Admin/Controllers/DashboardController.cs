using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AdmissionWeb.Services.Interfaces;

namespace AdmissionWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,AdmissionOfficer")]
    public class DashboardController : Controller
    {
        private readonly IApplicationService _applicationService;
        private readonly IAdmissionService _admissionService;

        public DashboardController(IApplicationService applicationService, IAdmissionService admissionService)
        {
            _applicationService = applicationService;
            _admissionService = admissionService;
        }

        public async Task<IActionResult> Index()
        {
            var applications = await _applicationService.GetAllApplicationsAsync();
            var appList = applications.ToList();

            ViewBag.TotalApplications = appList.Count;
            ViewBag.PendingCount = appList.Count(a => a.Status == "Pending");
            ViewBag.ApprovedCount = appList.Count(a => a.Status == "Approved");
            ViewBag.RejectedCount = appList.Count(a => a.Status == "Rejected");

            var latestApplications = appList
                .OrderByDescending(a => a.SubmittedAt)
                .Take(10)
                .ToList();

            return View(latestApplications);
        }
    }
}
