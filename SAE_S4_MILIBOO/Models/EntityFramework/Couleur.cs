using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAE_S4_MILIBOO.Models.EntityFramework
{
    [Table("t_e_couleur_clr")]
    public class Couleur
    {
        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("clr_id")]
        public int IdCouleur { get; set; }

        [Column("clr_libelle")]
        [StringLength(30)]
        [Required]
        public string Libelle { get; set; } = null!;

        [Column("clr_code", TypeName="char(7)")]
        [StringLength(7)]
        [Required]
        public string CodeHexa { get; set; } = null!;

        [InverseProperty("CouleurVarianteNavigation")]
        public virtual ICollection<Variante> VariantesCouleurNavigation { get; set; } = new List<Variante>();
    }
}
