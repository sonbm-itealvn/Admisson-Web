using System.Collections.Generic;
using System.Threading.Tasks;
using AdmissionWeb.Models.Entities;

namespace AdmissionWeb.Services.Interfaces
{
    public interface IAdmissionService
    {
        Task<IEnumerable<AdmissionPeriod>> GetActivePeriodsAsync();
        Task<AdmissionPeriod> GetPeriodByIdAsync(int id);
        Task<IEnumerable<ProgramOption>> GetProgramOptionsAsync();
        Task<IEnumerable<NewsArticle>> GetLatestNewsAsync(int count);
        Task<IEnumerable<NewsArticle>> GetNewsByCategoryAsync(string category, int count);
        Task<NewsArticle> GetNewsByIdAsync(int id);
        Task<IEnumerable<Banner>> GetActiveBannersAsync();
    }
}
