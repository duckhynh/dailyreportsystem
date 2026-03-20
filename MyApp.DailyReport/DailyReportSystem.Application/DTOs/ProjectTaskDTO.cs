using System;
namespace DailyReportSystem.Application.DTOs
{
    public class ProjectTaskDTO
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string TaskName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public string ProjectName { get; set; } = string.Empty;
    }
}
