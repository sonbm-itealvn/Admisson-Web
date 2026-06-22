using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AdmissionWeb.Data;
using AdmissionWeb.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using System;

namespace AdmissionWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,AdmissionOfficer")]
    public class NewsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public NewsController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            var news = await _context.NewsArticles.OrderByDescending(n => n.PublishedAt).ToListAsync();
            return View(news);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _context.NewsCategories.ToListAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NewsArticle article, IFormFile? ImageUpload)
        {
            ModelState.Remove("ImageUrl");
            if (string.IsNullOrEmpty(article.Author)) ModelState.Remove("Author");
            if (string.IsNullOrEmpty(article.Category)) ModelState.Remove("Category");

            if (ModelState.IsValid)
            {
                article.Author ??= "Quản trị viên";
                article.Category ??= "Thông báo chung";
                try
                {
                    if (ImageUpload != null)
                    {
                        var webRootPath = _webHostEnvironment.WebRootPath;
                        if (string.IsNullOrWhiteSpace(webRootPath))
                        {
                            webRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                        }
                        string uploadsFolder = Path.Combine(webRootPath, "images", "news");
                        if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);
                        
                        string uniqueFileName = Guid.NewGuid().ToString() + "_" + ImageUpload.FileName;
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await ImageUpload.CopyToAsync(fileStream);
                        }
                        article.ImageUrl = "/images/news/" + uniqueFileName;
                    }
                    else
                    {
                        // Default image if none provided
                        if (string.IsNullOrEmpty(article.ImageUrl))
                            article.ImageUrl = "/images/default-news.png";
                    }

                    article.PublishedAt = System.DateTime.Now;
                    _context.Add(article);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Lỗi khi lưu bài viết hoặc hình ảnh: " + ex.Message);
                }
            }
            ViewBag.Categories = await _context.NewsCategories.ToListAsync();
            return View(article);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var article = await _context.NewsArticles.FindAsync(id);
            if (article == null) return NotFound();
            
            ViewBag.Categories = await _context.NewsCategories.ToListAsync();
            return View(article);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, NewsArticle article, IFormFile? ImageUpload)
        {
            if (id != article.Id) return NotFound();

            ModelState.Remove("ImageUrl");
            if (string.IsNullOrEmpty(article.Author)) ModelState.Remove("Author");
            if (string.IsNullOrEmpty(article.Category)) ModelState.Remove("Category");

            if (ModelState.IsValid)
            {
                article.Author ??= "Quản trị viên";
                article.Category ??= "Thông báo chung";
                try
                {
                    var existing = await _context.NewsArticles.FindAsync(id);
                    if (existing == null) return NotFound();

                    if (ImageUpload != null)
                    {
                        string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images", "news");
                        if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);
                        
                        string uniqueFileName = Guid.NewGuid().ToString() + "_" + ImageUpload.FileName;
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await ImageUpload.CopyToAsync(fileStream);
                        }
                        existing.ImageUrl = "/images/news/" + uniqueFileName;
                    }
                    else if (!string.IsNullOrEmpty(article.ImageUrl))
                    {
                        // Fallback if they didn't upload a new one but passed the old one via hidden field
                        existing.ImageUrl = article.ImageUrl;
                    }

                    existing.Title = article.Title;
                    existing.Content = article.Content;
                    existing.Category = article.Category;
                    existing.Author = article.Author;
                    existing.IsPublished = article.IsPublished;

                    _context.Update(existing);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NewsArticleExists(article.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(article);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var article = await _context.NewsArticles.FindAsync(id);
            if (article != null)
            {
                _context.NewsArticles.Remove(article);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool NewsArticleExists(int id)
        {
            return _context.NewsArticles.Any(e => e.Id == id);
        }
    }
}
