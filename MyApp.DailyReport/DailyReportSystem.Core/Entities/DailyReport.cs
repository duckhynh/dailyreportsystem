using System;

namespace DailyReportSystem.Core.Entities
{
    public class DailyReport
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public int ProjectId { get; set; }
        public Project Project { get; set; } = null!;
        public DateTime ReportDate { get; set; }
        public string TasksDone { get; set; } = string.Empty;
        public string Issues { get; set; } = string.Empty;
        public string TomorrowPlan { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
