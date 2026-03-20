using System.Collections.Generic;
using System.Threading.Tasks;
using DailyReportSystem.Application.DTOs;

namespace DailyReportSystem.Application.Services
{
    public interface IProjectService
    {
        Task<IEnumerable<ProjectDTO>> GetAllProjectsAsync();
        Task<ProjectDTO?> GetProjectByIdAsync(int id);
        Task<ProjectDTO> CreateProjectAsync(ProjectCreateDTO dto);
        Task<bool> UpdateProjectAsync(int id, ProjectCreateDTO dto);
        Task<bool> DeleteProjectAsync(int id);

        Task<IEnumerable<ProjectTaskDTO>> GetTasksByProjectIdAsync(int projectId);
        Task<ProjectTaskDTO?> GetTaskByIdAsync(int id);
        Task<ProjectTaskDTO> CreateTaskAsync(ProjectTaskCreateDTO dto);
        Task<bool> UpdateTaskAsync(int id, ProjectTaskCreateDTO dto);
        Task<bool> DeleteTaskAsync(int id);
    }
}
