using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAPI.Domain.Aggregates.Common;
using WebAPI.Domain.Aggregates.EmployeeAggregate;

namespace WebAPI.Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DataContext _db;

        public EmployeeRepository(DataContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Employee>> GetAllAsync(CancellationToken token, EmployeeFiltering filter, string orderBy, string fieldOrder)
        {
            IEnumerable<Employee> employees = await _db.Employees
                .Skip((filter.PagingModel.PageNumber - 1) * filter.PagingModel.PageSize)
                .Take(filter.PagingModel.PageSize).ToListAsync(token);

            if (!string.IsNullOrEmpty(filter.FirstName))
                employees = employees.Where(e =>
                    e.FirstName.Equals(filter.FirstName, StringComparison.CurrentCultureIgnoreCase));

            if (!string.IsNullOrEmpty(filter.LastName))
                employees = employees.Where(e =>
                    e.LastName.Equals(filter.FirstName, StringComparison.CurrentCultureIgnoreCase));

            if (filter.IsHigherEducation.HasValue)
                employees = employees.Where(p => p.IsHigherEducation == filter.IsHigherEducation.Value);

            return orderBy switch
            {
                OrderingConstants.AscendingOrder => OrderByAscending(employees, fieldOrder),
                OrderingConstants.DescendingOrder => OrderByDescending(employees, fieldOrder),
                _ => OrderByAscending(employees, fieldOrder)
            };
        }

        public Task<Employee> GetAsync(int id, CancellationToken token)
        {
            return _db.Employees.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id, token);
        }

        public IEnumerable<Employee> Find(int? id, int? projectId, string fName, string lName, bool? isHigherEducation)
        {
            IEnumerable<Employee> result = _db.Employees;

            if (!string.IsNullOrEmpty(fName))
                result = result.Where(e => e.FirstName == fName);
            if (!string.IsNullOrEmpty(lName))
                result = result.Where(e => e.LastName == lName);
            if (isHigherEducation.HasValue)
                result = result.Where(e => e.IsHigherEducation == isHigherEducation.Value);
            if (id.HasValue)
                result = result.Where(e => e.Id == id.Value);
            if (projectId.HasValue)
                result = result.Where(e => e.ProjectId == projectId);

            return result;
        }

        public async Task CreateAsync(Employee item, CancellationToken token)
        {
            await _db.Employees.AddAsync(item, token);
        }

        public void Update(Employee item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }

        public async Task DeleteAsync(int id, CancellationToken token)
        {
            var employee = await _db.Employees.FindAsync(id, token);
            if (employee != null)
                _db.Employees.Remove(employee);
        }

        private static IEnumerable<Employee> OrderByAscending(IEnumerable<Employee> employees, string fieldOrder)
        {
            return fieldOrder switch
            {
                FieldsOrderBy.FirstName => employees.OrderBy(e => e.FirstName),
                FieldsOrderBy.LastName => employees.OrderBy(e => e.FirstName),
                FieldsOrderBy.IsEducated => employees.OrderBy(e => e.IsHigherEducation),
                _ => employees.OrderBy(e => e.Id)
            };
        }

        private static IEnumerable<Employee> OrderByDescending(IEnumerable<Employee> employees, string fieldOrder)
        {
            return fieldOrder switch
            {
                FieldsOrderBy.FirstName => employees.OrderByDescending(e => e.FirstName),
                FieldsOrderBy.LastName => employees.OrderByDescending(e => e.FirstName),
                FieldsOrderBy.IsEducated => employees.OrderByDescending(e => e.IsHigherEducation),
                _ => employees.OrderByDescending(e => e.Id)
            };
        }
    }
}
