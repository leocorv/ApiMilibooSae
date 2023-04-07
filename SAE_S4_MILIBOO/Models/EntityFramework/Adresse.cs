using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace SAE_S4_MILIBOO.Models.EntityFramework
{
    [Table("t_e_adresse_adr")]
    public class Adresse
    {
        public Adresse()
        {
            
        }

        
        [Column("adr_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AdresseId { get; set; }

        [Column("adr_rue")]
        [StringLength(50)]
        public string Rue { get; set; }

        [Column("adr_numero")]
        [StringLength(10)]
        public string Numero { get; set; }

        [Column("adr_cp")]
        [StringLength(10)]
        public string Cp { get; set; }

        [Column("adr_ville")]
        [StringLength(50)]
        public string Ville { get; set; }

        [Column("adr_pays")]
        [StringLength(50)]
        public string Pays { get; set; }

        [Column("adr_tel_fixe")]
        [StringLength(15)]
        public string? TelFixe { get; set; }

        [Column("adr_remarque")]
        [StringLength(200)]
        public string? Remarque { get; set; } 


        [InverseProperty("AdresseALivreNavigation")]
        public virtual ICollection<AdresseLivraison> AdressesClientsNavigation { get; set; } = new List<AdresseLivraison>();

        [InverseProperty("AdresseCommandeNavigation")]
        public virtual ICollection<Commande> CommandeAdresseNavigation { get; set; } = new List<Commande>();
    }
}
