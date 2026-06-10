using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AdmissionWeb.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;

namespace AdmissionWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,AdmissionOfficer")]
    public class ContactController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContactController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var contacts = await _context.ContactRequests.OrderByDescending(c => c.CreatedAt).ToListAsync();
            return View(contacts);
        }

        public async Task<IActionResult> Details(int id)
        {
            var contact = await _context.ContactRequests.FindAsync(id);
            if (contact == null)
            {
                return NotFound();
            }

            if (!contact.IsRead)
            {
                contact.IsRead = true;
                _context.Update(contact);
                await _context.SaveChangesAsync();
            }

            return View(contact);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var contact = await _context.ContactRequests.FindAsync(id);
            if (contact != null)
            {
                _context.ContactRequests.Remove(contact);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Đã xóa yêu cầu liên hệ.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
