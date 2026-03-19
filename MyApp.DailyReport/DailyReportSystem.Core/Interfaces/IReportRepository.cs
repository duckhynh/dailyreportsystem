using System.Collections.Generic;
using System.Threading.Tasks;
using DailyReportSystem.Core.Entities;

namespace DailyReportSystem.Core.Interfaces
{
    public interface IReportRepository : IGenericRepository<DailyReport>
    {
        Task<IEnumerable<DailyReport>> GetByUserIdAsync(int userId);
    }
}
