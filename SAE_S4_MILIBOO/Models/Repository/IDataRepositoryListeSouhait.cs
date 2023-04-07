using Microsoft.AspNetCore.Mvc;

namespace SAE_S4_MILIBOO.Models.Repository
{
    public interface IDataRepositoryListeSouhait<TEntity>
    {
        Task<ActionResult<TEntity>> GetListeSouhaitById(int id);
        Task<ActionResult<IEnumerable<TEntity>>> GetAllListeSouhaitsByClientId(int clientId);
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entityToUpdate, TEntity entity);
        Task DeleteAsync(TEntity entity);
    }
}
