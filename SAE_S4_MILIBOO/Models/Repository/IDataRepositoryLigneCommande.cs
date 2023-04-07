using Microsoft.AspNetCore.Mvc;

namespace SAE_S4_MILIBOO.Models.Repository
{
    public interface IDataRepositoryLigneCommande<TEntity>
    {
        Task AddAsync(TEntity entity);

        Task DeleteAsync(TEntity entity);

        Task<ActionResult<IEnumerable<TEntity>>> GetByCommande(int idCommande);

        Task UpdateAsync(TEntity entityToUpdate, TEntity entity);
    }
}
