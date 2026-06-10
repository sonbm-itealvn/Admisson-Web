using System;
using System.Linq;
using System.Threading.Tasks;
using AdmissionWeb.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace AdmissionWeb.Data.SeedData
{
    public static class DbInitializer
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            using var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            using var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            using var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            context.Database.EnsureCreated();

            // Seed Roles
            string[] roleNames = { "Admin", "AdmissionOfficer", "Candidate" };
            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // Seed Admin User
            if (await userManager.FindByEmailAsync("admin@admission.edu.vn") == null)
            {
                var admin = new ApplicationUser
                {
                    UserName = "admin@admission.edu.vn",
                    Email = "admin@admission.edu.vn",
                    FullName = "Hệ thống Quản trị",
                    Address = "Hà Nội",
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(admin, "Admin@123");
                await userManager.AddToRoleAsync(admin, "Admin");
            }

            // Seed Admission Periods
            if (!context.AdmissionPeriods.Any())
            {
                context.AdmissionPeriods.Add(new AdmissionPeriod
                {
                    Name = "Tuyển sinh lớp 10 năm học 2026-2027",
                    Year = 2026,
                    StartDate = DateTime.Now.AddDays(-10),
                    EndDate = DateTime.Now.AddMonths(2),
                    IsActive = true,
                    Description = "Kỳ tuyển sinh chính thức cho học sinh lớp 9 vào lớp 10 THPT."
                });
                await context.SaveChangesAsync();
            }

            // Seed Program Options
            if (!context.ProgramOptions.Any())
            {
                context.ProgramOptions.AddRange(
                    new ProgramOption { Name = "Lớp 10 Cơ bản", Code = "CB", Quota = 400, IsSpecialized = false, Description = "" },
                    new ProgramOption { Name = "Lớp 10 Chuyên Toán", Code = "CT", Quota = 35, IsSpecialized = true, Description = "" },
                    new ProgramOption { Name = "Lớp 10 Chuyên Văn", Code = "CV", Quota = 35, IsSpecialized = true, Description = "" },
                    new ProgramOption { Name = "Lớp 10 Chuyên Anh", Code = "CA", Quota = 35, IsSpecialized = true, Description = "" }
                );
                await context.SaveChangesAsync();
            }

            // Seed News
            if (!context.NewsArticles.Any())
            {
                context.NewsArticles.Add(new NewsArticle
                {
                    Title = "Thông báo tuyển sinh năm học 2026-2027",
                    Content = "Trường THPT thông báo kế hoạch tuyển sinh lớp 10 năm học 2026-2027 với nhiều chỉ tiêu hấp dẫn...",
                    Author = "Ban Giám Hiệu",
                    PublishedAt = DateTime.Now,
                    Category = "Thông báo",
                    ImageUrl = "",
                    IsPublished = true
                });
                await context.SaveChangesAsync();
            }
        }
    }
}
