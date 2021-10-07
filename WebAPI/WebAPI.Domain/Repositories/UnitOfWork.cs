﻿using System;
using WebAPI.Application.Interfaces;
using WebAPI.Domain.Aggregates;
using WebAPI.Domain.Aggregates.EmployeeAggregate;
using WebAPI.Domain.Aggregates.ProjectAggregate;

namespace WebAPI.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;
        private IRepository<Employee> _employeeRepository;
        private IRepository<Project> _projectRepository;
        private bool _disposed;

        public UnitOfWork(DataContext context)
        {
            _context = context;
        }

        public IRepository<Employee> Employees => _employeeRepository ??= new EmployeeRepository(_context);
        public IRepository<Project> Projects => _projectRepository ??= new ProjectRepository(_context);

        public void Save()
        {
            _context.SaveChanges();
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
