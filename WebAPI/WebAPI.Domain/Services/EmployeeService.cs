using AutoMapper;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebAPI.Application.DataTransferObjects.Employee;
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

        public async Task<int> CreateEmployeeAsync(PostEmployeeDto employee, CancellationToken token)
        {
            var entity = _mapper.Map<Employee>(employee);
            await _unitOfWork.Employees.CreateAsync(entity, token);
            await _unitOfWork.SaveAsync(token);
            return entity.Id;
        }

        public async Task DeleteEmployeeAsync(int id, CancellationToken token)
        {
            await _unitOfWork.Employees.DeleteAsync(id, token);
            await _unitOfWork.SaveAsync(token);
        }

        public async Task<IEnumerable<GetEmployeeDto>> GetAllEmployeesAsync(EmployeeFiltering filter, string orderBy, string fieldOrder, CancellationToken token)
        {
            return _mapper.Map<IEnumerable<GetEmployeeDto>>(await _unitOfWork.Employees.GetAllAsync(token, filter, orderBy, fieldOrder));
        }

        public async Task<GetEmployeeDto> GetEmployeeByIdAsync(int id, CancellationToken token)
        {
            var employee = await _unitOfWork.Employees.GetAsync(id, token);
            return employee != null ? _mapper.Map<GetEmployeeDto>(employee) : null;
        }

        public async Task UpdateEmployeeAsync(int id, PostEmployeeDto employee, CancellationToken token)
        {
            var entity = await _unitOfWork.Employees.GetAsync(id, token);
            var model = new Employee(entity.Id, employee.FirstName, employee.LastName, employee.IsHigherEducation, entity.ProjectId);
            _unitOfWork.Employees.Update(model);
            await _unitOfWork.SaveAsync(token);
        }
    }
}