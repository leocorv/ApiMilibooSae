using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE_S4_MILIBOO.Models.EntityFramework;
using SAE_S4_MILIBOO.Models.Repository;

namespace SAE_S4_MILIBOO.Models.DataManager
{
    public class CollectionManager : IDataRepositoryCollection<Collection>
    {
        readonly MilibooDBContext? milibooDBContext;

        public CollectionManager() { }

        public CollectionManager(MilibooDBContext context)
        {
            milibooDBContext = context;
        }
        public async Task AddAsync(Collection entity)
        {
            await milibooDBContext.AddAsync(entity);
            await milibooDBContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Collection entity)
        {
            milibooDBContext.Collections.Remove(entity);
            await milibooDBContext.SaveChangesAsync();
        }

        public async Task<ActionResult<IEnumerable<Collection>>> GetAll()
        {
            return await milibooDBContext.Collections.ToListAsync<Collection>();
        }

        public async Task<ActionResult<Collection>> GetCollectionById(int CollectionId)
        {
            return await milibooDBContext.Collections.FirstOrDefaultAsync<Collection>(p => p.CollectionId == CollectionId);
        }

        public async Task UpdateAsync(Collection entityToUpdate, Collection entity)
        {
            milibooDBContext.Entry(entityToUpdate).State = EntityState.Modified;

            entityToUpdate.CollectionId = entity.CollectionId;
            entityToUpdate.CollectionLibelle = entity.CollectionLibelle;
            entityToUpdate.CollectionPrix = entity.CollectionPrix;
            entityToUpdate.CollectionPromo = entity.CollectionPromo;

            await milibooDBContext.SaveChangesAsync();
        }
    }
}
