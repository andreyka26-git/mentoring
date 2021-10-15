using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using WebAPI.Application.DataTransferObjects.Employee;
using WebAPI.Application.DataTransferObjects.Project;
using WebAPI.Application.Interfaces;
using WebAPI.Domain.Aggregates.ProjectAggregate;

namespace WebAPI.Infrastructure.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProjectService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<int> CreateProjectAsync(PostProjectDto project, CancellationToken token)
        {
            var entity = _mapper.Map<Project>(project);
            await _unitOfWork.Projects.CreateAsync(entity, token);
            await _unitOfWork.SaveAsync(token);
            return entity.Id;
        }

        public async Task DeleteProjectAsync(int id, CancellationToken token)
        {
            await _unitOfWork.Projects.DeleteAsync(id, token);
            await _unitOfWork.SaveAsync(token);
        }

        public async Task<IEnumerable<GetProjectDto>> GetAllProjectsAsync(CancellationToken token)
        {
            var projects = await _unitOfWork.Projects.GetAllAsync(token);
            return _mapper.Map<IEnumerable<GetProjectDto>>(projects);
        }

        public async Task<GetProjectDto> GetProjectByIdAsync(int id, CancellationToken token)
        {
            var project = await _unitOfWork.Projects.GetAsync(id, token);
            return project != null ? _mapper.Map<GetProjectDto>(project) : null;
        }

        public async Task UpdateProjectAsync(int id, PostProjectDto project, CancellationToken token)
        {
            var entity = await _unitOfWork.Projects.GetAsync(id, token);
            var model = new Project(entity.Id, project.Name, project.Duration);
            _unitOfWork.Projects.Update(model);
            await _unitOfWork.SaveAsync(token);
        }

        public async Task<bool> AssignToProjectAsync(int id, string name, CancellationToken token)
        {
            var project = (await _unitOfWork.Projects.FindAsync(token, name: name)).FirstOrDefault();
            if (project == null)
                return false;

            var employee = await _unitOfWork.Employees.GetAsync(id, token);
            employee.ProjectId = project.Id;

            _unitOfWork.Employees.Update(employee);
            await _unitOfWork.SaveAsync(token);

            return true;
        }

        public async Task<ProjectCompositionDto> GetProjectCompositionAsync(string name, CancellationToken token)
        {
            var project = (await _unitOfWork.Projects.FindAsync(token, name: name)).FirstOrDefault();
            if (project == null)
                return null;

            var composition = new ProjectCompositionDto
            {
                Project = _mapper.Map<PostProjectDto>(project),
                Employees = _mapper.Map<IEnumerable<GetEmployeeDto>>(await _unitOfWork.Employees.FindAsync(token, projectId: project.Id))
            };

            return composition;
        }

        public async Task<bool> UnAssignFromProjectAsync(int id, string name, CancellationToken token)
        {
            var project = (await _unitOfWork.Projects.FindAsync(token, name: name)).FirstOrDefault();
            if (project == null)
                return false;

            var employee = await _unitOfWork.Employees.GetAsync(id, token);
            employee.ProjectId = null;

            _unitOfWork.Employees.Update(employee);
            await _unitOfWork.SaveAsync(token);
            return true;
        }
    }
}
