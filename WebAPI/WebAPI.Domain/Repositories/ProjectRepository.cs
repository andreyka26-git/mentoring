using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebAPI.Domain.Entities;
using WebAPI.Domain.Interfaces;

namespace WebAPI.Domain.Repositories
{
    public class ProjectRepository : IRepository<Project>
    {
        private readonly DataContext _db;

        public ProjectRepository(DataContext db)
        {
            _db = db;
        }

        public IEnumerable<Project> GetAll()
        {
            return _db.Projects.AsNoTracking();
        }

        public Project Get(int id)
        {
            return _db.Projects.AsNoTracking().FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Project> Find(Func<Project, bool> predicate)
        {
            return _db.Projects.Where(predicate);
        }

        public void Create(Project item)
        {
            _db.Projects.Add(item);
        }

        public void Update(Project item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var project = _db.Projects.Find(id);
            if (project != null)
                _db.Projects.Remove(project);
        }
    }
}
