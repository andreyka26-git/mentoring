using System.Collections.Generic;
using WebAPI.BusinessLogic.DataTransferObjects;
using WebAPI.BusinessLogic.Helpers;

namespace WebAPI.BusinessLogic.Interfaces
{
    public interface IEmployeeService
    {
        IEnumerable<EmployeeDto> GetAllEmployees();
        EmployeeDto GetEmployeeById(int id);
        void CreateEmployee(EmployeeDto employee);
        int? GetEmployeeId(EmployeeDto employee);
        void UpdateEmployee(int id, EmployeeDto employee);
        void DeleteEmployee(int id);
        IEnumerable<EmployeeDto> FilteringAndOrderBy(EmployeeFiltering filter, string orderBy, string fieldOrder);
    }
}
