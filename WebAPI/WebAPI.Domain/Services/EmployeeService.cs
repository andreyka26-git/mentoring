using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Application.DataTransferObjects;
using WebAPI.Application.Interfaces;
using WebAPI.Domain.Aggregates.EmployeeAggregate;

namespace WebAPI.Infrastructure.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<int> CreateEmployeeAsync(PostEmployeeDto employee)
        {
            var entity = _mapper.Map<Employee>(employee);
            await _unitOfWork.Employees.CreateAsync(entity);
            await _unitOfWork.SaveAsync();
            return entity.Id;
        }

        public async Task DeleteEmployeeAsync(int id)
        {
            await _unitOfWork.Employees.DeleteAsync(id);
            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<GetEmployeeDto>> FilteringAndOrderByAsync(EmployeeFiltering filter, string orderBy, string fieldOrder)
        {
            var employees = _mapper.Map<IEnumerable<GetEmployeeDto>>(await _unitOfWork.Employees.GetAllAsync());

            if (!string.IsNullOrEmpty(filter.FirstName))
                employees = employees.Where(e =>
                    e.FirstName.Equals(filter.FirstName, StringComparison.CurrentCultureIgnoreCase));

            if (!string.IsNullOrEmpty(filter.LastName))
                employees = employees.Where(e =>
                    e.LastName.Equals(filter.FirstName, StringComparison.CurrentCultureIgnoreCase));

            employees = employees.Where(p => p.IsHigherEducation = filter.IsHigherEducation);

            return orderBy switch
            {
                OrderingConstants.AscendingOrder => OrderByAscending(employees, fieldOrder),
                OrderingConstants.DescendingOrder => OrderByDescending(employees, fieldOrder),
                _ => employees
            };
        }

        public async Task<IEnumerable<GetEmployeeDto>> GetAllEmployeesAsync()
        {
            var employees = await _unitOfWork.Employees.GetAllAsync();
            return _mapper.Map<IEnumerable<GetEmployeeDto>>(employees);
        }

        public async Task<GetEmployeeDto> GetEmployeeByIdAsync(int id)
        {
            var employee = await _unitOfWork.Employees.GetAsync(id);
            return employee != null ? _mapper.Map<GetEmployeeDto>(employee) : null;
        }

        public async Task UpdateEmployeeAsync(int id, PostEmployeeDto employee)
        {
            var entity = _mapper.Map<Employee>(employee);
            entity.Id = id;
            _unitOfWork.Employees.Update(entity);
            await _unitOfWork.SaveAsync();
        }

        private static IEnumerable<GetEmployeeDto> OrderByAscending(IEnumerable<GetEmployeeDto> employees, string fieldOrder)
        {
            return fieldOrder switch
            {
                FieldsOrderBy.FirstName => employees.OrderBy(e => e.FirstName),
                FieldsOrderBy.LastName => employees.OrderBy(e => e.FirstName),
                FieldsOrderBy.IsEducated => employees.OrderBy(e => e.IsHigherEducation),
                FieldsOrderBy.None => employees,
                _ => employees
            };
        }

        private static IEnumerable<GetEmployeeDto> OrderByDescending(IEnumerable<GetEmployeeDto> employees, string fieldOrder)
        {
            return fieldOrder switch
            {
                FieldsOrderBy.FirstName => employees.OrderByDescending(e => e.FirstName),
                FieldsOrderBy.LastName => employees.OrderByDescending(e => e.FirstName),
                FieldsOrderBy.IsEducated => employees.OrderByDescending(e => e.IsHigherEducation),
                FieldsOrderBy.None => employees,
                _ => employees
            };
        }
    }
}