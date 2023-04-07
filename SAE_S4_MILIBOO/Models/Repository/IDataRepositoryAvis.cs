using Microsoft.AspNetCore.Mvc;
using SAE_S4_MILIBOO.Models.EntityFramework;

namespace SAE_S4_MILIBOO.Models.Repository
{
    public interface IDataRepositoryAvis<TEntity>
    {
        Task AddAsync(TEntity entity);

        Task DeleteAsync(TEntity entity);

        Task UpdateAsync(TEntity entityToUpdate, TEntity entity);

        Task<ActionResult<IEnumerable<TEntity>>> GetAll();

        Task<ActionResult<TEntity>> GetAvisById(int AvisId);

        Task<ActionResult<IEnumerable<TEntity>>> GetAvisByProduit(int produitId);

        Task<ActionResult<IEnumerable<TEntity>>> GetAvisByVariante(int varianteId);

        Task<ActionResult<IEnumerable<TEntity>>> GetAvisByClient(int clientId);
    }
}
