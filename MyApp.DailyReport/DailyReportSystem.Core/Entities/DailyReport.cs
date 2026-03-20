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
        public int? ProjectTaskId { get; set; }
        public ProjectTask? ProjectTask { get; set; }
        public DateTime ReportDate { get; set; }
        public string TasksDone { get; set; } = string.Empty; // Nội dung công việc
        public string WorkQuantity { get; set; } = string.Empty; // Số lượng công việc
        public string WorkDescription { get; set; } = string.Empty; // Mô tả công việc
        public string Issues { get; set; } = string.Empty; // Khó khăn
        public string TomorrowPlan { get; set; } = string.Empty; // Công việc ưu tiên
        public string Department { get; set; } = string.Empty; // Phòng ban hoạt động
        public DateTime CreatedAt { get; set; }
    }
}
