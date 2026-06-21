using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AdmissionWeb.Models.Entities;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace AdmissionWeb.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<AdmissionPeriod> AdmissionPeriods { get; set; }
        public DbSet<ProgramOption> ProgramOptions { get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<ExamResult> ExamResults { get; set; }
        public DbSet<NewsArticle> NewsArticles { get; set; }
        public DbSet<NewsCategory> NewsCategories { get; set; }
        public DbSet<ContactRequest> ContactRequests { get; set; }
        public DbSet<Banner> Banners { get; set; }

        public override int SaveChanges()
        {
            ApplySoftDelete();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ApplySoftDelete();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void ApplySoftDelete()
        {
            foreach (var entry in ChangeTracker.Entries<ISoftDelete>())
            {
                if (entry.State == EntityState.Deleted)
                {
                    entry.State = EntityState.Modified;
                    entry.Entity.IsDeleted = true;
                }
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Global query filters for Soft Delete
            builder.Entity<AdmissionPeriod>().HasQueryFilter(e => !e.IsDeleted);
            builder.Entity<ProgramOption>().HasQueryFilter(e => !e.IsDeleted);
            builder.Entity<Application>().HasQueryFilter(e => !e.IsDeleted);
            builder.Entity<ExamResult>().HasQueryFilter(e => !e.IsDeleted);
            builder.Entity<NewsArticle>().HasQueryFilter(e => !e.IsDeleted);
            builder.Entity<NewsCategory>().HasQueryFilter(e => !e.IsDeleted);
            builder.Entity<ContactRequest>().HasQueryFilter(e => !e.IsDeleted);
            builder.Entity<Banner>().HasQueryFilter(e => !e.IsDeleted);

            builder.Entity<Application>()
                .HasOne(a => a.AdmissionPeriod)
                .WithMany(p => p.Applications)
                .HasForeignKey(a => a.AdmissionPeriodId);

            builder.Entity<Application>()
                .HasOne(a => a.ProgramOption)
                .WithMany()
                .HasForeignKey(a => a.ProgramOptionId);

            builder.Entity<ExamResult>()
                .HasOne(r => r.Application)
                .WithMany(a => a.ExamResults)
                .HasForeignKey(r => r.ApplicationId);
        }
    }
}
