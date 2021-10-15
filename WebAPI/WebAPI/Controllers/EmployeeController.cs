using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Application.DataTransferObjects.Employee;
using WebAPI.Application.Interfaces;
using WebAPI.Domain.Aggregates.EmployeeAggregate;

namespace WebAPI.Controllers
{
    [Route("api/employees")] 
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(int id, CancellationToken cancellationToken)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id, cancellationToken);
            return employee != null ? Ok(employee) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployeeAsync([FromBody] PostEmployeeDto employee, CancellationToken cancellationToken)
        {
            if (employee == null)
                return BadRequest();

            var id = await _employeeService.CreateEmployeeAsync(employee, cancellationToken);
            return CreatedAtAction(nameof(GetEmployeeById),  new { id }, employee);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] PostEmployeeDto employee, CancellationToken cancellationToken)
        {
            if (employee == null)
                return BadRequest();

            var model = await _employeeService.GetEmployeeByIdAsync(id, cancellationToken);
            if (model == null)
                return NotFound();

            await _employeeService.UpdateEmployeeAsync(id, employee, cancellationToken);
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var model = await _employeeService.GetEmployeeByIdAsync(id, cancellationToken);
            if (model == null)
                return NotFound();

            await _employeeService.DeleteEmployeeAsync(id, cancellationToken);
            return new NoContentResult();
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployees([FromQuery] EmployeeFiltering filtering, string orderBy, string fieldOrder, CancellationToken cancellationToken)
        {
            return Ok(await _employeeService.GetAllEmployeesAsync(filtering, orderBy, fieldOrder, cancellationToken));
        }
    }
}
