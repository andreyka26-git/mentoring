using WebAPI.BusinessLogic.DataTransferObjects;

namespace WebAPI.BusinessLogic.Interfaces
{
    public interface IMainService
    {
        bool AssignToProject(int id, string name);
        bool UnAssignFromProject(int id, string name);
        ProjectCompositionDto GetProjectComposition(string name);
    }
}
