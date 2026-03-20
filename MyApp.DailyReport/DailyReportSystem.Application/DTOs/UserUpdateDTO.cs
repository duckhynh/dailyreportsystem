using DailyReportSystem.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace DailyReportSystem.Application.DTOs
{
    public class UserUpdateDTO
    {
        [Required]
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        [Required]
        public RoleType Role { get; set; }
        public string? Password { get; set; }
    }
}
