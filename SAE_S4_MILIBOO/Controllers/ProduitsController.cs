using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SAE_S4_MILIBOO.Models.DataManager;
using SAE_S4_MILIBOO.Models.EntityFramework;
using SAE_S4_MILIBOO.Models.Repository;

namespace SAE_S4_MILIBOO.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProduitsController : ControllerBase
    {
        private readonly IDataRepositoryProduits<Produit> dataRepository;

        readonly DeleteAllCycles? deleteAllCycles;

        public ProduitsController(IDataRepositoryProduits<Produit> dataRepo)
        {
            dataRepository = dataRepo;
        }

        // GET: api/Produits
        [HttpGet]
        [ActionName("GetAll")]
        public async Task<ActionResult<IEnumerable<Produit>>> GetProduits()
        {
            return await dataRepository.GetAll();
        }

        // GET: api/Produits/5
        [HttpGet]
        [ActionName("GetById")]
        public async Task<ActionResult<Produit>> GetProduit(int id)
        {
            var produit = await dataRepository.GetProduitById(id);
            //produit = deleteAllCycles.DeleteAllCyclesFunction(produit);

            if (produit == null)
            {
                return NotFound();
            }

            return produit;
        }

        // GET: api/Produits/fauteuil
        [HttpGet]
        [ActionName("GetByLibelle")]
        public async Task<ActionResult<Produit>> GetProduitByLibelle(string libelle)
        {
            var produit = await dataRepository.GetByStringAsync(libelle);

            if (produit == null)
            {
                return NotFound();
            }

            return produit;
        }

        // GET: api/Produits/5
        [HttpGet]
        [ActionName("GetByAllByPageAndCategorie")]
        public async Task<ActionResult<IEnumerable<Produit>>> GetProduitsByPageAndCategorie(int page, int categorieId)
        {
            if (page == 0)
            {
                Response.StatusCode = 400;
                var codeError = Response.StatusCode;

                return BadRequest("Erreur " + codeError + " : Bad Request \nLe numéro de page est requis");
            }

            var produit = await dataRepository.GetAllByPageByCategorie(page, categorieId);

            if (produit == null)
            {
                return NotFound();
            }

            return produit;
        }

        // GET: api/Produits/5
        [HttpGet]
        [ActionName("GetByAllByPageAndCouleur")]
        public async Task<ActionResult<IEnumerable<Produit>>> GetByAllByPageAndCouleur(int page, int categorieId, [FromQuery]int[] couleurId)
        {
            if (page == 0)
            {
                Response.StatusCode = 400;
                var codeError = Response.StatusCode;

                return BadRequest("Erreur " + codeError + " : Bad Request \nLe numéro de page est requis");
            }

            var produit = await dataRepository.GetAllByPageByCouleur(page, categorieId, couleurId.ToList());

            if (produit == null)
            {
                return NotFound();
            }

            return produit;
        }

        // GET: api/Produits/5
        [HttpGet]
        [ActionName("GetByPageAndPrixMini")]
        public async Task<ActionResult<IEnumerable<Produit>>> GetProduitsByPageAndPrixMini(int page, int categorieId, int min)
        {
            if (page == 0)
            {
                Response.StatusCode = 400;
                var codeError = Response.StatusCode;

                return BadRequest("Erreur " + codeError + " : Bad Request \nLe numéro de page est requis");
            }

            var listProduit = await dataRepository.GetAllByPageByPrixMini(page, categorieId, min);

            if (listProduit.Value.Count() == 0)
            {
                return NotFound();
            }

            return listProduit;
        }

        // GET: api/Produits/5
        [HttpGet]
        [ActionName("GetByPageAndPrixMaxi")]
        public async Task<ActionResult<IEnumerable<Produit>>> GetProduitsByPageAndPrixMaxi(int page,  int categorieId, int max)
        {
            if (page == 0)
            {
                Response.StatusCode = 400;
                var codeError = Response.StatusCode;

                return BadRequest("Erreur " + codeError + " : Bad Request \nLe numéro de page est requis");
            }

            var listProduit = await dataRepository.GetAllByPageByPrixMaxi(page, categorieId, max);

            if (listProduit.Value.Count() == 0)
            {
                return NotFound();
            }

            return listProduit;
        }

        // GET: api/Produits/5
        [HttpGet]
        [ActionName("GetAllByPageAndCollection")]
        public async Task<ActionResult<IEnumerable<Produit>>> GetByAllByPageAndCollection(int page, int collectionId)
        {
            if (page == 0)
            {
                Response.StatusCode = 400;
                var codeError = Response.StatusCode;

                return BadRequest("Erreur " + codeError + " : Bad Request \nLe numéro de page est requis");
            }

            var produit = await dataRepository.GetAllByPageByCollection(page, collectionId);

            if (produit == null)
            {
                return NotFound();
            }

            return produit;
        }

        // GET: api/Produits/5
        [HttpGet]
        [ActionName("GetAllByAllFilters")]
        public async Task<ActionResult<IEnumerable<Produit>>> GetAllByPageByAllFilters(int page, int? categorieId, int? collectionId, [FromQuery] int[] couleurId, double? maxprix, double? minprix)
        {
            if (page == 0)
            {
                Response.StatusCode = 400;
                var codeError = Response.StatusCode;

                return BadRequest("Erreur " + codeError + " : Bad Request \nLe numéro de page est requis");
            }

            if (categorieId == null && collectionId == null)
            {
                Response.StatusCode = 400;
                var codeError = Response.StatusCode;

                return BadRequest("Erreur " + codeError + " : Bad Request \nSoit la catégorie, soit la collection est requise");
            }

            if (categorieId == null && (couleurId != null || minprix != null || maxprix != null ))
            {
                Response.StatusCode = 400;
                var codeError = Response.StatusCode;

                return BadRequest("Erreur " + codeError + " : Bad Request \nLes filtres qui s'appliquent sur la couleur et les prix nécessitent en argument implicite le numéro de catégorie");
            }

            var produit = await dataRepository.GetByAllFiltersByPage(page, categorieId, collectionId, couleurId.ToList(), maxprix, minprix);

            if (produit == null)
            {
                return NotFound();
            }

            return produit;
        }

        // GET: api/Produits/5
        //[HttpGet]
        //[ActionName("GetAllByAllFilters")]
        //public async Task<ActionResult<IEnumerable<Produit>>> GetAllByPageByAllFilters(int page, int? categorieId, int? collectionId, [FromQuery] int[] couleurId, double? maxprix, double? minprix)
        //{
        //    var produit = await dataRepository.GetByAllFiltersByPage(page, categorieId, collectionId, couleurId.ToList(), maxprix, minprix);

        //    if (produit == null)
        //    {
        //        return NotFound();
        //    }

        //    return produit;
        //}


        [HttpGet]
        [ActionName("GetNumberPagesByCategorie")]
        public async Task<ActionResult<decimal>> GetNumberPagesByCategorie(int categorieId)
        {
            var nbrpages = await dataRepository.GetNumberPagesByCategorie(categorieId);

            if (nbrpages == 0)
            {
                return NotFound();
            }

            return nbrpages;
        }

        [HttpGet]
        [ActionName("GetNumberPagesByCouleur")]
        public async Task<ActionResult<decimal>> GetNumberPagesByCouleur(int categorieId, [FromQuery]int[] couleurId)
        {
            var nbrpages = await dataRepository.GetNumberPagesByCouleur(categorieId, couleurId.ToList());

            if (nbrpages < 0)
            {
                return BadRequest();
            }

            return nbrpages;
        }

        [HttpGet]
        [ActionName("GetNumberPagesByPrixMaxi")]
        public async Task<ActionResult<decimal>> GetNumberPagesByPrixMaxi(int categorieId, int max)
        {
            var nbrpages = await dataRepository.GetNumberPagesByPrixMaxi(categorieId, max);

            if (nbrpages < 0)
            {
                return BadRequest();
            }

            return nbrpages;
        }

        [HttpGet]
        [ActionName("GetNumberPagesByPrixMini")]
        public async Task<ActionResult<decimal>> GetNumberPagesByPrixMin(int categorieId, int min)
        {
            var nbrpages = await dataRepository.GetNumberPagesByPrixMini(categorieId, min);

            if (nbrpages < 0)
            {
                return BadRequest();
            }

            return nbrpages;
        }


        [HttpGet]
        [ActionName("GetNumberPagesByCollection")]
        public async Task<ActionResult<decimal>> GetNumberPagesByCollection(int collectionId)
        {
            var nbrpages = await dataRepository.GetNumberPagesByCollection(collectionId);

            if (nbrpages < 0)
            {
                return BadRequest();
            }

            return nbrpages;
        }

        [HttpGet]
        [ActionName("GetNumberPagesByAllFilters")]
        public async Task<ActionResult<decimal>> GetNumberPagesByAllFilters(int? categorieId, int? collectionId, [FromQuery] int[] couleurId, double? maxprix, double? minprix)
        {
            var nbrpages = await dataRepository.GetNumberPagesByAllFilters(categorieId, collectionId, couleurId.ToList(), maxprix, minprix);

            if (nbrpages < 0)
            {
                return BadRequest();
            }

            

            return nbrpages;
        }

        // PUT: api/Produits/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ActionName("Put")]
        public async Task<IActionResult> PutProduit(int id, Produit produit)
        {
            if (id != produit.IdProduit)
            {
                return BadRequest();
            }

            var userToUpdate = await dataRepository.GetProduitById(id);
            if (userToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await dataRepository.UpdateAsync(userToUpdate.Value, produit);
                return NoContent();
            }
        }

        // POST: api/Produits
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ActionName("Post")]
        public async Task<ActionResult<Produit>> PostProduit(Produit produit)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await dataRepository.AddAsync(produit);


            return CreatedAtAction("GetById", new { id = produit.IdProduit }, produit);
        }

        // DELETE: api/Produits/5
        [HttpDelete("{id}")]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteProduit(int id)
        {
            var produit = await dataRepository.GetProduitById(id);
            if (produit == null)
            {
                return NotFound();
            }

            await dataRepository.DeleteAsync(produit.Value);

            return NoContent();
        }

        //private bool ProduitExists(int id)
        //{
        //    return _context.Produits.Any(e => e.IdProduit == id);
        //}
    }
}
