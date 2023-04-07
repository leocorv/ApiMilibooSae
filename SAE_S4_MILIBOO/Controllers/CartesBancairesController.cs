using Microsoft.AspNetCore.Mvc;
using SAE_S4_MILIBOO.Models.EntityFramework;
using SAE_S4_MILIBOO.Models.Repository;

namespace SAE_S4_MILIBOO.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CartesBancairesControlleur : ControllerBase
    {

        private readonly IDataRepositoryCarteBancaire<CarteBancaire> dataRepository;

        public CartesBancairesControlleur(IDataRepositoryCarteBancaire<CarteBancaire> dataRepo)
        {
            dataRepository = dataRepo;
        }


        // GET: api/CarteBancaires/5
        [HttpGet]
        [ActionName("GetById")]
        public async Task<ActionResult<CarteBancaire>> GetCarteBancaire(int id)
        {
            var carteBancaire = await dataRepository.GetByIdAsync(id);

            if (carteBancaire == null)
            {
                return NotFound();
            }

            return carteBancaire;
        }

        // GET: api/CarteBancaires/5
        [HttpGet]
        [ActionName("GetByClient")]
        public async Task<ActionResult<IEnumerable<CarteBancaire>>> GetAllCartesBancairesByClientId(int idClient)
        {
            var carteBancaire = await dataRepository.GetAllCartesBancairesByClientId(idClient);

            if (carteBancaire == null)
            {
                return NotFound();
            }

            return carteBancaire;
        }

        // POST: api/CarteBancaires
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CarteBancaire>> PostCarteBancaire(CarteBancaire carteBancaire)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await dataRepository.AddAsync(carteBancaire);


            return CreatedAtAction("GetById", new { id = carteBancaire.CarteBancaireId }, carteBancaire);
        }

        // DELETE: api/CarteBancaires/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarteBancaire(int id)
        {
            var produit = await dataRepository.GetByIdAsync(id);
            if (produit == null)
            {
                return NotFound();
            }

            await dataRepository.DeleteAsync(produit.Value);

            return NoContent();
        }
    }
}
