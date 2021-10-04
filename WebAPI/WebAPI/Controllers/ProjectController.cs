using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebAPI.BusinessLogic.DataTransferObjects;
using WebAPI.BusinessLogic.Interfaces;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet]
        public IEnumerable<ProjectDto> GetProjects()
        {
            return _projectService.GetAllProjects();
        }

        [HttpGet("{id}")]
        public IActionResult GetEmployeeById(int id)
        {
            var project = _projectService.GetProjectById(id);
            return project != null ? Ok(project) : NotFound();
        }

        [HttpGet("Id")]
        public IActionResult GetProjectId(string name)
        {
            if (string.IsNullOrEmpty(name))
                return BadRequest();

            var project = new ProjectDto { Name = name };
            var id = _projectService.GetProjectId(project);
            return id == null ? NotFound() : Ok(id);
        }

        [HttpPost]
        public IActionResult AddProject([FromBody] ProjectDto project)
        {
            if (project == null)
                return BadRequest();

            _projectService.CreateProject(project);
            var id = _projectService.GetProjectId(project);
            return CreatedAtAction(nameof(GetProjectId), new { id }, project);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] ProjectDto project)
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
    }
}
