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
    public class ProjectService : IProjectService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProjectService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public void CreateProject(ProjectDto project)
        {
            var entity = _mapper.Map<Project>(project);
            _unitOfWork.Projects.Create(entity);
            _unitOfWork.Save();
        }

        public void DeleteProject(int id)
        {
            _unitOfWork.Projects.Delete(id);
            _unitOfWork.Save();
        }

        public IEnumerable<ProjectDto> GetAllProjects()
        {
            var projects = _unitOfWork.Projects.GetAll();
            return _mapper.Map<IEnumerable<ProjectDto>>(projects);
        }

        public ProjectDto GetProjectById(int id)
        {
            var project = _unitOfWork.Projects.Get(id);
            return project != null ? _mapper.Map<ProjectDto>(project) : null;
        }

        public int? GetProjectId(ProjectDto project)
        {
            return _unitOfWork.Projects
                .Find(p => p.Name.Equals(project.Name, StringComparison.CurrentCultureIgnoreCase))
                .FirstOrDefault()?.Id;
        }

        public void UpdateProject(int id, ProjectDto project)
        {
            var entity = _mapper.Map<Project>(project);
            entity.Id = id;
            _unitOfWork.Projects.Update(entity);
            _unitOfWork.Save();
        }
    }
}
