using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAE_S4_MILIBOO.Models.EntityFramework
{
    [Table("t_e_ligne_commande_lcm")]
    public class LigneCommande
    {
        public LigneCommande()
        {

        }

        [Column("lcm_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LigneCommandeId { get; set; }

        [Column("vrt_id")]
        public int VarianteId { get; set; }

        [Column("cmd_id")]
        public int CommandeId { get; set; }

        [Column("lcm_quantite")]
        public int Quantite { get; set; }

        ////Lien vers les commandes
        //[InverseProperty("LignesDansLaCommandeNavigation")]
        //[JsonIgnore]
        //public virtual Commande? LigneAppartientACommandeNavigation { get; set; } = null!;

        [InverseProperty("LigneCommandeCommandeNavigation")]
        [JsonIgnore]
        public virtual Commande? CommandeLigneCommandeNavigation { get; set; } = null!;

        //Lien vers les variantes
        [InverseProperty("LignesCommandeVarianteNavigation")]
        public virtual Variante? VarianteLigneCommandeNavigation { get; set; } = null!;
    }
}
