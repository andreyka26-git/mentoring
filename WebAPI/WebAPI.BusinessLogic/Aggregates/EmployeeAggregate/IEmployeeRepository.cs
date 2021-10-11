using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Domain.Aggregates.EmployeeAggregate
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllAsync();
        Task<Employee> GetAsync(int id);
        IQueryable<Employee> Find(int? id = null, int? projectId = null, string fName = null, string lName = null, bool? isHigherEducation = null);
        Task CreateAsync(Employee item);
        void Update(Employee item);
        Task DeleteAsync(int id);
    }
}
