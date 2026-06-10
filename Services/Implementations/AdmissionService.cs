using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdmissionWeb.Data;
using AdmissionWeb.Models.Entities;
using AdmissionWeb.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AdmissionWeb.Services.Implementations
{
    public class AdmissionService : IAdmissionService
    {
        private readonly ApplicationDbContext _context;

        public AdmissionService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AdmissionPeriod>> GetActivePeriodsAsync()
        {
            return await _context.AdmissionPeriods
                .Where(p => p.IsActive && p.EndDate >= DateTime.Now)
                .ToListAsync();
        }

        public async Task<AdmissionPeriod> GetPeriodByIdAsync(int id)
        {
            return await _context.AdmissionPeriods.FindAsync(id);
        }

        public async Task<IEnumerable<ProgramOption>> GetProgramOptionsAsync()
        {
            return await _context.ProgramOptions.ToListAsync();
        }

        public async Task<IEnumerable<NewsArticle>> GetLatestNewsAsync(int count)
        {
            return await _context.NewsArticles
                .Where(a => a.IsPublished)
                .OrderByDescending(a => a.PublishedAt)
                .Take(count)
                .ToListAsync();
        }

        public async Task<NewsArticle> GetNewsByIdAsync(int id)
        {
            return await _context.NewsArticles.FindAsync(id);
        }
    }
}
