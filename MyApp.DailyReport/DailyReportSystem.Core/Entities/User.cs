using System.Collections.Generic;
using DailyReportSystem.Core.Enums;

namespace DailyReportSystem.Core.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public RoleType Role { get; set; }

        public ICollection<DailyReport> DailyReports { get; set; } = new List<DailyReport>();
        public ICollection<UserProject> UserProjects { get; set; } = new List<UserProject>();
    }
}
