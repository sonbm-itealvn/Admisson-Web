using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AdmissionWeb.Services.Interfaces;
using AdmissionWeb.Models.ViewModels;

namespace AdmissionWeb.Controllers
{
    public class AdmissionController : Controller
    {
        private readonly IAdmissionService _admissionService;

        public AdmissionController(IAdmissionService admissionService)
        {
            _admissionService = admissionService;
        }

        public async Task<IActionResult> Index()
        {
            var periods = await _admissionService.GetActivePeriodsAsync();
            return View(periods);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var period = await _admissionService.GetPeriodByIdAsync(id);
            if (period == null) return NotFound();

            var model = new AdmissionDetailViewModel
            {
                Period = period,
                Options = await _admissionService.GetProgramOptionsAsync()
            };
            return View(model);
        }

        public async Task<IActionResult> News(int id)
        {
            var news = await _admissionService.GetNewsByIdAsync(id);
            if (news == null) return NotFound();
            return View(news);
        }
    }
}
