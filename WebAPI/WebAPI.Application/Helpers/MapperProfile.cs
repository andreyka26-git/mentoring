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
            CreateMap<Employee, EmployeeGetDto>();
            CreateMap<EmployeeGetDto, Employee>();
            CreateMap<EmployeePostDto, Employee>();

            // Project mapping.
            CreateMap<Project, ProjectGetDto>();
            CreateMap<ProjectGetDto, Project>();
            CreateMap<ProjectPostDto, Project>();
        }
    }
}
