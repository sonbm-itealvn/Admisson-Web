using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AdmissionWeb.Data;
using AdmissionWeb.Models.Entities;

namespace AdmissionWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,AdmissionOfficer")]
    public class BannerController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public BannerController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            var banners = await _context.Banners.OrderBy(b => b.DisplayOrder).ToListAsync();
            return View(banners);
        }

        public IActionResult Create()
        {
            return View(new Banner { DisplayOrder = 0, IsActive = true });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Banner banner, IFormFile? imageFile)
        {
            if (ModelState.IsValid)
            {
                if (imageFile != null && imageFile.Length > 0)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                    var filePath = Path.Combine(_env.WebRootPath, "images", "banners", fileName);
                    
                    Directory.CreateDirectory(Path.Combine(_env.WebRootPath, "images", "banners"));
                    
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }
                    banner.ImageUrl = "/images/banners/" + fileName;
                }

                _context.Add(banner);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(banner);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var banner = await _context.Banners.FindAsync(id);
            if (banner == null) return NotFound();
            return View(banner);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Banner banner, IFormFile? imageFile)
        {
            if (id != banner.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var existingBanner = await _context.Banners.AsNoTracking().FirstOrDefaultAsync(b => b.Id == id);
                    if (existingBanner == null) return NotFound();

                    banner.ImageUrl = existingBanner.ImageUrl;

                    if (imageFile != null && imageFile.Length > 0)
                    {
                        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                        var filePath = Path.Combine(_env.WebRootPath, "images", "banners", fileName);
                        
                        Directory.CreateDirectory(Path.Combine(_env.WebRootPath, "images", "banners"));
                        
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await imageFile.CopyToAsync(stream);
                        }
                        banner.ImageUrl = "/images/banners/" + fileName;
                    }

                    _context.Update(banner);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BannerExists(banner.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(banner);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var banner = await _context.Banners.FindAsync(id);
            if (banner != null)
            {
                _context.Banners.Remove(banner);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> ToggleStatus(int id)
        {
            var banner = await _context.Banners.FindAsync(id);
            if (banner != null)
            {
                banner.IsActive = !banner.IsActive;
                await _context.SaveChangesAsync();
                return Json(new { success = true, isActive = banner.IsActive });
            }
            return Json(new { success = false });
        }

        private bool BannerExists(int id)
        {
            return _context.Banners.Any(e => e.Id == id);
        }
    }
}
