using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE_S4_MILIBOO.Models.EntityFramework;
using SAE_S4_MILIBOO.Models.Repository;
using System.Linq;

namespace SAE_S4_MILIBOO.Models.DataManager
{
    public class CouleurManager : IDataRepositoryCouleur<Couleur>
    {
        readonly MilibooDBContext? milibooDBContext;

        readonly DeleteAllCycles? deleteAllCycles;
        public CouleurManager() { }

        public CouleurManager(MilibooDBContext context)
        {
            milibooDBContext = context;

            deleteAllCycles = new DeleteAllCycles(context);
        }

        public async Task<ActionResult<IEnumerable<Couleur>>> GetAll()
        {
            return await milibooDBContext.Couleurs.ToListAsync<Couleur>();
        }


        public async Task<ActionResult<IEnumerable<Couleur>>> GetCouleurofProduit(int produitId)
        {
            var lesVariantes = await milibooDBContext.Variantes.Where<Variante>(var => var.IdProduit == produitId).ToListAsync();
            List<Couleur> lesCouleurs = new List<Couleur>();
            for(int i = 0; i<lesVariantes.Count; i++)
            {
                lesCouleurs.Add(await milibooDBContext.Couleurs.FirstAsync<Couleur>(c => c.IdCouleur == lesVariantes[i].IdCouleur));
            }
            
            List<Couleur> allCouleursAfterDeleteCycles = deleteAllCycles.DeleteAllCyclesFunction(lesCouleurs);

            return allCouleursAfterDeleteCycles;
        }
    }
}
