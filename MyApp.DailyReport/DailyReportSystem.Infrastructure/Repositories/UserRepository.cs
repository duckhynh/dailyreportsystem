using System.Threading.Tasks;
using DailyReportSystem.Core.Entities;
using DailyReportSystem.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DailyReportSystem.Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<User?> AuthenticateAsync(string username, string password)
        {
            return await _dbSet.FirstOrDefaultAsync(u => u.Username == username && u.PasswordHash == password);
        }

        public async Task<bool> IsAssignedToProjectAsync(int userId, int projectId)
        {
            return await _context.UserProjects.AnyAsync(up => up.UserId == userId && up.ProjectId == projectId);
        }
    }
}
