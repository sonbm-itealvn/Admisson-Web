using System.Collections.Generic;
using System.Threading.Tasks;
using AdmissionWeb.Models.Entities;

namespace AdmissionWeb.Services.Interfaces
{
    public interface IApplicationService
    {
        Task<Application> SubmitApplicationAsync(Application application);
        Task<Application> GetApplicationByIdAsync(int id);
        Task<Application> GetApplicationByCodeAsync(string code);
        Task<IEnumerable<Application>> GetUserApplicationsAsync(string userId);
        Task<IEnumerable<Application>> GetAllApplicationsAsync();
        Task UpdateStatusAsync(int id, string status, string reason = null);
    }
}
