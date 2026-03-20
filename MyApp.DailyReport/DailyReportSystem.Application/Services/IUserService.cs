using System.Collections.Generic;
using System.Threading.Tasks;
using DailyReportSystem.Application.DTOs;

namespace DailyReportSystem.Application.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserDTO>> GetAllUsersAsync();
        Task<UserDTO?> GetUserByIdAsync(int id);
        Task<UserDTO> CreateUserAsync(UserCreateDTO dto);
        Task<bool> UpdateUserAsync(int id, UserUpdateDTO dto);
        Task<bool> DeleteUserAsync(int id);
    }
}
