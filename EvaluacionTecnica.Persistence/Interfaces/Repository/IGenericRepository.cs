using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluacionTecnica.Persistence.Interfaces.Repository
{
    public interface IGenericRepository<Entity> where Entity : class
    {
        Task<Entity> CreateAsync(Entity entity);
        Task<bool> UpdateAsync(Entity entity, int id);
        Task<bool> DeleteAsync(Entity entity);
        Task<IEnumerable<Entity>> GetAllAsync();
        Task<Entity> GetByIdAsync(int id);
        Task<bool> Save();

    }
}
