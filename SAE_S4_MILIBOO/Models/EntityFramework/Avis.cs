using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAE_S4_MILIBOO.Models.EntityFramework
{
    [Table("t_e_avis_avi")]
    public class Avis
    {
        
        [Column("avi_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AvisId { get; set; }

        [ForeignKey("VarianteId")]
        [Column("vrt_id")]
        public int VarianteId { get; set; }

        [ForeignKey("CollectionId")]
        [Column("clt_id")]
        public int ClientId { get; set; }

        [Column("avi_titre")]
        [StringLength(100)]
        public string AvisTitre { get; set; } = null!;

        [Column("avi_texte")]
        [StringLength(1000)]
        public string AvisTexte { get; set; } = null!;

        [Column("avi_note")]
        public int AvisNote { get; set; }

        [Column("avi_date", TypeName ="date")]
        public DateTime AvisDate { get; set;}


        [InverseProperty("AvisPhotosNavigation")]
        public virtual ICollection<Photo>? PhotosAvisNavigation { get; set; } = new List<Photo>();

        [InverseProperty("AvisClientsNavigation")]
        public virtual Client? ClientsAvisNavigation { get; set; } = null!;

        [InverseProperty("AvisVarianteNavigation")]
        public virtual Variante? VarianteAvisNavigation { get; set; } = null!;


    }

}

