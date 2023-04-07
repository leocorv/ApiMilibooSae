using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE_S4_MILIBOO.Models.EntityFramework;

namespace SAE_S4_MILIBOO.Models.Repository
{
    public interface IDataRepositoryClient<TEntity>
    {
        Task<ActionResult<TEntity>> GetByIdAsync(int id);
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entityToUpdate, TEntity entity);
        Task DeleteAsync(TEntity entity);
        Task<ActionResult<IEnumerable<TEntity>>> GetAll();
        Task<ActionResult<TEntity>> GetClientByEmail(string email);
        Task<ActionResult<TEntity>> ReplacePassword(string newPassword, int idClient);
        Task<ActionResult<TEntity>> GetClientByPortable(string portable);
        Task<ActionResult<IEnumerable<TEntity>>> GetAllClientsByNomPrenom(string recherche);
        Task<ActionResult<IEnumerable<TEntity>>> GetAllClientsNewsletterM();
        Task<ActionResult<IEnumerable<TEntity>>> GetAllClientsNewsletterP();
        Task<ActionResult<TEntity>> GetClientByIdAdresse(int idAdresse);
    }

}
