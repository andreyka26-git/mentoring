using System.Collections.Generic;

namespace WebAPI.Application.DataTransferObjects
{
    public class ProjectCompositionDto
    {
        public PostProjectDto Project { get; set; }
        public IEnumerable<GetEmployeeDto> Employees { get; set; }
    }
}
