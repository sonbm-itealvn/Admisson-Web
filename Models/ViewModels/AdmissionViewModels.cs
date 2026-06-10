using System.Collections.Generic;
using AdmissionWeb.Models.Entities;

namespace AdmissionWeb.Models.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<AdmissionPeriod> ActivePeriods { get; set; }
        public IEnumerable<NewsArticle> LatestNews { get; set; }
    }

    public class AdmissionDetailViewModel
    {
        public AdmissionPeriod Period { get; set; }
        public IEnumerable<ProgramOption> Options { get; set; }
    }

    public class ApplicationFormViewModel
    {
        public int AdmissionPeriodId { get; set; }
        public string PeriodName { get; set; }
        public Application Application { get; set; }
        public IEnumerable<ProgramOption> ProgramOptions { get; set; }
    }

    public class ResultSearchViewModel
    {
        public string SearchTerm { get; set; }
        public Application Application { get; set; }
        public bool IsSearched { get; set; }
    }
}
