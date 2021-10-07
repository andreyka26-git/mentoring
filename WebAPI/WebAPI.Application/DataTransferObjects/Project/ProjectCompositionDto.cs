using System.Collections.Generic;

namespace WebAPI.Application.DataTransferObjects
{
    public class ProjectCompositionDto
    {
        public ProjectPostDto Project { get; set; }
        public IEnumerable<EmployeeGetDto> Employees { get; set; }
    }
}
