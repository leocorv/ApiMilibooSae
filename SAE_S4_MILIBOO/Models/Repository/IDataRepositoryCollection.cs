using Microsoft.AspNetCore.Mvc;
using SAE_S4_MILIBOO.Models.EntityFramework;
using System.Collections.ObjectModel;

namespace SAE_S4_MILIBOO.Models.Repository
{
    public interface IDataRepositoryCollection<TEntity>
    {
        Task AddAsync(TEntity entity);

        Task UpdateAsync(TEntity entityToUpdate, TEntity entity);

        Task DeleteAsync(TEntity entity);

        Task<ActionResult<IEnumerable<TEntity>>> GetAll();

        Task<ActionResult<TEntity>> GetCollectionById(int AvisId);
    }
}
