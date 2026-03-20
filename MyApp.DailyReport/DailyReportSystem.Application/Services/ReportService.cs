using System;
using System.Threading.Tasks;
using DailyReportSystem.Application.DTOs;
using DailyReportSystem.Core.Entities;
using DailyReportSystem.Core.Interfaces;

namespace DailyReportSystem.Application.Services
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository _reportRepository;
        private readonly IUserRepository _userRepository;

        public ReportService(IReportRepository reportRepository, IUserRepository userRepository)
        {
            _reportRepository = reportRepository;
            _userRepository = userRepository;
        }

        public async Task<DailyReport> CreateReportAsync(ReportCreateDTO dto)
        {
            // Do yêu cầu "Không cần bắt buộc assign employee", ta bỏ qua validation IsAssignedToProjectAsync
            
            var report = new DailyReport
            {
                UserId = dto.UserId,
                ProjectId = dto.ProjectId,
                ProjectTaskId = dto.ProjectTaskId,
                ReportDate = DateTime.UtcNow.Date,
                TasksDone = dto.TasksDone,
                WorkQuantity = dto.WorkQuantity,
                WorkDescription = dto.WorkDescription,
                Issues = dto.Issues,
                TomorrowPlan = dto.TomorrowPlan,
                Department = dto.Department,
                CreatedAt = DateTime.UtcNow
            };

            return await _reportRepository.AddAsync(report);
        }

        public async Task<System.Collections.Generic.IEnumerable<DailyReport>> GetReportsByUserAsync(int userId)
        {
            return await _reportRepository.GetByUserIdAsync(userId);
        }
    }
}
