using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SAE_S4_MILIBOO.Models.EntityFramework
{
    [Table("t_j_adresse_livraison_adl")]
    public class AdresseLivraison
    {
        public AdresseLivraison()
        {

        }

        
        [Column("clt_id")]
        public int ClientId { get; set; }


        
        [Column("adr_id")]
        public int AdresseId { get; set; }


        //Lien vers les clients
        [InverseProperty("AdressesClientsNavigation")]
        public virtual Adresse? AdresseALivreNavigation { get; set; } = null!;

        //Lien vers les adresse
        [InverseProperty("ClientsLivraisonsNavigation")]
        public virtual Client? ClientALivreNavigation { get; set; } = null!;
    }
}
