using DailyReportSystem.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace DailyReportSystem.Application.DTOs
{
    public class UserCreateDTO
    {
        [Required]
        public string Username { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        [Required]
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        [Required]
        public RoleType Role { get; set; }
    }
}
