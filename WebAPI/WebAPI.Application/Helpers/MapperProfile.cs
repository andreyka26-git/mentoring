using AutoMapper;
using WebAPI.Application.DataTransferObjects;
using WebAPI.Domain.Aggregates.EmployeeAggregate;
using WebAPI.Domain.Aggregates.ProjectAggregate;

namespace WebAPI.Application.Helpers
{
    public class MapperProfile: Profile
    {
        public MapperProfile()
        {
            // Employee mapping.
            CreateMap<Employee, GetEmployeeDto>();
            CreateMap<GetEmployeeDto, Employee>();
            CreateMap<PostEmployeeDto, Employee>();

            // Project mapping.
            CreateMap<Project, GetProjectDto>();
            CreateMap<GetProjectDto, Project>();
            CreateMap<PostProjectDto, Project>();
            CreateMap<Project, PostProjectDto>();
        }
    }
}
