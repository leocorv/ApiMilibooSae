using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE_S4_MILIBOO.Models.EntityFramework;
using SAE_S4_MILIBOO.Models.Repository;

namespace SAE_S4_MILIBOO.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LignePaniersController : ControllerBase
    {
        private readonly IDataRepositoryLignePanier<LignePanier> dataRepository;

        public LignePaniersController(IDataRepositoryLignePanier<LignePanier> dataRepo)
        {
            dataRepository = dataRepo;
        }

        // GET: api/LignePaniers/5
        [HttpGet]
        [ActionName("GetById")]
        public async Task<ActionResult<LignePanier>> GetLignePanier(int id)
        {
            var LignePanier = await dataRepository.GetByIdAsync(id);

            if (LignePanier == null)
            {
                return NotFound();
            }

            return LignePanier;
        }

        // GET: api/LignePaniers/5
        [HttpGet]
        [ActionName("GetByClientId")]
        public async Task<ActionResult<IEnumerable<LignePanier>>> GetLignePaniersByClientId(int idClient)
        {
            var lignePanier = await dataRepository.GetLignePaniersByClientId(idClient);

            if (lignePanier == null)
            {
                return NotFound();
            }

            return lignePanier;
        }

        // PUT: api/LignePaniers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLignePanier(int id, LignePanier lignePanier)
        {
            if (id != lignePanier.LigneId)
            {
                return BadRequest();
            }

            var userToUpdate = await dataRepository.GetByIdAsync(id);
            if (userToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await dataRepository.UpdateAsync(userToUpdate.Value, lignePanier);
                return NoContent();
            }
        }

        // POST: api/LignePaniers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<LignePanier>> PostLignePanier(LignePanier lignePanier)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await dataRepository.AddAsync(lignePanier);


            return CreatedAtAction("GetById", new { id = lignePanier.LigneId }, lignePanier);
        }

        // DELETE: api/LignePaniers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLignePanier(int id)
        {
            var produit = await dataRepository.GetByIdAsync(id);
            if (produit == null)
            {
                return NotFound();
            }

            await dataRepository.DeleteAsync(produit.Value);

            return NoContent();
        }

        //private bool LignePanierExists(int id)
        //{
        //    return _context.LignePaniers.Any(e => e.LignePanierId == id);
        //}
    }
}
