using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace WebAPI.Domain.Aggregates.EmployeeAggregate
{
    public interface IEmployeeRepository
    {
        Task <List<Employee>> GetAllAsync(CancellationToken token, EmployeeFiltering filter = null, string orderBy = null, string fieldOrder = null);
        Task<Employee> GetAsync(int id, CancellationToken token);
        Task<List<Employee>> FindAsync(CancellationToken token, int? id = null, int? projectId = null, string fName = null, string lName = null, bool? isHigherEducation = null);
        Task CreateAsync(Employee item, CancellationToken token);
        void Update(Employee item);
        Task DeleteAsync(int id, CancellationToken token);
    }
}
