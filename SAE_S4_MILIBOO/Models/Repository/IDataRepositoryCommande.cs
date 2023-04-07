using Microsoft.AspNetCore.Mvc;
using SAE_S4_MILIBOO.Models.EntityFramework;

namespace SAE_S4_MILIBOO.Models.Repository
{
    public interface IDataRepositoryCommande<TEntity>
    {
        Task<ActionResult<IEnumerable<TEntity>>> GetAll();
        Task<ActionResult<TEntity>> GetByIdAsync(int id);
        Task<ActionResult<IEnumerable<TEntity>>> GetAllCommandeByClientId(int clientId);
        Task<ActionResult<IEnumerable<TEntity>>> GetAllCommandeByEtat(int etatId);
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entityToUpdate, TEntity entity);
        Task DeleteAsync(TEntity entity);
        Task<ActionResult<Commande>> GetPanierByIdClient(int clientId);
    }
}
