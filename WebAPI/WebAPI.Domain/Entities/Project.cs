using System.Collections.Generic;

namespace WebAPI.Domain.Entities
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Duration { get; set; }
        public virtual List<Employee> Employees { get; set; }
    }
}
