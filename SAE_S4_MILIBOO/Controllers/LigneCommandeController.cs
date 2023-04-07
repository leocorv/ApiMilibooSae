using Microsoft.AspNetCore.Mvc;
using SAE_S4_MILIBOO.Models.EntityFramework;
using SAE_S4_MILIBOO.Models.Repository;

namespace SAE_S4_MILIBOO.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LigneCommandeController : ControllerBase
    {
        private readonly IDataRepositoryLigneCommande<LigneCommande> dataRepository;

        public LigneCommandeController(IDataRepositoryLigneCommande<LigneCommande> dataRepo)
        {
            dataRepository = dataRepo;
        }

        
        [HttpGet]
        [ActionName("GetByCommandeId")]
        public async Task<ActionResult<IEnumerable<LigneCommande>>> GetByCommandeId(int idCommande)
        {
            var lignescommandes = await dataRepository.GetByCommande(idCommande);

            if (lignescommandes == null)
            {
                return NotFound();
            }

            return lignescommandes;
        }
    }
}
