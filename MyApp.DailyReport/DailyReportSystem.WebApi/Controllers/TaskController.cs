using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using DailyReportSystem.Application.DTOs;
using DailyReportSystem.Application.Services;

namespace DailyReportSystem.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TaskController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public TaskController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet("Project/{projectId}")]
        public async Task<IActionResult> GetTasksByProjectId(int projectId)
        {
            var tasks = await _projectService.GetTasksByProjectIdAsync(projectId);
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskById(int id)
        {
            var t = await _projectService.GetTaskByIdAsync(id);
            if (t == null) return NotFound();
            return Ok(t);
        }

        [HttpPost]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> CreateTask([FromBody] ProjectTaskCreateDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await _projectService.CreateTaskAsync(dto);
            return CreatedAtAction(nameof(GetTaskById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> UpdateTask(int id, [FromBody] ProjectTaskCreateDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var success = await _projectService.UpdateTaskAsync(id, dto);
            if (!success) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var success = await _projectService.DeleteTaskAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
