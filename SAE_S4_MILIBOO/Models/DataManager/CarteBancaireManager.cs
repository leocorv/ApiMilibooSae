using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE_S4_MILIBOO.Models.EntityFramework;
using SAE_S4_MILIBOO.Models.Repository;

namespace SAE_S4_MILIBOO.Models.DataManager
{
    public class CarteBancaireManager : IDataRepositoryCarteBancaire<CarteBancaire>
    {
        readonly MilibooDBContext? milibooDBContext;

        public CarteBancaireManager() { }

        public CarteBancaireManager(MilibooDBContext context)
        {
            milibooDBContext = context;
        }

        public async Task AddAsync(CarteBancaire entity)
        {
            await milibooDBContext.AddAsync(entity);
            await milibooDBContext.SaveChangesAsync();
        }
        public async Task<ActionResult<CarteBancaire>> GetByIdAsync(int id)
        {
            return await milibooDBContext.CarteBancaires.FirstOrDefaultAsync<CarteBancaire>(c => c.CarteBancaireId == id);
        }

        public async Task DeleteAsync(CarteBancaire entity)
        {
            milibooDBContext.CarteBancaires.Remove(entity);
            await milibooDBContext.SaveChangesAsync();
        }

        public async Task<ActionResult<IEnumerable<CarteBancaire>>> GetAllCartesBancairesByClientId(int ClientId)
        {
            return await milibooDBContext.CarteBancaires.Where<CarteBancaire>(c => c.ClientId == ClientId).ToListAsync();
        }
    }
}
