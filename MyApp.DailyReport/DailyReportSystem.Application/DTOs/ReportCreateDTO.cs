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
        
        [Required(ErrorMessage = "Today's tasks are required.")]
        public string TasksDone { get; set; } = string.Empty;
        
        public string Issues { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Tomorrow's plan is required.")]
        public string TomorrowPlan { get; set; } = string.Empty;
    }
}
