using Microsoft.AspNetCore.Mvc;
using SAE_S4_MILIBOO.Models.EntityFramework;

namespace SAE_S4_MILIBOO.Models.Repository
{
    public interface IDataRepositoryPhoto<TEntity>
    {
        Task<ActionResult<TEntity>> GetPhotoById(int id);
        Task<ActionResult<IEnumerable<Photo>>> GetAllPhotosByAvis(int avisId);
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entityToUpdate, TEntity entity);
        Task DeleteAsync(TEntity entity);
        Task<ActionResult<IEnumerable<string>>> GetAllPhotosByVariante(int varianteId);

    }
}
