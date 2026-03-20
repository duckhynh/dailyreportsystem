using System;
using System.Collections.Generic;

namespace DailyReportSystem.Core.Entities
{
    public class Project
    {
        public int Id { get; set; }
        public string ProjectName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        public ICollection<DailyReport> DailyReports { get; set; } = new List<DailyReport>();
        public ICollection<UserProject> UserProjects { get; set; } = new List<UserProject>();
        public ICollection<ProjectTask> ProjectTasks { get; set; } = new List<ProjectTask>();
    }
}
