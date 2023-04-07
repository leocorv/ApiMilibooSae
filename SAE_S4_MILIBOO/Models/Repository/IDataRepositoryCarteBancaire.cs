using Microsoft.AspNetCore.Mvc;
using SAE_S4_MILIBOO.Models.EntityFramework;

namespace SAE_S4_MILIBOO.Models.Repository
{
    public interface IDataRepositoryCarteBancaire<TEntity>
    {
        Task AddAsync(TEntity entity);
        Task<ActionResult<TEntity>> GetByIdAsync(int id);
        Task DeleteAsync(TEntity entity);
        Task<ActionResult<IEnumerable<TEntity>>> GetAllCartesBancairesByClientId(int ClientId);
    }
}
