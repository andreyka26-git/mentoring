using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebAPI.Application.DataTransferObjects;
using WebAPI.Application.Interfaces;
using WebAPI.Domain.Aggregates.EmployeeAggregate;
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

        public int CreateProject(ProjectPostDto project)
        {
            var entity = _mapper.Map<Project>(project);
            _unitOfWork.Projects.Create(entity);
            _unitOfWork.Save();
            return entity.Id;
        }

        public void DeleteProject(int id)
        {
            _unitOfWork.Projects.Delete(id);
            _unitOfWork.Save();
        }

        public IEnumerable<ProjectGetDto> GetAllProjects()
        {
            var projects = _unitOfWork.Projects.GetAll();
            return _mapper.Map<IEnumerable<ProjectGetDto>>(projects);
        }

        public ProjectGetDto GetProjectById(int id)
        {
            var project = _unitOfWork.Projects.Get(id);
            return project != null ? _mapper.Map<ProjectGetDto>(project) : null;
        }

        public void UpdateProject(int id, ProjectPostDto project)
        {
            var entity = _mapper.Map<Project>(project);
            entity.Id = id;
            _unitOfWork.Projects.Update(entity);
            _unitOfWork.Save();
        }

        public bool AssignToProject(int id, string name)
        {
            var project =
                _unitOfWork.Projects.Find(p => p.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            if (project == null)
                return false;

            var employee = _unitOfWork.Employees.Get(id);
            project.Employees ??= new List<Employee>();
            project.Employees.Add(employee);

            _unitOfWork.Projects.Update(project);
            _unitOfWork.Save();

            return true;
        }

        public ProjectCompositionDto GetProjectComposition(string name)
        {
            var project =
                _unitOfWork.Projects.Find(p => p.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            if (project == null)
                return null;

            var composition = new ProjectCompositionDto
            {
                Project = _mapper.Map<ProjectPostDto>(project),
                Employees = _mapper.Map<IEnumerable<EmployeeGetDto>>(project.Employees)
            };

            return composition;
        }

        public bool UnAssignFromProject(int id, string name)
        {
            var project =
                _unitOfWork.Projects.Find(p => p.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            if (project?.Employees == null)
                return false;

            var employee = _unitOfWork.Employees.Get(id);
            project.Employees.RemoveAll(s => s.Id == employee.Id);

            _unitOfWork.Projects.Update(project);
            _unitOfWork.Save();
            return true;

        }
    }
}
