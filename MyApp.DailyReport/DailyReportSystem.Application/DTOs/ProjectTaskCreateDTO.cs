using System.ComponentModel.DataAnnotations;
namespace DailyReportSystem.Application.DTOs
{
    public class ProjectTaskCreateDTO
    {
        [Required]
        public int ProjectId { get; set; }
        [Required]
        public string TaskName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Status { get; set; } = "Open";
    }
}
