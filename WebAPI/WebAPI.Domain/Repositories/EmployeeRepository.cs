using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebAPI.Domain.Aggregates;
using WebAPI.Domain.Aggregates.EmployeeAggregate;

namespace WebAPI.Infrastructure.Repositories
{
    public class EmployeeRepository: IRepository<Employee>
    {
        private readonly DataContext _db;

        public EmployeeRepository(DataContext db)
        {
            _db = db;
        }

        public IEnumerable<Employee> GetAll()
        {
            return _db.Employees.AsNoTracking();
        }

        public Employee Get(int id)
        {
            return _db.Employees.AsNoTracking().FirstOrDefault(e => e.Id == id);
        }

        public IEnumerable<Employee> Find(Func<Employee, bool> predicate)
        {
            return _db.Employees.Where(predicate);
        }

        public void Create(Employee item)
        {
            _db.Employees.Add(item);
        }

        public void Update(Employee item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var employee = _db.Employees.Find(id);
            if (employee != null)
                _db.Employees.Remove(employee);
        }
    }
}
