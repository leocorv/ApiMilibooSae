using Microsoft.EntityFrameworkCore;
using SAE_S4_MILIBOO.Models.EntityFramework;
using SAE_S4_MILIBOO.Models.Repository;

namespace SAE_S4_MILIBOO.Models.DataManager
{
    public class AdresseLivraisonManager : IDataRepositoryAdresseLivraison<AdresseLivraison>
    {
        readonly MilibooDBContext? milibooDBContext;


        public AdresseLivraisonManager() { }

        public AdresseLivraisonManager(MilibooDBContext context)
        {
            milibooDBContext = context;
        }

        public async Task AddAsync(AdresseLivraison entity)
        {
            await milibooDBContext.AddAsync(entity);
            await milibooDBContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(AdresseLivraison entity)
        {
            milibooDBContext.AdresseLivraisons.Remove(entity);
            await milibooDBContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(AdresseLivraison entityToUpdate, AdresseLivraison entity)
        {
            milibooDBContext.Entry(entityToUpdate).State = EntityState.Modified;

            entityToUpdate.ClientId = entity.ClientId;
            entityToUpdate.AdresseId = entity.AdresseId;

            await milibooDBContext.SaveChangesAsync();
        }
    }
}
