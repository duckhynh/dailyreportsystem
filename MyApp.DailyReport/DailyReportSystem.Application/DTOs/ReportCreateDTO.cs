using System;
using System.ComponentModel.DataAnnotations;

namespace DailyReportSystem.Application.DTOs
{
    public class ReportCreateDTO
    {
        [Required]
        public int UserId { get; set; }
        
        [Required]
        public int ProjectId { get; set; }
        
        // Nối vào "Project Task" ID từ màn cũ (vẫn giữ nếu có)
        public int? ProjectTaskId { get; set; }
        
        [Required(ErrorMessage = "Work content is required.")]
        public string TasksDone { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Quantitative value is required.")]
        public string WorkQuantity { get; set; } = string.Empty;
        
        public string WorkDescription { get; set; } = string.Empty;
        
        public string Issues { get; set; } = string.Empty;
        
        public string TomorrowPlan { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Department is required.")]
        public string Department { get; set; } = string.Empty;
    }
}
