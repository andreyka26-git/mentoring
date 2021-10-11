using System;
using System.Threading;
using System.Threading.Tasks;
using WebAPI.Application.Interfaces;
using WebAPI.Domain.Aggregates.EmployeeAggregate;
using WebAPI.Domain.Aggregates.ProjectAggregate;

namespace WebAPI.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;
        private IEmployeeRepository _employeeRepository;
        private IProjectRepository _projectRepository;
        private bool _disposed;

        public UnitOfWork(DataContext context)
        {
            _context = context;
        }
        //change to DI
        public IEmployeeRepository Employees => _employeeRepository ??= new EmployeeRepository(_context);
        public IProjectRepository Projects => _projectRepository ??= new ProjectRepository(_context);

        public async Task SaveAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) 
                return;
            
            if (disposing)
            {
                _context.Dispose();
            }

            _disposed = true;
        }
    }
}
