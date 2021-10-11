using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAPI.Domain.Aggregates.ProjectAggregate;

namespace WebAPI.Infrastructure.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly DataContext _db;

        public ProjectRepository(DataContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Project>> GetAllAsync()
        {
            return await _db.Projects.AsNoTracking().ToListAsync();
        }

        public async Task<Project> GetAsync(int id)
        {
            return await _db.Projects.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        }

        public IQueryable<Project> Find(int? id, string name, int? duration)
        {
            IQueryable<Project> result = _db.Projects;

            if (!string.IsNullOrEmpty(name))
                result = result.Where(p => p.Name == name);
            if (duration.HasValue)
                result = result.Where(p => p.Duration == duration.Value);
            if (id.HasValue)
                result = result.Where(p => p.Id == id.Value);

            return result;
        }

        public async Task CreateAsync(Project item)
        {
            await _db.Projects.AddAsync(item);
        }

        public void Update(Project item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }

        public async Task DeleteAsync(int id)
        {
            var project = await _db.Projects.FindAsync(id);
            if (project != null)
                _db.Projects.Remove(project);
        }
    }
}
