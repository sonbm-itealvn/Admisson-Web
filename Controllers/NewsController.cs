using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using AdmissionWeb.Data;
using Microsoft.EntityFrameworkCore;

namespace AdmissionWeb.Controllers
{
    public class NewsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NewsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var news = await _context.NewsArticles
                .Where(n => n.IsPublished)
                .OrderByDescending(n => n.PublishedAt)
                .ToListAsync();
            return View(news);
        }

        public async Task<IActionResult> Details(int id)
        {
            var article = await _context.NewsArticles.FindAsync(id);
            if (article == null || !article.IsPublished) return NotFound();
            return View(article);
        }
    }
}
