using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE_S4_MILIBOO.Models.EntityFramework;
using SAE_S4_MILIBOO.Models.Repository;

namespace SAE_S4_MILIBOO.Models.DataManager
{
    public class LignePanierManager : IDataRepositoryLignePanier<LignePanier>
    {


        readonly MilibooDBContext? milibooDBContext;

        public LignePanierManager() { }

        public LignePanierManager(MilibooDBContext context)
        {
            milibooDBContext = context;
        }

        public async Task AddAsync(LignePanier entity)
        {
            await milibooDBContext.AddAsync(entity);
            await milibooDBContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(LignePanier entity)
        {
            milibooDBContext.LignePaniers.Remove(entity);
            await milibooDBContext.SaveChangesAsync();
        }

        public async Task<ActionResult<IEnumerable<LignePanier>>> GetLignePaniersByClientId(int idClient)
        {
            var lesLignes = await milibooDBContext.LignePaniers.Where<LignePanier>(c => c.ClientId == idClient).ToListAsync();

            DeleteAllCycles d = new DeleteAllCycles(milibooDBContext);

            lesLignes = d.ChargeComposants(lesLignes, new List<String> { "Variante", "Photo", "Produit", "Couleur" });
            return lesLignes;

            //var lesVariantes = new List<Variante>();
            //var lesPhotos = new List<Photo>();
            //var lesProduits = new List<Produit>();
            //var lesCouleurs = new List<Couleur>();
            //for(int i=0; i<lesLignes.Count; i++)
            //{
            //    lesVariantes.Add(await milibooDBContext.Variantes.FirstOrDefaultAsync<Variante>(var => var.IdVariante == lesLignes[i].VarianteId));
            //    lesCouleurs.Add(await milibooDBContext.Couleurs.FirstOrDefaultAsync<Couleur>(color => color.IdCouleur == lesVariantes[i].IdCouleur));
            //    lesProduits.Add(await milibooDBContext.Produits.FirstOrDefaultAsync<Produit>(produit => produit.IdProduit == lesVariantes[i].IdProduit));
            //    lesPhotos.Add(await milibooDBContext.Photos.FirstOrDefaultAsync<Photo>(foto => foto.VarianteId == lesVariantes[i].IdVariante));

            //    lesLignes[i].VariantesLignePanierNavigation.LignePanierVarianteNavigation = null;
            //    lesVariantes[i].CouleurVarianteNavigation.VariantesCouleurNavigation = null;
            //    lesVariantes[i].ProduitVarianteNavigation.VariantesProduitNavigation = null;
            //    lesPhotos[i].VariantePhotoNavigation = null;
            //}



        }

        public async Task<ActionResult<LignePanier>> GetByIdAsync(int id)
        {
            return await milibooDBContext.LignePaniers.FirstOrDefaultAsync<LignePanier>(c => c.LigneId == id);
        }

        public async Task UpdateAsync(LignePanier entityToUpdate, LignePanier entity)
        {
            milibooDBContext.Entry(entityToUpdate).State = EntityState.Modified;

            entityToUpdate.LigneId = entity.LigneId;
            entityToUpdate.ClientId = entity.ClientId;
            entityToUpdate.VarianteId = entity.VarianteId;
            entityToUpdate.Quantite = entity.Quantite;

            await milibooDBContext.SaveChangesAsync();
        }
    }
}
