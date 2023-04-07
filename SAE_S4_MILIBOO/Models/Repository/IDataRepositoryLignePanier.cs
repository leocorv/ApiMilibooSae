using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE_S4_MILIBOO.Models.EntityFramework;

namespace SAE_S4_MILIBOO.Models.Repository
{
    public interface IDataRepositoryLignePanier<TEntity>
    {
        Task AddAsync(TEntity entity);

        Task DeleteAsync(TEntity entity);

        //Task DeleteByClientAsync(int idClient);

        Task<ActionResult<IEnumerable<TEntity>>> GetLignePaniersByClientId(int idClient);

        Task<ActionResult<TEntity>> GetByIdAsync(int id);

        Task UpdateAsync(TEntity entityToUpdate, TEntity entity);
    }
}
