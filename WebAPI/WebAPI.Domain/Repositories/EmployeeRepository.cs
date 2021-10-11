using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _db.Employees.AsNoTracking().ToListAsync();
        }

        public async Task<Employee> GetAsync(int id)
        {
            return await _db.Employees.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
        }

        public IQueryable<Employee> Find(int? id, int? projectId, string fName, string lName, bool? isHigherEducation)
        {
            IQueryable<Employee> result = _db.Employees;

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

        public async Task CreateAsync(Employee item)
        {
            await _db.Employees.AddAsync(item);
        }

        public void Update(Employee item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }

        public async Task DeleteAsync(int id)
        {
            var employee = await _db.Employees.FindAsync(id);
            if (employee != null)
                _db.Employees.Remove(employee);
        }
    }
}
