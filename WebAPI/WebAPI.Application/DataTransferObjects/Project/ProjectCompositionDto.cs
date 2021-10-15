using System.Collections.Generic;
using WebAPI.Application.DataTransferObjects.Employee;

namespace WebAPI.Application.DataTransferObjects.Project
{
    public class ProjectCompositionDto
    {
        public PostProjectDto Project { get; set; }
        public IEnumerable<GetEmployeeDto> Employees { get; set; }
    }
}
