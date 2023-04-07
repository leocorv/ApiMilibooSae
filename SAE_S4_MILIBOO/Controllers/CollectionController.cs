using Microsoft.AspNetCore.Mvc;
using SAE_S4_MILIBOO.Models.EntityFramework;
using SAE_S4_MILIBOO.Models.Repository;
using System.Collections.ObjectModel;

namespace SAE_S4_MILIBOO.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CollectionController : ControllerBase
    {
        private readonly IDataRepositoryCollection<Collection> dataRepository;

        public CollectionController(IDataRepositoryCollection<Collection> dataRepo)
        {
            dataRepository = dataRepo;
        }

        // GET: api/Collections
        [HttpGet]
        [ActionName("GetAll")]
        public async Task<ActionResult<IEnumerable<Collection>>> GetCollections()
        {
            return await dataRepository.GetAll();
        }

        // GET: api/Collections/5
        [HttpGet]
        [ActionName("GetById")]
        public async Task<ActionResult<Collection>> GetCollection(int id)
        {
            var Collection = await dataRepository.GetCollectionById(id);

            if (Collection == null)
            {
                return NotFound();
            }

            return Collection;
        }

        // PUT: api/Collections/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ActionName("Put")]
        public async Task<IActionResult> PutCollection(int id, Collection Collection)
        {
            if (id != Collection.CollectionId)
            {
                return BadRequest();
            }

            var userToUpdate = await dataRepository.GetCollectionById(id);
            if (userToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await dataRepository.UpdateAsync(userToUpdate.Value, Collection);
                return NoContent();
            }
        }

        // POST: api/Collections
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ActionName("Post")]
        public async Task<ActionResult<Collection>> PostCollection(Collection Collection)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await dataRepository.AddAsync(Collection);


            return CreatedAtAction("GetById", new { id = Collection.CollectionId }, Collection);
        }

        // DELETE: api/Collections/5
        [HttpDelete("{id}")]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteCollection(int id)
        {
            var Collection = await dataRepository.GetCollectionById(id);
            if (Collection == null)
            {
                return NotFound();
            }

            await dataRepository.DeleteAsync(Collection.Value);

            return NoContent();
        }
    }
}
