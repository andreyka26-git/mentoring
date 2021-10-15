using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebAPI.Application.DataTransferObjects.Project;

namespace WebAPI.Application.Interfaces
{
    public interface IProjectService
    {
        Task<IEnumerable<GetProjectDto>> GetAllProjectsAsync(CancellationToken cancellationToken);
        Task<GetProjectDto> GetProjectByIdAsync(int id, CancellationToken cancellationToken);
        Task<int> CreateProjectAsync(PostProjectDto project, CancellationToken cancellationToken);
        Task UpdateProjectAsync(int id, PostProjectDto projectDto, CancellationToken cancellationToken);
        Task DeleteProjectAsync(int id, CancellationToken cancellationToken);
        Task<bool> AssignToProjectAsync(int id, string name, CancellationToken cancellationToken);
        Task<bool> UnAssignFromProjectAsync(int id, string name, CancellationToken cancellationToken);
        Task<ProjectCompositionDto> GetProjectCompositionAsync(string name, CancellationToken cancellationToken);
    }
}
