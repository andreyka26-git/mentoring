using System;
using WebAPI.Domain.Entities;

namespace WebAPI.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Employee> Employees { get; }
        IRepository<Project> Projects { get; }
        void Save();
    }
}
