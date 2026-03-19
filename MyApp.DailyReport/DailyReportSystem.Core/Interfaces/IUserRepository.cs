using System.Threading.Tasks;
using DailyReportSystem.Core.Entities;

namespace DailyReportSystem.Core.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User?> AuthenticateAsync(string username, string password);
        Task<bool> IsAssignedToProjectAsync(int userId, int projectId);
    }
}
