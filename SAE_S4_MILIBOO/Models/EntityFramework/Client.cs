using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace SAE_S4_MILIBOO.Models.EntityFramework
{
    [Table("t_e_client_clt")]
    public class Client
    {
        public Client()
        {
            
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("clt_id")]
        public int ClientId { get; set; }

        [Column("clt_email")]
        [EmailAddress]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "La longueur d’un email doit être comprise entre 6 et 100 caractères.")]
        public string Mail { get; set; }

        [Column("clt_password")]
        [StringLength(100)]
        public string Password { get; set; }

        [Column("clt_nom")]
        [StringLength(30)]
        public string Nom { get; set; }

        [Column("clt_prenom")]
        [StringLength(30)]
        public string Prenom { get; set; }

        [Column("clt_portable")]
        [StringLength(10)]
        public string Portable { get; set; }

        [Column("clt_newsletter_miliboo")]
        public bool NewsMiliboo { get; set; }

        [Column("clt_newsletter_partenaire")]
        public bool NewsPartenaire { get; set; }

        [Column("clt_solde_fidelite")]
        public int SoldeFidelite { get; set; }

        [Column("clt_derniere_connexion", TypeName ="date")]
        public DateTime? DerniereConnexion { get; set; }

        [Column("clt_date_creation", TypeName = "date")]
        public DateTime DateCreation { get; set; }

        [Column("clt_civilite")]
        [StringLength(30)]
        public string Civilite { get; set; }



        [InverseProperty("ClientCarteBancaireNavigation")]
        public virtual ICollection<CarteBancaire> CarteBancaireClientNavigation { get; set; } = new List<CarteBancaire>();

        [InverseProperty("ClientALivreNavigation")]
        public virtual ICollection<AdresseLivraison> ClientsLivraisonsNavigation { get; set; } = new List<AdresseLivraison>();


        [InverseProperty("ClientNavigation")]
        public virtual ICollection<Liste> ListesNavigation { get; set; } = new List<Liste>();

        [InverseProperty("ClientsAvisNavigation")]
        public virtual ICollection<Avis> AvisClientsNavigation { get; set; } = new List<Avis>();

        [InverseProperty("ClientCommandeNavigation")]
        public virtual ICollection<Commande> CommandesClientNavigation { get; set; } = new List<Commande>();

        [InverseProperty("ClientLignePanierNavigation")]
        public virtual ICollection<LignePanier> LignesPanierClientNavigation { get; set; } = new List<LignePanier>();

    }
}
