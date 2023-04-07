using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE_S4_MILIBOO.Models.EntityFramework;
using SAE_S4_MILIBOO.Models.Repository;

namespace SAE_S4_MILIBOO.Models.DataManager
{
    public class ClientManager : IDataRepositoryClient<Client>
    {
        readonly MilibooDBContext? milibooDBContext;

        public ClientManager() { }

        public ClientManager(MilibooDBContext context)
        {
            milibooDBContext = context;
        }

        public async Task<ActionResult<IEnumerable<Client>>> GetAll()
        {
            return await milibooDBContext.Clients.ToListAsync<Client>();
        }

        public async Task<ActionResult<Client>> GetByIdAsync(int id)
        {          
            var leclient =  await milibooDBContext.Clients.FirstOrDefaultAsync<Client>(c => c.ClientId == id);

            return leclient;
        }

        public async Task<ActionResult<Client>> GetClientByEmail(string email)
        {
            var leClient = await milibooDBContext.Clients.FirstOrDefaultAsync<Client>(c => c.Mail == email);

            DeleteAllCycles deleteAllCycles = new DeleteAllCycles(milibooDBContext);
            leClient = deleteAllCycles.ChargeComposants(leClient, new List<string>() { "AdresseLivraison" });

            //DeleteAllCycles deleteAllCycles = new DeleteAllCycles(milibooDBContext);
            //deleteAllCycles.DeleteAllCyclesFunction(leClient);

            return leClient;

        }


        public async Task<ActionResult<Client>> GetClientByIdAdresse(int idAdresse)
        {
            var a = await milibooDBContext.Adresses.FirstOrDefaultAsync<Adresse>(a => a.AdresseId == idAdresse);
            return await milibooDBContext.Clients.FirstOrDefaultAsync<Client>(c => c.ClientId == a.AdresseId);
        }

        public async Task<ActionResult<Client>> GetClientByPortable(string portable)
        {
            return await milibooDBContext.Clients.FirstOrDefaultAsync<Client>(c => c.Portable == portable);
        }

        public async Task<ActionResult<IEnumerable<Client>>> GetAllClientsByNomPrenom(string recherche)
        {
            return await milibooDBContext.Clients.Where<Client>(c => c.Nom == recherche || c.Prenom == recherche).ToListAsync();
        }

        public async Task<ActionResult<IEnumerable<Client>>> GetAllClientsNewsletterM()
        {
            return await milibooDBContext.Clients.Where<Client>(c => c.NewsMiliboo == true).ToListAsync();
        }
        public async Task<ActionResult<IEnumerable<Client>>> GetAllClientsNewsletterP()
        {
            return await milibooDBContext.Clients.Where<Client>(c => c.NewsPartenaire == true).ToListAsync();
        }


        public async Task<ActionResult<Client>> ReplacePassword(string newPassword, int idClient)
        {
            var c = await milibooDBContext.Clients.FirstOrDefaultAsync<Client>(c => c.ClientId == idClient);

            c.Password = newPassword;

            await milibooDBContext.SaveChangesAsync();

            return await milibooDBContext.Clients.FirstOrDefaultAsync<Client>(c => c.ClientId == idClient);
        }

        public async Task AddAsync(Client entity)
        {
            await milibooDBContext.AddAsync(entity);
            await milibooDBContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Client entity)
        {
            milibooDBContext.Clients.Remove(entity);
            await milibooDBContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Client entityToUpdate, Client entity)
        {
            milibooDBContext.Entry(entityToUpdate).State = EntityState.Modified;

            entityToUpdate.ClientId = entity.ClientId;
            entityToUpdate.CarteBancaireClientNavigation = entity.CarteBancaireClientNavigation;
            entityToUpdate.Mail = entity.Mail;
            entityToUpdate.Password = entity.Password;
            entityToUpdate.Nom = entity.Nom;
            entityToUpdate.Prenom = entity.Prenom;
            entityToUpdate.NewsMiliboo = entity.NewsMiliboo;
            entityToUpdate.NewsPartenaire = entity.NewsPartenaire;
            entityToUpdate.SoldeFidelite = entity.SoldeFidelite;
            entityToUpdate.DerniereConnexion = entity.DerniereConnexion;
            entityToUpdate.DateCreation = entity.DateCreation;
            entityToUpdate.Civilite = entity.Civilite;

            await milibooDBContext.SaveChangesAsync();
        }
    }
}
