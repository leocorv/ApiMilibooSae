using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE_S4_MILIBOO.Models.EntityFramework;
using SAE_S4_MILIBOO.Models.Repository;

namespace SAE_S4_MILIBOO.Models.DataManager
{
    public class VarianteManager : IDataRepositoryVariante<Variante>
    {
        readonly MilibooDBContext? milibooDBContext;

        public VarianteManager() { }

        public VarianteManager(MilibooDBContext context)
        {
            milibooDBContext = context;
        }

        public async Task<ActionResult<IEnumerable<Variante>>> GetAll()
        {
            return await milibooDBContext.Variantes.ToListAsync<Variante>();
        }

        public async Task<ActionResult<IEnumerable<Variante>>> GetAllByByCouleur(int couleurId)
        {
            return await milibooDBContext.Variantes.Where<Variante>(v => v.IdCouleur == couleurId).ToListAsync();
        }

        public async Task<ActionResult<IEnumerable<Variante>>> GetAllByProduit(int produitId)
        {
            var avis = await milibooDBContext.Avis.ToListAsync();
            var allprds = await milibooDBContext.Variantes.Where<Variante>(v => v.IdProduit== produitId).ToListAsync();

            return allprds;
        }

        public async Task<List<int>> GetProduitsIdByMaxPrix(double maxPrix)
        {
            var lesVariantes = await milibooDBContext.Variantes.Where(var => var.Prix <= maxPrix).ToListAsync();
            List<int> lesIdProduits = new List<int>();

            foreach (Variante var in lesVariantes)
            {
                lesIdProduits.Add(var.IdProduit);
            }

            return lesIdProduits;
        }

        public async Task<List<int>> GetProduitsIdByMinPrix(double minPrix)
        {
            var lesVariantes = await milibooDBContext.Variantes.Where(var => var.Prix >= minPrix).ToListAsync();
            List<int> lesIdProduits = new List<int>();

            foreach (Variante var in lesVariantes)
            {
                lesIdProduits.Add(var.IdProduit);
            }

            return lesIdProduits;
        }
    }
}
