using DailyReportSystem.Core.Interfaces;
using DailyReportSystem.Core.Entities;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using System.Linq;

namespace DailyReportSystem.WebApi.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IGenericRepository<Project> _projectRepo;

        public IndexModel(IGenericRepository<Project> projectRepo)
        {
            _projectRepo = projectRepo;
        }

        public int ProjectCount { get; set; }

        public async Task OnGetAsync()
        {
            var projects = await _projectRepo.GetAllAsync();
            ProjectCount = projects.Count();
        }
    }
}
