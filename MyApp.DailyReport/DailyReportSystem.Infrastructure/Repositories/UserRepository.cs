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
            var user = await _dbSet.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null) return null;
            
            // Check plain text backward compatibility
            if (user.PasswordHash == password) return user;
            
            // Verify BCrypt hash
            try
            {
                if (BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                    return user;
            }
            catch (System.Exception) { }

            return null;
        }

        public async Task<bool> IsAssignedToProjectAsync(int userId, int projectId)
        {
            return await _context.UserProjects.AnyAsync(up => up.UserId == userId && up.ProjectId == projectId);
        }
    }
}
