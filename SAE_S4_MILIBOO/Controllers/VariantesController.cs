using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SAE_S4_MILIBOO.Models.EntityFramework;
using SAE_S4_MILIBOO.Models.Repository;

namespace SAE_S4_MILIBOO.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class VariantesController : ControllerBase
    {
        private readonly IDataRepositoryVariante<Variante> dataRepository;

        public VariantesController(IDataRepositoryVariante<Variante> dataRepo)
        {
            dataRepository = dataRepo;
        }

        // GET: api/Produits
        [HttpGet]
        [ActionName("GetAll")]
        public async Task<ActionResult<IEnumerable<Variante>>> GetProduits()
        {
            return await dataRepository.GetAll();
        }

        // GET: api/Produits/5
        [HttpGet]
        [ActionName("GetAllByCouleur")]
        public async Task<ActionResult<IEnumerable<Variante>>> GetProduitsByPage(int couleurId)
        {
            var produit = await dataRepository.GetAllByByCouleur(couleurId);

            if (produit == null)
            {
                return NotFound();
            }

            return produit;
        }

        // GET: api/Produits/5
        [HttpGet]
        [ActionName("GetAllByProduit")]
        public async Task<ActionResult<IEnumerable<Variante>>> GetProduitsByProduit(int produitId)
        {
            var produit = await dataRepository.GetAllByProduit(produitId);

            if (produit == null)
            {
                return NotFound();
            }

            return produit;
        }

    }
}
