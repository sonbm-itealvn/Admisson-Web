using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AdmissionWeb.Services.Interfaces;
using AdmissionWeb.Models.ViewModels;

namespace AdmissionWeb.Controllers
{
    public class ResultController : Controller
    {
        private readonly IApplicationService _applicationService;

        public ResultController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        public IActionResult Search()
        {
            return View(new ResultSearchViewModel { IsSearched = false });
        }

        [HttpPost]
        public async Task<IActionResult> Search(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm)) 
                return View(new ResultSearchViewModel { IsSearched = false });

            var app = await _applicationService.GetApplicationByCodeAsync(searchTerm);
            var model = new ResultSearchViewModel
            {
                SearchTerm = searchTerm,
                Application = app,
                IsSearched = true
            };
            return View(model);
        }
    }
}
