using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebAPI.BusinessLogic.DataTransferObjects;
using WebAPI.BusinessLogic.Helpers;
using WebAPI.BusinessLogic.Interfaces;

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
        public IEnumerable<EmployeeDto> GetEmployees()
        {
            return _employeeService.GetAllEmployees();
        }

        [HttpGet("{id}")]
        public IActionResult GetEmployeeById(int id)
        {
            var employee = _employeeService.GetEmployeeById(id);
            return employee != null ? Ok(employee) : NotFound();
        }

        [HttpGet("Id")]
        public IActionResult GetEmployeeId(string firstName, string lastName)
        {
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
                return BadRequest();

            var employee = new EmployeeDto { FirstName = firstName, LastName = lastName };
            var id = _employeeService.GetEmployeeId(employee);
            return id == null ? NotFound() : Ok(id);
        }

        [HttpPost]
        public IActionResult AddEmployee([FromBody] EmployeeDto employee)
        {
            if (employee == null)
                return BadRequest();

            _employeeService.CreateEmployee(employee);
            var id = _employeeService.GetEmployeeId(employee);
            return CreatedAtAction(nameof(GetEmployeeById), new { id }, employee);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] EmployeeDto employee)
        {
            if (employee == null)
                return BadRequest();

            var model = _employeeService.GetEmployeeById(id);
            if (model == null)
                return NotFound();

            _employeeService.UpdateEmployee(id, employee);
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var model = _employeeService.GetEmployeeById(id);
            if (model == null)
                return NotFound();

            _employeeService.DeleteEmployee(id);
            return new NoContentResult();
        }

        [HttpGet("filter")]
        public IActionResult GetEmployees([FromQuery] EmployeeFiltering filtering, string orderBy = OrderingConstants.AscendingOrder, string fieldOrder = FieldsOrderBy.None)
        {
            return filtering != null ? Ok(_employeeService.FilteringAndOrderBy(filtering, orderBy, fieldOrder)) : BadRequest();
        }
    }
}
