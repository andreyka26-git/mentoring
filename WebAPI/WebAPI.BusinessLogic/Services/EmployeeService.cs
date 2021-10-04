using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using WebAPI.BusinessLogic.DataTransferObjects;
using WebAPI.BusinessLogic.Helpers;
using WebAPI.BusinessLogic.Interfaces;
using WebAPI.Domain.Entities;
using WebAPI.Domain.Interfaces;

namespace WebAPI.BusinessLogic.Services
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

        public void CreateEmployee(EmployeeDto employee)
        {
            var entity = _mapper.Map<Employee>(employee);
            _unitOfWork.Employees.Create(entity);
            _unitOfWork.Save();
        }

        public void DeleteEmployee(int id)
        {
            _unitOfWork.Employees.Delete(id);
            _unitOfWork.Save();
        }

        public IEnumerable<EmployeeDto> FilteringAndOrderBy(EmployeeFiltering filter, string orderBy, string fieldOrder)
        {
            var employees =  _mapper.Map<IEnumerable<EmployeeDto>>(_unitOfWork.Employees.GetAll());

            if (!string.IsNullOrEmpty(filter.FirstName))
                employees = employees.Where(e =>
                    e.FirstName.Equals(filter.FirstName, StringComparison.CurrentCultureIgnoreCase));

            if (!string.IsNullOrEmpty(filter.LastName))
                employees = employees.Where(e =>
                    e.LastName.Equals(filter.FirstName, StringComparison.CurrentCultureIgnoreCase));

            employees = employees.Where(p => p.IsHigherEducation = filter.IsHigherEducation);

            return orderBy switch
            {
                OrderingConstants.AscendingOrder => employees = OrderByAscending(employees, fieldOrder),
                OrderingConstants.DescendingOrder => employees = OrderByDescending(employees, fieldOrder),
                _ => employees
            };
        }

        public IEnumerable<EmployeeDto> GetAllEmployees()
        {
            var employees = _unitOfWork.Employees.GetAll();
            return _mapper.Map<IEnumerable<EmployeeDto>>(employees);
        }

        public EmployeeDto GetEmployeeById(int id)
        {
            var employee = _unitOfWork.Employees.Get(id);
            return employee != null ? _mapper.Map<EmployeeDto>(employee) : null;
        }

        public int? GetEmployeeId(EmployeeDto employee)
        {
            return _unitOfWork.Employees.Find(e =>
                    e.FirstName.Equals(employee.FirstName, StringComparison.CurrentCultureIgnoreCase) &&
                    e.LastName.Equals(employee.LastName, StringComparison.CurrentCultureIgnoreCase))
                .FirstOrDefault()?.Id;
        }

        public void UpdateEmployee(int id, EmployeeDto employee)
        {
            var entity = _mapper.Map<Employee>(employee);
            entity.Id = id;
            _unitOfWork.Employees.Update(entity);
            _unitOfWork.Save();
        }

        private static IEnumerable<EmployeeDto> OrderByAscending(IEnumerable<EmployeeDto> employees, string fieldOrder)
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

        private static IEnumerable<EmployeeDto> OrderByDescending(IEnumerable<EmployeeDto> employees, string fieldOrder)
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
