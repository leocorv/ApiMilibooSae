using Microsoft.AspNetCore.Mvc;
using SAE_S4_MILIBOO.Models.EntityFramework;
using SAE_S4_MILIBOO.Models.Repository;

namespace SAE_S4_MILIBOO.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategorieController : ControllerBase
    {
        private readonly IDataRepositoryCategorie<Categorie> dataRepository;

        public CategorieController(IDataRepositoryCategorie<Categorie> dataRepo)
        {
            dataRepository = dataRepo;
        }


        // GET: api/Adresses/5
        [HttpGet]
        [ActionName("GetById")]
        public async Task<ActionResult<Categorie>> GetCategorie(int id)
        {
            var adresse = await dataRepository.GetByIdAsync(id);

            if (adresse == null)
            {
                return NotFound();
            }

            return adresse;
        }



        // GET: api/Adresses/5
        [HttpGet]
        [ActionName("GetAllCategoriesPremierNiveau")]
        public async Task<ActionResult<List<Categorie>>> GetAllCategoriesPremierNiveau()
        {
            return await dataRepository.GetCategoriesPremierNiveau();
        }

        // GET: api/Adresses/5
        [HttpGet]
        [ActionName("GetCategorieParent")]
        public async Task<ActionResult<Categorie>> GetParent(int id)
        {
            var adresse = await dataRepository.GetParent(id);

            if (adresse.Value == null)
            {
                return NotFound();
            }

            return adresse;
        }

        // GET: api/Adresses/5
        [HttpGet]
        [ActionName("GetSousCategorie")]
        public async Task<ActionResult<List<Categorie>>> GetSousCategorie(int id)
        {
            var adresse = await dataRepository.GetSousCategories(id); 

            if (adresse == null)
            {
                return NotFound();
            }

            return adresse;
        }
    }
}
