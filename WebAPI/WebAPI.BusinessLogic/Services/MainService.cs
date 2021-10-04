using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebAPI.BusinessLogic.DataTransferObjects;
using WebAPI.BusinessLogic.Interfaces;
using WebAPI.Domain.Entities;
using WebAPI.Domain.Interfaces;

namespace WebAPI.BusinessLogic.Services
{
    public class MainService : IMainService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IEmployeeService _employeeService;
        private readonly IProjectService _projectService;

        public MainService(IUnitOfWork unitOfWork, IMapper mapper, IEmployeeService employeeService, IProjectService projectService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _employeeService = employeeService;
            _projectService = projectService;
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
                Project = _mapper.Map<ProjectDto>(project),
                Employees = _mapper.Map<IEnumerable<EmployeeDto>>(project.Employees)
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
