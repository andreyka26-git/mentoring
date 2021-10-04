using Microsoft.AspNetCore.Mvc;
using WebAPI.BusinessLogic.Interfaces;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly IMainService _mainService;
        private readonly IEmployeeService _employeeService;

        public MainController(IMainService mainService, IEmployeeService employeeService)
        {
            _mainService = mainService;
            _employeeService = employeeService;
        }

        [HttpPost("assign")]
        public ActionResult AssignToProject(int id, string name)
        {
            if (string.IsNullOrEmpty(name))
                return BadRequest();

            var model = _employeeService.GetEmployeeById(id);
            if (model == null)
                return BadRequest();

            var isSuccessful = _mainService.AssignToProject(id, name);
            return isSuccessful ? Ok() : BadRequest();
        }

        [HttpPost("unassign")]
        public ActionResult UnAssignFromProject(int id, string name)
        {
            if (string.IsNullOrEmpty(name))
                return BadRequest();

            var model = _employeeService.GetEmployeeById(id);
            if (model == null)
                return BadRequest();

            var isSuccessful = _mainService.UnAssignFromProject(id, name);
            return isSuccessful ? Ok() : BadRequest();
        }

        [HttpGet("composition")]
        public ActionResult GetProjectsComposition(string name)
        {
            if (string.IsNullOrEmpty(name))
                return BadRequest();

            var composition = _mainService.GetProjectComposition(name);
            return composition != null ? Ok(composition) : NotFound();
        }
    }
}
