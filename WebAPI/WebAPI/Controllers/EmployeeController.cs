using System.Collections.Generic;
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
        public IEnumerable<EmployeeGetDto> GetEmployees()
        {
            return _employeeService.GetAllEmployees();
        }

        [HttpGet("{id}")]
        public IActionResult GetEmployeeById(int id)
        {
            var employee = _employeeService.GetEmployeeById(id);
            return employee != null ? Ok(employee) : NotFound();
        }

        [HttpPost]
        public IActionResult AddEmployee([FromBody] EmployeePostDto employee)
        {
            if (employee == null)
                return BadRequest();

            var id = _employeeService.CreateEmployee(employee);
            return CreatedAtAction(nameof(GetEmployeeById), new { id }, employee);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] EmployeePostDto employee)
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
