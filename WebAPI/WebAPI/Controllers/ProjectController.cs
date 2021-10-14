using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Application.DataTransferObjects;
using WebAPI.Application.DataTransferObjects.Project;
using WebAPI.Application.Interfaces;

namespace WebAPI.Controllers
{
    [Route("api/projects")]
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
        public async Task<IEnumerable<GetProjectDto>> GetProjectsAsync(CancellationToken cancellationToken)
        {
            return await _projectService.GetAllProjectsAsync(cancellationToken);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProject(int id, CancellationToken cancellationToken)
        {
            var project = await _projectService.GetProjectByIdAsync(id, cancellationToken);
            return project != null ? Ok(project) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> AddProjectAsync([FromBody] PostProjectDto project, CancellationToken cancellationToken)
        {
            if (project == null)
                return BadRequest();

            var id = await _projectService.CreateProjectAsync(project, cancellationToken);
            return CreatedAtAction(nameof(GetProject), new { id }, project);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PostProjectDto project, CancellationToken cancellationToken)
        {
            if (project == null)
                return BadRequest();

            var model = _projectService.GetProjectByIdAsync(id, cancellationToken);
            if (model == null)
                return NotFound();

            await _projectService.UpdateProjectAsync(id, project, cancellationToken);
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var model = await _projectService.GetProjectByIdAsync(id, cancellationToken);
            if (model == null)
                return NotFound();

            await _projectService.DeleteProjectAsync(id, cancellationToken);
            return new NoContentResult();
        }

        [HttpPost("assign")]
        public async Task<ActionResult> AssignToProjectAsync(int id, string name, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(name))
                return BadRequest();

            var model = await _employeeService.GetEmployeeByIdAsync(id, cancellationToken);
            if (model == null)
                return BadRequest();

            var isSuccessful = await _projectService.AssignToProjectAsync(id, name, cancellationToken);
            return isSuccessful ? Ok() : BadRequest();
        }

        [HttpPost("unassign")]
        public async Task<ActionResult> UnAssignFromProjectAsync(int id, string name, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(name))
                return BadRequest();

            var model = await _employeeService.GetEmployeeByIdAsync(id, cancellationToken);
            if (model == null)
                return BadRequest();

            var isSuccessful = await _projectService.UnAssignFromProjectAsync(id, name, cancellationToken);
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
