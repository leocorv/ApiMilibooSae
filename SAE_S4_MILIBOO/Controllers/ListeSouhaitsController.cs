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
    public class ListeSouhaitsController : ControllerBase
    {
        private readonly IDataRepositoryListeSouhait<Liste> dataRepository;

        public ListeSouhaitsController(IDataRepositoryListeSouhait<Liste> dataRepo)
        {
            dataRepository = dataRepo;
        }


        // GET: api/ListeSouhaits/5
        [HttpGet]
        [ActionName("GetById")]
        public async Task<ActionResult<Liste>> GetListeSouhaitById(int id)
        {
            var listeSouhait = await dataRepository.GetListeSouhaitById(id);

            if (listeSouhait == null)
            {
                return NotFound();
            }

            return listeSouhait;
        }

        // GET: api/ListeSouhaits/5
        [HttpGet]
        [ActionName("GetByClient")]
        public async Task<ActionResult<IEnumerable<Liste>>> GetAllListeSouhaitsByClientId(int idClient)
        {
            var listeSouhait = await dataRepository.GetAllListeSouhaitsByClientId(idClient);

            if (listeSouhait == null)
            {
                return NotFound();
            }

            return listeSouhait;
        }

        // PUT: api/ListeSouhaits/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutListeSouhait(int id, Liste listeSouhait)
        {
            if (id != listeSouhait.ListeId)
            {
                return BadRequest();
            }

            var userToUpdate = await dataRepository.GetListeSouhaitById(id);
            if (userToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await dataRepository.UpdateAsync(userToUpdate.Value, listeSouhait);
                return NoContent();
            }
        }

        // POST: api/ListeSouhaits
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Liste>> PostListeSouhait(Liste listeSouhait)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await dataRepository.AddAsync(listeSouhait);


            return CreatedAtAction("GetById", new { id = listeSouhait.ListeId }, listeSouhait);
        }

        // DELETE: api/ListeSouhaits/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteListeSouhait(int id)
        {
            var produit = await dataRepository.GetListeSouhaitById(id);
            if (produit == null)
            {
                return NotFound();
            }

            await dataRepository.DeleteAsync(produit.Value);

            return NoContent();
        }

        //private bool ListeSouhaitExists(int id)
        //{
        //    return _context.ListeSouhaits.Any(e => e.ListeSouhaitId == id);
        //}
    }
}
