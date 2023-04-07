using Microsoft.AspNetCore.Mvc;
using SAE_S4_MILIBOO.Models.EntityFramework;

namespace SAE_S4_MILIBOO.Models.Repository
{
    public interface IDataRepositoryVariante<TEntity>
    {
        Task<ActionResult<IEnumerable<TEntity>>> GetAll();
        Task<ActionResult<IEnumerable<TEntity>>> GetAllByByCouleur( int couleurId);
        Task<ActionResult<IEnumerable<TEntity>>> GetAllByProduit( int produitId);
        Task<List<int>> GetProduitsIdByMinPrix(double minPrix);
        Task<List<int>> GetProduitsIdByMaxPrix(double maxPrix);

        //Task AddAsync(TEntity entity);
        //Task UpdateAsync(TEntity entityToUpdate, TEntity entity);
        //Task DeleteAsync(TEntity entity);
    }
}
