using System.Threading.Tasks;
using DailyReportSystem.Application.DTOs;

namespace DailyReportSystem.Application.Services
{
    public interface IAuthService
    {
        Task<AuthResponseDTO?> LoginAsync(LoginDTO dto);
    }
}
