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
    public class ApplicationService : IApplicationService
    {
        private readonly ApplicationDbContext _context;

        public ApplicationService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Application> SubmitApplicationAsync(Application application)
        {
            application.SubmittedAt = DateTime.Now;
            application.Status = "Pending";
            application.ApplicationCode = "TS" + DateTime.Now.ToString("yyyyMMdd") + new Random().Next(1000, 9999);
            
            _context.Applications.Add(application);
            await _context.SaveChangesAsync();
            return application;
        }

        public async Task<Application> GetApplicationByIdAsync(int id)
        {
            return await _context.Applications
                .Include(a => a.AdmissionPeriod)
                .Include(a => a.ProgramOption)
                .Include(a => a.ExamResults)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Application> GetApplicationByCodeAsync(string code)
        {
            return await _context.Applications
                .Include(a => a.AdmissionPeriod)
                .Include(a => a.ProgramOption)
                .Include(a => a.ExamResults)
                .FirstOrDefaultAsync(a => a.ApplicationCode == code || a.IDNumber == code);
        }

        public async Task<IEnumerable<Application>> GetUserApplicationsAsync(string userId)
        {
            return await _context.Applications
                .Include(a => a.AdmissionPeriod)
                .Include(a => a.ProgramOption)
                .Where(a => a.UserId == userId)
                .OrderByDescending(a => a.SubmittedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Application>> GetAllApplicationsAsync()
        {
            return await _context.Applications
                .Include(a => a.AdmissionPeriod)
                .Include(a => a.ProgramOption)
                .OrderByDescending(a => a.SubmittedAt)
                .ToListAsync();
        }

        public async Task UpdateStatusAsync(int id, string status, string reason = null)
        {
            var app = await _context.Applications.FindAsync(id);
            if (app != null)
            {
                app.Status = status;
                app.RejectionReason = reason;
                await _context.SaveChangesAsync();
            }
        }
    }
}
