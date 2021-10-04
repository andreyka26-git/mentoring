using AutoMapper;
using WebAPI.BusinessLogic.DataTransferObjects;
using WebAPI.Domain.Entities;

namespace WebAPI.BusinessLogic.Helpers
{
    public class MapperProfile: Profile
    {
        public MapperProfile()
        {
            CreateMap<Employee, EmployeeDto>();
            CreateMap<EmployeeDto, Employee>();
            CreateMap<Project, ProjectDto>();
            CreateMap<ProjectDto, Project>();
        }
    }
}
