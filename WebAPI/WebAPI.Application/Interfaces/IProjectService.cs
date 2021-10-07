using System.Collections.Generic;
using WebAPI.Application.DataTransferObjects;

namespace WebAPI.Application.Interfaces
{
    public interface IProjectService
    {
        IEnumerable<ProjectGetDto> GetAllProjects();
        ProjectGetDto GetProjectById(int id);
        int CreateProject(ProjectPostDto project);
        void UpdateProject(int id, ProjectPostDto projectDto);
        void DeleteProject(int id);
        bool AssignToProject(int id, string name);
        bool UnAssignFromProject(int id, string name);
        ProjectCompositionDto GetProjectComposition(string name);
    }
}
