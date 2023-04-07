using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAE_S4_MILIBOO.Models.EntityFramework
{
    [Table("t_e_etat_eta")]
    public class Etat
    {
        public Etat()
        {

        }

        
        [Column("eta_id")]
        public int EtatId { get; set; }

        [Column("eta_libelle")]
        [StringLength(100)]
        public string Libelle { get; set; }

        [InverseProperty("EtatCommandeNavigation")]
        public virtual ICollection<Commande> CommandeEtatNavigation{ get; set; } = new List<Commande>();


    }
}
