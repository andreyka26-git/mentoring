using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebAPI.Application.DataTransferObjects.Employee;
using WebAPI.Domain.Aggregates.EmployeeAggregate;

namespace WebAPI.Application.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<GetEmployeeDto>> GetAllEmployeesAsync(EmployeeFiltering filter, string orderBy, string fieldOrder, CancellationToken token);
        Task<GetEmployeeDto> GetEmployeeByIdAsync(int id, CancellationToken token);
        Task<int> CreateEmployeeAsync(PostEmployeeDto employee, CancellationToken token);
        Task UpdateEmployeeAsync(int id, PostEmployeeDto employee, CancellationToken token);
        Task DeleteEmployeeAsync(int id, CancellationToken token);
    }
}
