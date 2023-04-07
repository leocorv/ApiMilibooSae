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
    public class AdressesController : ControllerBase
    {
        private readonly IDataRepositoryAdresse<Adresse> dataRepository;

        public AdressesController(IDataRepositoryAdresse<Adresse> dataRepo)
        {
            dataRepository = dataRepo;
        }


        // GET: api/Adresses/5
        [HttpGet]
        [ActionName("GetById")]
        public async Task<ActionResult<Adresse>> GetAdresse(int id)
        {
            var adresse = await dataRepository.GetByIdAsync(id);

            if (adresse == null)
            {
                return NotFound();
            }

            return adresse;
        }

        [HttpGet]
        [ActionName("GetAdresseByValues")]
        public async Task<ActionResult<int>> GetAdresseByValues(string numero, string rue, string cp)
        {
            var result = await dataRepository.GetAdresseByValues(numero, rue, cp);

            if (result == null)
            {
                return NotFound();
            }

            return result;
        }

        // PUT: api/Adresses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdresse(int id, Adresse adresse)
        {
            if (id != adresse.AdresseId)
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
                await dataRepository.UpdateAsync(userToUpdate.Value, adresse);
                return NoContent();
            }
        }

        // POST: api/Adresses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ActionName("PostAdresse")]
        public async Task<ActionResult<Adresse>> PostAdresse(Adresse adresse)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await dataRepository.AddAsync(adresse);


            return CreatedAtAction("GetById", new { id = adresse.AdresseId }, adresse);
        }

        // POST: api/Adresses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754 
        [HttpPost]
        [ActionName("PostAdresseWithClient")]
        public async Task<ActionResult<Adresse>> PostAdresseWithClient(Adresse adresse, int clientId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await dataRepository.AddAsyncWithClient(adresse, clientId);


            return CreatedAtAction("GetById", new { id = adresse.AdresseId }, adresse);
        }

        // DELETE: api/Adresses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdresse(int id)
        {
            var adresse = await dataRepository.GetByIdAsync(id);
            if (adresse == null)
            {
                return NotFound();
            }

            await dataRepository.DeleteAsync(adresse.Value);

            return NoContent();
        }

        [HttpGet]
        [ActionName("GetAdresseByIdClient")]
        public async Task<ActionResult<Adresse>> GetAdresseByIdClient(int idClient)
        {
            var adresse = await dataRepository.GetAdresseByIdClient(idClient);

            if (adresse == null)
            {
                return NotFound();
            }

            return adresse;
        }

    }
}
