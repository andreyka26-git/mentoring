using System.Collections.Generic;
using WebAPI.Domain.Aggregates.EmployeeAggregate;

namespace WebAPI.Domain.Aggregates.ProjectAggregate
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Duration { get; set; }
        public virtual List<Employee> Employees { get; set; }
    }
}
