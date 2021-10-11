using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Application.DataTransferObjects;
using WebAPI.Application.Interfaces;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<IEnumerable<GetEmployeeDto>> GetEmployeesAsync()
        {
            return await _employeeService.GetAllEmployeesAsync();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            return employee != null ? Ok(employee) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployeeAsync([FromBody] PostEmployeeDto employee)
        {
            if (employee == null)
                return BadRequest();

            var id = await _employeeService.CreateEmployeeAsync(employee);
            return CreatedAtAction(nameof(GetEmployeeById),  new { id }, employee);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] PostEmployeeDto employee)
        {
            if (employee == null)
                return BadRequest();

            var model = await _employeeService.GetEmployeeByIdAsync(id);
            if (model == null)
                return NotFound();

            await _employeeService.UpdateEmployeeAsync(id, employee);
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var model = await _employeeService.GetEmployeeByIdAsync(id);
            if (model == null)
                return NotFound();

            await _employeeService.DeleteEmployeeAsync(id);
            return new NoContentResult();
        }

        [HttpGet("employees")]
        public async Task<IActionResult> GetEmployees([FromQuery] EmployeeFiltering filtering, string orderBy = OrderingConstants.AscendingOrder, string fieldOrder = FieldsOrderBy.None)
        {
            return filtering != null ? Ok(await _employeeService.FilteringAndOrderByAsync(filtering, orderBy, fieldOrder)) : BadRequest();
        }
    }
}
