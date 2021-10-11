using System;
using System.Threading.Tasks;
using WebAPI.Domain.Aggregates.EmployeeAggregate;
using WebAPI.Domain.Aggregates.ProjectAggregate;

namespace WebAPI.Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IEmployeeRepository Employees { get; }
        IProjectRepository Projects { get; }
        Task SaveAsync();
    }
}
