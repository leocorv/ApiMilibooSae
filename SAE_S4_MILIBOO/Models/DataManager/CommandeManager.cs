using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE_S4_MILIBOO.Models.EntityFramework;
using SAE_S4_MILIBOO.Models.Repository;
using System.Drawing.Drawing2D;

namespace SAE_S4_MILIBOO.Models.DataManager
{
    public class CommandeManager : IDataRepositoryCommande<Commande>
    {
        readonly MilibooDBContext? milibooDBContext;

        readonly LignePanierManager? lignePanierManager;

        readonly LigneCommandeManager? ligneCommandeManager;

        readonly AdresseManager? adresseManager;

        private readonly IDataRepositoryCommande<Commande> dataRepository;

        public CommandeManager() { }

        public CommandeManager(MilibooDBContext context)
        {
            milibooDBContext = context;
            ligneCommandeManager = new LigneCommandeManager(context);
            lignePanierManager = new LignePanierManager(context);
            adresseManager = new AdresseManager(context);
        }

        public async Task DeleteAsync(Commande entity)
        {
            var lcommande = await milibooDBContext.LigneCommandes.Where<LigneCommande>(lc => lc.CommandeId == entity.CommandeId).ToListAsync();
            foreach(LigneCommande lc in lcommande)
            {
                await ligneCommandeManager.DeleteAsync(lc);
            }
            milibooDBContext.Commandes.Remove(entity);
            await milibooDBContext.SaveChangesAsync();
        }

        public async Task<ActionResult<IEnumerable<Commande>>> GetAll()
        {
            return await milibooDBContext.Commandes.ToListAsync<Commande>();
        }

        public async Task<ActionResult<IEnumerable<Commande>>> GetAllCommandeByClientId(int clientId)
        {
            DeleteAllCycles deleteAllCycles = new DeleteAllCycles(milibooDBContext);

            var lesCommandes = await milibooDBContext.Commandes.Where<Commande>(c => c.ClientId == clientId).ToListAsync();

            //lesCommandes = deleteAllCycles.ChargeComposants(lesCommandes, new List<string> { "Etat" });
            //lesCommandes = deleteAllCycles.DeleteAllCyclesFunction(lesCommandes);

            return lesCommandes;
        }

        public async Task<ActionResult<IEnumerable<Commande>>> GetAllCommandeByEtat(int etatId)
        {
            return await milibooDBContext.Commandes.Where<Commande>(c => c.EtatId == etatId).ToListAsync();
        }

        public async Task<ActionResult<Commande>> GetPanierByIdClient(int clientId)
        {
            var commandesByClient = await GetAllCommandeByClientId(clientId);
            var commandesByEtat = await GetAllCommandeByEtat(1);

            List<Commande> commandesByClientList = commandesByClient.Value.ToList();
            List<Commande> commandesByEtatList = commandesByEtat.Value.ToList();
            List<Commande> finalList = commandesByClientList.Intersect(commandesByEtatList).ToList();

            Commande panier = null;

            if (finalList.Count != 0) 
            {
                panier = finalList[0];
            }

            return panier;   
        }

        public async Task<ActionResult<Commande>> GetByIdAsync(int id)
        {
            return await milibooDBContext.Commandes.FirstOrDefaultAsync(u => u.CommandeId == id);
        }


        public async Task UpdateAsync(Commande entityToUpdate, Commande entity)
        {
            milibooDBContext.Entry(entityToUpdate).State = EntityState.Modified;

            entityToUpdate.EtatId = entity.EtatId;
            entityToUpdate.AdresseId = entity.AdresseId;
            entityToUpdate.ClientId = entity.ClientId;
            entityToUpdate.Collecte = entity.Collecte;
            entityToUpdate.CommandeId = entity.CommandeId;
            entityToUpdate.Express = entity.Express;
            entityToUpdate.Instructions = entity.Instructions;
            entityToUpdate.PointsFideliteUtilises = entity.PointsFideliteUtilises;

            await milibooDBContext.SaveChangesAsync();
        }

        public async Task AddAsync(Commande entity)
        {
            await milibooDBContext.AddAsync(entity);
            await milibooDBContext.SaveChangesAsync();
        }
    }
}
