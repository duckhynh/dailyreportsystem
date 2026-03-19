using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DailyReportSystem.Core.Entities;
using DailyReportSystem.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DailyReportSystem.Infrastructure.Repositories
{
    public class ReportRepository : GenericRepository<DailyReport>, IReportRepository
    {
        public ReportRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<DailyReport>> GetByUserIdAsync(int userId)
        {
            return await _dbSet
                .Where(r => r.UserId == userId)
                .ToListAsync();
        }
    }
}
