using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Domain.Aggregates.ProjectAggregate
{
    public interface IProjectRepository
    {
        Task<IEnumerable<Project>> GetAllAsync();
        Task<Project> GetAsync(int id);
        IQueryable<Project> Find(int? id = null, string name = null, int? duration = null);
        Task CreateAsync(Project item);
        void Update(Project item);
        Task DeleteAsync(int id);
    }
}
