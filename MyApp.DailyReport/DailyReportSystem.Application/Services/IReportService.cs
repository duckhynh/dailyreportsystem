using System.Collections.Generic;
using System.Threading.Tasks;
using DailyReportSystem.Application.DTOs;
using DailyReportSystem.Core.Entities;

namespace DailyReportSystem.Application.Services
{
    public interface IReportService
    {
        Task<DailyReport> CreateReportAsync(ReportCreateDTO dto);
        Task<IEnumerable<DailyReport>> GetReportsByUserAsync(int userId);
    }
}
