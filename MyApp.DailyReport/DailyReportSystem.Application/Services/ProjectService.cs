using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DailyReportSystem.Application.DTOs;
using DailyReportSystem.Core.Entities;
using DailyReportSystem.Core.Interfaces;

namespace DailyReportSystem.Application.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IGenericRepository<Project> _projectRepo;
        private readonly IGenericRepository<ProjectTask> _taskRepo;

        public ProjectService(IGenericRepository<Project> projectRepo, IGenericRepository<ProjectTask> taskRepo)
        {
            _projectRepo = projectRepo;
            _taskRepo = taskRepo;
        }

        public async Task<IEnumerable<ProjectDTO>> GetAllProjectsAsync()
        {
            var projects = await _projectRepo.GetAllAsync();
            return projects.Select(p => new ProjectDTO
            {
                Id = p.Id,
                ProjectName = p.ProjectName,
                Description = p.Description,
                CreatedAt = p.CreatedAt
            }).OrderByDescending(p => p.CreatedAt);
        }

        public async Task<ProjectDTO?> GetProjectByIdAsync(int id)
        {
            var p = await _projectRepo.GetByIdAsync(id);
            if (p == null) return null;
            return new ProjectDTO
            {
                Id = p.Id,
                ProjectName = p.ProjectName,
                Description = p.Description,
                CreatedAt = p.CreatedAt
            };
        }

        public async Task<ProjectDTO> CreateProjectAsync(ProjectCreateDTO dto)
        {
            var entity = new Project
            {
                ProjectName = dto.ProjectName,
                Description = dto.Description,
                CreatedAt = DateTime.UtcNow
            };
            var result = await _projectRepo.AddAsync(entity);
            
            if (dto.TaskNames != null && dto.TaskNames.Any())
            {
                foreach (var taskName in dto.TaskNames)
                {
                    if (!string.IsNullOrWhiteSpace(taskName))
                    {
                        await _taskRepo.AddAsync(new ProjectTask
                        {
                            ProjectId = result.Id,
                            TaskName = taskName.Trim(),
                            Description = string.Empty,
                            Status = "Open",
                            CreatedAt = DateTime.UtcNow
                        });
                    }
                }
            }

            return new ProjectDTO
            {
                Id = result.Id,
                ProjectName = result.ProjectName,
                Description = result.Description,
                CreatedAt = result.CreatedAt
            };
        }

        public async Task<bool> UpdateProjectAsync(int id, ProjectCreateDTO dto)
        {
            var entity = await _projectRepo.GetByIdAsync(id);
            if (entity == null) return false;
            
            entity.ProjectName = dto.ProjectName;
            entity.Description = dto.Description;
            await _projectRepo.UpdateAsync(entity);
            return true;
        }

        public async Task<bool> DeleteProjectAsync(int id)
        {
            var entity = await _projectRepo.GetByIdAsync(id);
            if (entity == null) return false;
            
            await _projectRepo.DeleteAsync(entity);
            return true;
        }

        public async Task<IEnumerable<ProjectTaskDTO>> GetTasksByProjectIdAsync(int projectId)
        {
            var tasks = await _taskRepo.FindAsync(t => t.ProjectId == projectId);
            return tasks.Select(t => new ProjectTaskDTO
            {
                Id = t.Id,
                ProjectId = t.ProjectId,
                TaskName = t.TaskName,
                Description = t.Description,
                Status = t.Status,
                CreatedAt = t.CreatedAt
            }).OrderByDescending(t => t.CreatedAt);
        }

        public async Task<ProjectTaskDTO?> GetTaskByIdAsync(int id)
        {
            var t = await _taskRepo.GetByIdAsync(id);
            if (t == null) return null;
            return new ProjectTaskDTO
            {
                Id = t.Id,
                ProjectId = t.ProjectId,
                TaskName = t.TaskName,
                Description = t.Description,
                Status = t.Status,
                CreatedAt = t.CreatedAt
            };
        }

        public async Task<ProjectTaskDTO> CreateTaskAsync(ProjectTaskCreateDTO dto)
        {
            var entity = new ProjectTask
            {
                ProjectId = dto.ProjectId,
                TaskName = dto.TaskName,
                Description = dto.Description,
                Status = dto.Status,
                CreatedAt = DateTime.UtcNow
            };
            var result = await _taskRepo.AddAsync(entity);
            return new ProjectTaskDTO
            {
                Id = result.Id,
                ProjectId = result.ProjectId,
                TaskName = result.TaskName,
                Description = result.Description,
                Status = result.Status,
                CreatedAt = result.CreatedAt
            };
        }

        public async Task<bool> UpdateTaskAsync(int id, ProjectTaskCreateDTO dto)
        {
            var entity = await _taskRepo.GetByIdAsync(id);
            if (entity == null) return false;

            entity.TaskName = dto.TaskName;
            entity.Description = dto.Description;
            entity.Status = dto.Status;
            
            await _taskRepo.UpdateAsync(entity);
            return true;
        }

        public async Task<bool> DeleteTaskAsync(int id)
        {
            var entity = await _taskRepo.GetByIdAsync(id);
            if (entity == null) return false;
            
            await _taskRepo.DeleteAsync(entity);
            return true;
        }
    }
}
