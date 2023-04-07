using Microsoft.AspNetCore.Mvc;
using SAE_S4_MILIBOO.Models.EntityFramework;
using SAE_S4_MILIBOO.Models.Repository;

namespace SAE_S4_MILIBOO.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AvisController : ControllerBase
    {
        private readonly IDataRepositoryAvis<Avis> dataRepository;

        public AvisController(IDataRepositoryAvis<Avis> dataRepo)
        {
            dataRepository = dataRepo;
        }

        // GET: api/Avis
        [HttpGet]
        [ActionName("GetAll")]
        public async Task<ActionResult<IEnumerable<Avis>>> GetAllAvis()
        {
            return await dataRepository.GetAll();
        }

        // GET: api/Avis/5
        [HttpGet]
        [ActionName("GetById")]
        public async Task<ActionResult<Avis>> GetAvis(int id)
        {
            var Avis = await dataRepository.GetAvisById(id);

            if (Avis == null)
            {
                return NotFound();
            }

            return Avis;
        }

        // GET: api/Avis/fauteuil
        [HttpGet]
        [ActionName("GetByProduit")]
        public async Task<ActionResult<IEnumerable<Avis>>> GetAvisByProduit(int produitId)
        {
            var Avis = await dataRepository.GetAvisByProduit(produitId);

            if (Avis == null)
            {
                return NotFound();
            }

            return Avis;
        }

        [HttpGet]
        [ActionName("GetByVariante")]
        public async Task<ActionResult<IEnumerable<Avis>>> GetAvisByVariante(int varianteId)
        {
            var avis = await dataRepository.GetAvisByVariante(varianteId);

            if (avis == null)
            {
                return NotFound();
            }

            return avis;
        }

        [HttpGet]
        [ActionName("GetByClient")]
        public async Task<ActionResult<IEnumerable<Avis>>> GetAvisByClient(int clientId)
        {
            var avis = await dataRepository.GetAvisByClient(clientId);

            if (avis == null)
            {
                return NotFound();
            }

            return avis;
        }

        // PUT: api/Aviss/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ActionName("Put")]
        public async Task<IActionResult> PutAvis(int id, Avis avis)
        {
            if (id != avis.AvisId)
            {
                return BadRequest();
            }

            var userToUpdate = await dataRepository.GetAvisById(id);
            if (userToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await dataRepository.UpdateAsync(userToUpdate.Value, avis);
                return NoContent();
            }
        }

        // POST: api/Avis
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ActionName("Post")]
        public async Task<ActionResult<Avis>> PostAvis(Avis avis)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await dataRepository.AddAsync(avis);


            return CreatedAtAction("GetById", new { id = avis.AvisId }, avis);
        }

        // DELETE: api/Avis/5
        [HttpDelete("{id}")]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteAvis(int id)
        {
            var avis = await dataRepository.GetAvisById(id);
            if (avis == null)
            {
                return NotFound();
            }

            await dataRepository.DeleteAsync(avis.Value);

            return NoContent();
        }
    }
}
