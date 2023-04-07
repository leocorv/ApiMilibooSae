using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAE_S4_MILIBOO.Models.EntityFramework
{
    [Table("t_e_commande_cmd")]
    public class Commande
    {
        public Commande()
        {

        }

        
        [Column("cmd_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CommandeId { get; set; }

        [Column("eta_id")]
        public int EtatId { get; set; }

        [Column("clt_id")]
        public int ClientId { get; set; }

        [Column("adr_id")]
        public int AdresseId { get; set; }

        [Column("cmd_express")]
        public bool Express { get; set; }

        [Column("cmd_collect")]
        public bool Collecte { get; set; }

        [Column("cmd_points_fidelite_utilises")]
        public int PointsFideliteUtilises { get; set; }

        [Column("cmd_instructions")]
        [StringLength(500)]
        public string? Instructions { get; set; }

        //Lien vers les clients
        [InverseProperty("CommandesClientNavigation")]
        public virtual Client? ClientCommandeNavigation { get; set; }

        //Lien vers les adresse
        [InverseProperty("CommandeAdresseNavigation")]
        public virtual Adresse? AdresseCommandeNavigation { get; set; }

        //Lien vers les lignes de etat
        [InverseProperty("CommandeEtatNavigation")]
        public virtual Etat? EtatCommandeNavigation { get; set; }

        //[InverseProperty("LigneAppartientACommandeNavigation")]
        //public virtual ICollection<LigneCommande> LignesDansLaCommandeNavigation { get; set; } = new List<LigneCommande>();

        [InverseProperty("CommandeLigneCommandeNavigation")]
        public virtual ICollection<LigneCommande> LigneCommandeCommandeNavigation { get; set; } = new List<LigneCommande>();
    }
}
