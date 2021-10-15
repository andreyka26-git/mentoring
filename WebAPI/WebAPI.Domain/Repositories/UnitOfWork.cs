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
        private bool _disposed;
        public IEmployeeRepository Employees { get; }
        public IProjectRepository Projects { get; }

        public UnitOfWork(DataContext context, IEmployeeRepository employeeRepository, IProjectRepository projectRepository)
        {
            _context = context;
            Employees = employeeRepository;
            Projects = projectRepository;
        }

        public Task SaveAsync(CancellationToken cancellationToken)
        {
            return _context.SaveChangesAsync(cancellationToken);
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
