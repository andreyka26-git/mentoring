using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Application.DataTransferObjects;
using WebAPI.Application.Interfaces;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;
        private readonly IEmployeeService _employeeService;

        public ProjectController(IProjectService projectService, IEmployeeService employeeService)
        {
            _projectService = projectService;
            _employeeService = employeeService;
        }

        [HttpGet]
        public IEnumerable<ProjectGetDto> GetProjects()
        {
            return _projectService.GetAllProjects();
        }

        [HttpGet("{id}")]
        public IActionResult GetProject(int id)
        {
            var project = _projectService.GetProjectById(id);
            return project != null ? Ok(project) : NotFound();
        }

        [HttpPost]
        public IActionResult AddProject([FromBody] ProjectPostDto project)
        {
            if (project == null)
                return BadRequest();

            var id = _projectService.CreateProject(project);
            return CreatedAtAction(nameof(GetProject), new { id }, project);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] ProjectPostDto project)
        {
            if (project == null)
                return BadRequest();

            var model = _projectService.GetProjectById(id);
            if (model == null)
                return NotFound();

            _projectService.UpdateProject(id, project);
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var model = _projectService.GetProjectById(id);
            if (model == null)
                return NotFound();

            _projectService.DeleteProject(id);
            return new NoContentResult();
        }

        [HttpPost("assign")]
        public ActionResult AssignToProject(int id, string name)
        {
            if (string.IsNullOrEmpty(name))
                return BadRequest();

            var model = _employeeService.GetEmployeeById(id);
            if (model == null)
                return BadRequest();

            var isSuccessful = _projectService.AssignToProject(id, name);
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

            var isSuccessful = _projectService.UnAssignFromProject(id, name);
            return isSuccessful ? Ok() : BadRequest();
        }

        [HttpGet("composition")]
        public ActionResult GetProjectsComposition(string name)
        {
            if (string.IsNullOrEmpty(name))
                return BadRequest();

            var composition = _projectService.GetProjectComposition(name);
            return composition != null ? Ok(composition) : NotFound();
        }
    }
}
