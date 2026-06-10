using Microsoft.AspNetCore.Mvc;
using AdmissionWeb.Data;
using AdmissionWeb.Models.Entities;
using System.Threading.Tasks;

namespace AdmissionWeb.Controllers
{
    public class ContactController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContactController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Submit(ContactRequest request)
        {
            if (ModelState.IsValid)
            {
                _context.ContactRequests.Add(request);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Cảm ơn bạn đã liên hệ! Chúng tôi sẽ phản hồi trong thời gian sớm nhất.";
                return RedirectToAction(nameof(Index));
            }
            return View("Index", request);
        }
    }
}
