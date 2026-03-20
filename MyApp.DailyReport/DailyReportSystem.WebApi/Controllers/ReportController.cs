using System.Threading.Tasks;
using DailyReportSystem.Application.DTOs;
using DailyReportSystem.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace DailyReportSystem.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpPost]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> CreateReport([FromBody] ReportCreateDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var report = await _reportService.CreateReportAsync(dto);
            return CreatedAtAction(nameof(CreateReport), new { id = report.Id }, report);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetReportsByUser(int userId)
        {
            var reports = await _reportService.GetReportsByUserAsync(userId);
            return Ok(reports);
        }
    }
}
