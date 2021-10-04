using System.Collections.Generic;

namespace WebAPI.BusinessLogic.DataTransferObjects
{
    public class ProjectCompositionDto
    {
        public ProjectDto Project { get; set; }
        public IEnumerable<EmployeeDto> Employees { get; set; }
    }
}
