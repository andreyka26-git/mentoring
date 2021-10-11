using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using WebAPI.Application.DataTransferObjects;
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

        public async Task<int> CreateProjectAsync(PostProjectDto project)
        {
            var entity = _mapper.Map<Project>(project);
            await _unitOfWork.Projects.CreateAsync(entity);
            await _unitOfWork.SaveAsync();
            return entity.Id;
        }

        public async Task DeleteProjectAsync(int id)
        {
            await _unitOfWork.Projects.DeleteAsync(id);
            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<GetProjectDto>> GetAllProjectsAsync()
        {
            var projects = await _unitOfWork.Projects.GetAllAsync();
            return _mapper.Map<IEnumerable<GetProjectDto>>(projects);
        }

        public async Task<GetProjectDto> GetProjectByIdAsync(int id)
        {
            var project = await _unitOfWork.Projects.GetAsync(id);
            return project != null ? _mapper.Map<GetProjectDto>(project) : null;
        }

        public async Task UpdateProjectAsync(int id, PostProjectDto project)
        {
            var entity = _mapper.Map<Project>(project);
            entity.Id = id;
            _unitOfWork.Projects.Update(entity);
            await _unitOfWork.SaveAsync();
        }

        public async Task<bool> AssignToProjectAsync(int id, string name)
        {
            var project =
                _unitOfWork.Projects.Find(name: name).FirstOrDefault();
            if (project == null)
                return false;

            var employee = await _unitOfWork.Employees.GetAsync(id);
            employee.ProjectId = project.Id;

            _unitOfWork.Employees.Update(employee);
            await _unitOfWork.SaveAsync();

            return true;
        }

        public ProjectCompositionDto GetProjectComposition(string name)
        {
            var project =
                _unitOfWork.Projects.Find(name: name)
                    .FirstOrDefault();
            if (project == null)
                return null;

            var composition = new ProjectCompositionDto
            {
                Project = _mapper.Map<PostProjectDto>(project),
                Employees = _mapper.Map<IEnumerable<GetEmployeeDto>>(_unitOfWork.Employees.Find(projectId: project.Id))
            };

            return composition;
        }

        public async Task<bool> UnAssignFromProjectAsync(int id, string name)
        {
            var project =
                _unitOfWork.Projects.Find(name: name).FirstOrDefault();
            if (project == null)
                return false;

            var employee = await _unitOfWork.Employees.GetAsync(id);
            employee.ProjectId = null;

            _unitOfWork.Employees.Update(employee);
            await _unitOfWork.SaveAsync();
            return true;
        }
    }
}
