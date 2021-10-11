using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Application.DataTransferObjects;

namespace WebAPI.Application.Interfaces
{
    public interface IProjectService
    {
        Task<IEnumerable<GetProjectDto>> GetAllProjectsAsync();
        Task<GetProjectDto> GetProjectByIdAsync(int id);
        Task<int> CreateProjectAsync(PostProjectDto project);
        Task UpdateProjectAsync(int id, PostProjectDto projectDto);
        Task DeleteProjectAsync(int id);
        Task<bool> AssignToProjectAsync(int id, string name);
        Task<bool> UnAssignFromProjectAsync(int id, string name);
        ProjectCompositionDto GetProjectComposition(string name);
    }
}
