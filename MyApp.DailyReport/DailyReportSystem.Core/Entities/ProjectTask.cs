using System;
using System.Collections.Generic;

namespace DailyReportSystem.Core.Entities
{
    public class ProjectTask
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; } = null!;
        public string TaskName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Status { get; set; } = "Open"; // Open, In Progress, Done
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<DailyReport> DailyReports { get; set; } = new List<DailyReport>();
    }
}
