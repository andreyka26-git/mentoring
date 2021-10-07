using System.Collections.Generic;
using WebAPI.Application.DataTransferObjects;

namespace WebAPI.Application.Interfaces
{
    public interface IEmployeeService
    {
        IEnumerable<EmployeeGetDto> GetAllEmployees();
        EmployeeGetDto GetEmployeeById(int id);
        int CreateEmployee(EmployeePostDto employee);
        void UpdateEmployee(int id, EmployeePostDto employee);
        void DeleteEmployee(int id);
        IEnumerable<EmployeeGetDto> FilteringAndOrderBy(EmployeeFiltering filter, string orderBy, string fieldOrder);
    }
}
