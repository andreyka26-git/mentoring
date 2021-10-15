using System.Collections.Generic;
using System.Linq;
using System.Threading;
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

        public Task<List<Project>> GetAllAsync(CancellationToken token)
        {
            return _db.Projects.AsNoTracking().ToListAsync(token);
        }

        public Task<Project> GetAsync(int id, CancellationToken token)
        {
            return _db.Projects.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id, token);
        }

        public Task<List<Project>> FindAsync(CancellationToken token, int? id, string name, int? duration)
        {
            var result = _db.Projects.AsQueryable();

            if (!string.IsNullOrEmpty(name))
                result = result.Where(p => p.Name == name);
            if (duration.HasValue)
                result = result.Where(p => p.Duration == duration.Value);
            if (id.HasValue)
                result = result.Where(p => p.Id == id.Value);

            return result.ToListAsync(token);
        }

        public async Task CreateAsync(Project item, CancellationToken token)
        {
            await _db.Projects.AddAsync(item, token);
        }

        public void Update(Project item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }

        public async Task DeleteAsync(int id, CancellationToken token)
        {
            var project = await _db.Projects.FindAsync(id, token);
            if (project != null)
                _db.Projects.Remove(project);
        }
    }
}
