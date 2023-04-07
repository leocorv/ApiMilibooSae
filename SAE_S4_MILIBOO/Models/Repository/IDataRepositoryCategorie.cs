using Microsoft.AspNetCore.Mvc;
using SAE_S4_MILIBOO.Models.EntityFramework;

namespace SAE_S4_MILIBOO.Models.Repository
{
    public interface IDataRepositoryCategorie<TEntity>
    {

        Task<ActionResult<TEntity>> GetByIdAsync(int id);
        Task<ActionResult<TEntity>> GetParent(int id);
        Task<ActionResult<List<Categorie>>> GetCategoriesPremierNiveau();
        Task<ActionResult<List<TEntity>>> GetSousCategories(int id);
        Task<ActionResult<List<TEntity>>> RecursivelyAllChildsCategories(Categorie cat);
        //Task AddAsync(TEntity entity);
        //Task UpdateAsync(TEntity entityToUpdate, TEntity entity);
        //Task DeleteAsync(TEntity entity);
    }
}
