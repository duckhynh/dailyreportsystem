using System.ComponentModel.DataAnnotations;
namespace DailyReportSystem.Application.DTOs
{
    public class ProjectCreateDTO
    {
        [Required]
        public string ProjectName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        
        // Mảng tên các công việc để khởi tạo kèm theo Dự án mới
        public System.Collections.Generic.List<string> TaskNames { get; set; } = new System.Collections.Generic.List<string>();
    }
}
