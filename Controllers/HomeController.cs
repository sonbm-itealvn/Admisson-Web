using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AdmissionWeb.Models;
using AdmissionWeb.Models.ViewModels;
using AdmissionWeb.Services.Interfaces;

namespace AdmissionWeb.Controllers;

public class HomeController : Controller
{
    private readonly IAdmissionService _admissionService;

    public HomeController(IAdmissionService admissionService)
    {
        _admissionService = admissionService;
    }

    public async Task<IActionResult> Index()
    {
        var model = new HomeViewModel
        {
            ActivePeriods = await _admissionService.GetActivePeriodsAsync(),
            LatestNews = await _admissionService.GetLatestNewsAsync(3),
            Banners = await _admissionService.GetActiveBannersAsync(),
            FeaturedEvents = await _admissionService.GetNewsByCategoryAsync("Sự kiện nổi bật", 4),
            AdmissionNews = await _admissionService.GetNewsByCategoryAsync("Thông báo tuyển sinh", 4),
            GeneralNews = await _admissionService.GetNewsByCategoryAsync("Tin tức chung", 4)
        };
        return View(model);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
