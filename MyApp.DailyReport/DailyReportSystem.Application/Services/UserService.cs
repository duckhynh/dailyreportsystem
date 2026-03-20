using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DailyReportSystem.Application.DTOs;
using DailyReportSystem.Core.Entities;
using DailyReportSystem.Core.Interfaces;

namespace DailyReportSystem.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<UserDTO>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return users.Select(u => new UserDTO
            {
                Id = u.Id,
                Username = u.Username,
                FullName = u.FullName,
                Email = u.Email,
                Role = u.Role
            });
        }

        public async Task<UserDTO?> GetUserByIdAsync(int id)
        {
            var u = await _userRepository.GetByIdAsync(id);
            if (u == null) return null;
            return new UserDTO
            {
                Id = u.Id,
                Username = u.Username,
                FullName = u.FullName,
                Email = u.Email,
                Role = u.Role
            };
        }

        public async Task<UserDTO> CreateUserAsync(UserCreateDTO dto)
        {
            var user = new User
            {
                Username = dto.Username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                FullName = dto.FullName,
                Email = dto.Email,
                Role = dto.Role
            };
            var created = await _userRepository.AddAsync(user);
            return new UserDTO
            {
                Id = created.Id,
                Username = created.Username,
                FullName = created.FullName,
                Email = created.Email,
                Role = created.Role
            };
        }

        public async Task<bool> UpdateUserAsync(int id, UserUpdateDTO dto)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return false;

            user.FullName = dto.FullName;
            user.Email = dto.Email;
            user.Role = dto.Role;
            if (!string.IsNullOrEmpty(dto.Password))
            {
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            }

            await _userRepository.UpdateAsync(user);
            return true;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return false;
            
            await _userRepository.DeleteAsync(user);
            return true;
        }
    }
}
