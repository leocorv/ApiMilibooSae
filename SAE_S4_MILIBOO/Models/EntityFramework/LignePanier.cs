using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAE_S4_MILIBOO.Models.EntityFramework
{
    [Table("t_e_ligne_panier_lpn")]
    public class LignePanier
    {
        public LignePanier()
        {
            
        }

 
        [Column("lpn_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LigneId { get; set; }

        [Column("clt_id")]
        public int ClientId { get; set; }

        [Column("vrt_id")]
        public int VarianteId { get; set; }

        [Column("lpn_quantite")]
        public int Quantite { get; set; }

        [InverseProperty("LignesPanierClientNavigation")]
        public virtual Client? ClientLignePanierNavigation { get; set; } = null!;

        [InverseProperty("LignePanierVarianteNavigation")]
        public virtual Variante? VariantesLignePanierNavigation { get; set; } = null!;
    }
}
