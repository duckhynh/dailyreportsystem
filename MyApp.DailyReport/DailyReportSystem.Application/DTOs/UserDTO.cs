using DailyReportSystem.Core.Enums;

namespace DailyReportSystem.Application.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public RoleType Role { get; set; }
        public string RoleName => Role.ToString();
    }
}
