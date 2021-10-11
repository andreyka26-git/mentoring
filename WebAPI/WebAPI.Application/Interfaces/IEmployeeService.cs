using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Application.DataTransferObjects;

namespace WebAPI.Application.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<GetEmployeeDto>> GetAllEmployeesAsync();
        Task<GetEmployeeDto> GetEmployeeByIdAsync(int id);
        Task<int> CreateEmployeeAsync(PostEmployeeDto employee);
        Task UpdateEmployeeAsync(int id, PostEmployeeDto employee);
        Task DeleteEmployeeAsync(int id);
        Task<IEnumerable<GetEmployeeDto>> FilteringAndOrderByAsync(EmployeeFiltering filter, string orderBy, string fieldOrder);
    }
}
