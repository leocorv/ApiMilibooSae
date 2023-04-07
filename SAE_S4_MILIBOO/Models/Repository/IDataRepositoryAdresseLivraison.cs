namespace SAE_S4_MILIBOO.Models.Repository
{
    public interface IDataRepositoryAdresseLivraison<TEntity>
    {
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entityToUpdate, TEntity entity);
        Task DeleteAsync(TEntity entity);
    }
}
