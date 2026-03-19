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
            // Xác thực nghiệp vụ: Nhân viên có thuộc dự án không?
            bool isAssigned = await _userRepository.IsAssignedToProjectAsync(dto.UserId, dto.ProjectId);
            if (!isAssigned)
            {
                throw new Exception("Lỗi nghiệp vụ: Bạn không được phân công vào Dự án này!");
            }
            
            // Validation is mostly handled by DTO annotations, but additional logic can be added here
            
            var report = new DailyReport
            {
                UserId = dto.UserId,
                ProjectId = dto.ProjectId,
                ReportDate = DateTime.UtcNow.Date,
                TasksDone = dto.TasksDone,
                Issues = dto.Issues,
                TomorrowPlan = dto.TomorrowPlan,
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
