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
            { FirstName = "ClientNew5", LastName = "ClientNew5", IsHigherEducation = true };
            Console.WriteLine(await service.AddEmployee(employeeToAdd));

            // Update employee.
            const int id = 6;
            var employeeToUpdate = new PostEmployeeDto
            { FirstName = "NewUpdate", LastName = "NewUpdate", IsHigherEducation = false };
            await service.UpdateEmployee(id, employeeToUpdate);

            //Get employee by id
            const int idToFind = 6;
            Console.WriteLine(await service.GetEmployee(idToFind));

            //Delete employee by id
            const int employeeToDelete = 6;
            await service.DeleteEmployee(employeeToDelete);

            // Get employees.
            employees = await service.GetEmployees("FirstName");
            foreach (var employee in employees)
                Console.WriteLine(service.EmployeeToString(employee));
        }
    }
}
