using Microsoft.AspNetCore.Mvc;
using SAE_S4_MILIBOO.Models.EntityFramework;

namespace SAE_S4_MILIBOO.Models.Repository
{
    public interface IDataRepositoryAdresse<TEntity>
    {
        Task<ActionResult<Adresse>> GetAdresseByIdClient(int idClient);
        Task<ActionResult<TEntity>> GetByIdAsync(int id);
        Task<int> GetAdresseByValues(string numero, string rue, string cp);
        Task AddAsync(TEntity entity);
        Task AddAsyncWithClient(TEntity entity, int clientId);
        Task UpdateAsync(TEntity entityToUpdate, TEntity entity);
        Task DeleteAsync(TEntity entity);
        
    }
}
