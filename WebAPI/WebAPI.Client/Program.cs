using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using WebAPI.Application.DataTransferObjects.Employee;

namespace WebAPI.Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();
            var service = new EmployeeService(configuration["apiEmployeeDomain"]);

            // Get employees.
            var employees = await service.GetEmployees("FirstName");
            foreach (var employee in employees)
                Console.WriteLine(service.EmployeeToString(employee));

            // Create employee.
            var employeeToAdd = new PostEmployeeDto
            { FirstName = "ClientFN3", LastName = "ClientLN3", IsHigherEducation = true };
            Console.WriteLine(await service.AddEmployee(employeeToAdd));

            //Update employee.
            const int id = 10;
            var employeeToUpdate = new PostEmployeeDto
            { FirstName = "ClientUpdateFN1", LastName = "ClientUpdateLN2", IsHigherEducation = false };
            await service.UpdateEmployee(id, employeeToUpdate);

            //Get employee by id
            const int idToFind = 10;
            Console.WriteLine(await service.GetEmployee(idToFind));

            //Delete employee by id
            const int employeeToDelete = 10;
            await service.DeleteEmployee(employeeToDelete);
        }
    }
}
