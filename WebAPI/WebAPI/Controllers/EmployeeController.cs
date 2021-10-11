using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Application.DataTransferObjects;
using WebAPI.Application.Interfaces;

namespace WebAPI.Controllers
{
    //TODO change to explicit "api/employees"

    //TODO change order by to be ID ASC by default in whole project

    //TODO if you have one line statement with Task return type - don't use await

    //TODO add cancellationToken whenever possible
    [Route("api/[controller]")] 
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            return employee != null ? Ok(employee) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployeeAsync([FromBody] PostEmployeeDto employee, CancellationToken cancellationToken)
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

        //TODO implement paging
        //int page / int limit
        //dbSet.Skip().Take();
        //
        //
        //
        //

        //TODO drop name from here
        [HttpGet("employees")]
        public async Task<IActionResult> GetEmployees([FromQuery] EmployeeFiltering filtering, string orderBy = OrderingConstants.AscendingOrder, string fieldOrder = FieldsOrderBy.None)
        {
            //TODO you don't need that if
            return filtering != null ? Ok(await _employeeService.FilteringAndOrderByAsync(filtering, orderBy, fieldOrder)) : BadRequest();
        }
    }
}
