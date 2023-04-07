using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE_S4_MILIBOO.Models.EntityFramework;
using SAE_S4_MILIBOO.Models.Repository;

namespace SAE_S4_MILIBOO.Models.DataManager
{
    public class ListeSouhaitManager : IDataRepositoryListeSouhait<Liste>
    {
        readonly MilibooDBContext? milibooDBContext;

        public ListeSouhaitManager() { }

        public ListeSouhaitManager(MilibooDBContext context)
        {
            milibooDBContext = context;
        }
        public async Task AddAsync(Liste entity)
        {
            await milibooDBContext.AddAsync(entity);
            await milibooDBContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Liste entity)
        {
            milibooDBContext.Listes.Remove(entity);
            await milibooDBContext.SaveChangesAsync();
        }

        public async Task<ActionResult<Liste>> GetListeSouhaitById(int ListeSouhaitId)
        {
            return await milibooDBContext.Listes.FirstOrDefaultAsync<Liste>(p => p.ListeId == ListeSouhaitId);
        }

        public async Task<ActionResult<IEnumerable<Liste>>> GetAllListeSouhaitsByClientId(int clientId)
        {
            return await milibooDBContext.Listes.Where<Liste>(p => p.ClientId == clientId).ToListAsync(); 
        }

        public async Task UpdateAsync(Liste entityToUpdate, Liste entity)
        {
            milibooDBContext.Entry(entityToUpdate).State = EntityState.Modified;

            entityToUpdate.ListeId = entity.ListeId;
            entityToUpdate.ClientId = entity.ClientId;
            entityToUpdate.Libelle = entity.Libelle;
            entityToUpdate.DateCreation = entity.DateCreation;
            entityToUpdate.DerniereModif = entity.DerniereModif;

            await milibooDBContext.SaveChangesAsync();
        }
    }
}
