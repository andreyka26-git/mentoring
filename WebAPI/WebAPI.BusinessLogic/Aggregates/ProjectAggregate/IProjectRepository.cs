using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace WebAPI.Domain.Aggregates.ProjectAggregate
{
    public interface IProjectRepository
    {
        Task<List<Project>> GetAllAsync(CancellationToken token);
        Task<Project> GetAsync(int id, CancellationToken token);
        Task<List<Project>> FindAsync(CancellationToken token, int? id = null, string name = null, int? duration = null);
        Task CreateAsync(Project item, CancellationToken token);
        void Update(Project item);
        Task DeleteAsync(int id, CancellationToken token);
    }
}
