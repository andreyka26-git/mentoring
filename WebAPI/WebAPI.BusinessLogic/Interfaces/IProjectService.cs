using System.Collections.Generic;
using WebAPI.BusinessLogic.DataTransferObjects;

namespace WebAPI.BusinessLogic.Interfaces
{
    public interface IProjectService
    {
        IEnumerable<ProjectDto> GetAllProjects();
        ProjectDto GetProjectById(int id);
        void CreateProject(ProjectDto project);
        int? GetProjectId(ProjectDto employee);
        void UpdateProject(int id, ProjectDto projectDto);
        void DeleteProject(int id);
    }
}
